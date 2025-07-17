
using Mentoria.Services.Mentoring.Application.MentorAssignments.Common;
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.GetByUser
{
    public sealed class MentorAssignmentsGetByUserQueryHandler : IRequestHandler<MentorAssignmentsGetByUserQuery, ErrorOr<MentorAssignmentUsersResponse>>
    {
        private readonly IMentorAssignmentRepository _mentorAssignmentRepository;
        private readonly IUserRepository _userRepository;

        public MentorAssignmentsGetByUserQueryHandler(IMentorAssignmentRepository mentorAssignmentRepository, IUserRepository userRepository)
        {
            _mentorAssignmentRepository = mentorAssignmentRepository ?? throw new ArgumentNullException(nameof(mentorAssignmentRepository));
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<MentorAssignmentUsersResponse>> Handle(MentorAssignmentsGetByUserQuery request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdUser(new IdUser(request.IdUser)) is not User mentor)
            {
                return Error.NotFound("User.NotFound", "El usuario no se encuentra registrado.");
            }

            var mentorAssignments = _mentorAssignmentRepository.GetByMentorId(mentor.Id);

            if (!mentorAssignments.Any())
            {
                return Error.NotFound("MentorAssignment.NotFound", "No se encontraron asignaciones de mentor para el mentor especificado.");
            }

            var userResponses = await mentorAssignments.Select(mentorAssignment => new MentorAssignmentResponse(
                mentorAssignment.Id.Value,
                new UserResponse(
                    mentorAssignment.Mentor!.Id.Value,
                    new PersonalInformationResponse(
                        mentorAssignment.Mentor.PersonalInformation!.Id.Value,
                        mentorAssignment.Mentor.PersonalInformation.DNI,
                        mentorAssignment.Mentor.PersonalInformation.Name,
                        mentorAssignment.Mentor.PersonalInformation.LastName,
                        mentorAssignment.Mentor.PersonalInformation.Sex,
                        mentorAssignment.Mentor.PersonalInformation.Phone
                    ),
                    new RoleResponse(
                        mentorAssignment.Mentor.Role!.Id.Value,
                        mentorAssignment.Mentor.Role.Name
                    ),
                    new AcademicInformationResponse(
                        mentorAssignment.Mentor.AcademicInformation!.Id.Value,
                        mentorAssignment.Mentor.AcademicInformation.Code,
                        mentorAssignment.Mentor.AcademicInformation.Email,
                        new CareerResponse(
                            mentorAssignment.Mentor.AcademicInformation.Career!.Id.Value,
                            mentorAssignment.Mentor.AcademicInformation.Career.Name
                        ),
                        mentorAssignment.Mentor.AcademicInformation.Cicle,
                        mentorAssignment.Mentor.AcademicInformation.Expectative
                    ),
                    mentorAssignment.Mentor.UserName,
                    mentorAssignment.Mentor.State
                ),
                new UserResponse(
                    mentorAssignment.User!.Id.Value,
                    new PersonalInformationResponse(
                        mentorAssignment.User.PersonalInformation!.Id.Value,
                        mentorAssignment.User.PersonalInformation.DNI,
                        mentorAssignment.User.PersonalInformation.Name,
                        mentorAssignment.User.PersonalInformation.LastName,
                        mentorAssignment.User.PersonalInformation.Sex,
                        mentorAssignment.User.PersonalInformation.Phone
                    ),
                    new RoleResponse(
                        mentorAssignment.User.Role!.Id.Value,
                        mentorAssignment.User.Role.Name
                    ),
                    new AcademicInformationResponse(
                        mentorAssignment.User.AcademicInformation!.Id.Value,
                        mentorAssignment.User.AcademicInformation.Code,
                        mentorAssignment.User.AcademicInformation.Email,
                        new CareerResponse(
                            mentorAssignment.User.AcademicInformation.Career!.Id.Value,
                            mentorAssignment.User.AcademicInformation.Career.Name
                        ),
                        mentorAssignment.User.AcademicInformation.Cicle,
                        mentorAssignment.User.AcademicInformation.Expectative
                    ),
                    mentorAssignment.User.UserName,
                    mentorAssignment.User.State
                )
            )).ToListAsync(cancellationToken);

            var responseAssingment = new MentorAssignmentUsersResponse(
                    userResponses[0].Id,
                    userResponses[0].Mentor,
                    userResponses.Select(u => u.User).ToList()
            );

            return responseAssingment;

        }
    }
}
