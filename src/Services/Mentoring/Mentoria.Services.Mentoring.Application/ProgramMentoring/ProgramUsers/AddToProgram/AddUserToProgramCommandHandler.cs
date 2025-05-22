

using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramUsers;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.AddToProgram
{
    public sealed class AddUserToProgramCommandHandler : IRequestHandler<AddUserToProgramCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;

        public AddUserToProgramCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork;
            _programRepository = programRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(AddUserToProgramCommand request, CancellationToken cancellationToken)
        {
            if(await _programRepository.GetById(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }

            if (program.GetUserById(new IdUser(request.IdUser)) is ProgramUser programUser)
            {
                return Error.Conflict("UserAlreadyInProgram", "El usuario ya se encuentra en el programa.");
            }

            program.AddUser(new ProgramUser(
                new IdProgramUser(Guid.NewGuid()),
                new IdUser(request.IdUser),
                program.Id
            ));

            _programRepository.Update(program);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
