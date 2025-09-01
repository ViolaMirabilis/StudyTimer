namespace StudyTimer.Model
{
    public class TimerSettingsManager
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public event Action TimerSettingsChanged;

        public void ChangeTimerSettings(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
            TimerSettingsChanged?.Invoke();     // Notifies the StudyTimerView (and view model) when settings changed
        }
    }
}
