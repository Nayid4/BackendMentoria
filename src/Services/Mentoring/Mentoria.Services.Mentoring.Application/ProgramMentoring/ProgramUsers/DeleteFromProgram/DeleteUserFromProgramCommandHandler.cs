

using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramUsers;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.DeleteFromProgram
{
    public sealed class DeleteUserFromProgramCommandHandler : IRequestHandler<DeleteUserFromProgramCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;

        public DeleteUserFromProgramCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteUserFromProgramCommand request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetById(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }

            if (program.GetUserById(new IdUser(request.IdUser)) is not ProgramUser programUser)
            {
                return Error.Conflict("UserAlreadyInProgram", "El usuario no se encuentra en el programa.");
            }

            program.RemoveUser(programUser);

            _programRepository.Update(program);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
