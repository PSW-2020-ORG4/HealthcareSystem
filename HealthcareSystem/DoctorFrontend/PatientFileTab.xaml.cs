using Controller.DrugAndTherapy;
using Controller.ExaminationAndPatientCard;
using Controller.NotificationSurveyAndFeedback;
using Controller.PlacementInARoomAndRenovationPeriod;
using Controller.RoomAndEquipment;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Manager;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for PatienFileTab.xaml
    /// </summary>
    public partial class PatienFileTab : Window
    {
       private int FIRST_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["first_shift_start"]);
       private int SECOND_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["second_shift_start"]);
       private int THIRD_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["third_shift_start"]);
       private int appointment = -1;
       private int drugSelCell = -1;
       private int idDrug = 0;
       private int drugQuantity = 0;
       private Doctor d;
       private int id = 0;
       private int idExam = 0;
       private DateTime dt;
       private Drug drugSelected;
       private PatientCard patientCard;
       private Room roomPlacment;
       private string name;
       private static PatientCardController ap = new PatientCardController();
       private static DoctorController dc = new DoctorController();
       private static DrugController dcon = new DrugController();
       private static RoomController rc = new RoomController();
       private static ExaminationController ec = new ExaminationController();
       private static TherapyController tc = new TherapyController();
       private static NotificationController nc = new NotificationController();
       private static PlacementInSickRoomController prc = new PlacementInSickRoomController();
       private static WorkingTimeController wc = new WorkingTimeController();
       private static RenovationPeriodController rpc = new RenovationPeriodController();

        public PatienFileTab(string patientName, Doctor doctor, int id)
        {
            InitializeComponent();

            idExam = id;
            txtbox1.Text = DateTime.Now.ToShortDateString();
            logInDoctor.Text = doctor.Username;
            d = doctor;
            showName.Text = patientName;
            name = patientName;     
            
            PatientCard pc = new PatientCard();
            
            string[] ss = showName.Text.Split(' ');
            pc = ap.ViewPatientCard(ss[ss.Length-1]);
            patientCard = pc;

            txtAlergije.Text = pc.Alergies;      
            txtDatumRodjenja.Text = pc.Patient.DateOfBirth.ToShortDateString();
            txtKrvnaGrupa.Text = pc.BloodType.ToString();
            txtRhFaktor.Text = pc.RhFactor.ToString();
            txtLbo.Text = pc.Lbo.ToString();       
            
            List<Drug> drugs = new List<Drug>();
            drugs = dcon.ViewConfirmedDrugs();

            List<DrugDTO> dDTOs = new List<DrugDTO>();
            foreach (Drug d in drugs) {

                DrugDTO dDTO = new DrugDTO();
                dDTO.Id = d.Id;
                dDTO.Name = d.Name;
                dDTO.Quantity = d.Quantity;
                dDTO.drugType = d.drugType.Type;

                dDTOs.Add(dDTO);
            }

            DataGrid3.ItemsSource = dDTOs;

            //po default-u danasnji datum
            dataExam.SelectedDate = DateTime.Today.Date;
            dataExam.DisplayDateStart = DateTime.Today;
            dataExam.DisplayDateEnd = (new DateTime(2020, 12, 31));

            //dataOd.SelectedDate = DateTime.Today.Date;
            //da.DisplayDateStart = DateTime.Today;
            dataOd.SelectedDate = DateTime.Today.Date;
            dataOd.DisplayDateEnd = (new DateTime(2020, 12, 31));

            dataDo.DisplayDateEnd = (new DateTime(2020, 12, 31));

            //po default-u prva smjena
            first.IsSelected = true;
           // opsti.IsSelected = true;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            Examination exam = ec.ViewScheduledExaminationById(idExam);
            ec.SaveExaminationInPatientCard(exam);
            ec.DeleteScheduledExamination(idExam);

            var s = new HomePage(d);
            s.Show();
            this.Close();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var s = new MainWindow();
            s.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(txtbox2.Text))
            {
                MessageBox.Show("Morate unijeti tok bolesti.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox2.Clear();
                txtbox2.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtbox3.Text))
            {
                MessageBox.Show("Morate unijeti terapiju", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox3.Clear();
                txtbox3.Focus();
                return;
            }
            try
            {
                
                string date = txtbox1.Text;
                string disease = txtbox2.Text;
                string therapy = txtbox3.Text;
              
                PatientCard pc = new PatientCard();
                string[] ss = showName.Text.Split(' ');
                pc = ap.ViewPatientCard(ss[ss.Length - 1]);
                string mh = pc.MedicalHistory;
              
                pc.MedicalHistory = mh + date + ":" + disease + ":" + therapy + ";";
                      
                ap.EditPatientCard(pc);
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }

            var s = new ScheduleExaminaton();
            s.Show();

            txtbox2.Clear();
            txtbox3.Clear();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var s = new ViewPatientFile(showName.Text);
            s.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new EditProfile(d);
            s.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var s = new AboutAplication();
            s.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            HelpViewer help = new HelpViewer("index");
            help.ShowDialog();
        }
 

        private void Button_Click_Drug(object sender, RoutedEventArgs e)
        {
            IngredientsOfDrug s = new IngredientsOfDrug(drugSelected);
            s.Show();
            
        }

        private void DataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cell = DataGrid4.SelectedIndex;
            drugSelCell = (int)cell;

            if (DataGrid3.SelectedItem != null)
            {
              
                var cellC = DataGrid3.Columns[2].GetCellContent(DataGrid3.SelectedItem);
                var row = cellC.Parent;
                string s = row.ToString();
        
                string[] lines = s.Split(':');
                txtDrug.Text = lines[1];

                var cellC1 = DataGrid3.Columns[1].GetCellContent(DataGrid3.SelectedItem);
                var row1 = cellC1.Parent;
                string s1 = row1.ToString();
                string[] lines1 = s1.Split(':');
                string d = lines1[1];
                idDrug = Int32.Parse(d);
             
                List<Drug> drugs1 = dcon.ViewConfirmedDrugs();
                Drug drug = new Drug();
                foreach (Drug dd in drugs1)
                {
                    if (dd.Id == idDrug)
                    {
                        drug = dd;
                        break;
                    }
                }
                drugSelected = drug;


                var cellC2 = DataGrid3.Columns[3].GetCellContent(DataGrid3.SelectedItem);
                var row2 = cellC2.Parent;
                string s2 = row2.ToString();
                string[] lines2 = s2.Split(':');
                string d2 = lines2[1];
                drugQuantity = Int32.Parse(d2);
               
                txtbox3.Text = drugSelected.Name;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtDrug.Text))
            {
                MessageBox.Show("Morate selektovati lijek u tabeli koji želite propisati.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDrug.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtbox2.Text))
            {
                MessageBox.Show("Morate unijeti tok bolesti na prethodnom tabu.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox2.Clear();
                txtbox2.Focus();
                return;
            }


            drugSelected.Quantity = --drugQuantity;
            dcon.EditConfirmedDrug(drugSelected);

            List<Drug> drugs = new List<Drug>();
            drugs = dcon.ViewConfirmedDrugs();

            List<DrugDTO> dDTOs = new List<DrugDTO>();
            foreach (Drug d in drugs)
            {

                DrugDTO dDTO = new DrugDTO();
                dDTO.Id = d.Id;
                dDTO.Name = d.Name;
                dDTO.Quantity = d.Quantity;
                dDTO.drugType = d.drugType.Type;

                dDTOs.Add(dDTO);
            }

            DataGrid3.ItemsSource = dDTOs;

            if (String.IsNullOrEmpty(txtbox2.Text))
            {
                MessageBox.Show("Morate unijeti tok bolesti.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox2.Clear();
                txtbox2.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtbox3.Text))
            {
                MessageBox.Show("Morate unijeti terapiju", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox3.Clear();
                txtbox3.Focus();
                return;
            }
            try
            {

                string date = txtbox1.Text;
                string disease = txtbox2.Text;
                string therapy11 = txtbox3.Text;

                PatientCard pc = new PatientCard();
                string[] ss = showName.Text.Split(' ');
                pc = ap.ViewPatientCard(ss[ss.Length - 1]);
                string mh = pc.MedicalHistory;

                pc.MedicalHistory = mh + date + ":" + disease + ":" + therapy11 + ";";

                ap.EditPatientCard(pc);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            ScheduleExaminaton win = new ScheduleExaminaton();
            win.Show();

            txtbox2.Clear();
            txtbox3.Clear();

            drugSelCell = -1;

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            string s = dateTherapy.SelectedDate.ToString();
            string[] lines = s.Split(' ');
            string datum = lines[0];

            if (String.IsNullOrEmpty(txtDrug.Text))
            {
                MessageBox.Show("Morate selektovati lijek u tabeli koji želite propisati.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);              
                txtDrug.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtPrescribe.Text))
            {
                MessageBox.Show("Morate unijeti dnevnu dozu lijeka.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPrescribe.Clear();
                txtPrescribe.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtbox2.Text))
            {
                MessageBox.Show("Morate unijeti tok bolesti na prethodnom tabu.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox2.Clear();
                txtbox2.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(datum))
            {
                MessageBox.Show("Morate izabrati datum do kog pacijent treba da koristi terapiju.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                dateTherapy.Focus();
                return;
            }

            int idTherapy = tc.getLastId();

            Therapy therapy = new Therapy();
            therapy.drug = drugSelected;
            therapy.patientCard = patientCard;
            therapy.StartDate = DateTime.Now;     
            therapy.EndDate = (DateTime)dateTherapy.SelectedDate;
            therapy.DailyDose = Int32.Parse(txtPrescribe.Text);
            therapy.Anamnesis = txtbox2.Text;
            therapy.Id = ++idTherapy;
       
            tc.AddTherapy(therapy);

            Notification n = new Notification();
            int idNotification = nc.getLastId();
            n.Id = ++idNotification;
            n.Type = TypeOfNotification.Terapija;
            n.JmbgOfReceiver = patientCard.Patient.Jmbg;
            n.Message = "Nova terapija: " + drugSelected.Name;

            nc.SendNotification(n);


            if (String.IsNullOrEmpty(txtbox2.Text))
            {
                MessageBox.Show("Morate unijeti tok bolesti.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox2.Clear();
                txtbox2.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtbox3.Text))
            {
                MessageBox.Show("Morate unijeti terapiju", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                txtbox3.Clear();
                txtbox3.Focus();
                return;
            }
            try
            {

                string date = txtbox1.Text;
                string disease = txtbox2.Text;
                string therapy11 = txtbox3.Text;

                PatientCard pc = new PatientCard();
                string[] ss = showName.Text.Split(' ');
                pc = ap.ViewPatientCard(ss[ss.Length - 1]);
                string mh = pc.MedicalHistory;

                pc.MedicalHistory = mh + date + ":" + disease + ":" + therapy11 + ";";

                ap.EditPatientCard(pc);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            ScheduleExaminaton win = new ScheduleExaminaton();
            win.Show();

            txtbox2.Clear();
            txtbox3.Clear();

            drugSelCell = -1;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string s = dataExam.SelectedDate.ToString();
            string[] lines = s.Split(' ');
            string datum = lines[0];

            if (String.IsNullOrEmpty(datum))
            {
                MessageBox.Show("Morate izabrati datum za koji želite zakazati prelged.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                dataExam.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(textBoxDoctorExam.Text))
            {
                MessageBox.Show("Morate izabrati doktora kod kog želite zakazati pregled.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
              
                textBoxDoctorExam.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtSmjena.Text))
            {
                MessageBox.Show("Morate izabrati smjenu u kojoj zelite zakazati pregled.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
              
                txtSmjena.Focus();
                return;
            }

            TimeSpan ts = new TimeSpan(FIRST_SHIFT_TIME, 0, 0);
            DateTime dt = (DateTime)dataExam.SelectedDate;

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
         
            List<Examination> allAppointments;
            Doctor selectedDoctor = (Doctor)textBoxDoctorExam.SelectedItem;
          
            List<String> typesExam = new List<string>();     
            
            if (selectedDoctor.Type == TypeOfDoctor.drOpstePrakse) {
                typesExam.Add(TypeOfExamination.Opsti.ToString());
            }
            else
            {
                typesExam.Add(TypeOfExamination.Operacija.ToString());
                typesExam.Add(TypeOfExamination.Specijalisticki.ToString());
            }
            txtVrsta.DataContext = typesExam;


            allAppointments = ec.getAllAppointments(selectedDoctor, dt);

            List<ExaminationDTO> examDTO = new List<ExaminationDTO>();
            foreach (Examination exm in allAppointments)
            {
                string room = "";
                string type = "";
                if (exm.room.Number != 0)
                {
                    room = exm.room.Number.ToString();
                }
                if (room.Equals(""))
                {
                    type = "";
                }
                else
                {
                    type = exm.Type.ToString();
                }
                ExaminationDTO e1 = new ExaminationDTO();
                e1.IdExamination = exm.IdExamination;
                e1.doctor = exm.doctor.Name + " " + exm.doctor.Name;
                e1.patientCard = exm.patientCard.Patient.Name + " " + exm.patientCard.Patient.Surname + " " + exm.patientCard.Patient.Jmbg;
                e1.DateAndTime = exm.DateAndTime.ToString();
                e1.room = room;
                e1.Type = type;

                examDTO.Add(e1);

            }

            if (examDTO.Count == 0)
            {
                MessageBox.Show("Nema slobodnih pregelda za odabrani datum.", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }
            DataGrid4.ItemsSource = examDTO;

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

            if (appointment < 0)
            {
                MessageBox.Show("Morate selektovati red u tabeli da bi zakazali pregled.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            else if (String.IsNullOrEmpty(txtVrsta.Text))
            {
                System.Windows.MessageBox.Show("Morate izabrati vrstu pregleda.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                txtVrsta.Focus();
                return;
            }

            Examination exam = new Examination();
  
            exam.IdExamination = id;
            exam.DateAndTime = dt;
            exam.doctor = (Doctor)textBoxDoctorExam.SelectedItem;
            exam.Type = TypeOfExamination.Opsti;
          

            if (txtVrsta.SelectedItem.ToString().Equals("Operacija"))
            {
                exam.Type = TypeOfExamination.Operacija;
             
            }
            else if (txtVrsta.SelectedItem.ToString().Equals("Specijalisticki")) {
                exam.Type = TypeOfExamination.Specijalisticki;
                
            }
           

            if (String.IsNullOrEmpty(txtSoba.Text))
            {
                System.Windows.MessageBox.Show("Morate izabrati sobu u kojoj zelite zakazati pregled.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
              
                txtSoba.Focus();
                return;
            }
            string roomStr = txtSoba.SelectedValue.ToString();
            int roomId = Int32.Parse(roomStr);
            exam.room = rc.ViewRoomByNumber(roomId);

            string[] ss = showName.Text.Split(' ');
           
            exam.patientCard = ap.ViewPatientCard(ss[ss.Length - 1]);

            ec.ScheduleExamination(exam);

            TimeSpan ts = new TimeSpan(8, 0, 0);
            if (second.IsSelected)
            {
                ts = new TimeSpan(16, 0, 0);
            }
            else if (third.IsSelected)
            {
                ts = new TimeSpan(12, 0, 0);
            }
            List<Examination> allAppointments;
            Doctor selectedDoctor = (Doctor)textBoxDoctorExam.SelectedItem;

            allAppointments = ec.getAllAppointments(selectedDoctor, (DateTime)dataExam.SelectedDate + ts);

            List<ExaminationDTO> examDTO = new List<ExaminationDTO>();
            foreach (Examination exm in allAppointments)
            {
                string room = "";
                string type = "";
                if (exm.room.Number != 0)
                {
                    room = exm.room.Number.ToString();
                }
                if (room.Equals(""))
                {
                    type = "";
                }
                else
                {
                    type = exm.Type.ToString();
                }
                ExaminationDTO e1 = new ExaminationDTO();
                e1.IdExamination = exm.IdExamination;
                e1.doctor = exm.doctor.Name + " " + exm.doctor.Name;
                e1.patientCard = exm.patientCard.Patient.Name + " " + exm.patientCard.Patient.Surname + " " + exm.patientCard.Patient.Jmbg;
                e1.DateAndTime = exm.DateAndTime.ToString();
                e1.room = room;
                e1.Type = type;

                examDTO.Add(e1);

            }

            if (examDTO.Count == 0)
            {
                MessageBox.Show("Nema slobodnih pregelda za odabrani datum.", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }
            DataGrid4.ItemsSource = examDTO;
            Notification n = new Notification();
            int idNotification = nc.getLastId();
            n.Id = ++idNotification;
            n.Type = TypeOfNotification.Pregled;
            n.Message = "Novi termin zakazan " + dt.ToString() + " dr. " + selectedDoctor.Name + " " + selectedDoctor.Surname;
            n.JmbgOfReceiver = patientCard.Patient.Jmbg;
            nc.SendNotification(n);

            appointment = -1;
        }

      
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string s1 = dataOd.SelectedDate.ToString();
            string[] lines1 = s1.Split(' ');
            string datumOd = lines1[0];

            if (String.IsNullOrEmpty(datumOd))
            {
                MessageBox.Show("Morate izabrati datum početka ležanja.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                dataOd.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(comboSoba.Text))
            {
                MessageBox.Show("Morate izabrati sobu u koju želite smjesiti pacijenta.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                comboSoba.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(comboKrevet.Text))
            {
                MessageBox.Show("Morate izabrati krevet u koji želite smjesiti pacijenta.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                comboKrevet.Focus();
                return;
            }
            else if (comboKrevet.Text.Contains("zauzet"))
            {
                MessageBox.Show("Ne možete smjestiti pacijenta u krevet koji je zauzet.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                comboKrevet.Focus();
                return;
            }

            string[] t1 = datumOd.Split('/');

            string s2 = dataDo.SelectedDate.ToString();
            string[] lines2 = s2.Split(' ');
            string datumDo = lines2[0];

            if (String.IsNullOrEmpty(datumDo))
            {
                MessageBox.Show("Morate izabrati datum kraja ležanja.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                dataDo.Focus();
                return;
            }

            string[] t2 = datumDo.Split('/');

            int god1 = Int32.Parse(t1[2]);
            int god2 = Int32.Parse(t2[2]);

            int mj1 = Int32.Parse(t1[0]);
            int mj2 = Int32.Parse(t2[0]);

            int dan1 = Int32.Parse(t1[1]);
            int dan2 = Int32.Parse(t2[1]);

            if (god2 < god1)
            {
                MessageBox.Show("Greška! Krajnji datum je stariji od početnog.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                dataOd.Focus();
                return;
            }
            else if (god1 == god2 && mj1 > mj2)
            {
                MessageBox.Show("Greška! Krajnji datum je stariji od početnog.", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);

                dataOd.Focus();
                return;
            }
            else if (god1 == god2 && mj1 == mj2 && dan1 > dan2)
            {
                MessageBox.Show("Greška! Krajnji datum je stariji od početnog.", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);

                dataOd.Focus();
                return;
            }
            Room r = roomPlacment;       
            r.Occupation = r.Occupation + 1;
            rc.EditRoom(r);

            List<Examination> examinations = ec.ViewExaminationsByRoom(r.Number);
            foreach (Examination exm in examinations)
            {
                exm.room = r;
                ec.EditExamination(exm);

            }

            RenovationPeriod period = rpc.ViewRenovationByRoomNumber(r.Number);

            period.room = r;
            rpc.EditRenovation(period);

            PlacemetnInARoom placement = new PlacemetnInARoom();         
            int idPlacement = prc.getLastId();
            placement.Id = ++idPlacement;
            placement.DateOfPlacement = (DateTime)dataOd.SelectedDate;
            placement.DateOfDismison = (DateTime)dataDo.SelectedDate;
            placement.room = r;
            placement.patientCard = patientCard;

            prc.PlaceInRoom(placement);

            var p = new PlaceInSickRoom();
            p.Show();
        }

        private void DataGrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid4.SelectedItem != null)
            {

                var cell = DataGrid4.SelectedIndex;
                appointment = (int)cell;
                
                var cellC = DataGrid4.Columns[0].GetCellContent(DataGrid4.SelectedItem);
                var row = cellC.Parent;
                string s = row.ToString();

                string[] lines = s.Split(':');
                string idStr = lines[1];
                id = Int32.Parse(idStr);

                var cell1 = DataGrid4.SelectedIndex;
                var cellC1 = DataGrid4.Columns[1].GetCellContent(DataGrid4.SelectedItem);
                var row1 = cellC1.Parent;
                string s1 = row1.ToString();

                string[] lines1 = s1.Split(':');
                
                string date = lines1[1] +":"+ lines1[2] +":"+ lines1[3];
               
                dt = DateTime.Parse(date);
               

            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (appointment < 0)
            {
                MessageBox.Show("Morate selektovati red u tabeli da bi otkazali pregled.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);


                return;
            }
  
            ec.DeleteScheduledExamination(id);

            TimeSpan ts = new TimeSpan(8, 0, 0);
            if (second.IsSelected)
            {
                ts = new TimeSpan(16, 0, 0);
            }
            else if (third.IsSelected)
            {
                ts = new TimeSpan(12, 0, 0);
            }
            List<Examination> allAppointments;
            Doctor selectedDoctor = (Doctor)textBoxDoctorExam.SelectedItem;

            allAppointments = ec.getAllAppointments(selectedDoctor, (DateTime)dataExam.SelectedDate + ts);

            List<ExaminationDTO> examDTO = new List<ExaminationDTO>();
            foreach (Examination exm in allAppointments)
            {
                string room = "";
                string type = "";
                if (exm.room.Number != 0)
                {
                    room = exm.room.Number.ToString();
                }
                if (room.Equals(""))
                {
                    type = "";
                }
                else
                {
                    type = exm.Type.ToString();
                }
                ExaminationDTO e1 = new ExaminationDTO();
                e1.IdExamination = exm.IdExamination;
                e1.doctor = exm.doctor.Name + " " + exm.doctor.Name;
                e1.patientCard = exm.patientCard.Patient.Name + " " + exm.patientCard.Patient.Surname + " " + exm.patientCard.Patient.Jmbg;
                e1.DateAndTime = exm.DateAndTime.ToString();
                e1.room = room;
                e1.Type = type;

                examDTO.Add(e1);

                appointment = -1;

            }

            if (examDTO.Count == 0)
            {
                MessageBox.Show("Nema slobodnih pregelda za odabrani datum.", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }
            DataGrid4.ItemsSource = examDTO;
        }
 
        
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            IInputElement focusedControl = FocusManager.GetFocusedElement(activeWindow);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                if (str.Equals("index"))
                {
                    str = "karton";
                }
                HelpProvider.ShowHelp(str);
            }
            else
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)activeWindow);
                HelpProvider.ShowHelp(str);
            }
        }

        private void comboSoba_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int idRoom = (int)comboSoba.SelectedItem;     
            Room room= rc.ViewRoomByNumber(idRoom);
            roomPlacment = room;
            int numberCapacity = room.Capacity;
            int numberOcupation = room.Occupation;
            int numberFree = numberCapacity - numberOcupation;

            string ocupation = "Krevet (zauzet)";
            string free = "Krevet (slobodan)";

            List<string> beds = new List<string>();
            for (int i = 0; i < numberOcupation; i++) {
                beds.Add(ocupation);
            }
            for (int i = 0; i < numberFree; i++)
            {
                beds.Add(free);
            }

            comboKrevet.DataContext = beds;
            
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            EditPatient s = new EditPatient(patientCard);
            s.Show();
            
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                string unos = txtSearch.Text;
                List<Drug> searchedDrugs = new List<Drug>();
                List<Drug> drugs = dcon.ViewConfirmedDrugs();
                List<DrugDTO> dDTOs = new List<DrugDTO>();

                if (unos.Equals(""))
                {
                    
                    foreach (Drug d in drugs)
                    {

                        DrugDTO dDTO = new DrugDTO();
                        dDTO.Id = d.Id;
                        dDTO.Name = d.Name;
                        dDTO.Quantity = d.Quantity;
                        dDTO.drugType = d.drugType.Type;

                        dDTOs.Add(dDTO);
                    }

                    DataGrid3.ItemsSource = dDTOs;

                    return;
                }

                foreach (Drug d in drugs)
                {
                    if (d.Name.ToUpper().Contains(unos.ToUpper()))
                    {
                        searchedDrugs.Add(d);
                    }
                }

                foreach (Drug d in searchedDrugs)
                {

                    DrugDTO dDTO = new DrugDTO();
                    dDTO.Id = d.Id;
                    dDTO.Name = d.Name;
                    dDTO.Quantity = d.Quantity;
                    dDTO.drugType = d.drugType.Type;

                    dDTOs.Add(dDTO);
                }

                DataGrid3.ItemsSource = dDTOs;

                
            }
        }

        private void txtSmjena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = dataExam.SelectedDate.ToString();
            string[] lines = s.Split(' ');
            string datum = lines[0];

            if (String.IsNullOrEmpty(datum))
            {
                MessageBox.Show("Morate izabrati datum za koji želite zakazati prelged.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                dataExam.Focus();
                return;
            }

            
            WorkShifts shift;
            if (first.IsSelected)
            {
                shift = WorkShifts.FIRST;
            }
            else if (second.IsSelected) {

                shift = WorkShifts.SECOND;
            }
            else
            {
                shift = WorkShifts.THIRD;
            }

            List<Doctor> doctorsWhoWork = wc.ViewDoctorsWhoWork(dataExam.SelectedDate.Value, shift);
            List<Doctor> doctors= new List<Doctor>();
            List<User> users = dc.ViewAllUsers();
            foreach (User u in users)
            {
                foreach(Doctor d in doctorsWhoWork)
                {
                    if (d.Jmbg.Equals(u.Jmbg))
                    {
                        doctors.Add((Doctor)u);
                    }
                }
                
            }


            textBoxDoctorExam.DataContext = doctors;

            
        }

        private void dataDo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            TypeOfUsage t = TypeOfUsage.Soba_za_lezanje;
            DateTime odDate = (DateTime)dataOd.SelectedDate;
            DateTime doDate = (DateTime)dataDo.SelectedDate;

            List<Room> rooms = rc.ViewRoomByUsage(t, odDate, doDate);
            List<int> roomNumbers = new List<int>();
            foreach (Room r in rooms)
            {

                roomNumbers.Add(r.Number);
            }
            comboSoba.DataContext = roomNumbers;
        }

        private void txtVrsta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeOfUsage t = TypeOfUsage.Soba_za_pregled;

            if (txtVrsta.SelectedItem.ToString().Equals("Operacija"))
            {
               
                t = TypeOfUsage.Operaciona_sala;
            }
            else if (txtVrsta.SelectedItem.ToString().Equals("Specijalisticki"))
            {
             
                t = TypeOfUsage.Soba_za_pregled;
            }

            List<Room> rooms = rc.ViewRoomByUsage(t, dt, dt);
            List<int> roomNumbers = new List<int>();
            foreach (Room r in rooms)
            {

                roomNumbers.Add(r.Number);
            }
            txtSoba.DataContext = roomNumbers;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ViewPlacements s = new ViewPlacements(name);
            s.Show();
        }
    }
}
