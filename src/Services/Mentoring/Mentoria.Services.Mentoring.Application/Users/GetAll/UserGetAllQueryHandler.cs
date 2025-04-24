
using Mentoria.Services.Mentoring.Application.Users.Common;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.Users.GetAll
{
    public sealed class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, ErrorOr<IReadOnlyList<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public UserGetAllQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<UserResponse>>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers()
                .Select(user => new UserResponse(
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
            )).ToListAsync(cancellationToken);

            return users;

        }
    }
}
