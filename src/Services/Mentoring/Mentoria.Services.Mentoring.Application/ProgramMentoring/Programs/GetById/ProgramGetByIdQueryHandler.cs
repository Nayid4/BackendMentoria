using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetById
{
    public sealed class ProgramGetByIdQueryHandler : IRequestHandler<ProgramGetByIdQuery, ErrorOr<ProgramResponse>>
    {
        private readonly IProgramRepository _programRepository;

        public ProgramGetByIdQueryHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository)) ;
        }

        public async Task<ErrorOr<ProgramResponse>> Handle(ProgramGetByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetById(new IdProgram(request.Id)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }

            return new ProgramResponse(
                program.Id.Value,
                new CareerResponse(
                    program.Career!.Id.Value,
                    program.Career.Name
                ),
                program.Name,
                program.Type,
                program.Description,
                program.MaximumNumberOfParticipants
            );
        }
    }
}
