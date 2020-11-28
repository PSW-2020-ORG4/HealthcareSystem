using Controller.DrugAndTherapy;
using Controller.ExaminationAndPatientCard;
using Controller.PlacementInARoomAndRenovationPeriod;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Secretary;
using Model.Users;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ZdravoKorporacija
{
    public partial class EditInformationWindow : Window
    {
        public CityController cityController = new CityController();
        private List<City> cities = new List<City>();
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        private PatientCard patientCard = new PatientCard();
        private PatientCardController patientCardController = new PatientCardController();
        private ExaminationController examinationController = new ExaminationController();
        private PlacementInSickRoomController placementInSickRoomController = new PlacementInSickRoomController();
        private TherapyController therapyController = new TherapyController();
        private Patient patient = new Patient();
        private City patientCity = new City();
        public EditInformationWindow()
        {
            InitializeComponent();

            cities = cityController.ViewCities();
            cityAdress.DataContext = cities;

            patient = (Patient)userController.ViewProfile(MainWindow.patient.Jmbg);

            patientCard = patientCardController.ViewPatientCard(patient.Jmbg);

            FillPatientInformation();

        }

        private void FillPatientInformation()
        {

            firstName.Text = patient.Name;
            lastName.Text = patient.Surname;
            jmbg.Text = patient.Jmbg;
            if (patient.Gender == GenderType.M)
            {
                male.IsChecked = true;
            }
            else
            {
                female.IsChecked = false;
            }
            dateOfBirthPicker.SelectedDate = patient.DateOfBirth;
            homeAdress.Text = patient.HomeAddress;

            patientCity = cityController.getCityByZipCode(patient.City.ZipCode);
            foreach (City c in cities)
            {
                if (c.ZipCode == patientCity.ZipCode)
                {
                    cityAdress.SelectedItem = c;
                    break;
                }
            }

            telephoneNumber.Text = patient.Phone;
            emailAdress.Text = patient.Email;
        }

        private void buttonConfrim_Click(object sender, RoutedEventArgs e)
        {

            if (firstName.Text.Equals("") || lastName.Text.Equals("") || jmbg.Text.Equals("") || dateOfBirthPicker.Text.Equals("") || telephoneNumber.Text.Equals("") || homeAdress.Text.Equals("") || cityAdress.Text.Equals("") || emailAdress.Text.Equals(""))
            {
                ValidationMessageWindow validationWindow = new ValidationMessageWindow();
                validationWindow.Show();
            }
            else
            {
                string name = firstName.Text;

                string surname = lastName.Text;

                GenderType gender;

                if (male.IsChecked == true)
                {
                    gender = GenderType.M;
                }
                else
                {
                    gender = GenderType.Z;
                }

                DateTime dateOfBirth = (DateTime)dateOfBirthPicker.SelectedDate;

                string telephone = telephoneNumber.Text;

                string street = homeAdress.Text;

                City city = (City)cityAdress.SelectedItem;

                string email = emailAdress.Text;

                patient = new Patient(MainWindow.patient.Jmbg, name, surname, dateOfBirth, gender, city, street, telephone, email, MainWindow.patient.Username, MainWindow.patient.Password, MainWindow.patient.DateOfRegistration, MainWindow.patient.IsGuest);

                PatientCard pc = new PatientCard(patient);

                if (userController.EditProfile(patient) != null && patientCardController.EditPatientCard(pc) != null)
                {
                    List<Examination> examinations = examinationController.ViewExaminationsByPatient(patient.Jmbg);

                    foreach (Examination exm in examinations)
                    {
                        exm.patientCard = pc;
                        examinationController.EditExamination(exm);
                    }

                    List<Therapy> therapies = therapyController.ViewAllTherapyByPatient(patient.Jmbg);

                    foreach (Therapy t in therapies)
                    {
                        t.patientCard = pc;
                        therapyController.EditTherapy(t);
                    }

                    List<PlacemetnInARoom> placemetnInARooms = placementInSickRoomController.ViewPatientPlacements(patient.Jmbg);

                    foreach (PlacemetnInARoom p in placemetnInARooms)
                    {
                        p.patientCard = pc;
                        placementInSickRoomController.EditPlacement(p);
                    }

                    ProfileWindow profileWindow = new ProfileWindow();
                    profileWindow.Show();
                    this.Close();
                }
                else
                {
                    ValidationMessageWindow validationMessageWindow = new ValidationMessageWindow();
                    validationMessageWindow.caption.Text = "Neuspešna izmena podataka!";
                    validationMessageWindow.Show();
                }
            }
        }
        
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
            this.Close();
        }
       
        private void Male_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                female.IsChecked = false;
                male.IsChecked = true;
            }
        }

        private void Female_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                female.IsChecked = true;
                male.IsChecked = false;
            }
        }

        private void cityAdress_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                cityAdress.IsDropDownOpen = true;
            }
        }

        private void dateOfBirth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                dateOfBirthPicker.IsDropDownOpen = true;
            }
        }
    }
}

