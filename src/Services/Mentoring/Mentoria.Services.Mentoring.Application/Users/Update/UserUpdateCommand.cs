﻿using Mentoria.Services.Mentoring.Application.Users.Common;

namespace Mentoria.Services.Mentoring.Application.Users.Update
{
    public record UserUpdateCommand(
        Guid Id,
        PersonalInformationCommand PersonalInformation,
        RoleCommand Role,
        AcademicInformationCommand AcademicInformation
    ) : IRequest<ErrorOr<Unit>>;
}
