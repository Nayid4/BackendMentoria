
using Mentoria.Services.Mentoring.Application.Careers.Common;

namespace Mentoria.Services.Mentoring.Application.Careers.GetById
{
    public record CareerGetByIdQuery(Guid Id) : IRequest<ErrorOr<CareerResponse>>;
}
