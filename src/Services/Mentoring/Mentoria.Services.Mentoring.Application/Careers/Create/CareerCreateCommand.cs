
namespace Mentoria.Services.Mentoring.Application.Careers.Create
{
    public record CareerCreateCommand(string Name) : IRequest<ErrorOr<Unit>>;
}
