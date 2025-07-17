
using Mentoria.Services.Mentoring.Application.MentorAssignments.Common;
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.GetById
{
    public sealed class MentorAssignmentsGetByIdQueryHandler : IRequestHandler<MentorAssignmentsGetByIdQuery, ErrorOr<MentorAssignmentResponse>>
    {
        private readonly IMentorAssignmentRepository _mentorAssignmentRepository;
        private readonly IUserRepository _userRepository;

        public MentorAssignmentsGetByIdQueryHandler(IMentorAssignmentRepository mentorAssignmentRepository, IUserRepository userRepository)
        {
            _mentorAssignmentRepository = mentorAssignmentRepository ?? throw new ArgumentNullException(nameof(mentorAssignmentRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<MentorAssignmentResponse>> Handle(MentorAssignmentsGetByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _mentorAssignmentRepository.GetById(new IdMentorAssignment(request.Id)) is not MentorAssignment mentorAssignment)
            {
                return Error.NotFound("MentorAssignment.NotFound", "La asignación de mentor no se encuentra registrada.");
            }

            if (await _userRepository.GetByIdUser(mentorAssignment.IdMentor) is not User mentor)
            {
                return Error.NotFound("User.NotFound", "El mentor no se encuentra registrado.");
            }

            if (await _userRepository.GetByIdUser(mentorAssignment.IdUser) is not User user)
            {
                return Error.NotFound("User.NotFound", "El usuario no se encuentra registrado.");
            }

            var userResult = new UserResponse(
                user.Id.Value,
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
            );

            var mentorResult = new UserResponse(
                mentor.Id.Value,
                    new PersonalInformationResponse(
                        mentor.PersonalInformation!.Id.Value,
                        mentor.PersonalInformation.DNI,
                        mentor.PersonalInformation.Name,
                        mentor.PersonalInformation.LastName,
                        mentor.PersonalInformation.Sex,
                        mentor.PersonalInformation.Phone
                    ),
                    new RoleResponse(
                        mentor.Role!.Id.Value,
                        mentor.Role.Name
                    ),
                    new AcademicInformationResponse(
                        mentor.AcademicInformation!.Id.Value,
                        mentor.AcademicInformation.Code,
                        mentor.AcademicInformation.Email,
                        new CareerResponse(
                            mentor.AcademicInformation.Career!.Id.Value,
                            mentor.AcademicInformation.Career.Name
                        ),
                        mentor.AcademicInformation.Cicle,
                        mentor.AcademicInformation.Expectative
                    ),
                    mentor.UserName,
                    mentor.State
            );

            var response = new MentorAssignmentResponse(
                mentorAssignment.Id.Value,
                mentorResult,
                userResult
            );

            return response;
        }
    }
}
