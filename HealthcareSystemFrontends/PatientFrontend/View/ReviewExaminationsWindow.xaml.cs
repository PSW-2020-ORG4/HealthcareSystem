using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Users;
using Syncfusion.XPS;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ZdravoKorporacija.DTO;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for ReviewExaminationsWindow.xaml
    /// </summary>
    ///
    public partial class ReviewExaminationsWindow : Window
    {
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        private ExaminationController examinationController = new ExaminationController();
        private List<Examination> patientExamiantions = new List<Examination>();
        private List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
        private Patient patient = new Patient();

        public ReviewExaminationsWindow()
        {
            InitializeComponent();
            patient = (Patient)userController.ViewProfile(MainWindow.patient.Jmbg);
            patientExamiantions = examinationController.ViewExaminationsByPatient(patient.Jmbg);

            foreach (Examination e in patientExamiantions)
            {
                string type;

                if (e.Type == TypeOfExamination.Opsti)
                {
                    type = "OPŠTI";
                }
                else if (e.Type == TypeOfExamination.Operacija)
                {
                    type = "OPERACIJA";
                }
                else
                {
                    type = "SPECIJALISTIČKI";
                }

                int id = e.IdExamination;

                string date = e.DateAndTime.ToShortDateString();

                string time = e.DateAndTime.ToShortTimeString();

                string doctor = e.doctor.Name + " " + e.doctor.Surname + " " + e.doctor.Jmbg;

                ExaminationDTO examinationDTO = new ExaminationDTO(id, type, date, time, doctor);

                examinationDTOs.Add(examinationDTO);
            }

            dataGridExaminations.DataContext = examinationDTOs;
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
    }
}
