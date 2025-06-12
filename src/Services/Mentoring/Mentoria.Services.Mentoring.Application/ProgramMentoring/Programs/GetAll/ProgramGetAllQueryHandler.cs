using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetAll
{
    public sealed class ProgramGetAllQueryHandler : IRequestHandler<ProgramGetAllQuery, ErrorOr<IReadOnlyList<ProgramResponse>>>
    {
        private readonly IProgramRepository _programRepository;
        public ProgramGetAllQueryHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }
        public async Task<ErrorOr<IReadOnlyList<ProgramResponse>>> Handle(ProgramGetAllQuery request, CancellationToken cancellationToken)
        {
            var programs = await _programRepository.GetAll()
                .Include(p => p.Career)
                .Select(p => new ProgramResponse(
                p.Id.Value,
                new CareerResponse(
                    p.Career!.Id.Value,
                    p.Career.Name
                ),
                p.Name,
                p.Type,
                p.Description,
                p.MaximumNumberOfParticipants
            )).ToListAsync(cancellationToken);

            return programs;
        }
    }
}
