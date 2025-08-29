using StudyTimer.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Windows.Threading;

namespace StudyTimer.Model
{
    public class TimerHandler : ViewModelBase
    {
        public TimeSpan RemainingTime { get; private set; }
        public DateTime CreationTime { get; private set; }      // Not private because it's assinged in the StudyTimerViewModel
        public TimeSpan DurationTime { get; private set; }      // How long the sesison lasted
        public  DispatcherTimer Timer { get; set; }
        public int Hours { get; private set; } = 0;
        public int Minutes { get; private set; } = 30;      // 30 minutes is the default value of the timer
        public bool IsPaused { get; set; } = false;

        // Constructor for the handler
        public TimerHandler()
        {
            RemainingTime = TimeSpan.FromMinutes(Hours * 60 + Minutes);     // e.g., 1h 30 minutes => 1* 60 + 30 = 90 minutes. Formatting is handled in the ViewModel
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);      // Updates the timer every second
        }

        public void Tick()
        {
            // If not paused and the timer isn't at 00:00:00
            if (!IsPaused && RemainingTime.TotalSeconds > 0)
            {
                RemainingTime = RemainingTime.Add(TimeSpan.FromSeconds(-1));        // Adds "-1" (so substracts) 1 every second, so the timer decreases     
            }
        }

        public void SetTime(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
            RemainingTime = new TimeSpan(Hours, Minutes, 0);        // Seconds always as zero, so the user can only modify hours/minutes
        }

        public void SetCreationTime()
        {
            CreationTime = DateTime.Now;
        }

        public void SetDurationTime()
        {
            DurationTime = DateTime.Now - CreationTime;
        }

        public void Start()
        {
            Timer.Start();
        }

        public void PauseResume()
        {
            IsPaused = !IsPaused;       // a quick toggle
        }

        public void Stop()
        {
            IsPaused = true;
            RemainingTime = TimeSpan.Zero;
            Timer.Stop();
        }

        public string DurationTimeFormatted()
        {
            return DurationTime.ToString(@"hh\:mm\:ss");
        }

        public string FormattedRemainingTime()
        {
            return RemainingTime.ToString(@"hh\:mm\:ss");
        }
    }
}
