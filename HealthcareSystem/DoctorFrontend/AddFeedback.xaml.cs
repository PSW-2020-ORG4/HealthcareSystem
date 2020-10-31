using Controller.NotificationSurveyAndFeedback;
using Model.Users;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddFeedback.xaml
    /// </summary>
    public partial class AddFeedback : Window
    {
        public AddFeedback()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            IInputElement focusedControl = FocusManager.GetFocusedElement(activeWindow);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                if (str.Equals("index"))
                {
                    str = "ocjena";
                }
                HelpProvider.ShowHelp(str);
            }
            else
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)activeWindow);
                HelpProvider.ShowHelp(str);
            }
        }
    }
}
