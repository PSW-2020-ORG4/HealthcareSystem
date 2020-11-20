using Controller.ExaminationAndPatientCard;
using Controller.NotificationSurveyAndFeedback;
using Controller.RoomAndEquipment;
using Controller.UsersAndWorkingTime;
using Model.PerformingExamination;
using Model.Manager;
using Model.NotificationSurveyAndFeedback;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Model.Enums;

namespace ProjekatZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for ScheduleExamination.xaml
    /// </summary>
    public partial class ScheduleExamination : Window
    {
        private ExaminationDTO examination;
        private static PatientController patientController = new PatientController();
        private static DoctorController doctorController = new DoctorController();
        private PatientCardController patientCardController = new PatientCardController();
        private UserController userPatientController = new UserController(patientController);
        private UserController userDoctorController = new UserController(doctorController);
        private RoomController roomController = new RoomController();
        private NotificationController notificationController = new NotificationController();
        List<Patient> patients = new List<Patient>();
        List<Room> rooms = new List<Room>();
        List<TypeOfExaminationDTO> typeOfExaminationDTOs = new List<TypeOfExaminationDTO>();
        private 
        ExaminationController examinationController = new ExaminationController();
        public ScheduleExamination(Object o)
        {
            InitializeComponent();

            examination = (ExaminationDTO)o;

            List<User> users = userPatientController.ViewAllUsers();
            foreach (User u in users)
            {
                patients.Add((Patient)u);
            }

            txtPatient.DataContext = patients;

            InitializeWindowInformation(examination);
        }

        private void InitializeWindowInformation(ExaminationDTO examination)
        {
            txtDoctor.Text = examination.Doctor;
            string[] parts = examination.Doctor.Split(' ');
            Doctor selectedDoctor = (Doctor)userDoctorController.ViewProfile(parts[parts.Length-1]);

            if (selectedDoctor.Type == TypeOfDoctor.drOpstePrakse)
            {
                typeOfExaminationDTOs.Add(new TypeOfExaminationDTO("Opšti pregled"));
                cmbTypeOfExamination.DataContext = typeOfExaminationDTOs;
            }
            else
            {
                typeOfExaminationDTOs.Add(new TypeOfExaminationDTO("Specijalistički pregled"));
                typeOfExaminationDTOs.Add(new TypeOfExaminationDTO("Operacija"));
                cmbTypeOfExamination.DataContext = typeOfExaminationDTOs;
            }

            dpDate.SelectedDate = DateTime.Parse(examination.Date);
            tpTime.SelectedTime = DateTime.Parse(examination.Time);
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)txtPatient.SelectedItem;
            Room selectedRoom = (Room)cmbNumberOfRoom.SelectedItem;

            if(selectedPatient == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati pacijenta!";
                okMbx.ShowDialog();
                return;
            }
            if (cmbTypeOfExamination.SelectedItem == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati vrstu pregleda!";
                okMbx.ShowDialog();
                return;
            }
            if(selectedRoom == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati sobu!";
                okMbx.ShowDialog();
                return;
            }
                TypeOfExamination type;
                TypeOfExaminationDTO selectedType = (TypeOfExaminationDTO)cmbTypeOfExamination.SelectedItem;

                if(selectedType.Type.Equals("Opšti pregled"))
                {
                    type = TypeOfExamination.Opsti;
                }
                else if(selectedType.Type.Equals("Specijalistički pregled"))
                {
                    type = TypeOfExamination.Specijalisticki;
                }
                else
                {
                    type = TypeOfExamination.Operacija;
                }
                DateTime date = Convert.ToDateTime(examination.Date+" "+examination.Time, CultureInfo.InvariantCulture);
                string[] partsDoctor = examination.Doctor.Split(' ');
                Doctor selectedDoctor = (Doctor)userDoctorController.ViewProfile(partsDoctor[partsDoctor.Length-1]);
                PatientCard selectedPatientCard = patientCardController.ViewPatientCard(selectedPatient.Jmbg);

            examinationController.ScheduleExamination(new Examination(examination.Id, type, date, selectedDoctor, selectedRoom, selectedPatientCard));
                

                int lastId = notificationController.getLastId();
                string message = "Zakazan pregled\n" + "Doktor: " + selectedDoctor.Name + " " + selectedDoctor.Surname
                                 + "\nBroj sobe: " + selectedRoom.Id + "\nDatum:" + date.ToShortDateString() + "\nVrijeme: " + date.ToShortTimeString();

                notificationController.SendNotification(new Notification(++lastId,TypeOfNotification.Pregled,message, selectedPatientCard.Patient.Jmbg));


                var okMb = new OKMessageBox(this, 0);
                okMb.titleMsgBox.Text = "Obavještenje";
                okMb.textMsgBox.Text = "Uspješno ste zakazali pregled.";
                okMb.ShowDialog();
                this.Close();

               foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new ExaminationViewByDoctor(selectedDoctor.Jmbg);
                    }
                }
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/close.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Zatvaranje prozora";
            mb.textMsgBox.Text = "Da li ste sigurni da želite odustati od zakazivanja pregleda?";
            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void cmbTypeOfExamination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbNumberOfRoom.DataContext = null;
            rooms.Clear();

            if (cmbTypeOfExamination.SelectedIndex == 0)
            {
                rooms.AddRange(roomController.ViewRoomByUsage(TypeOfUsage.Soba_za_pregled,(DateTime)dpDate.SelectedDate, (DateTime)dpDate.SelectedDate));
                cmbNumberOfRoom.DataContext = rooms;
            }
            else
            {
                rooms.AddRange(roomController.ViewRoomByUsage(TypeOfUsage.Operaciona_sala, (DateTime)dpDate.SelectedDate, (DateTime)dpDate.SelectedDate));
                cmbNumberOfRoom.DataContext = rooms;
            }
        }
    }
}
