

using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.Update
{
    public sealed class UpdateProgramActivityCommandHandler : IRequestHandler<UpdateProgramActivityCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;

        public UpdateProgramActivityCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateProgramActivityCommand request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetByIdProgram(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }
            
            if (program.GetActivityById(new IdProgramActivity(request.Id)) is not ProgramActivity programActivity)
            {
                return Error.Conflict("ActivityAlreadyInProgram", "La actividad no se encuentra en el programa.");
            }

            programActivity.Update(
                program.Id,
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.State
            );

            _programRepository.Update(program);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
