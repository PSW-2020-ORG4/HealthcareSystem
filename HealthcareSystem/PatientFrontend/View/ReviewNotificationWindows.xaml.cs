using Controller.NotificationSurveyAndFeedback;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model.Users;
using Controller.UsersAndWorkingTime;

namespace ZdravoKorporacija
{
    public partial class ReviewNotificationWindows : Window
    {
        public string[] typeNotify { get; set; }
        public NotificationController notificationController = new NotificationController();
        readonly List<Notification> notifications = new List<Notification>();
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        private Patient patient;
        public ReviewNotificationWindows()
        {
            InitializeComponent();
            typeNotify = new string[] { "PREGLED", "TERAPIJA", "PREGLED I TERAPIJA" };

            patient = (Patient)userController.ViewProfile(MainWindow.patient.Jmbg);

            notifications = notificationController.ViewNotificationByJmbg(patient.Jmbg);

            dataGridNotification.DataContext = notifications;
 
            DataContext = this;
        }

        private void buttonViewProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }

        private void buttonHomePage_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
            this.Close();
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void notificationComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F1)
            {
                notificationComboBox.IsDropDownOpen = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                ProfileWindow profileWindow = new ProfileWindow();
                profileWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.H)
            {
                HomePageWindow homePageWindow = new HomePageWindow();
                homePageWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.Escape)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        private void notificationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Notification> resultNotifications = new List<Notification>();

            if (notificationComboBox.SelectedItem.ToString().Equals("PREGLED"))
            {
                foreach (Notification notify in notifications)
                {
                    if (notify.Type == TypeOfNotification.Pregled)
                    {
                        resultNotifications.Add(notify);
                    }
                }
            }
            if (notificationComboBox.SelectedItem.ToString().Equals("TERAPIJA"))
            {
                foreach (Notification notify in notifications)
                {
                    if (notify.Type == TypeOfNotification.Terapija)
                    {
                        resultNotifications.Add(notify);
                    }
                }
            }
            
            if (notificationComboBox.SelectedItem.ToString().Equals("PREGLED I TERAPIJA"))
            {
                resultNotifications = notifications;
            }
            
            dataGridNotification.DataContext = resultNotifications;
        }
    }
}
