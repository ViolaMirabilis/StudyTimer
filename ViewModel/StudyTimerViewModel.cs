using StudyTimer.MVVM;
using StudyTimer.Model;
using System.Windows.Threading;

namespace StudyTimer.ViewModel
{
    public class StudyTimerViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _timer;
        private readonly TimerHandler _handler;     // TimerHandlerModel

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
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _handler.Tick();        //  calls the Tick method from the Model (which subtracts 1 second from the timer, every tick)
            OnPropertyChanged(nameof(CurrentTime));     // \nameof is needed, because it needs the NAME of the property, not the value.
        }

        public void Pause()
        {
            _handler.Pause();
        }
        public void Resume()
        {
            _handler.Resume();
        }
        public void Stop()
        {
            _handler.Stop();
            OnPropertyChanged(nameof(CurrentTime));     // updates the UI to 00:00:00
        }
    }
}
