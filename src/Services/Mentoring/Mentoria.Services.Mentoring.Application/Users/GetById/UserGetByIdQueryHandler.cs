
using Mentoria.Services.Mentoring.Application.Users.Common;
using Mentoria.Services.Mentoring.Domain.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mentoria.Services.Mentoring.Application.Users.GetById
{
    public sealed class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, ErrorOr<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public UserGetByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<UserResponse>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdUser(new IdUser(request.Id)) is not User user)
            {
                return Error.NotFound("User.NotFound", "El usuario no se encuentra registrado.");
            }

            var userResult = new UserResponse(
                    new PersonalInformationResponse(
                        user.PersonalInformation!.Id.Value,
                        user.PersonalInformation.DNI,
                        user.PersonalInformation.Name,
                        user.PersonalInformation.LastName,
                        user.PersonalInformation.Sex,
                        user.PersonalInformation.Phone
                    ),
                    new RoleResponse(
                        user.Role!.Id.Value,
                        user.Role.Name
                    ),
                    new AcademicInformationResponse(
                        user.AcademicInformation!.Id.Value,
                        user.AcademicInformation.Code,
                        user.AcademicInformation.Email,
                        new CareerResponse(
                            user.AcademicInformation.Career!.Id.Value,
                            user.AcademicInformation.Career.Name
                        ),
                        user.AcademicInformation.Cicle,
                        user.AcademicInformation.Expectative
                    ),
                    user.UserName
            );

            return userResult;
        }
    }
}
