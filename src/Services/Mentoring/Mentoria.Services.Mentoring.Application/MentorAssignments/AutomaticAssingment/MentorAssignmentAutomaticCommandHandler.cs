using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Users;
using Mentoria.Services.Mentoring.Domain.Roles;
using Mentoria.Services.Mentoring.Domain.AcademicInformations;
using Mentoria.Services.Mentoring.Domain.PersonalInformations;
using Mentoria.Services.Mentoring.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.AutomaticAssingment
{
    public sealed class MentorAssignmentAutomaticCommandHandler : IRequestHandler<MentorAssignmentAutomaticCommand, ErrorOr<Unit>>
    {
        private readonly IMentorAssignmentRepository _mentorAssignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MentorAssignmentAutomaticCommandHandler(IMentorAssignmentRepository mentorAssignmentRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mentorAssignmentRepository = mentorAssignmentRepository ?? throw new ArgumentNullException(nameof(mentorAssignmentRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(MentorAssignmentAutomaticCommand request, CancellationToken cancellationToken)
        {
            // Obtener todos los usuarios con el rol "Ingresante" que no tienen mentor asignado
            var ingresantes = await _userRepository.GetAllUsers()
                .Where(u => u.Role.Name == "Ingresante")
                .ToListAsync(cancellationToken);

            if (ingresantes.Count == 0)
            {
                return Error.NotFound("User.NotFound", "No se encontraron usuarios 'Ingresante' sin mentor asignado.");
            }

            // Obtener todos los mentores con el rol "Mentor"
            var mentors = await _userRepository.GetAllUsers()
                .Where(u => u.Role.Name == "Mentor")
                .Include(u => u.AcademicInformation)
                .ThenInclude(ai => ai.Career) // Aseguramos que Career esté incluido
                .ToListAsync(cancellationToken);

            // Obtener asignaciones de mentoría existentes para verificar usuarios sin mentor
            var existingAssignments = await _mentorAssignmentRepository.GetAllMentorAssignment()
                .ToListAsync(cancellationToken);

            // Asignar mentores a los ingresantes
            foreach (var mentor in mentors)
            {
                // Verificar que el mentor no tenga más de 2 mentorías
                var mentorAssignmentsCount = existingAssignments.Count(assignment => assignment.IdMentor == mentor.Id);
                if (mentorAssignmentsCount >= 2)
                {
                    continue; // El mentor ya tiene el máximo permitido de asignaciones
                }

                // Filtrar los ingresantes por la misma carrera del mentor
                var compatibleIngresantes = ingresantes
                    .Where(u => u.AcademicInformation.IdCareer.Value == mentor.AcademicInformation.IdCareer.Value)
                    .ToList();

                // Asignar uno masculino y uno femenino si es posible
                var maleIngresante = compatibleIngresantes.FirstOrDefault(u => u.PersonalInformation.Sex == "Masculino");
                var femaleIngresante = compatibleIngresantes.FirstOrDefault(u => u.PersonalInformation.Sex == "Femenino");

                // Si no hay más opciones, asignar por cualquier sexo
                if (maleIngresante != null && femaleIngresante != null)
                {
                    await AssignMentorToUser(mentor, maleIngresante);
                    await AssignMentorToUser(mentor, femaleIngresante);
                }
                else
                {
                    // Asignar los restantes, si no hay dos de diferentes sexos
                    var remainingIngresantes = compatibleIngresantes.Where(u => u != maleIngresante && u != femaleIngresante).ToList();
                    foreach (var ingresante in remainingIngresantes)
                    {
                        // Si ya se asignaron 2 mentorías al mentor, se debe buscar otro mentor disponible
                        if (mentorAssignmentsCount < 2)
                        {
                            await AssignMentorToUser(mentor, ingresante);
                        }
                        else
                        {
                            // Buscar otro mentor con cupo disponible
                            var availableMentor = mentors.FirstOrDefault(m => existingAssignments.Count(a => a.IdMentor == m.Id) < 2);
                            if (availableMentor != null)
                            {
                                await AssignMentorToUser(availableMentor, ingresante);
                            }
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task AssignMentorToUser(User mentor, User ingresante)
        {
            // Verificar que el ingresante no tenga mentor
            var existingAssignment = await _mentorAssignmentRepository.GetByUserId(ingresante.Id).FirstOrDefaultAsync();
            if (existingAssignment != null)
            {
                return; // El ingresante ya tiene un mentor asignado
            }

            // Crear la asignación de mentoría
            var mentorAssignment = new MentorAssignment(
                new IdMentorAssignment(Guid.NewGuid()),
                mentor.Id,
                ingresante.Id
            );

            // Crear la asignación en el repositorio
            _mentorAssignmentRepository.Create(mentorAssignment);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
        }
    }
}
