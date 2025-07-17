
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.GetAllByUser
{
    public sealed class ProgramUserGetAllByUserQueryHandler : IRequestHandler<ProgramUserGetAllByUserQuery, ErrorOr<IReadOnlyList<ProgramResponse>>>
    {
        private readonly IProgramRepository _programRepository;

        public ProgramUserGetAllByUserQueryHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }


        public async Task<ErrorOr<IReadOnlyList<ProgramResponse>>> Handle(ProgramUserGetAllByUserQuery request, CancellationToken cancellationToken)
        {
            var id = new IdUser(request.IdUser);

            var programs = await _programRepository.GetAll()
                .Include(p => p.Career)
                .Include(p => p.Users)
                    .ThenInclude(u => u.User)
                .Where(p => p.Users.Any(u => u.IdUser == id))
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
