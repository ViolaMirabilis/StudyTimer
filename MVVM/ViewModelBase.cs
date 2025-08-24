using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudyTimer.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)     // CallerMemberName so the method can be called without property's name
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      // if property isn't null
        }
    }
}
