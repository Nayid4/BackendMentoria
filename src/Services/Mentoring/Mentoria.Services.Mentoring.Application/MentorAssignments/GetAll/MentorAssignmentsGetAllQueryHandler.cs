
using Mentoria.Services.Mentoring.Application.MentorAssignments.Common;
using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.GetAll
{
    public sealed class MentorAssignmentsGetAllQueryHandler : IRequestHandler<MentorAssignmentsGetAllQuery, ErrorOr<IReadOnlyList<MentorAssignmentResponse>>>
    {
        private readonly IMentorAssignmentRepository _mentorAssignmentRepository;

        public MentorAssignmentsGetAllQueryHandler(IMentorAssignmentRepository mentorAssignmentRepository)
        {
            _mentorAssignmentRepository = mentorAssignmentRepository ?? throw new ArgumentNullException(nameof(mentorAssignmentRepository));
        }

        public async Task<ErrorOr<IReadOnlyList<MentorAssignmentResponse>>> Handle(MentorAssignmentsGetAllQuery request, CancellationToken cancellationToken)
        {
            var mentorAssignments = _mentorAssignmentRepository.GetAllMentorAssignment();

            var response = await mentorAssignments.Select(mentorAssignment => new MentorAssignmentResponse(
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

            return response;
        }
    }
}
