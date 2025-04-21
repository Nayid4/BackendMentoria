
using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.Careers.Common;
using Mentoria.Services.Mentoring.Domain.Careers;
using System.Linq.Expressions;

namespace Mentoria.Services.Mentoring.Application.Careers.GetByFilter
{
    public sealed class CareerGetByFilterQueryHandler : IRequestHandler<CareerGetByFilterQuery, ErrorOr<DataList<CareerResponse>>>
    {
        private readonly ICareerRepository _careerRepository;

        public CareerGetByFilterQueryHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository ?? throw new ArgumentNullException(nameof(careerRepository));
        }

        public async Task<ErrorOr<DataList<CareerResponse>>> Handle(CareerGetByFilterQuery request, CancellationToken cancellationToken)
        {
            var roles = _careerRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                roles = roles.Where(at =>
                    at.Name.ToLower().Contains(request.SearchTerm.ToLower())
                );
            }

            if (request.OrderList?.ToLower() == "desc")
            {
                roles = roles.OrderByDescending(ListarOrdenDePropiedad(request));
            }
            else
            {
                roles = roles.OrderBy(ListarOrdenDePropiedad(request));
            }



            var result = roles.Select(role => new CareerResponse(
                role.Id.Value,
                role.Name
            ));

            var listCareer = await DataList<CareerResponse>.CreateAsync(
                    result,
                    request.Page,
                    request.SizePage
                );

            return listCareer;
        }

        private static Expression<Func<Career, object>> ListarOrdenDePropiedad(CareerGetByFilterQuery request)
        {
            return request.OrderColumn?.ToLower() switch
            {
                "name" => role => role.Name,
                _ => role => role.Id
            };
        }
    }
}
