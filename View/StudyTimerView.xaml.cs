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

namespace StudyTimer.View
{
    /// <summary>
    /// Interaction logic for StudyTimerView.xaml
    /// </summary>
    public partial class StudyTimerView : Page
    {
        public TimerHandler Timer { get; set; }
        public StudyTimerView()
        {
            InitializeComponent();
            Timer = new TimerHandler();
            DataContext = Timer;
            Timer.Start();
        }
    }
}
