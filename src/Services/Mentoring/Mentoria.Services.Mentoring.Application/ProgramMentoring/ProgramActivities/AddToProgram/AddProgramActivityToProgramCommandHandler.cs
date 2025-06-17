
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.AddToProgram
{
    public sealed class AddProgramActivityToProgramCommandHandler : IRequestHandler<AddProgramActivityToProgramCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProgramRepository _programRepository;
        public AddProgramActivityToProgramCommandHandler(IUnitOfWork unitOfWork, IProgramRepository programRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }
        public async Task<ErrorOr<Unit>> Handle(AddProgramActivityToProgramCommand request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetByIdProgram(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }
            var programActivity = new ProgramActivity(
                new IdProgramActivity(Guid.NewGuid()),
                new IdProgram(request.IdProgram),
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.State
            );
            program.AddActivity(programActivity);
            _programRepository.Update(program);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
