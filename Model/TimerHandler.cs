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
        public TimeSpan RemainingTime { get; private set; }
        public bool IsPaused { get; private set; }


        // Constructor for the handler
        public TimerHandler(int hours, int minutes)
        {
            RemainingTime = TimeSpan.FromMinutes(hours * 60 + minutes);     // e.g., 1h 30 minutes => 1* 60 + 30 = 90 minutes. Formatting is handled in the ViewModel
        }

        // Needs to be async to separate it from the UI 
        public void Tick()
        {
            // If not paused and the timer isn't at 00:00:00
            if (!IsPaused && RemainingTime.TotalSeconds > 0)
            {
                RemainingTime = RemainingTime.Add(TimeSpan.FromSeconds(-1));        // Adds "-1" (so substracts) 1 every second, so the timer decreases     
            }
        }
        // useless?
        public void SetTime(int hours, int minutes)
        {
            RemainingTime = new TimeSpan(hours, minutes, 0);        // Seconds always as zero, so the user can only modify hours/minutes
        }

        public void PauseResume()
        {
            if (IsPaused)       // if true make it false and vice versa. A simple toggle button.
            {
                IsPaused = false;
            }
            else
            {
                IsPaused = true;
            }
        }

        public void Stop()
        {
            IsPaused = true;
            RemainingTime = TimeSpan.Zero;
        }
    }
}
