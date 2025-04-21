

namespace Mentoria.Services.Mentoring.Application.Careers.Update
{
    public record CareerUpdateCommand(
        Guid Id,
        string Name
    ) : IRequest<ErrorOr<Unit>>;
}
