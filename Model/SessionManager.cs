using System.Collections.ObjectModel;

namespace StudyTimer.Model
{
    public class SessionManager
    {
        public ObservableCollection<Session> Sessions { get; } = new ObservableCollection<Session>();       // Readonly, just adding contents to it
    }
}
