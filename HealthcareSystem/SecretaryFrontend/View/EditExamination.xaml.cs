using Controller.ExaminationAndPatientCard;
using Controller.NotificationSurveyAndFeedback;
using Controller.RoomAndEquipment;
using Controller.UsersAndWorkingTime;
using Model.PerformingExamination;
using Model.Manager;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using Syncfusion.Windows.Shared;
using System;
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
using Model.Enums;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for EditExamination.xaml
    /// </summary>
    public partial class EditExamination : Window
    {
        private ExaminationDTO examination;
        private Page parent;
        private static DoctorController doctorController = new DoctorController();
        private UserController userDoctorController = new UserController(doctorController);
        private RoomController roomController = new RoomController();
        private PatientCardController patientCardController = new PatientCardController();
        NotificationController notificationController = new NotificationController();
        ExaminationController examinationController = new ExaminationController();
        WorkingTimeController workingTimeController = new WorkingTimeController();
        List<Room> rooms = new List<Room>();
        List<Doctor> doctors = new List<Doctor>();
        List<Examination> allAppointments = new List<Examination>();
        List<TimeDTO> freeAppointments = new List<TimeDTO>();
        List<TypeOfExaminationDTO> typeOfExaminationDTOs = new List<TypeOfExaminationDTO>();
        string[] parts;
        Doctor doctorForSelection;
        public EditExamination(Object obj,Page par)
        {
            examination = (ExaminationDTO)obj;
            parent = par;
            parts = examination.Doctor.Split(' ');
            doctorForSelection = (Doctor)userDoctorController.ViewProfile(parts[parts.Length - 1]);

            InitializeComponent();     

            List<User> users = userDoctorController.ViewAllUsers();
            foreach (User u in users)
            {
                doctors.Add((Doctor)u);
            }

            cmbDoctor.DataContext = doctors;

            InitializeWindowInformation();
            
        }

        private void InitializeWindowInformation()
        {
            for(int i=0;i<doctors.Count;i++)
            {
                if (doctorForSelection != null && doctors[i].Jmbg.Equals(doctorForSelection.Jmbg))
                {
                    cmbDoctor.SelectedItem = doctors[i];
                    break;
                }
            }

            txtPatient.Text = examination.Patient;

            dpDate.SelectedDate = DateTime.Parse(examination.Date);
            dpDate.DisplayDateStart = DateTime.Today;
            dpDate.DisplayDateEnd = (new DateTime(2020, 12, 31));

        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/close.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Zatvaranje prozora";
            mb.textMsgBox.Text = "Da li ste sigurni da želite odustati od izmjene pregleda?";
             
            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {         
            if (txtPatient.Text.Equals(""))
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati pacijenta!";
                okMbx.ShowDialog();
                return;
            }
            if (cmbDoctor.SelectedItem == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati doktora!";
                okMbx.ShowDialog();
                return;
            }
            if (cmbTypeOfExamination.SelectedItem == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati vrstu pregleda!";
                okMbx.ShowDialog();
            }
            if (cmbNumberOfRoom.SelectedItem == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati sobu!";
                okMbx.ShowDialog();
                return;
            }
            if (dpDate.SelectedDate == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati datum!";
                okMbx.ShowDialog();
                return;
            }
            if (tpTime.SelectedItem == null)
            {
                var okMbx = new OKMessageBox(this, 4);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Morate odabrati vrijeme!";
                okMbx.ShowDialog();
                return;
            }


            DateTime selectedDate = (DateTime)dpDate.SelectedDate;
            Room selectedRoom = (Room)cmbNumberOfRoom.SelectedItem;
            Doctor selectedDoctor = (Doctor)cmbDoctor.SelectedItem;
            TimeDTO timeDTO = (TimeDTO)tpTime.SelectedItem;
            DateTime date = Convert.ToDateTime(selectedDate.ToShortDateString() + " " + timeDTO.Time, CultureInfo.InvariantCulture);
            string[] partsPatient = examination.Patient.Split(' ');
            PatientCard selectedPatientCard = patientCardController.ViewPatientCard(partsPatient[partsPatient.Length-1]);

            TypeOfExamination type;
            TypeOfExaminationDTO selectedType = (TypeOfExaminationDTO)cmbTypeOfExamination.SelectedItem;

            if (selectedType.Type.Equals("Opšti pregled"))
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

                if (parent.GetType() == typeof(CanceledExaminationsView))
                {
                examinationController.ScheduleExamination(new Examination(examination.Id, type, date, selectedDoctor, selectedRoom, selectedPatientCard));
                   
                        int lastId = notificationController.getLastId();
                        string message = "Ponovo zakazan otkazani pregled\n" + "Doktor: " + selectedDoctor.Name + " " + selectedDoctor.Surname
                                     + "\nBroj sobe: " + selectedRoom.Id + "\nDatum:" + date.ToShortDateString() + "\nVrijeme: " + date.ToShortTimeString();
                        notificationController.SendNotification(new Notification(++lastId, TypeOfNotification.Pregled, message, selectedPatientCard.Patient.Jmbg));

                        var okMb = new OKMessageBox(this, 0);
                        okMb.titleMsgBox.Text = "Obavještenje";
                        okMb.textMsgBox.Text = "Uspješno ste zakazali pregled koji je bio otkazan. Pacijent je obaviješten o izmjeni.";
                        okMb.ShowDialog();
                        this.Close();
                              

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).Main.Content = new CanceledExaminationsView();
                        }
                    }
                }
                else if (parent.GetType() == typeof(ExaminationViewByDoctor))
                {
                examinationController.UpdateExamination(new Examination(examination.Id, type, date, selectedDoctor, selectedRoom, selectedPatientCard));
                   
                        int lastId = notificationController.getLastId();
                        string message = "Pregled izmijenjen\n" + "Doktor: " + selectedDoctor.Name + " " + selectedDoctor.Surname
                                    + "\nBroj sobe: " + selectedRoom.Id + "\nDatum:" + date.ToShortDateString() + "\nVrijeme: " + date.ToShortTimeString();
                        notificationController.SendNotification(new Notification(++lastId, TypeOfNotification.Pregled, message, selectedPatientCard.Patient.Jmbg));

                        var okMb = new OKMessageBox(this, 0);
                        okMb.titleMsgBox.Text = "Obavještenje";
                        okMb.textMsgBox.Text = "Uspješno ste izmijenili pregled. Pacijent je obaviješten o izmjeni.";
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
                else if (parent.GetType() == typeof(SearchExaminations))
                {
                examinationController.UpdateExamination(new Examination(examination.Id, type, date, selectedDoctor, selectedRoom, selectedPatientCard));
               
                        int lastId = notificationController.getLastId();
                        string message = "Pregled izmijenjen\n" + "Doktor: " + selectedDoctor.Name + " " + selectedDoctor.Surname
                                    + "\nBroj sobe: " + selectedRoom.Id + "\nDatum:" + date.ToShortDateString() + "\nVrijeme: " + date.ToShortTimeString();
                        notificationController.SendNotification(new Notification(++lastId, TypeOfNotification.Pregled, message, selectedPatientCard.Patient.Jmbg));

                        var okMb = new OKMessageBox(this, 0);
                        okMb.titleMsgBox.Text = "Obavještenje";
                        okMb.textMsgBox.Text = "Uspješno ste izmijenili pregled. Pacijent je obaviješten o izmjeni.";
                        okMb.ShowDialog();
                        this.Close();
                                    

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).Main.Content = new SearchExaminations();
                        }
                    }
                }
        }

        private void changeTypeOfExaminationAndRooms()
        {
            cmbNumberOfRoom.DataContext = null;
            rooms.Clear();

            if (cmbTypeOfExamination.SelectedIndex == 0)
            {
                rooms.AddRange(roomController.ViewRoomByUsage(TypeOfUsage.Soba_za_pregled, (DateTime)dpDate.SelectedDate, (DateTime)dpDate.SelectedDate));
                cmbNumberOfRoom.DataContext = rooms;
            }
            else
            {
                rooms.AddRange(roomController.ViewRoomByUsage(TypeOfUsage.Operaciona_sala, (DateTime)dpDate.SelectedDate, (DateTime)dpDate.SelectedDate));
                cmbNumberOfRoom.DataContext = rooms;
            }

            Room roomForSelection = roomController.ViewRoomByNumber(Int32.Parse(examination.Room));
            if (roomForSelection != null)
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].Id == roomForSelection.Id)
                    {
                        cmbNumberOfRoom.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void cmbTypeOfExamination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeTypeOfExaminationAndRooms();
        }

        private void fillTime()
        {
            WorkShifts workShifts;
            Doctor selectedDoctor;

            if (cmbDoctor.SelectedItem != null)
            {
                selectedDoctor = (Doctor)cmbDoctor.SelectedItem;
                workShifts = workingTimeController.getShiftForDoctor(selectedDoctor.Jmbg);
            }
            else
            {
                selectedDoctor = doctorForSelection;
                workShifts = workingTimeController.getShiftForDoctor(examination.Doctor);
            }

            DateTime firstShift;
            DateTime secondShift;
            DateTime thirdShift;

            if (dpDate.SelectedDate != null)
            {
                DateTime dp = (DateTime)dpDate.SelectedDate;

                firstShift = Convert.ToDateTime(dp.ToShortDateString() + " " + "8:00 AM", CultureInfo.InvariantCulture); //pocetak prve smjene
                secondShift = Convert.ToDateTime(dp.ToShortDateString() + " " + "4:00 PM", CultureInfo.InvariantCulture); //pocetak druge smjene
                thirdShift = Convert.ToDateTime(dp.ToShortDateString() + " " + "12:00 AM", CultureInfo.InvariantCulture); //pocetak trece smjene
            }
            else
            {
                firstShift = Convert.ToDateTime(examination.Date + " " + "8:00 AM", CultureInfo.InvariantCulture); //pocetak prve smjene
                secondShift = Convert.ToDateTime(examination.Date + " " + "4:00 PM", CultureInfo.InvariantCulture); //pocetak druge smjene
                thirdShift = Convert.ToDateTime(examination.Date + " " + "12:00 AM", CultureInfo.InvariantCulture); //pocetak trece smjene
            }

            if(workShifts == WorkShifts.FIRST)
            {
                allAppointments = examinationController.getAllAppointments(selectedDoctor, firstShift);
            }else if(workShifts == WorkShifts.SECOND)
            {
                allAppointments = examinationController.getAllAppointments(selectedDoctor, secondShift);
            }
            else
            {
                allAppointments = examinationController.getAllAppointments(selectedDoctor, thirdShift);
            }

            freeAppointments.Clear();
            tpTime.DataContext = null;
            foreach (Examination exm in allAppointments)
            {
                if (exm.Room.Id == 0) //ovo je slobodan termin
                {
                    freeAppointments.Add(new TimeDTO(exm.DateAndTime.ToShortTimeString()));
                }
            }
            
            tpTime.DataContext = freeAppointments;
        }
        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            changeTypeOfExaminationAndRooms();
            fillTime();
        }

        private void cmbNumberOfRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillTime();
        }

        private void cmbDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbTypeOfExamination.DataContext = null;
            typeOfExaminationDTOs.Clear();

            Doctor selectedDoctor = (Doctor)cmbDoctor.SelectedItem;

            if (selectedDoctor.Type == TypeOfDoctor.drOpstePrakse)
            {
                typeOfExaminationDTOs.Add(new TypeOfExaminationDTO("Opšti pregled"));
                cmbTypeOfExamination.DataContext = typeOfExaminationDTOs;
                cmbTypeOfExamination.SelectedIndex = 0;
            }
            else
            {
                typeOfExaminationDTOs.Add(new TypeOfExaminationDTO("Specijalistički pregled"));
                typeOfExaminationDTOs.Add(new TypeOfExaminationDTO("Operacija"));
                cmbTypeOfExamination.DataContext = typeOfExaminationDTOs;

                if (examination.TypeOfExamination.Equals("Specijalistički pregled"))
                {
                    cmbTypeOfExamination.SelectedIndex = 0;
                }
                else
                {
                    cmbTypeOfExamination.SelectedIndex = 1;
                }
            }

            fillTime();
        }

    }
}
