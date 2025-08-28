using StudyTimer.Controls;
using StudyTimer.Model;
using StudyTimer.MVVM;
using System.Collections.ObjectModel;

namespace StudyTimer.ViewModel
{
    public class SessionsViewModel : ViewModelBase
    {
        // list of all the sessions (eventually will be remade to .JSON, hopefully)
        private static readonly ObservableCollection<Session> _sessions = new ObservableCollection<Session>();
        public static int SessionsCount
        {
            get { return _sessions.Count; }
        }

        public ObservableCollection<Session> Sessions
        {
            get { return _sessions; }
        }


        public SessionsViewModel()
        {
            AddSession();
        }

        // for public access
        public static void AddSession(Session session)
        {
            _sessions.Add(session);
        }

        // for local testing
        private void AddSession()
        {
            int sessionsCount = Sessions.Count + 1;        // ID of the first session is 1 and the rest is just plus one
            var session = new Session(sessionsCount, DateTime.Now, "test session", TimeSpan.FromSeconds(20).ToString());        // creates a new session
            Sessions.Add(session);
        }



    }
}
