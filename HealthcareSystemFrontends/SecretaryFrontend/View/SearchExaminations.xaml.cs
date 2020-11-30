using Controller.ExaminationAndPatientCard;
using Controller.NotificationSurveyAndFeedback;
using Controller.RoomAndEquipment;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Manager;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using Syncfusion.Windows.Shared;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for SearchExaminations.xaml
    /// </summary>
    public partial class SearchExaminations : Page
    {
        List<ExaminationDTO> scheduledExaminations = new List<ExaminationDTO>();
        List<ExaminationDTO> filteredExaminations = new List<ExaminationDTO>();
        private ExaminationController examinationController = new ExaminationController();
        List<Examination> examinations = new List<Examination>();
        List<Room> rooms = new List<Room>();
        RoomController roomController = new RoomController();
        private static PatientController patientController = new PatientController();
        private static DoctorController doctorController = new DoctorController();
        private UserController userPatientController = new UserController(patientController);
        private UserController userDoctorController = new UserController(doctorController);
        NotificationController notificationController = new NotificationController();
        public SearchExaminations()
        {
            InitializeComponent();

            searchPatients.Text = "";
            cmbSearchDoctors.Text = "";
            datePicker.SelectedDate = null;
            tpTime.SelectedTime = null;
            cmbTypeExm.SelectedItem = null;
            cmbRoom.SelectedItem = null;

            datePicker.DisplayDateStart = DateTime.Today;
            datePicker.DisplayDateEnd = (new DateTime(2020, 12, 31));

            examinations = examinationController.ViewScheduledExaminations();

            List<Room> allRooms = roomController.ViewRooms();
            foreach(Room r in allRooms)
            {
                if(r.Usage != TypeOfUsage.Soba_za_lezanje)
                {
                    rooms.Add(r);
                }
            }

            cmbRoom.DataContext = rooms;

            string type = "Operacija";

            foreach (Examination e in examinations)
            {
                if (e.Type == TypeOfExamination.Opsti)
                {
                    type = "Opšti pregled";
                }
                else if (e.Type == TypeOfExamination.Specijalisticki)
                {
                    type = "Specijalistički pregled";
                }
                scheduledExaminations.Add(new ExaminationDTO(e.IdExamination, e.doctor.Name + " " + e.doctor.Surname + " " + e.doctor.Jmbg, 
                                    e.patientCard.patient.Name + " " + e.patientCard.patient.Surname + " " + e.patientCard.patient.Jmbg, e.room.Number.ToString(), 

                                    type, e.DateAndTime.ToShortDateString(), e.DateAndTime.ToShortTimeString()));
            }

            dgSearchExaminations.ItemsSource = scheduledExaminations;
        }

        private void allExmBtn_Click(object sender, RoutedEventArgs e)
        {
            searchPatients.Text = "";
            cmbSearchDoctors.Text = "";
            datePicker.SelectedDate = null;
            tpTime.SelectedTime = null;
            cmbTypeExm.SelectedItem = null;
            cmbRoom.SelectedItem = null;

            dgSearchExaminations.ItemsSource = scheduledExaminations;
        }

        private void btnEditExm_Click(object sender, RoutedEventArgs e)
        {
            ExaminationDTO examinationToEdit = (ExaminationDTO)dgSearchExaminations.SelectedItem;
            if (examinationToEdit == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Prvo odaberite pregled koji mijenjate!";
                okMb.ShowDialog();
            }
            else
            {
                var ee = new EditExamination(examinationToEdit, this);
                ee.ShowDialog();
            }
        }

        private List<Patient> getPatientsFromSearch(string nameSurname)
        {
            string[] parts = nameSurname.Split(' ');
            List<User> users = userPatientController.ViewAllUsers();
            List<Patient> patients = new List<Patient>();
            foreach(User u in users)
            {
                if(u.Name.Equals(parts[0]) || u.Surname.Equals(parts[1]))
                {
                    patients.Add((Patient)u);
                }
            }
            return patients;
        }

        private List<Doctor> getDoctorsFromSearch(string nameSurname)
        {
            string[] parts = nameSurname.Split(' ');
            List<User> users = userDoctorController.ViewAllUsers();
            List<Doctor> doctors = new List<Doctor>();
            foreach (User u in users)
            {
                if (u.Name.Equals(parts[0]) || u.Surname.Equals(parts[1]))
                {
                    doctors.Add((Doctor)u);
                }
            }
            return doctors;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            filteredExaminations.Clear();
            filteredExaminations.AddRange(scheduledExaminations);

            if(!searchPatients.Text.Equals(""))
            {
                if(!searchPatients.Text.Contains(' '))
                {
                    var okMb = new OKMessageBox(this, 0);
                    okMb.titleMsgBox.Text = "Greška";
                    okMb.textMsgBox.Text = "Unesite ime i prezime pacijenta za pretragu!";
                    okMb.ShowDialog();
                    searchPatients.Text = null;
                    return;
                }
                else if(getPatientsFromSearch(searchPatients.Text).Count < 1)
                {
                    var okMb = new OKMessageBox(this, 0);
                    okMb.titleMsgBox.Text = "Greška";
                    okMb.textMsgBox.Text = "Odabrani pacijent ne postoji u sistemu!";
                    okMb.ShowDialog();
                    searchPatients.Text = null;
                    return;
                }
            }
            else if (!cmbSearchDoctors.Text.Equals(""))
            {
                if (!cmbSearchDoctors.Text.Contains(' '))
                {
                    var okMb = new OKMessageBox(this, 0);
                    okMb.titleMsgBox.Text = "Greška";
                    okMb.textMsgBox.Text = "Unesite ime i prezime doktora za pretragu!";
                    okMb.ShowDialog();
                    searchPatients.Text = null;
                    return;
                }
                else if (getDoctorsFromSearch(cmbSearchDoctors.Text).Count < 1)
                {
                    var okMb = new OKMessageBox(this, 0);
                    okMb.titleMsgBox.Text = "Greška";
                    okMb.textMsgBox.Text = "Odabrani doktor ne postoji u sistemu!";
                    okMb.ShowDialog();
                    cmbSearchDoctors.Text = null;
                    return;
                }
            }

            Room selectedRoom = (Room)cmbRoom.SelectedItem;

            if (!searchPatients.Text.Equals(""))
            {
                filteredExaminations = filteredExaminations.Where(exm => exm.Patient.Contains(searchPatients.Text.ToString())).ToList();
            }
            if (!cmbSearchDoctors.Text.Equals(""))
            {
                filteredExaminations = filteredExaminations.Where(exm => exm.Doctor.Contains(cmbSearchDoctors.Text.ToString())).ToList();
            }
            if(cmbTypeExm.SelectedItem != null)
            {
                filteredExaminations = filteredExaminations.Where(exm => exm.TypeOfExamination.Equals(cmbTypeExm.SelectedItem.ToString())).ToList();
            }
            if(selectedRoom != null)
            {
                filteredExaminations = filteredExaminations.Where(exm => exm.Room.Contains(selectedRoom.Number.ToString())).ToList();
            }
            if (datePicker.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)datePicker.SelectedDate;
                filteredExaminations = filteredExaminations.Where(exm => exm.Date.Equals(selectedDate.ToShortDateString())).ToList();
            }
            if (tpTime.SelectedTime != null)
            {
                DateTime selectedTime = (DateTime)tpTime.SelectedTime;
                filteredExaminations = filteredExaminations.Where(exm => exm.Time.Equals(selectedTime.ToShortTimeString())).ToList();
            }
            
            dgSearchExaminations.ItemsSource = filteredExaminations;
        }
        
        private void cmbTypeExm_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            List<String> retVal = new List<string>();
            retVal.Add("Opšti pregled");
            retVal.Add("Specijalistički pregled");
            retVal.Add("Operacija");
            combo.ItemsSource = retVal;
        }

        private void btnCancelExm_Click(object sender, RoutedEventArgs e)
        {
            ExaminationDTO examinationToCancel = (ExaminationDTO)dgSearchExaminations.SelectedItem;
            if (examinationToCancel == null)
            {
                var okMb = new OKMessageBox(this, 0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Odaberite pregled koji želite otkazati!";
                okMb.ShowDialog();
            }
            else
            {
                if(examinationController.CancelExamination(examinationToCancel.Id) == false)
                {
                    var okMb = new OKMessageBox(this, 0);
                    okMb.titleMsgBox.Text = "Greška";
                    okMb.textMsgBox.Text = "Došlo je do greške prilikom otkazivanja pregleda!";
                    okMb.ShowDialog();
                    return;
                }
                int lastId = notificationController.getLastId();
                string message = "Vas pregled za datum " + examinationToCancel.Date + " u " + examinationToCancel.Time + " sati je otkazan.";

                string[] partsPatient = examinationToCancel.Patient.Split(' ');
                notificationController.SendNotification(new Notification(++lastId, TypeOfNotification.Pregled, message, partsPatient[partsPatient.Length - 1]));

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new SearchExaminations();
                        return;
                    }
                }
            }
        }

    }
}
