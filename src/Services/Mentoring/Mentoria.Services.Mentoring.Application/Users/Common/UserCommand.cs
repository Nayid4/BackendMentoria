

namespace Mentoria.Services.Mentoring.Application.Users.Common
{
    public record RoleCommand(
        Guid Id
    );

    public record PersonalInformationCommand(
        string DNI,
        string Name,
        string LastName,
        string Sex,
        string Phone
    );

    public record AcademicInformationCommand(
        string Code,
        string Email,
        CareerCommand Career,
        int Cicle,
        string Expectative
    );

    public record CareerCommand(
        Guid Id
    );
}
