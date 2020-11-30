using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Manager;
using Model.Secretary;
using Model.Users;
using Syncfusion.Windows.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
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
using ZdravoKorporacija.View;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for RecommendationAppointmentWindow.xaml
    /// </summary>
    public partial class RecommendationAppointmentWindow : Window
    {
        private ExaminationController examinationController = new ExaminationController();
        private List<Doctor> doctors = new List<Doctor>();
        private WorkingTimeController workingTimeController = new WorkingTimeController();
        private PatientCard patientCard = new PatientCard();
        private static PatientController patientController = new PatientController();
        private PatientCardController patientCardController = new PatientCardController();
        private readonly UserController userControllerPatient = new UserController(patientController);
        private static DoctorController doctorController = new DoctorController();
        private readonly UserController userControllerDoctor = new UserController(doctorController);
        private Patient patient = new Patient();
        private Doctor doctor = new Doctor();
        private DateTime endDateInYear = new DateTime(2020, 12, 31);
        private WorkShifts workShifts;
        private Examination recommendExamination = new Examination();

        public RecommendationAppointmentWindow()
        {
            InitializeComponent();

            patient = (Patient)userControllerPatient.ViewProfile(MainWindow.patient.Jmbg);

            List<User> users = userControllerDoctor.ViewAllUsers();

            foreach (User u in users)
            {
                Doctor d = (Doctor)u;
                if (d.Type == TypeOfDoctor.drOpstePrakse)
                {
                    doctors.Add(d);
                }
            }
            
            startDatePicker.DisplayDateStart = DateTime.Now.AddDays(1);
            startDatePicker.SelectedDate = DateTime.Now.AddDays(1);
            startDatePicker.DisplayDateEnd = endDateInYear;

            endDatePicker.DisplayDateStart = DateTime.Now.AddDays(7);
            endDatePicker.SelectedDate = DateTime.Now.AddDays(7);
            endDatePicker.DisplayDateEnd = endDateInYear;

            comboBoxDoctor.DataContext = doctors;
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

       
        private void buttonRecommendAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxDoctor.Text.Equals("") || startDatePicker.Text.Equals("") || endDatePicker.Text.Equals(""))
            {
                ValidationMessageWindow validationWindow = new ValidationMessageWindow();
                validationWindow.Show();
            }
            else
            {

                doctor = (Doctor)comboBoxDoctor.SelectedItem;
                DateTime sDate = (DateTime)startDatePicker.SelectedDate;
                DateTime eDate = (DateTime)endDatePicker.SelectedDate;

                if (DateTime.Compare(sDate, eDate) >= 0)
                {
                    DateMessageWindow dateMessageWindow = new DateMessageWindow();
                    dateMessageWindow.caption.Text = "Pažnja, datum početka mora biti pre datuma kraja vremenskog intervala!";
                    dateMessageWindow.Show();
                }
                else
                {

                    string dateStart = sDate.ToShortDateString();
                    string dateEnd = eDate.ToShortDateString();
                    string time;
                    DateTime startDate;
                    DateTime endDate;

                    workShifts = workingTimeController.getShiftForDoctor(doctor.Jmbg);

                    if (priorityDoctor.IsChecked == true)
                    {
                    
                        if (workShifts == WorkShifts.FIRST)
                        {
                            time = "8:00 AM";
                            startDate = Convert.ToDateTime(dateStart + " " + time, CultureInfo.InvariantCulture);
                            endDate = Convert.ToDateTime(dateEnd + " " + time, CultureInfo.InvariantCulture);
                            recommendExamination = examinationController.AppointmentRecommendationByDoctor(doctor, startDate, endDate);
                        }
                        else if (workShifts == WorkShifts.SECOND)
                        {
                            time = "4:00 PM";
                            startDate = Convert.ToDateTime(dateStart + " " + time, CultureInfo.InvariantCulture);
                            endDate = Convert.ToDateTime(dateEnd + " " + time, CultureInfo.InvariantCulture);
                            recommendExamination = examinationController.AppointmentRecommendationByDoctor(doctor, startDate, endDate);
                        }
                        else
                        {
                            time = "12:00 AM";
                            startDate = Convert.ToDateTime(dateStart + " " + time, CultureInfo.InvariantCulture);
                            endDate = Convert.ToDateTime(dateEnd + " " + time, CultureInfo.InvariantCulture);
                            recommendExamination = examinationController.AppointmentRecommendationByDoctor(doctor, startDate, endDate);
                        }

                        
                        recommendedDate.Text = recommendExamination.DateAndTime.ToShortDateString();

                        recommendedTime.Text = recommendExamination.DateAndTime.ToShortTimeString();

                        buttonMakeAppointment.IsEnabled = true;
                    
                    }
                    else
                    {
                        if (workShifts == WorkShifts.FIRST)
                        {
                            time = "8:00 AM";
                            startDate = Convert.ToDateTime(dateStart + " " + time, CultureInfo.InvariantCulture);
                            endDate = Convert.ToDateTime(dateEnd + " " + time, CultureInfo.InvariantCulture);
                            recommendExamination = examinationController.AppointmentRecommendationByDate(doctor, startDate, endDate);
                        }
                        else if (workShifts == WorkShifts.SECOND)
                        {
                            time = "4:00 PM";
                            startDate = Convert.ToDateTime(dateStart + " " + time, CultureInfo.InvariantCulture);
                            endDate = Convert.ToDateTime(dateEnd + " " + time, CultureInfo.InvariantCulture);
                            recommendExamination = examinationController.AppointmentRecommendationByDate(doctor, startDate, endDate);
                        }
                        else
                        {
                            time = "12:00 AM";
                            startDate = Convert.ToDateTime(dateStart + " " + time, CultureInfo.InvariantCulture);
                            endDate = Convert.ToDateTime(dateEnd + " " + time, CultureInfo.InvariantCulture);
                            recommendExamination = examinationController.AppointmentRecommendationByDate(doctor, startDate, endDate);
                        }


                        recommendedDate.Text = recommendExamination.DateAndTime.ToShortDateString();

                        recommendedTime.Text = recommendExamination.DateAndTime.ToShortTimeString();

                        buttonMakeAppointment.IsEnabled = true;
                    }
                }
            }
        }

        private void buttonMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            patientCard = patientCardController.ViewPatientCard(MainWindow.patient.Jmbg);

            examinationController.ScheduleExamination(new Examination(recommendExamination.IdExamination, recommendExamination.Type, recommendExamination.DateAndTime, recommendExamination.doctor, doctor.DoctorsOffice, patientCard));

            ReviewExaminationsWindow reviewExaminationsWindow = new ReviewExaminationsWindow();
            reviewExaminationsWindow.Show();
            this.Close();
            
        }

        private void Doctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                priorityDoctor.IsChecked = true;
                priorityDate.IsChecked = false;
            }
        }

        private void Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                priorityDoctor.IsChecked = false;
                priorityDate.IsChecked = true;
            }
        }

        private void startDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                startDatePicker.IsDropDownOpen = true;
            }
        }

        private void endDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                endDatePicker.IsDropDownOpen = true;
            }
        }

        private void doctor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                comboBoxDoctor.IsDropDownOpen = true;
            }
        }

    }
}
