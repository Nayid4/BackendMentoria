using Mentoria.Services.Mentoring.Domain.Careers;
using Mentoria.Services.Mentoring.Domain.Generics;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramActivities;
using Mentoria.Services.Mentoring.Domain.ProgramMentoring.ProgramUsers;
using Mentoria.Services.Mentoring.Domain.Users;

namespace Mentoria.Services.Mentoring.Domain.ProgramMentoring.Programs
{
    public sealed class Program : EntityGeneric<IdProgram>
    {
        public IdCareer IdCareer { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Type { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public int MaximumNumberOfParticipants { get; private set; } = 0;

        public Career? Career { get; private set; } = default!;


        private HashSet<ProgramUser> _users = new HashSet<ProgramUser>();
        public IReadOnlyCollection<ProgramUser> Users => _users.ToList();

        private HashSet<ProgramActivity> _activities = new HashSet<ProgramActivity>();
        public IReadOnlyCollection<ProgramActivity> Activities => _activities.ToList();

        public Program(IdProgram id, IdCareer idCareer, string name, string type, string description, int maximumNumberOfParticipants)
            : base(id)
        {
            IdCareer = idCareer ?? throw new ArgumentNullException(nameof(idCareer));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            MaximumNumberOfParticipants = maximumNumberOfParticipants;
        }

        public void Update(IdCareer idCareer, string name, string type, string description, int maximumNumberOfParticipants)
        {
            IdCareer = idCareer ?? throw new ArgumentNullException(nameof(idCareer));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            MaximumNumberOfParticipants = maximumNumberOfParticipants;
            UpdateAt = DateTime.Now;
        }

        public void AddUser(ProgramUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _users.Add(user);
            UpdateAt = DateTime.Now;
        }

        public ProgramUser? GetUserById(IdUser idProgramUser)
        {
            if (idProgramUser == null) throw new ArgumentNullException(nameof(idProgramUser));
            return _users.FirstOrDefault(x => x.User!.Id == idProgramUser) ?? null;
        }

        public void RemoveUser(ProgramUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _users.Remove(user);
            UpdateAt = DateTime.Now;
        }

        public void ClearUsers()
        {
            _users.Clear();
            UpdateAt = DateTime.Now;
        }


        public void AddActivity(ProgramActivity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));
            _activities.Add(activity);
            UpdateAt = DateTime.Now;
        }

        public ProgramActivity? GetActivityById(IdProgramActivity idProgramActivity)
        {
            if (idProgramActivity == null) throw new ArgumentNullException(nameof(idProgramActivity));
            return _activities.FirstOrDefault(x => x.Id == idProgramActivity) ?? null;
        }

        public void RemoveActivity(ProgramActivity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));
            _activities.Remove(activity);
            UpdateAt = DateTime.Now;
        }

        public void ClearActivities()
        {
            _activities.Clear();
            UpdateAt = DateTime.Now;
        }
    }
}
