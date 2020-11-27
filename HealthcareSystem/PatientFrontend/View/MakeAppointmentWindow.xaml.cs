using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Manager;
using Model.Secretary;
using Model.Users;
using Repository;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using ZdravoKorporacija.View;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for MakeAppointmentWindow.xaml
    /// </summary>
    public partial class MakeAppointmentWindow : Window
    {
        private ExaminationController examinationController = new ExaminationController();
        private List<Doctor> doctors = new List<Doctor>();
        private WorkingTimeController workingTimeController = new WorkingTimeController();
        private PatientCardController patientCardController = new PatientCardController();
        private List<Doctor> doctorsFirstShift = new List<Doctor>();
        private List<Doctor> doctorsSecondShift = new List<Doctor>();
        private bool doctorWorkInFirstShift;
        private Doctor doctor = new Doctor();
        private PatientCard patientCard = new PatientCard();
        public MakeAppointmentWindow()
        {
            InitializeComponent();
            datePickerExamination.SelectedDate = DateTime.Now.AddDays(1);
            datePickerExamination.DisplayDateStart = DateTime.Now.AddDays(1);
            DateTime endDateInYear = new DateTime(2020, 12, 31);
            datePickerExamination.DisplayDateEnd = endDateInYear;
            patientCard = patientCardController.ViewPatientCard(MainWindow.patient.Jmbg);
        }

        private void datePickerExamination_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            doctorsFirstShift = workingTimeController.ViewDoctorsWhoWork((DateTime)datePickerExamination.SelectedDate, WorkShifts.FIRST);
            doctorsSecondShift = workingTimeController.ViewDoctorsWhoWork((DateTime)datePickerExamination.SelectedDate, WorkShifts.SECOND);

            if (doctorsFirstShift.Count() == 0 && doctorsSecondShift.Count() == 0)
            {
                AppointmentMessageWindow appointmentMessageWindow = new AppointmentMessageWindow();
                appointmentMessageWindow.Show();
            }
            else if (doctorsFirstShift.Count() == 0)
            {
                doctors = doctorsSecondShift;
            }
            else if (doctorsSecondShift.Count() == 0)
            {
                doctors = doctorsFirstShift;
            }
            else
            {
                doctors = doctorsSecondShift; 
                doctors.AddRange(doctorsFirstShift);
            }

            comboBoxDoctor.DataContext = doctors;

            comboBoxDoctor.SelectedItem = doctors[0];
        }

        private void comboBoxDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctor = (Doctor)comboBoxDoctor.SelectedItem;

            doctorWorkInFirstShift = isDoctorWorkInFirstShift(doctor);

            if (doctorWorkInFirstShift == true)
            {
                comboBoxAppointemtTime.DataContext = firstShift;
            }
            else
            {
                comboBoxAppointemtTime.DataContext = secondShift;
            }
        }

        private bool isDoctorWorkInFirstShift(Doctor doctor)
        {
            if (doctor == null)
                return false;
           
            WorkShifts workShifts = workingTimeController.getShiftForDoctor(doctor.Jmbg);

            if (workShifts == WorkShifts.FIRST)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        private void buttonMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxDoctor.Text.Equals("") || datePickerExamination.Text.Equals("") || comboBoxAppointemtTime.Text.Equals(""))
            {
                ValidationMessageWindow validationWindow = new ValidationMessageWindow();
                validationWindow.Show();
            }
            else
            {

                patientCard = patientCardController.ViewPatientCard(MainWindow.patient.Jmbg);
                int lastId = examinationController.getLastId();
                DateTime dateAndTime = (DateTime)datePickerExamination.SelectedDate;
                string date = dateAndTime.ToShortDateString();
                string time = comboBoxAppointemtTime.Text;
                DateTime dateAndTimeExamination = Convert.ToDateTime(date + " " + time, CultureInfo.InvariantCulture);
                TypeOfExamination general = TypeOfExamination.Opsti;
                doctor = (Doctor)comboBoxDoctor.SelectedItem;

                Examination examination = examinationController.ScheduleExamination(new Examination(++lastId, general, dateAndTimeExamination, doctor, doctor.DoctorsOffice, patientCard));

                if (examination == null)
                {
                    BusyTermWindow busyTermWindow = new BusyTermWindow();
                    busyTermWindow.caption.Text = "Termin koji ste izabrali " + date +" " + time + " " + "je već zauzet kod doktora " + doctor.Name + " " + doctor.Surname + " " + doctor.Jmbg;
                    busyTermWindow.Show();
                }
                else
                {
                    ReviewExaminationsWindow reviewExaminationsWindow = new ReviewExaminationsWindow();
                    reviewExaminationsWindow.Show();
                    this.Close();
                }
            }
        }

        private void doctor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                comboBoxDoctor.IsDropDownOpen = true;
            }
        }

        private void examinationDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                datePickerExamination.IsDropDownOpen = true;
            }
        }

        private void appointemtTime_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                comboBoxAppointemtTime.IsDropDownOpen = true;
            }
        }

        public string[] firstShift = new string[] { "8:00 AM", "8:30 AM", "9:00 AM", "9:30 AM", "10:00 AM", "10:30 AM", "11:00 AM", "11:30 AM", "12:00 PM", "12:30 PM", "1:00 PM", "1:30 PM", "2:00 PM", "2:30 PM", "3:00 PM", "3:30 PM" };

        public string[] secondShift = new string[] { "4:00 PM", "4:30 PM", "5:00 PM", "5:30 PM", "6:00 PM", "6:30 PM", "7:00 PM", "7:30 PM", "8:00 PM", "8:30 PM", "9:00 PM", "9:30 PM", "10:00 PM", "10:30 PM", "11:00 PM", "11:30 PM" };

       
    }
}
