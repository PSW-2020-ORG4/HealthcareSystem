using Controller.DrugAndTherapy;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Users;
using Repository;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZdravoKorporacija.DTO;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for ReviewTherapyWindow.xaml
    /// </summary>
    public partial class ReviewTherapyWindow : Window
    {
        TherapyController therapyController = new TherapyController();
        List<Therapy> therapies = new List<Therapy>();
        List<TherapyDTO> therapyDTOs = new List<TherapyDTO>();
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        private Patient patient;
       

        public ReviewTherapyWindow()
        {
            InitializeComponent();

            patient = (Patient)userController.ViewProfile(MainWindow.patient.Jmbg);

            therapies = therapyController.ViewActiveTherapyByPatient(patient.Jmbg);

            foreach (Therapy t in therapies)
            {
                string drugName = t.drug.Name;
                string anamnesis = t.Anamnesis;
                int dailyDose = t.DailyDose;
                string startDate = t.StartDate.ToShortDateString();
                string endDate = t.EndDate.ToShortDateString();

                TherapyDTO therapyDTO = new TherapyDTO(anamnesis, drugName, dailyDose, startDate, endDate);

                therapyDTOs.Add(therapyDTO);
            }

            dataGridTherapy.DataContext = therapyDTOs;
            
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

        private void buttonReviewWeeklyTherapy_Click(object sender, RoutedEventArgs e)
        {
            ReviewWeeklyTherapyWindow reviewWeeklyTherapyWindow = new ReviewWeeklyTherapyWindow();
            reviewWeeklyTherapyWindow.Show();
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
            else if (e.Key == Key.U)
            {
                ReviewWeeklyTherapyWindow reviewWeeklyTherapyWindow = new ReviewWeeklyTherapyWindow();
                reviewWeeklyTherapyWindow.Show();
                this.Close();
            }
        }
    }
}
