using StudyTimer.MVVM;
using StudyTimer.Model;
using StudyTimer.ViewModel;
using System.Windows.Threading;
using System.Windows;
using System.Collections.ObjectModel;

namespace StudyTimer.ViewModel
{
    public class StudyTimerViewModel : ViewModelBase
    {
        private Session _session;        // Needs to bew public because it's used for binding
        private readonly DispatcherTimer _timer;
        private readonly TimerHandler _handler;   // TimerHandlerModel
        private readonly SoundManager _soundManager;

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

        public string CurrentTime       // Returns formatted Timer Countdown UI. It is a public property, so it can be bound to the UI.
        {
            get
            {
                return _handler.RemainingTime.ToString(@"hh\:mm\:ss");
            }
        }


        public StudyTimerViewModel()
        {
            _soundManager = new SoundManager();

            // Instantiating the Timer
            _handler = new TimerHandler(0, 0);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);      // Updates the timer every second
            _timer.Tick += Timer_Tick;                  // It's a delegate, so no () next to the method needed

        }

        public void Start()
        {
            PlayNotificationSound();    // temporary ofc
            _handler.IsPaused = false;      // Without this, "pause" is needed to be pressed in order to start a new timer, once the previous one has been stopped. (Stop then Play didn't work before).
            _handler.SetTime(0, 30);
            _handler.SetCreationTime();     // DateTime.Now
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _handler.Tick();        //  calls the Tick method from the Model (which subtracts 1 second from the timer, every tick)
            OnPropertyChanged(nameof(CurrentTime));     // nameof is needed, because it needs the NAME of the property, not the value.
        }

        public void PauseResume()
        {
            _handler.PauseResume();

            PasueResumeButtonContent = _handler.IsPaused ? "Resume" : "Pause";

        }
        public void Stop()
        {

            _handler.Stop();
            _handler.SetDurationTime();

            _session = new Session(SessionsViewModel.SessionsCount+1, _handler.CreationTime, DescriptionContent, _handler.FormatDuration());     // need to get rid of the static counter
            //MessageBox.Show($"NEW STUDY SESSION\nID: {_session.SessionId}\nCreation time: {_session.CreationTime}\nDescription: {_session.Description}");
            SessionsViewModel.AddSession(_session);     // Static is a bad choice, not OOP/MVVM friendly, but idk how to do it for now.
            OnPropertyChanged(nameof(CurrentTime));     // Updates the UI to 00:00:00
        }

        // Sound manager methods
        public void PlayNotificationSound()
        {
            _soundManager.PlaySound();
        }
    }
}
