
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetById
{
    public sealed class ProgramActivityGetByIdQueryHandler : IRequestHandler<ProgramActivityGetByIdQuery, ErrorOr<ProgramActivityResponse>>
    {
        private readonly IProgramRepository _programRepository;

        public ProgramActivityGetByIdQueryHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }

        public async Task<ErrorOr<ProgramActivityResponse>> Handle(ProgramActivityGetByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetById(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }

            if (program.GetActivityById(new IdProgramActivity(request.IdProgramActivity)) is not ProgramActivity programActivity)
            {
                return Error.Conflict("ActivityAlreadyInProgram", "La actividad no se encuentra en el programa.");
            }

            var programActivities = new ProgramActivityResponse(
                    programActivity.Id.Value,
                    program.Id.Value,
                    programActivity.Name,
                    programActivity.Description,
                    programActivity.StartDate,
                    programActivity.EndDate,
                    programActivity.State
                );

            return programActivities;

        }
    }
}
