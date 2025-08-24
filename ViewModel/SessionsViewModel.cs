using StudyTimer.Controls;
using StudyTimer.Model;
using StudyTimer.MVVM;
using System.Collections.ObjectModel;

namespace StudyTimer.ViewModel
{
    public class SessionsViewModel : ViewModelBase
    {
        // list of all the sessions (eventually will be remade to .JSON, hopefully)
        private readonly ObservableCollection<Session> _sessions;
        public ObservableCollection<Session> Sessions
        {
            get { return _sessions; }
        }


        public SessionsViewModel()
        {
            _sessions = new ObservableCollection<Session>();
            AddSession();       // temporary filler
            AddSession();
            AddSession();
        }

        private void AddSession()
        {
            int sessionsCount = _sessions.Count + 1;        // ID of the first session is 1 and the rest is just plus one
            var session = new Session(sessionsCount, DateTime.Now, "xd", TimeSpan.FromMinutes(90));        // creates a new session
            _sessions.Add(session);
        }

    }
}
