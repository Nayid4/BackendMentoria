
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.AddToProgramActivity
{
    public sealed class AddSolutionToProgramActivityCommandHandler : IRequestHandler<AddSolutionToProgramActivityCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramActivityRepository _programActivityRepository;
        private readonly IUserRepository _userRepository;
        public AddSolutionToProgramActivityCommandHandler(IUnitOfWork unitOfWork, IProgramActivityRepository programActivityRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _programActivityRepository = programActivityRepository ?? throw new ArgumentNullException(nameof(programActivityRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<ErrorOr<Unit>> Handle(AddSolutionToProgramActivityCommand request, CancellationToken cancellationToken)
        {
            if (await _programActivityRepository.GetById(new IdProgramActivity(request.IdProgramActivity)) is not ProgramActivity programActivity)
            {
                return Error.NotFound("ProgramActivityNotFound", "No se encontro la actividad del programa.");
            }
            if (await _userRepository.GetById(new IdUser(request.IdUser)) is not null)
            {
                return Error.Conflict("ProgramActivitySolutionAlreadyInProgram", "La solucion ya se encuentra en la actividad del programa.");
            }
            var programActivitySolution = new ProgramActivitySolution(
                new IdProgramActivitySolution(Guid.NewGuid()),
                new IdProgramActivity(request.IdProgramActivity),
                new IdUser(request.IdUser),
                request.IdFileSolution,
                0
            );

            programActivity.AddProgramActivitySolution(programActivitySolution);

            _programActivityRepository.Update(programActivity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
