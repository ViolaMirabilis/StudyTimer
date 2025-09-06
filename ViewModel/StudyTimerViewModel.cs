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
        private readonly TimerSettingsManager _timerSettingsManager;
        public TimerHandler TimerHandler { get; }       // public {get;} for binding purposes
        public SoundManager SoundManager {get;}
        public SessionManager SessionManager { get; }

        #region RelayCommands
        public RelayCommand StartCommand => new RelayCommand(execute => Start(), canExecute => { return true; });       // "can execute" and beyond is optional. This will allow the user to run the command whenever
        public RelayCommand PauseResumeCommand => new RelayCommand(execute => PauseResume(), canExecute => { return true; });
        public RelayCommand StopCommand => new RelayCommand(execute => Stop(), canExecute => { return true; });
        #endregion

        private string _pauseResumeButtonContent = "Pause";
        public string PauseResumeButtonContent
        {
            get
            {
                return !TimerHandler.IsPaused ? "Pause" : "Resume";     // gets a value based on the bool
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

        private string _hours = "00";
        public string Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        private string _minutes = "00";
        public string Minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        private string _seconds = "00";
        public string Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public StudyTimerViewModel(SoundManager soundManager, SessionManager sessionManager, TimerSettingsManager timerSettingsManager)
        {
            _timerSettingsManager = timerSettingsManager;
            SessionManager = sessionManager;
            SoundManager = soundManager;
            TimerHandler = new TimerHandler(soundManager);
            TimerHandler.Timer.Tick += Timer_Tick;

            _timerSettingsManager.TimerSettingsChanged += OnTimerSettingsChanged;
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
            OnPropertyChanged(nameof(PauseResumeButtonContent));
        }
        public void Stop()
        {
            TimerHandler.Stop();
            OnPropertyChanged(nameof(CurrentTime));
            TimerHandler.SetDurationTime();

            var session = new Session(SessionManager.Sessions.Count+1, TimerHandler.CreationTime, DescriptionContent, TimerHandler.DurationTimeFormatted());     // need to get rid of the static counter
            SessionManager.Sessions.Add(session);
            SessionManager.WriteSessionsToJson(session);
        }

        // Sound manager methods
        public void PlayNotificationSound()
        {
            SoundManager.PlayNotification();
        }

        public void OnTimerSettingsChanged()
        {
            TimerHandler.SetTime(_timerSettingsManager.Hours, _timerSettingsManager.Minutes);       // Assigns the values to the timer (both UI and logic)
        }

    }
}
