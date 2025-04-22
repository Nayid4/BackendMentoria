

using Mentoria.Services.Mentoring.Application.Careers.Common;
using Mentoria.Services.Mentoring.Domain.Careers;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.Careers.GetAll
{
    public sealed class CareerGetAllQueryHandler : IRequestHandler<CareerGetAllQuery, ErrorOr<IReadOnlyList<CareerResponse>>>
    {
        private readonly ICareerRepository _careerRepository;

        public CareerGetAllQueryHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<ErrorOr<IReadOnlyList<CareerResponse>>> Handle(CareerGetAllQuery request, CancellationToken cancellationToken)
        {
            var listCareer = await _careerRepository.GetAll()
                .Select(role => new CareerResponse(
                    role.Id.Value,
                    role.Name
                )).ToListAsync(cancellationToken);

            return listCareer;
        }
    }
}
