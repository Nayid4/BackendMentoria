
using Mentoria.Services.Mentoring.Application.Common.DataList;
using Mentoria.Services.Mentoring.Application.Users.Common;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.Users;
using System.Linq.Expressions;

namespace Mentoria.Services.Mentoring.Application.Users.GetByFilter
{
    public sealed class UserGetByFilterQueryHandler : IRequestHandler<UserGetByFilterQuery, ErrorOr<DataList<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public UserGetByFilterQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<DataList<UserResponse>>> Handle(UserGetByFilterQuery request, CancellationToken cancellationToken)
        {
            var users = _userRepository.GetAllUsers();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                users = users.Where(at =>
                    at.PersonalInformation!.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.PersonalInformation!.LastName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.Role!.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.UserName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.Role!.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    at.State.ToLower().Contains(request.SearchTerm.ToLower())
                );
            }

            if (!string.IsNullOrWhiteSpace(request.State))
            {
                users = users.Where(at => at.State.ToLower().Contains(request.State.ToLower()));
            }

            if (request.RoleId != Guid.Empty && request.RoleId is not null)
            {
                var idRole = new IdRole(request.RoleId ?? Guid.Empty);
                users = users.Where(p => p.IdRole == idRole);
            }

            if (request.OrderList?.ToLower() == "desc")
            {
                users = users.OrderByDescending(ListarOrdenDePropiedad(request));
            }
            else
            {
                users = users.OrderBy(ListarOrdenDePropiedad(request));
            }



            var result = users.Select(user => new UserResponse(
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
                    user.UserName,
                    user.State
            ));

            var ListUser = await DataList<UserResponse>.CreateAsync(
                    result,
                    request.Page,
                    request.SizePage
                );

            return ListUser;
        }

        private static Expression<Func<User, object>> ListarOrdenDePropiedad(UserGetByFilterQuery request)
        {
            return request.OrderColumn?.ToLower() switch
            {
                "name" => user => user.PersonalInformation!.Name,
                "lastName" => user => user.PersonalInformation!.LastName,
                "role" => user => user.Role!.Name,
                "userName" => user => user.UserName,
                "email" => user => user.AcademicInformation!.Email,
                _ => user => user.Id
            };
        }
    }
}
