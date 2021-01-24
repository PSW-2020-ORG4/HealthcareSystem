using Controller.UsersAndWorkingTime;
using Model.Users;
using Syncfusion.XPS;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View;

namespace ZdravoKorporacija
{
    public partial class MainWindow
    {
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        public static Patient patient = new Patient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSignIn_Click(object sender, RoutedEventArgs e)
        {
            string patientUserName = username.Text;
            string patientPassword = password.Password;

            if (patientUserName == "" || patientPassword == "")
            {
                ValidationMessageWindow validationMessage = new ValidationMessageWindow();
                validationMessage.Show();

            }
            else
            {
                patient = (Patient)userController.SignIn(patientUserName, patientPassword);

                if (patient != null)
                { 
                    HomePageWindow homePageWindow = new HomePageWindow();
                    homePageWindow.Show();
                    this.Close();
                }
                else
                {
                    SignInMessageWindow signInMessage = new SignInMessageWindow();
                    signInMessage.Show();
                    resetInputText();
                }
            }

        }

        private void buttonStartRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            resetInputText();
        }

        private void resetInputText()
        {
            username.Text = "";
            password.Password = "";
        }
    }
}
