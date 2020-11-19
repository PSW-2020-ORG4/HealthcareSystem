using Controller.ExaminationAndPatientCard;
using Controller.NotificationSurveyAndFeedback;
using Controller.UsersAndWorkingTime;
using Model.PerformingExamination;
using Model.Manager;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using ProjekatZdravoKorporacija.View;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using Model.NotificationSurveyAndFeedback;
using Model.Enums;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for Examinations.xaml
    /// </summary>
    public partial class ExaminationViewByDoctor : Page
    {
        private int FIRST_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["first_shift_start"]);
        private int SECOND_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["second_shift_start"]);
        private int THIRD_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["third_shift_start"]);

        private List<Examination> allAppointments = new List<Examination>();
        private ExaminationController examinationController = new ExaminationController();
        private static DoctorController doctorController = new DoctorController();
        NotificationController notificationController = new NotificationController();
        UserController userController1 = new UserController(doctorController);
        WorkingTimeController workingTimeController = new WorkingTimeController();
        List<Doctor> doctors = new List<Doctor>();
        string jmbg = "";
        WorkShifts workShifts;

        public ExaminationViewByDoctor(string jmbg)
        {
            this.jmbg = jmbg;

            InitializeComponent();

            //po default-u danasnji datum
            dpDate.SelectedDate = DateTime.Today.Date;
            dpDate.DisplayDateStart = DateTime.Today;
            dpDate.DisplayDateEnd = (new DateTime(2020, 12, 31));

            //po default-u prva smjena
            first.IsSelected = true;
        }

        private void cmbShift_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            shiftAndDoctorChange();
        }

        private void shiftAndDoctorChange()
        {
            if (first.IsSelected)
            {
                workShifts = WorkShifts.FIRST;
            }
            else if (second.IsSelected)
            {
                workShifts = WorkShifts.SECOND;
            }
            else
            {
                workShifts = WorkShifts.THIRD;
            }

            doctors = workingTimeController.ViewDoctorsWhoWork((DateTime)dpDate.SelectedDate, workShifts);
            cmbDoctors.DataContext = doctors;

            setFirstDoctor();
        }

        private void setFirstDoctor()
        {
            //po default-u prvi doktor iz liste

            if (jmbg.Equals(""))
            {
                if (doctors.Count > 0)
                    cmbDoctors.SelectedItem = doctors[0];
                else
                    cmbDoctors.SelectedItem = null;
            }
            else
            {
                int count = 0;
                Doctor selectedDoctor = null;
                while (count < 3)
                {
                    for (int i = 0; i < doctors.Count; i++)
                    {
                        if (doctors[i].Jmbg.Equals(jmbg))
                        {
                            selectedDoctor = doctors[i];
                            break;
                        }
                    }
                    if(selectedDoctor == null)
                    {
                        if (workShifts == WorkShifts.FIRST)
                        {
                            second.IsSelected = true;
                        }else if (workShifts == WorkShifts.SECOND)
                        {
                            third.IsSelected = true;
                        }
                        else
                        {
                            first.IsSelected = true;
                        }
                    }
                    else
                    {                      
                        break;
                    }
                    count++;
                }
                cmbDoctors.SelectedItem = selectedDoctor;
            }
        }

        private void editExaminationBtn_Click(object sender, RoutedEventArgs e)
        {
            ExaminationDTO examinationToEdit = (ExaminationDTO)dgExaminations.SelectedItem;
            if (examinationToEdit == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Prvo odaberite pregled koji mijenjate!";
                okMb.ShowDialog();
            }
            else if (examinationToEdit.Room.Equals(""))
            {
                var okMb = new OKMessageBox(this, 0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Izabrani termin nije zakazan!";
                okMb.ShowDialog();
            }
            else
            {
                var ee = new EditExamination(examinationToEdit,this);
                ee.ShowDialog();
            }
        }

        private void btnCancelExm_Click(object sender, RoutedEventArgs e)
        {
            ExaminationDTO examinationToCancel = (ExaminationDTO)dgExaminations.SelectedItem;

            if (examinationToCancel == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Odaberite pregled koji želite otkazati!";
                okMb.ShowDialog();
            }
            else if (examinationToCancel.Room.Equals(""))
            {
                var okMb = new OKMessageBox(this, 0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Izabrani termin nije zakazan!";
                okMb.ShowDialog();
            }
            else
            {
                string[] partsDoctor = examinationToCancel.Doctor.Split(' ');
                if (examinationController.CancelExamination(examinationToCancel.Id) == false)
                {
                    var okMb = new OKMessageBox(this, 0);
                    okMb.titleMsgBox.Text = "Greška";
                    okMb.textMsgBox.Text = "Došlo je do greške prilikom otkazivanja pregleda!";
                    okMb.ShowDialog();
                    return;
                } 
                int lastId = notificationController.getLastId();
                string message = "Vas pregled za datum " +  examinationToCancel.Date + " u " + examinationToCancel.Time +  " sati je otkazan.";

                string[] partsPatient = examinationToCancel.Patient.Split(' ');
                notificationController.SendNotification(new Notification(++lastId, TypeOfNotification.Pregled, message, partsPatient[partsPatient.Length-1]));

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new ExaminationViewByDoctor(partsDoctor[partsDoctor.Length - 1]);
                    }
                }
            }
        }

        private void btnScheduleExm_Click(object sender, RoutedEventArgs e)
        {
            ExaminationDTO examinationToSchedule = (ExaminationDTO)dgExaminations.SelectedItem;
            if (examinationToSchedule == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Prvo odaberite slobodan termin doktora!";
                okMb.ShowDialog();
            }
            else if (!examinationToSchedule.Room.Equals(""))
            {
                var okMb = new OKMessageBox(this, 0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Izabrani pregled je već zakazan!";
                okMb.ShowDialog();
            }
            else
            {
                var ee = new ScheduleExamination(examinationToSchedule);
                ee.ShowDialog();
            }
        }

        private void displayAppointments()
        {
                setFirstDoctor();

                TimeSpan ts = new TimeSpan(FIRST_SHIFT_TIME, 0, 0);
                DateTime dt = (DateTime)dpDate.SelectedDate;

                if (first.IsSelected)
                {
                    ts = new TimeSpan(FIRST_SHIFT_TIME, 0, 0);
                    dt = dt + ts;
                 }
                else if (second.IsSelected)
                {
                    ts = new TimeSpan(SECOND_SHIFT_TIME, 0, 0);
                    dt = dt + ts;
                }
                else if (third.IsSelected)
                {
                    ts = new TimeSpan(THIRD_SHIFT_TIME, 0, 0);
                    dt = dt + ts;
                    dt = dt.AddDays(-1);
                }

                Doctor selectedDoctor = (Doctor)cmbDoctors.SelectedItem;
            if (selectedDoctor != null)
            {
                allAppointments = examinationController.getAllAppointments(selectedDoctor, dt);

                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                foreach (Examination exm in allAppointments)
                {
                    string room = "";
                    string type = "";
                    if (exm.Room.Number != 0)
                    {
                        room = exm.Room.Number.ToString();
                    }
                    if (room.Equals(""))
                    {
                        type = "";
                    }
                    else
                    {
                        if (exm.Type == TypeOfExamination.Opsti)
                        {
                            type = "Opšti pregled";
                        }
                        else if (exm.Type == TypeOfExamination.Specijalisticki)
                        {
                            type = "Specijalistički pregled";
                        }
                        else if (exm.Type == TypeOfExamination.Operacija)
                            type = "Operacija";
                        {

                        }
                    }
                    examinationDTOs.Add(new ExaminationDTO(exm.IdExamination, exm.Doctor.Name + " " + exm.Doctor.Surname + " " + exm.Doctor.Jmbg,
                        exm.PatientCard.Patient.Name + " " + exm.PatientCard.Patient.Surname + " " + exm.PatientCard.Patient.Jmbg, room, type, exm.DateAndTime.ToShortDateString(), exm.DateAndTime.ToShortTimeString()));
                }
                dgExaminations.ItemsSource = examinationDTOs;
            }
            }

        private void cmbDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor d = (Doctor)cmbDoctors.SelectedItem;
            if(d == null)
            {
                jmbg = "";
            }
            else
            {
                jmbg = d.Jmbg;
            }

            displayAppointments();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            shiftAndDoctorChange();
        }
    }
}
