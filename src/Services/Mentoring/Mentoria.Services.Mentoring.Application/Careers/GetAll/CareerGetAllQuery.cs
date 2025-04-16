using Mentoria.Services.Mentoring.Application.Careers.Common;

namespace Mentoria.Services.Mentoring.Application.Careers.GetAll
{
    public record CareerGetAllQuery() : IRequest<ErrorOr<IReadOnlyList<CareerResponse>>>;
}
