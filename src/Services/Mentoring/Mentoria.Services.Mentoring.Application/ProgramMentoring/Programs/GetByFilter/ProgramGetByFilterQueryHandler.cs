using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Common;
using Mentoria.Services.Mentoring.Application.Roles.GetByFilter;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.Roles;
using System.Linq.Expressions;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetByFilter
{
    public sealed class ProgramGetByFilterQueryHandler : IRequestHandler<ProgramGetByFilterQuery, ErrorOr<DataList<ProgramResponse>>>
    {
        private readonly IProgramRepository _programRepository;

        public ProgramGetByFilterQueryHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }

        public async Task<ErrorOr<DataList<ProgramResponse>>> Handle(ProgramGetByFilterQuery request, CancellationToken cancellationToken)
        {
            var programs = _programRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                programs = programs.Where(at =>
                    at.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.Description.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.Type.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.Career!.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.MaximumNumberOfParticipants.ToString().ToLower().Contains(request.SearchTerm.ToLower())
                );
            }

            if (request.OrderList?.ToLower() == "desc")
            {
                programs = programs.OrderByDescending(ListarOrdenDePropiedad(request));
            }
            else
            {
                programs = programs.OrderBy(ListarOrdenDePropiedad(request));
            }



            var result = programs.Select(p => new ProgramResponse(
                p.Id.Value,
                new CareerResponse(
                    p.Career!.Id.Value,
                    p.Career.Name
                ),
                p.Name,
                p.Type,
                p.Description,
                p.MaximumNumberOfParticipants
            ));

            var ListRole = await DataList<ProgramResponse>.CreateAsync(
                    result,
                    request.Page,
                    request.SizePage
                );

            return ListRole;
        }

        private static Expression<Func<Program, object>> ListarOrdenDePropiedad(ProgramGetByFilterQuery request)
        {
            return request.OrderColumn?.ToLower() switch
            {
                "name" => role => role.Name,
                "description" => role => role.Description,
                "type" => role => role.Type,
                "career" => role => role.Career!.Name,
                "maximumnumberofparticipants" => role => role.MaximumNumberOfParticipants,
                _ => role => role.Id
            };
        }
    }
}
