using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Careers;

namespace Mentoria.Services.Mentoring.Application.Careers.Update
{
    public sealed class CareerUpdateCommandHandler : IRequestHandler<CareerUpdateCommand, ErrorOr<Unit>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CareerUpdateCommandHandler(ICareerRepository careerRepository, IUnitOfWork unitOfWork)
        {
            _careerRepository = careerRepository ?? throw new ArgumentNullException(nameof(careerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CareerUpdateCommand request, CancellationToken cancellationToken)
        {
            if (await _careerRepository.GetById(new IdCareer(request.Id)) is not Career career)
            {
                return Error.NotFound("Career.NoEncontrado", "La carrera no existe.");
            }

            career.Update(
                request.Name
            );

            _careerRepository.Update(career);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
