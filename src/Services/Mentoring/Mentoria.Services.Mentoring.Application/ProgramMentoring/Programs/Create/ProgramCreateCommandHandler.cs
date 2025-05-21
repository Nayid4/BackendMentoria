using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Create
{
    public sealed class ProgramCreateCommandHandler : IRequestHandler<ProgramCreateCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;
        public ProgramCreateCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork;
            _programRepository = programRepository;
        }
        public async Task<ErrorOr<Unit>> Handle(ProgramCreateCommand request, CancellationToken cancellationToken)
        {
            var program = new Program(
                new IdProgram(Guid.NewGuid()),
                new IdCareer(request.IdCareer),
                request.Name,
                request.Type,
                request.Description,
                request.MaximumNumberOfParticipants
            );

            _programRepository.Create(program);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
