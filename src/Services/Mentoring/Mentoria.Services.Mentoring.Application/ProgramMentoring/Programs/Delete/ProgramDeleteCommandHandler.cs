using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Delete
{
    public sealed class ProgramDeleteCommandHandler : IRequestHandler<ProgramDeleteCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;
        public ProgramDeleteCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork;
            _programRepository = programRepository;
        }
        public async Task<ErrorOr<Unit>> Handle(ProgramDeleteCommand request, CancellationToken cancellationToken)
        {
            if(await _programRepository.GetById(new IdProgram(request.Id)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }

            _programRepository.Delete(program);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
