using Controller.NotificationSurveyAndFeedback;
using Model.NotificationSurveyAndFeedback;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for GenerateReport.xaml
    /// </summary>
    public partial class GenerateReport : Window
    {
        private static NotificationController nc = new NotificationController();
        public GenerateReport(Doctor d)
        {
            InitializeComponent();
            
            List<Notification> nots = nc.ViewNotificationByJmbg(d.Jmbg);
            int br = 1;
            txtNotification.Text = "";
            if (nots.Count > 0)
            {
                foreach (Notification n in nots)
                {
                    txtNotification.Text += br + ". " + n.Message + "\n";
                    ++br;
                }
            }
            else {
                txtNotification.Text = "Nemate novih notifikacija.";
            }
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
                    str = "izvjestaj";
                }
                HelpProvider.ShowHelp(str);
            }
            else
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)activeWindow);
                HelpProvider.ShowHelp(str);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
