

namespace Mentoria.Services.Mentoring.Application.Files.Delete
{
    public record FileDeleteCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
