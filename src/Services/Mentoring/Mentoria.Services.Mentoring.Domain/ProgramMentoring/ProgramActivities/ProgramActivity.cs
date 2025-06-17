using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivitiesSolutions;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities
{
    public sealed class ProgramActivity : EntityGeneric<IdProgramActivity>
    {
        public IdProgram IdProgram { get; private set; } = default!;
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string State { get; private set; } = string.Empty;

        public Program? Program { get; private set; } = default!;

        private readonly HashSet<ProgramActivitySolution> _programActivitySolutions = new();
        public IReadOnlyCollection<ProgramActivitySolution> ProgramActivitySolutions => _programActivitySolutions.ToList();

        public ProgramActivity() { }
        public ProgramActivity(IdProgramActivity id, IdProgram idProgram, string name, string description, DateTime startDate, DateTime endDate, string state) : base(id)
        {
            IdProgram = idProgram ?? throw new ArgumentNullException(nameof(idProgram));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            StartDate = startDate;
            EndDate = endDate;
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void Update(IdProgram idProgram, string name, string description, DateTime startDate, DateTime endDate, string state)
        {
            IdProgram = idProgram ?? throw new ArgumentNullException(nameof(idProgram));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            StartDate = startDate;
            EndDate = endDate;
            State = state ?? throw new ArgumentNullException(nameof(state));
            UpdateAt = DateTime.Now;
        }

        public void ChangeState(string state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
            UpdateAt = DateTime.Now;
        }

        public void AddProgramActivitySolution(ProgramActivitySolution programActivitySolution)
        {
            _programActivitySolutions.Add(programActivitySolution ?? throw new ArgumentNullException(nameof(programActivitySolution)));
        }

        public ProgramActivitySolution GetProgramActivitySolutionById(IdProgramActivitySolution idProgramActivitySolution)
        {
            return _programActivitySolutions.FirstOrDefault(p => p.Id == idProgramActivitySolution) ?? throw new ArgumentNullException(nameof(idProgramActivitySolution));
        }

        public ProgramActivitySolution? GetProgramActivitySolutionByUserId(IdUser idUser)
        {
            return _programActivitySolutions.FirstOrDefault(p => p.IdUser == idUser);
        }

        public void UpdateProgramActivitySolution(ProgramActivitySolution programActivitySolution)
        {
            if (_programActivitySolutions.Contains(programActivitySolution))
            {
                _programActivitySolutions.Remove(programActivitySolution);
                _programActivitySolutions.Add(programActivitySolution);
            }
            else
            {
                throw new ArgumentException("The program activity solution does not exist in the collection.");
            }
        }

        public void RemoveProgramActivitySolution(ProgramActivitySolution programActivitySolution)
        {
            if (_programActivitySolutions.Contains(programActivitySolution))
            {
                _programActivitySolutions.Remove(programActivitySolution);
            }
            else
            {
                throw new ArgumentException("The program activity solution does not exist in the collection.");
            }
        }

        public void ClearProgramActivitySolutions()
        {
            _programActivitySolutions.Clear();
        }
    }
}
