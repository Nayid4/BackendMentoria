


using Mentoria.Services.Mentoring.Domain.MentorAssignments;
using Mentoria.Services.Mentoring.Domain.Primitives;

namespace Mentoria.Services.Mentoring.Application.MentorAssignments.Delete
{
    public sealed class MentorAssignmentsDeleteCommandHandler : IRequestHandler<MentorAssignmentsDeleteCommand, ErrorOr<Unit>>
    {
        private readonly IMentorAssignmentRepository _mentorAssignmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MentorAssignmentsDeleteCommandHandler(IMentorAssignmentRepository mentorAssignmentRepository, IUnitOfWork unitOfWork)
        {
            _mentorAssignmentRepository = mentorAssignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(MentorAssignmentsDeleteCommand request, CancellationToken cancellationToken)
        {
            if(await _mentorAssignmentRepository.GetById(new IdMentorAssignment(request.Id)) is not MentorAssignment mentorAssignment)
            {
                return Error.NotFound("MentorAssignment.NotFound", "La asignación de mentor no se encuentra registrada.");
            }

            _mentorAssignmentRepository.Delete(mentorAssignment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
