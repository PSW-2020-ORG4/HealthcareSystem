using Controller.UsersAndWorkingTime;
using Model.Users;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ZdravoKorporacija
{
    public partial class ProfileWindow : Window
    { 
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        private Patient patient;
       
        public ProfileWindow()
        {
            InitializeComponent();
            patient = (Patient)userController.ViewProfile(MainWindow.patient.Jmbg);
            FillPatientInforamation();
        }

        private void FillPatientInforamation()
        {

            firstName.Text = patient.Name;
            lastName.Text = patient.Surname;
            jmbg.Text = patient.Jmbg;
            dateOfBirth.Text = patient.DateOfBirth.ToShortDateString();
            homeAdress.Text = patient.HomeAddress;
            cityAdress.Text = patient.City.Name;
            telephoneNumber.Text = patient.Phone;
            emailAdress.Text = patient.Email;
        }

        private void buttonHomePage_Click(object sender, RoutedEventArgs e)
        {
           HomePageWindow homePageWindow = new HomePageWindow();
           homePageWindow.Show();
           this.Close();
        }

        private void editPassword_Click(object sender, RoutedEventArgs e)
        {
            EditPasswordWindow editPasswordWindow = new EditPasswordWindow();
            editPasswordWindow.Show();
            this.Close();
        }

        private void buttonEditInfo_Click(object sender, RoutedEventArgs e)
        {
            EditInformationWindow editInformationWindow = new EditInformationWindow();
            editInformationWindow.Show();
            this.Close();
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void buttonNotification_Click(object sender, RoutedEventArgs e)
        {
            ReviewNotificationWindows reviewNotificationWindows = new ReviewNotificationWindows();
            reviewNotificationWindows.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.O)
            {
                ReviewNotificationWindows reviewNotificationWindows = new ReviewNotificationWindows();
                reviewNotificationWindows.Show();
                this.Close();
            }
            else if (e.Key == Key.Escape)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.I)
            {
                EditInformationWindow editInformationWindow = new EditInformationWindow();
                editInformationWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.H)
            {
                HomePageWindow homePageWindow = new HomePageWindow();
                homePageWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.L)
            {
                EditPasswordWindow editPasswordWindow = new EditPasswordWindow();
                editPasswordWindow.Show();
                this.Close();
            }
            else if (e.Key == Key.A)
            {
                // Moje ALERGIJE
                AllergiesWindow allergiesWindow = new AllergiesWindow();
                allergiesWindow.Show();
            }
            else if (e.Key == Key.T)
            {
                // MOJE Terapije
                ReviewTherapyWindow therapy = new ReviewTherapyWindow();
                therapy.Show();
                this.Close(); ;
            }
        }

        private void buttonAllergy_Click(object sender, RoutedEventArgs e)
        {
            AllergiesWindow allergiesWindow = new AllergiesWindow();
            allergiesWindow.Show();
        }

        private void buttonAnamnesis_Click(object sender, RoutedEventArgs e)
        {
            ReviewTherapyWindow therapy = new ReviewTherapyWindow();
            therapy.Show();
            this.Close();
        }
    }
}
