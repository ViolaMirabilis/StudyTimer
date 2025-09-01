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
using StudyTimer.Model;
using StudyTimer.MVVM;
using StudyTimer.ViewModel;

namespace StudyTimer.View
{
    /// <summary>
    /// Interaction logic for StudyTimerView.xaml
    /// </summary>
    public partial class StudyTimerView : Page
    {
        private StudyTimerViewModel _model;
        private SessionManager _sessionManager;
        public StudyTimerView(StudyTimerViewModel model, SessionManager sessionManager)
        {
            InitializeComponent();
            _sessionManager = sessionManager;
            _model = model;
            
            
            //StudyTimerViewModel vm = new StudyTimerViewModel();     // Creating an instance of the ViewModel and assigning a new DataContext for this View
            DataContext = _model;
            //this.vm = vm;
        }
    }
}
