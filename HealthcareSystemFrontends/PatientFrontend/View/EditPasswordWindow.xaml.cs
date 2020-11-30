using Controller.UsersAndWorkingTime;
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
using ZdravoKorporacija.View;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for EditPasswordWindow.xaml
    /// </summary>
    public partial class EditPasswordWindow : Window
    {
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        private Patient patient;

        public EditPasswordWindow()
        {
            InitializeComponent();

            patient = (Patient)userController.ViewProfile(MainWindow.patient.Jmbg);

            usernamePatient.Text = patient.Username;     
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
            this.Close();
        }

        private void buttonConfrim_Click(object sender, RoutedEventArgs e)
        {
            if (oldPassword.Password.Equals("") || newPassword.Password.Equals("") || confirmPassword.Password.Equals(""))
            {
                ValidationMessageWindow validationWindow = new ValidationMessageWindow();
                validationWindow.Show();
            }
            else if (!oldPassword.Password.Equals(userController.ViewProfile(MainWindow.patient.Jmbg).Password))
            {
                ValidationOldPasswordWindow validationOldPassword = new ValidationOldPasswordWindow();
                validationOldPassword.Show();
            }
            else if (!confirmPassword.Password.Equals(newPassword.Password))
            {
                ValidationNewPasswordWindow validationNewPassword = new ValidationNewPasswordWindow();
                validationNewPassword.Show();
            }
            else
            {

                patient.Password = newPassword.Password;

                if (userController.EditProfile(patient) != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    SuccessfulUpdatePasswordWindow successfulUpdatePasswordWindow = new SuccessfulUpdatePasswordWindow();
                    successfulUpdatePasswordWindow.Show();
                    this.Close();
                }
                else
                {
                    ValidationMessageWindow validationWindowPassword = new ValidationMessageWindow();
                    validationWindowPassword.caption.Text = "Unesite najmanje 8 karaktera, jedan broj i bar jedno malo i veliko slovo!";
                    validationWindowPassword.Show();
                }
            }
        }
    }
}
