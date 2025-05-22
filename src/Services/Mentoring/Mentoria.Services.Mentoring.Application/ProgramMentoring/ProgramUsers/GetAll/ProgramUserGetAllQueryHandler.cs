
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.Common;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;

namespace Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.GetAll
{
    public sealed class ProgramUserGetAllQueryHandler : IRequestHandler<ProgramUserGetAllQuery, ErrorOr<IReadOnlyList<ProgramUserResponse>>>
    {
        private readonly IProgramRepository _programRepository;

        public ProgramUserGetAllQueryHandler(IProgramRepository programRepository)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }

        public  async Task<ErrorOr<IReadOnlyList<ProgramUserResponse>>> Handle(ProgramUserGetAllQuery request, CancellationToken cancellationToken)
        {
            if (await _programRepository.GetById(new IdProgram(request.IdProgram)) is not Program program)
            {
                return Error.NotFound("ProgramNotFound", "No se encontro el programa.");
            }

            var programUsers = program.Users
                .Select(userProgram => new ProgramUserResponse(
                new PersonalInformationResponse(
                userProgram.User!.PersonalInformation!.Id.Value,
                userProgram.User.PersonalInformation.DNI,
                        userProgram.User.PersonalInformation.Name,
                        userProgram.User.PersonalInformation.LastName,
                        userProgram.User.PersonalInformation.Sex,
                        userProgram.User.PersonalInformation.Phone
                    ),
                new RoleResponse(
                userProgram.User.Role!.Id.Value,
                        userProgram.User.Role.Name
                    ),
                    new AcademicInformationResponse(
                        userProgram.User.AcademicInformation!.Id.Value,
                        userProgram.User.AcademicInformation.Code,
                        userProgram.User.AcademicInformation.Email,
                        new CareerResponse(
                            userProgram.User.AcademicInformation.Career!.Id.Value,
                            userProgram.User.AcademicInformation.Career.Name
                ),
                        userProgram.User.AcademicInformation.Cicle,
                        userProgram.User.AcademicInformation.Expectative
                ),
                    userProgram.User.UserName,
                    userProgram.User.State
                )).ToList();

            return programUsers;
        }
    }
}
