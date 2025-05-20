
using Mentoria.Services.Mentoring.Application.Email;
using Mentoria.Services.Mentoring.Application.Utils;
using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.Users.Create
{
    public sealed class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonalInformationRepository _personalInformationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAcademicInformationRepository _academicInformationRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IAuthToken _authToken;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreateCommandHandler(IUserRepository userRepository, IPersonalInformationRepository personalInformationRepository, IRoleRepository roleRepository, IAcademicInformationRepository academicInformationRepository, IUnitOfWork unitOfWork, ICareerRepository careerRepository, IAuthToken authToken, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _personalInformationRepository = personalInformationRepository ?? throw new ArgumentNullException(nameof(personalInformationRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _academicInformationRepository = academicInformationRepository ?? throw new ArgumentNullException(nameof(academicInformationRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _careerRepository = careerRepository ?? throw new ArgumentNullException(nameof(careerRepository));
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<ErrorOr<Unit>> Handle(UserCreateCommand command, CancellationToken cancellationToken)
        {
            if (await _academicInformationRepository.GetByEmail(command.AcademicInformation.Email) is AcademicInformation academicInformation)
            {
                return Error.Conflict("AcademicInformation.Found", "El correo ya se encuentra registrado.");
            }

            if (await _academicInformationRepository.GetByCode(command.AcademicInformation.Code) is AcademicInformation academicInformation2)
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

            var personalInformation = new PersonalInformation(
                new IdPersonalInformation(Guid.NewGuid()),
                command.PersonalInformation.DNI,
                command.PersonalInformation.Name,
                command.PersonalInformation.LastName,
                command.PersonalInformation.Sex,
                command.PersonalInformation.Phone
            );

            var academicInformationCommand = new AcademicInformation(
                new IdAcademicInformation(Guid.NewGuid()),
                command.AcademicInformation.Code,
                command.AcademicInformation.Email,
                career.Id,
                command.AcademicInformation.Cicle,
                command.AcademicInformation.Expectative
            );

            string password = _authToken.GeneratePass();

            var user = new User(
                new IdUser(Guid.NewGuid()),
                personalInformation.Id,
                role.Id,
                academicInformationCommand.Id,
                command.AcademicInformation.Code,
                _authToken.EncryptSHA256(password),
                "Pendiente"
            );

            _personalInformationRepository.Create(personalInformation);
            _academicInformationRepository.Create(academicInformationCommand);
            _userRepository.Create(user);

            await _emailService.Send(
                academicInformationCommand.Email, 
                "Bienvenido al Sistema de Mentoria!",
                "Su usuario es: " + user.UserName + " y su contraseña es: " + password
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
