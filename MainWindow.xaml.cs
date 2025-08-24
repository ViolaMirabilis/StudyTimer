using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudyTimer.View;

namespace StudyTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudyTimerView _studyTimerView;
        private readonly SessionsView _sessionsView;
        private readonly SettingsView _settingsView;
        public MainWindow()
        {
            InitializeComponent();
            // initialising all the pages once at the start of the program
            _studyTimerView = new StudyTimerView();
            _sessionsView = new SessionsView();
            _settingsView = new SettingsView();

            MainWindowDisplay.Navigate(_studyTimerView);      // The program starts with the StudyTimerView 
        }

        private void NavigateToHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Navigate(_studyTimerView);
        }
        
        private void NavigateToSessions_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Navigate(_sessionsView);
        }

        private void NavigateToSettings_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Navigate(_settingsView);
        }

    }
}