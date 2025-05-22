
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.GetByIdUser
{
    public sealed class ActivitySolutionGetByIdUserQueryHandler : IRequestHandler<ActivitySolutionGetByIdUserQuery, ErrorOr<ProgramActivitySolutionResponse>>
    {
        private readonly IProgramActivityRepository _programActivityRepository;
        private readonly IUserRepository _userRepository;

        public ActivitySolutionGetByIdUserQueryHandler(IProgramActivityRepository programActivityRepository, IUserRepository userRepository)
        {
            _programActivityRepository = programActivityRepository ?? throw new ArgumentNullException(nameof(programActivityRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<ErrorOr<ProgramActivitySolutionResponse>> Handle(ActivitySolutionGetByIdUserQuery request, CancellationToken cancellationToken)
        {
            if (await _programActivityRepository.GetById(new IdProgramActivity(request.IdProgramActivity)) is not ProgramActivity programActivity)
            {
                return Error.NotFound("ProgramActivityNotFound", "No se encontro la actividad del programa.");
            }
            if (await _userRepository.GetById(new IdUser(request.IdUser)) is not null)
            {
                return Error.Conflict("ProgramActivitySolutionAlreadyInProgram", "La solucion ya se encuentra en la actividad del programa.");
            }

            if (programActivity.GetProgramActivitySolutionByUserId(new IdUser(request.IdUser)) is not ProgramActivitySolution programActivitySolution)
            {
                return Error.Conflict("ProgramActivitySolutionAlreadyInProgram", "La solucion ya se encuentra en la actividad del programa.");
            }

            var response = new ProgramActivitySolutionResponse(
                programActivitySolution.Id.Value,
                programActivitySolution.IdProgramActivity.Value,
                programActivitySolution.IdUser.Value,
                programActivitySolution.IdFileSolution,
                programActivitySolution.Calification
            );

            return response;

        }
    }
}
