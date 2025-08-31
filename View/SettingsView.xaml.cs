using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;


namespace StudyTimer.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        // So the textbox accepts numbers only. @See https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
