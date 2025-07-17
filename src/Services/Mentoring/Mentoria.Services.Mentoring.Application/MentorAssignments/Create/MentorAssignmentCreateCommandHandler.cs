


using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.Create
{
    public sealed class MentorAssignmentCreateCommandHandler : IRequestHandler<MentorAssignmentCreateCommand, ErrorOr<Unit>>
    {
        private readonly IMentorAssignmentRepository _mentorAssignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MentorAssignmentCreateCommandHandler(IMentorAssignmentRepository mentorAssignmentRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _mentorAssignmentRepository = mentorAssignmentRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(MentorAssignmentCreateCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByIdUser(new IdUser(command.IdMentor)) is not User mentor)
            {
                return Error.NotFound("User.NotFound", "El mentor no se encuentra registrado.");
            }

            if (await _userRepository.GetByIdUser(new IdUser(command.IdUser)) is not User user)
            {
                return Error.NotFound("User.NotFound", "El usuario no se encuentra registrado.");
            }

            // Validar que el usuario no tenga un mentor asignado
            var existingMentorAssignment = await _mentorAssignmentRepository.GetByUserId(user.Id).FirstOrDefaultAsync(cancellationToken);
            if (existingMentorAssignment != null)
            {
                return Error.Validation("UserAlreadyHasMentor", "El usuario ya tiene un mentor asignado.");
            }

            // Validar que el mentor y el usuario sean de la misma carrera
            if (mentor.AcademicInformation.IdCareer.Value != user.AcademicInformation.IdCareer.Value)
            {
                return Error.Validation("MentorCareerMismatch", "El mentor debe ser de la misma carrera que el usuario.");
            }

            // Verificar que el mentor no tenga más de dos mentorías
            var mentorAssignmentsCount = await _mentorAssignmentRepository
                .GetByMentorId(mentor.Id) // Obtenemos las mentorías del mentor
                .CountAsync(cancellationToken);

            if (mentorAssignmentsCount >= 2)
            {
                return Error.Validation("MentorLimitExceeded", "El mentor ya tiene asignadas más de dos mentorías.");
            }

            var mentorAssignment = new MentorAssignment(
                        new IdMentorAssignment(Guid.NewGuid()),
                        mentor.Id, 
                        user.Id
            );

            _mentorAssignmentRepository.Create(mentorAssignment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
