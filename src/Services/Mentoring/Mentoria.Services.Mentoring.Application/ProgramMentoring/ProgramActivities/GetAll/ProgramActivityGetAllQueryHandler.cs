
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using System.Linq;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetAll
{
    public sealed class ProgramActivityGetAllQueryHandler : IRequestHandler<ProgramActivityGetAllQuery, ErrorOr<IReadOnlyList<ProgramActivityResponse>>>
    {
        private readonly IProgramRepository _programRepository;

        public ProgramActivityGetAllQueryHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<ProgramActivityResponse>>> Handle(ProgramActivityGetAllQuery request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetByIdProgram(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }

            var programActivities = program.Activities
                .Select(programActivity => new ProgramActivityResponse(
                    programActivity.Id.Value,
                    program.Id.Value,
                    programActivity.Name,
                    programActivity.Description,
                    programActivity.StartDate,
                    programActivity.EndDate,
                    programActivity.State
                )).ToList();

            return programActivities;
        }
    }
}
