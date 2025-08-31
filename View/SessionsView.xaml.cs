using StudyTimer.Model;
using StudyTimer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudyTimer.View
{
    /// <summary>
    /// Interaction logic for SessionsView.xaml
    /// </summary>
    public partial class SessionsView : Page
    {
        private readonly SessionManager _sessionManager;
        public SessionsView(SessionManager sessionManager)
        {
            InitializeComponent();
            _sessionManager = sessionManager;
            SessionsViewModel vm = new SessionsViewModel(sessionManager);
            DataContext = vm;
        }
    }
}
