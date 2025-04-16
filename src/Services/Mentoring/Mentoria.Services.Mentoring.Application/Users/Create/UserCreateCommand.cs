
using Mentoria.Services.Mentoring.Application.Users.Common;

namespace Mentoria.Services.Mentoring.Application.Users.Create
{
    public record UserCreateCommand(
        PersonalInformationCommand PersonalInformation,
        RoleCommand Role,
        AcademicInformationCommand AcademicInformation
    ) : IRequest<ErrorOr<Unit>>;
}
