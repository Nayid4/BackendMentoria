
namespace Mentoria.Services.Mentoring.Application.MentorAssignments.Common
{
    public record MentorAssignmentResponse(
        Guid Id,
        UserResponse Mentor,
        UserResponse User
    );

    public record MentorAssignmentUsersResponse(
        Guid Id,
        UserResponse Mentor,
        List<UserResponse> Users
    );

    public record UserResponse(
        Guid Id,
        PersonalInformationResponse PersonalInformation,
        RoleResponse Role,
        AcademicInformationResponse AcademicInformation,
        string UserName,
        string State
    );

    public record RoleResponse(
        Guid Id,
        string Name
    );

    public record PersonalInformationResponse(
        Guid Id,
        string DNI,
        string Name,
        string LastName,
        string Sex,
        string Phone
    );

    public record AcademicInformationResponse(
        Guid Id,
        string Code,
        string Email,
        CareerResponse Career,
        int Cicle,
        string Expectative
    );

    public record CareerResponse(
        Guid Id,
        string Name
    );
}
