using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for CancelExaminationWindow.xaml
    /// </summary>
    public partial class CancelExaminationWindow : Window
    {
        private static PatientController patientController = new PatientController();
        private readonly UserController userController = new UserController(patientController);
        private ExaminationController examinationController = new ExaminationController();
        private List<Examination> patientExamiantions = new List<Examination>();
        private List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
        private Patient patient = new Patient();

        public CancelExaminationWindow()
        {
            InitializeComponent();
            fillTable();
        }

        private void fillTable()
        {
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

        private void buttonHomePage_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow homePageWindow = new HomePageWindow();
            homePageWindow.Show();
            this.Close();
        }

        private void buttonViewProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void dataGridExaminations_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ExaminationDTO examinationCancel = (ExaminationDTO)dataGridExaminations.SelectedItem;


                if (dataGridExaminations.Items.Count > 0)
                {

                    if (examinationCancel == null)
                    {
                        ValidationMessageWindow validationMessageWindow = new ValidationMessageWindow();
                        validationMessageWindow.caption.Text = "Nemate izabran pregled za brisanje!";
                        validationMessageWindow.Show();
                    }
                    else
                    {
                        DateTime selectedExamiantionDateTime = Convert.ToDateTime(examinationCancel.Date + " " + examinationCancel.Time, CultureInfo.InvariantCulture);

                        if (DateTime.Compare(selectedExamiantionDateTime, DateTime.Now.AddDays(1)) <= 0)
                        {
                            ValidationMessageWindow validationMessageWindow = new ValidationMessageWindow();
                            validationMessageWindow.caption.Text = "Brisanje pregleda nije moguće!";
                            validationMessageWindow.Show();
                        }
                        else
                        {

                            MessageBoxResult messageBoxResult = MessageBox.Show("Da li želite da otkažete pregled?", "Potvrda otkazivanja", MessageBoxButton.YesNo, MessageBoxImage.Error);

                            if (messageBoxResult == MessageBoxResult.Yes)
                            {
                                examinationController.DeleteScheduledExamination(examinationCancel.Id);

                                dataGridExaminations.DataContext = null;

                                ReviewExaminationsWindow reviewExaminationsWindow = new ReviewExaminationsWindow();
                                reviewExaminationsWindow.Show();
                                this.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nije moguce brisati iz prazne tabele.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
    }
}