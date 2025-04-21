
using Mentoria.Services.Mentoring.Application.Careers.Common;
using Mentoria.Services.Mentoring.Domain.Careers;

namespace Mentoria.Services.Mentoring.Application.Careers.GetById
{
    public sealed class CareerGetByIdQueryHandler : IRequestHandler<CareerGetByIdQuery, ErrorOr<CareerResponse>>
    {
        private readonly ICareerRepository _careerRepository;

        public CareerGetByIdQueryHandler(ICareerRepository roleRepository)
        {
            _careerRepository = roleRepository;
        }

        public async Task<ErrorOr<CareerResponse>> Handle(CareerGetByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _careerRepository.GetById(new IdCareer(request.Id)) is not Career career)
            {
                return Error.NotFound("Career.NoEncontrado", "La carrera no existe.");
            }

            var response = new CareerResponse(
                career.Id.Value,
                career.Name
            );

            return response;
        }
    }
}
