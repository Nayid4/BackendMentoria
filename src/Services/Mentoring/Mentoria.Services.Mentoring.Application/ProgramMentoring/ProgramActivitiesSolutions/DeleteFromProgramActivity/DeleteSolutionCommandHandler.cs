

using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.DeleteFromProgramActivity
{
    public sealed class DeleteSolutionCommandHandler : IRequestHandler<DeleteSolutionCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramActivityRepository _programActivityRepository;

        public DeleteSolutionCommandHandler(IUnitOfWork unitOfWork, IProgramActivityRepository programActivityRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _programActivityRepository = programActivityRepository ?? throw new ArgumentNullException(nameof(programActivityRepository));
        }
        public async Task<ErrorOr<Unit>> Handle(DeleteSolutionCommand request, CancellationToken cancellationToken)
        {
            if(await _programActivityRepository.GetById(new IdProgramActivity(request.IdProgramActivity)) is not ProgramActivity programActivity)
            {
                return Error.NotFound("ProgramActivityNotFound", "No se encontro la actividad del programa.");
            }

            if (programActivity.GetProgramActivitySolutionById(new IdProgramActivitySolution(request.IdProgramActivitySolution)) is not ProgramActivitySolution programActivitySolution)
            {
                return Error.Conflict("ProgramActivitySolutionAlreadyInProgram", "La solucion no se encuentra en la actividad del programa.");
            }

            programActivity.RemoveProgramActivitySolution(programActivitySolution);

            _programActivityRepository.Update(programActivity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
