


using Mentoria.Services.Mentoring.Application.Utils;
using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mentoria.Services.Mentoring.Application.Users.Update
{
    public sealed class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonalInformationRepository _personalInformationRepository;
        private readonly Domain.Roles.IRoleRepository _roleRepository;
        private readonly IAcademicInformationRepository _academicInformationRepository;
        private readonly Domain.Careers.ICareerRepository _careerRepository;
        private readonly IAuthToken _authToken;
        private readonly IUnitOfWork _unitOfWork;

        public UserUpdateCommandHandler(IUserRepository userRepository, IPersonalInformationRepository personalInformationRepository, Domain.Roles.IRoleRepository roleRepository, IAcademicInformationRepository academicInformationRepository, IUnitOfWork unitOfWork, Domain.Careers.ICareerRepository careerRepository, IAuthToken authToken)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _personalInformationRepository = personalInformationRepository ?? throw new ArgumentNullException(nameof(personalInformationRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _academicInformationRepository = academicInformationRepository ?? throw new ArgumentNullException(nameof(academicInformationRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _careerRepository = careerRepository ?? throw new ArgumentNullException(nameof(careerRepository));
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
        }

        public async Task<ErrorOr<Unit>> Handle(UserUpdateCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdUser(new IdUser(command.Id)) is not User user)
            {
                return Error.NotFound("User.NotFound", "El usuario no se encuentra registrado.");
            }

            if (await _academicInformationRepository.GetByEmail(command.AcademicInformation.Email) is AcademicInformation academicInformation && user.AcademicInformation!.Email.Equals(academicInformation.Email))
            {
                return Error.Conflict("AcademicInformation.Found", "El correo ya se encuentra registrado.");
            }

            if (await _academicInformationRepository.GetByCode(command.AcademicInformation.Code) is AcademicInformation academicInformation2 && user.AcademicInformation!.Code.Equals(academicInformation2.Code))
            {
                return Error.Conflict("AcademicInformation.Found", "El codigo ya se encuentra registrado.");
            }

            if (await _roleRepository.GetById(new IdRole(command.Role.Id)) is not Role role)
            {
                return Error.NotFound("Role.NotFound", "El rol no se encuentra registrado.");
            }

            if (await _careerRepository.GetById(new IdCareer(command.AcademicInformation.Career.Id)) is not Career career)
            {
                return Error.NotFound("Career.NotFound", "La carrera no se encuentra registrada.");
            }

            user.PersonalInformation!.Update(
                command.PersonalInformation.DNI,
                command.PersonalInformation.Name,
                command.PersonalInformation.LastName,
                command.PersonalInformation.Sex,
                command.PersonalInformation.Phone
            );

            user.AcademicInformation!.Update(
                command.AcademicInformation.Code,
                command.AcademicInformation.Email,
                career.Id,
                command.AcademicInformation.Cicle,
                command.AcademicInformation.Expectative
            );


            user.Update(
                user.PersonalInformation.Id,
                role.Id,
                user.AcademicInformation.Id,
                command.UserName,
                command.State
            );

            _personalInformationRepository.Create(user.PersonalInformation);
            _academicInformationRepository.Create(user.AcademicInformation);
            _userRepository.Create(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
