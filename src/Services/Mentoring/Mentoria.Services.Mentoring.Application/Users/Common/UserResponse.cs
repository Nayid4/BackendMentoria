﻿

namespace Mentoria.Services.Mentoring.Application.Users.Common
{
    public record UserResponse(
        PersonalInformationResponse PersonalInformation,
        RoleResponse Role,
        AcademicInformationResponse AcademicInformation,
        string UserName
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
