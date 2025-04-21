
namespace Mentoria.Services.Mentoring.Application.Careers.Delete
{
    public record CareerDeleteCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
