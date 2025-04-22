


using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Careers;

namespace Mentoria.Services.Mentoring.Application.Careers.Create
{
    public sealed class CareerCreateCommandHandler : IRequestHandler<CareerCreateCommand, ErrorOr<Unit>>
    {
        private readonly ICareerRepository _careerRepository;    
        private readonly IUnitOfWork _unitOfWork;

        public CareerCreateCommandHandler(ICareerRepository careerRepository, IUnitOfWork unitOfWork)
        {
            _careerRepository = careerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(CareerCreateCommand command, CancellationToken cancellationToken)
        {
            if (await _careerRepository.GetByNameAsync(command.Name) is Career career)
            {
                return Error.Conflict("Career.Found", "El carerra ya esta registrado.");
            }

            var careerResult = new Career(
                new IdCareer(Guid.NewGuid()),
                command.Name
            );

            _careerRepository.Create(careerResult);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
