
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.GetAll
{
    public sealed class ActivitySolutionGetAllQueryHandler : IRequestHandler<ActivitySolutionGetAllQuery, ErrorOr<IReadOnlyList<ProgramActivitySolutionResponse>>>
    {
        private readonly IProgramActivityRepository _programActivityRepository;
        public ActivitySolutionGetAllQueryHandler(IProgramActivityRepository programActivityRepository)
        {
            _programActivityRepository = programActivityRepository ?? throw new ArgumentNullException(nameof(programActivityRepository));
        }
        public async Task<ErrorOr<IReadOnlyList<ProgramActivitySolutionResponse>>> Handle(ActivitySolutionGetAllQuery request, CancellationToken cancellationToken)
        {
            if (await _programActivityRepository.GetById(new IdProgramActivity(request.IdProgramActivity)) is not ProgramActivity programActivity)
            {
                return Error.NotFound("ProgramActivityNotFound", "No se encontro la actividad del programa.");
            }

            var programActivitySolutions = programActivity.ProgramActivitySolutions
                .Select(solution => new ProgramActivitySolutionResponse(
                    solution.Id.Value,
                    solution.IdProgramActivity.Value,
                    solution.IdUser.Value,
                    solution.IdFileSolution,
                    solution.Calification
                )).ToList();

            return programActivitySolutions;


        }
    }
}
