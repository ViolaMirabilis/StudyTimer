using StudyTimer.MVVM;
using StudyTimer.Model;
using StudyTimer.ViewModel;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace StudyTimer.ViewModel
{
    public class StudyTimerViewModel : ViewModelBase
    {
        private Session _session;        // Needs to bew public because it's used for binding
        public TimerHandler TimerHandler { get; }       // public {get;} for binding purposes
        public SoundManager SoundManager {get; set;}
        public RelayCommand StartCommand => new RelayCommand(execute => Start(), canExecute => { return true; });       // "can execute" and beyond is optional. This will allow the user to run the command whenever
        public RelayCommand PauseResumeCommand => new RelayCommand(execute => PauseResume(), canExecute => { return true; });
        public RelayCommand StopCommand => new RelayCommand(execute => Stop(), canExecute => { return true; });

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

        public StudyTimerViewModel(SoundManager soundManager)
        {
            SoundManager = soundManager;
            TimerHandler = new TimerHandler();
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

            _session = new Session(SessionsViewModel.SessionsCount+1, TimerHandler.CreationTime, DescriptionContent, TimerHandler.DurationTimeFormatted());     // need to get rid of the static counter
            MessageBox.Show($"NEW STUDY SESSION\nID: {_session.SessionId}\nCreation time: {_session.CreationTime}\nDescription: {_session.Description}");
            SessionsViewModel.AddSession(_session);     // Static is a bad choice, not OOP/MVVM friendly, but idk how to do it for now.
        }

        // Sound manager methods
        public void PlayNotificationSound()
        {
            SoundManager.PlaySound();
        }

    }
}
