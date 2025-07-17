using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Mentoria.Services.Mentoring.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.Update
{
    public sealed class MentorAssignmentUpdateCommandHandler : IRequestHandler<MentorAssignmentUpdateCommand, ErrorOr<Unit>>
    {
        private readonly IMentorAssignmentRepository _mentorAssignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MentorAssignmentUpdateCommandHandler(IMentorAssignmentRepository mentorAssignmentRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mentorAssignmentRepository = mentorAssignmentRepository ?? throw new ArgumentNullException(nameof(mentorAssignmentRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(MentorAssignmentUpdateCommand request, CancellationToken cancellationToken)
        {
            // Verificar que el mentor exista
            if (await _userRepository.GetByIdUser(new IdUser(request.IdMentor)) is not User mentor)
            {
                return Error.NotFound("User.NotFound", "El mentor no se encuentra registrado.");
            }

            // Verificar que el usuario exista
            if (await _userRepository.GetByIdUser(new IdUser(request.IdUser)) is not User user)
            {
                return Error.NotFound("User.NotFound", "El usuario no se encuentra registrado.");
            }

            // Validar que el mentor no tenga más de dos mentorías
            var mentorAssignmentsCount = await _mentorAssignmentRepository
                .GetByMentorId(mentor.Id) // Obtener las mentorías del mentor
                .CountAsync(cancellationToken);

            if (mentorAssignmentsCount >= 2)
            {
                return Error.Validation("MentorLimitExceeded", "El mentor ya tiene asignadas más de dos mentorías.");
            }

            // Validar que el usuario no tenga más de un mentor
            var existingMentorAssignment = await _mentorAssignmentRepository
                .GetByUserId(user.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingMentorAssignment != null)
            {
                return Error.Validation("UserAlreadyHasMentor", "El usuario ya tiene asignado un mentor.");
            }

            // Obtener la asignación de mentoría a actualizar
            var mentorAssignmentToUpdate = await _mentorAssignmentRepository
                .GetById(new IdMentorAssignment(request.Id));

            if (mentorAssignmentToUpdate == null)
            {
                return Error.NotFound("MentorAssignment.NotFound", "La asignación de mentoría no existe.");
            }

            // Actualizar la asignación de mentoría
            mentorAssignmentToUpdate.Update(new IdUser(request.IdMentor), new IdUser(request.IdUser));

            _mentorAssignmentRepository.Update(mentorAssignmentToUpdate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
