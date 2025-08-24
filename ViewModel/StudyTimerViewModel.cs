using StudyTimer.MVVM;
using StudyTimer.Model;
using System.Windows.Threading;
using System.Windows;

namespace StudyTimer.ViewModel
{
    public class StudyTimerViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _timer;
        private readonly TimerHandler _handler;     // TimerHandlerModel

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
            // Instantiating the Timer
            _handler = new TimerHandler(1, 30);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);      // Updates the timer every second
            _timer.Tick += Timer_Tick;                  // It's a delegate, so no () next to the method needed
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _handler.Tick();        //  calls the Tick method from the Model (which subtracts 1 second from the timer, every tick)
            OnPropertyChanged(nameof(CurrentTime));     // nameof is needed, because it needs the NAME of the property, not the value.
        }

        public void Start()
        {
            _timer.Start();
        }

        public void PauseResume()
        {
            _handler.PauseResume();

            if (_handler.IsPaused)
            {
                PasueResumeButtonContent = "Resume";
            }
            else
            {
                PasueResumeButtonContent = "Pause";
            }
        }
        public void Stop()
        {
            _handler.Stop();
            OnPropertyChanged(nameof(CurrentTime));     // updates the UI to 00:00:00

            
        }
    }
}
