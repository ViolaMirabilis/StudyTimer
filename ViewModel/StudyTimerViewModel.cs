using StudyTimer.MVVM;
using StudyTimer.Model;
using StudyTimer.ViewModel;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Printing;

namespace StudyTimer.ViewModel
{
    public class StudyTimerViewModel : ViewModelBase
    {
        public Session Session { get; set; }        // Needs to bew public because it's used for binding
        public TimerHandler TimerHandler { get; }       // public {get;} for binding purposes
        public SoundManager SoundManager {get;}
        public SessionManager SessionManager { get; }

        #region RelayCommands
        public RelayCommand StartCommand => new RelayCommand(execute => Start(), canExecute => { return true; });       // "can execute" and beyond is optional. This will allow the user to run the command whenever
        public RelayCommand PauseResumeCommand => new RelayCommand(execute => PauseResume(), canExecute => { return true; });
        public RelayCommand StopCommand => new RelayCommand(execute => Stop(), canExecute => { return true; });
        #endregion

        private string _pasueResumeButtonContent = "Pause";
        public string PasueResumeButtonContent
        {
            get { return _pasueResumeButtonContent; }
            set
            {
                if (_pasueResumeButtonContent != value)
                {
                    _pasueResumeButtonContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _DescriptionContent = "";
        public string DescriptionContent
        {
            get { return _DescriptionContent; }
            set
            {
                if (_DescriptionContent != value)
                {
                    _DescriptionContent = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CurrentTime
        {
            get
            {
                return TimerHandler.FormattedRemainingTime();
            }
        }

        public StudyTimerViewModel(SoundManager soundManager, SessionManager sessionManager)
        {
            SessionManager = sessionManager;
            SoundManager = soundManager;
            TimerHandler = new TimerHandler(soundManager);
            TimerHandler.Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimerHandler.Tick();
            OnPropertyChanged(nameof(CurrentTime));
        }

        public void Start()
        {
            PlayNotificationSound();    // temporary ofc
            TimerHandler.SetCreationTime();
            TimerHandler.Start();
        }

        public void PauseResume()
        {
            TimerHandler.PauseResume();
        }
        public void Stop()
        {
            TimerHandler.Stop();
            OnPropertyChanged(nameof(CurrentTime));
            TimerHandler.SetDurationTime();

            Session = new Session(SessionManager.Sessions.Count+1, TimerHandler.CreationTime, DescriptionContent, TimerHandler.DurationTimeFormatted());     // need to get rid of the static counter
            SessionManager.Sessions.Add(Session);
        }

        // Sound manager methods
        public void PlayNotificationSound()
        {
            SoundManager.PlayNotification();
        }

    }
}
