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
using StudyTimer.Model;
using StudyTimer.View;
using StudyTimer.ViewModel;

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

            #region Managers/Handlers
            SessionManager sessionManager = new SessionManager();
            TimerSettingsManager timerSettingsManager = new TimerSettingsManager();
            SoundManager soundManager = new SoundManager();
            #endregion

            #region ViewModels
            StudyTimerViewModel studyTimerViewModel = new StudyTimerViewModel(soundManager, sessionManager, timerSettingsManager);
            SettingsViewModel settingsViewModel = new SettingsViewModel(timerSettingsManager);
            #endregion

            // initialising all the pages once at the start of the program
            #region Views
            _studyTimerView = new StudyTimerView(studyTimerViewModel, sessionManager);
            _sessionsView = new SessionsView(sessionManager);
            _settingsView = new SettingsView(settingsViewModel);
            #endregion

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