using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.Secretary;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;


namespace ZdravoKorporacija
{

    public partial class RegistrationWindow : Window
    {
       
        public CityController cityController = new CityController();
        List<City> cities = new List<City>();
        private static PatientController patientController = new PatientController();
        private UserController userController = new UserController(patientController);
        PatientCardController patientCardController = new PatientCardController();
        Patient patient;
        PatientCard patientCard;

        public RegistrationWindow()
        {
            InitializeComponent();

            cities = cityController.ViewCities();
            cityAdress.DataContext = cities;
            DataContext = this;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text.Equals("") || lastName.Text.Equals("") || jmbg.Text.Equals("") || dateOfBirthPicker.Text.Equals("") || telephoneNumber.Text.Equals("") || homeAdress.Text.Equals("") || cityAdress.Text.Equals("") || emailAdress.Text.Equals("") || user.Text.Equals("") || pass.Text.Equals(""))
            {
                ValidationMessageWindow validationWindow = new ValidationMessageWindow();
                validationWindow.Show();
            }
            else
            {
                string jmbgPatient = jmbg.Text;

                string name = firstName.Text;

                string surname = lastName.Text;

                DateTime dateOfBirth = (DateTime)dateOfBirthPicker.SelectedDate;

                GenderType gender;

                if (male.IsChecked == true)
                {
                    gender = GenderType.M;
                }
                else
                {
                    gender = GenderType.Z;
                }

                string telephone = telephoneNumber.Text;

                string street = homeAdress.Text;

                City  city = (City)cityAdress.SelectedItem;

                string email = emailAdress.Text;

                string username = user.Text;

                string password = pass.Text;

                DateTime currentDate = DateTime.Now;

                bool isGuest = false;

                patient = new Patient(jmbgPatient, name, surname, dateOfBirth, gender, city, street, telephone, email, username, password, currentDate, isGuest);

                patientCard = new PatientCard(patient);

                if(userController.Register(patient) != null && patientCardController.CreatePatientCard(patientCard) != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                    SuccessfulRegistrationWindow successfulRegistrationWindow = new SuccessfulRegistrationWindow();
                    successfulRegistrationWindow.ShowDialog();
                }
                else
                {
                    NotSuccessfulRegistratonWindow notSuccessfulRegistratonWindow = new NotSuccessfulRegistratonWindow();
                    notSuccessfulRegistratonWindow.ShowDialog();
                }
            }
        }

        private void Male_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                female.IsChecked = false;
                male.IsChecked = true;
            }
        }

        private void Female_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                female.IsChecked = true;
                male.IsChecked = false;
            }
        }

        private void cityAdress_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                cityAdress.IsDropDownOpen = true;
            }

        }

        private void dateTime_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                dateOfBirthPicker.IsDropDownOpen = true;
            }
        }


        private string _firstName;

        private string _lastName;

        private string _jmbg;

        private string _telephoneNumber;

        private string _emailAddress;

        private string _username;

        private string _password;

        private string _homeAddress;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string JMBG
        {
            get { return _jmbg; }
            set { _jmbg = value; }
        }

        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set { _telephoneNumber = value; }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }

            set { _password = value; }
        }

        public string HomeAddress
        {
            get { return _homeAddress; }
            set { _homeAddress = value; }
        }
    }
}
