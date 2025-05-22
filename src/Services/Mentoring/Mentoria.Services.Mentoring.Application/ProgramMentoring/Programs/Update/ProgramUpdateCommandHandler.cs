using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Update
{
    public sealed class ProgramUpdateCommandHandler : IRequestHandler<ProgramUpdateCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;
        public ProgramUpdateCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork;
            _programRepository = programRepository;
        }
        public async Task<ErrorOr<Unit>> Handle(ProgramUpdateCommand request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetById(new IdProgram(request.Id)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }
            program.Update(
                new IdCareer(request.IdCareer),
                request.Name,
                request.Type,
                request.Description,
                request.MaximumNumberOfParticipants
            );
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
