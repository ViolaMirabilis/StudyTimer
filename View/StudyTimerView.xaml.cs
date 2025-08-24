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
using StudyTimer.ViewModel;

namespace StudyTimer.View
{
    /// <summary>
    /// Interaction logic for StudyTimerView.xaml
    /// </summary>
    public partial class StudyTimerView : Page
    {
        public StudyTimerView()
        {
            InitializeComponent();
            StudyTimerViewModel vm = new StudyTimerViewModel();     // Creating an instance of the ViewModel and assigning a new DataContext for this View
            DataContext = vm;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void PauseResume_Click(object sender, RoutedEventArgs e)
        {
            //asd
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            //asd
        }
    }
}
