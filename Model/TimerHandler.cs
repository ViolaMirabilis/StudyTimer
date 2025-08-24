using StudyTimer.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;

namespace StudyTimer.Model
{
    public class TimerHandler : ViewModelBase
    {
        private DispatcherTimer _timer;
        private TimeSpan _remainingTime;
        private bool _isPaused;

        public event PropertyChangedEventHandler PropertyChanged;

        // displays curre
        public string? CurrentTime
        {
            get { return _remainingTime.ToString(@"hh\:mm\:ss"); }
        }

        public TimerHandler()
        {
            _remainingTime = TimeSpan.FromMinutes(30);      // 30 minutes at the start
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;       // delegate, without ()
        }

        private void Timer_Tick(object sender, EventArgs e)     // tick needs to be in this format
        {
            if (!_isPaused && _remainingTime.TotalSeconds > 0)
            {
                _remainingTime = _remainingTime.Add(TimeSpan.FromSeconds(-1));      // every second it subtracts one, so the timer goes down
                OnPropertyChanged(nameof(CurrentTime));     
            }
        }
        public void SetTime(int hours, int minutes)
        {
            _remainingTime = new TimeSpan(hours, minutes, 0);        // minutes always 0
            OnPropertyChanged(nameof(CurrentTime));
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Pasue()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        public void Stop()
        {
            _timer.Stop();
            _remainingTime = TimeSpan.Zero;
            OnPropertyChanged(nameof(CurrentTime));
        }
    }
}
