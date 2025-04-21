
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Careers;

namespace Mentoria.Services.Mentoring.Application.Careers.Delete
{
    public sealed class CareerDeleteCommandHandler : IRequestHandler<CareerDeleteCommand, ErrorOr<Unit>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CareerDeleteCommandHandler(ICareerRepository careerRepository, IUnitOfWork unitOfWork)
        {
            _careerRepository = careerRepository ?? throw new ArgumentNullException(nameof(careerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CareerDeleteCommand command, CancellationToken cancellationToken)
        {
            if (await _careerRepository.GetById(new IdCareer(command.Id)) is not Career role)
            {
                return Error.NotFound("Career.NoEncontrado", "La carrera no existe.");
            }

            _careerRepository.Delete(role);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
