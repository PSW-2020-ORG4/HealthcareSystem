using Controller.NotificationSurveyAndFeedback;
using Controller.UsersAndWorkingTime;
using Model.NotificationSurveyAndFeedback;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Windows;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MessageUnconfirmedDrug.xaml
    /// </summary>
    public partial class MessageUnconfirmedDrug : Window
    {
        private static Notification n = new Notification();
        private static NotificationController nc = new NotificationController();
        private static ManagerController mc = new ManagerController();
        private static UserController uc = new UserController(mc);
        public MessageUnconfirmedDrug()
        {
            InitializeComponent();
            txtReason.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtReason.Text))
            {
                MessageBox.Show("Morate unijeti razlog odbijanja.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtReason.Clear();
                txtReason.Focus();
                return;
            }
            
            int idNotification = nc.getLastId();
            n.Id = ++idNotification;
            n.Type = TypeOfNotification.Lek;
            
            List<User> managers = uc.ViewAllUsers();
            n.JmbgOfReceiver = managers[0].Jmbg;
            n.Message = txtReason.Text;
            
            nc.SendNotification(n);
    
            this.Close();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var t = new CheckingDrug();
            t.Show();
            this.Close();
     
        }
    }
}
