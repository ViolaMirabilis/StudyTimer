using StudyTimer.MVVM;
using StudyTimer.Model;

namespace StudyTimer.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly TimerSettingsManager _timerSettingsManager;

        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        private int _minutes;
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                if (_minutes > 60)
                {
                    _minutes = 60;
                }

                OnPropertyChanged();
            }
        }

        public RelayCommand SubmitCommand { get; }      // For SettingsView binding

        public SettingsViewModel(TimerSettingsManager timerSettingsManager)
        {
            _timerSettingsManager = timerSettingsManager;
            Hours = timerSettingsManager.Hours;
            Minutes = timerSettingsManager.Minutes;

            SubmitCommand = new RelayCommand(execute => Submit());
        }

        private void Submit()
        {
            _timerSettingsManager.ChangeTimerSettings(Hours, Minutes);
        }
    }
}
