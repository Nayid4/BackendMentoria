
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.DeleteFromProgram
{
    public sealed class DeleteProgramActivityFromProgramCommandHandler : IRequestHandler<DeleteProgramActivityFromProgramCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;
        public DeleteProgramActivityFromProgramCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }
        public async Task<ErrorOr<Unit>> Handle(DeleteProgramActivityFromProgramCommand request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetById(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }
            if (program.GetActivityById(new IdProgramActivity(request.IdProgramActivity)) is not ProgramActivity programActivity)
            {
                return Error.Conflict("ActivityAlreadyInProgram", "La actividad no se encuentra en el programa.");
            }
            program.RemoveActivity(programActivity);
            _programRepository.Update(program);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
