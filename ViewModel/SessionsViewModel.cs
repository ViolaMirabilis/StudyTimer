using StudyTimer.Controls;
using StudyTimer.Model;
using StudyTimer.MVVM;
using System.Collections.ObjectModel;

namespace StudyTimer.ViewModel
{
    public class SessionsViewModel : ViewModelBase
    {

        private readonly SessionManager _sessionManager;
        public ObservableCollection<Session> ListOfSessions     // uses the manager to keep track of sessions
        {
            get {  return _sessionManager.Sessions; }
            set
            {
                ListOfSessions = _sessionManager.Sessions;
            }
        }

        public SessionsViewModel(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            AddPreloadedSession();
            AddPreloadedSession();
            AddPreloadedSession();
        }
        /*public static int SessionsCount
        {
            get { return Sessions.Count; }
        }*/

        // for public access
        public void AddSession(Session session)
        {
            ListOfSessions.Add(session);
        }

        // for local testing
        private void AddPreloadedSession()
        {
            int sessionsCount = ListOfSessions.Count + 1;        // ID of the first session is 1 and the rest is just plus one
            var session = new Session(sessionsCount, DateTime.Now, "test session", TimeSpan.FromSeconds(20).ToString());        // creates a new session
            ListOfSessions.Add(session);
        }



    }
}
