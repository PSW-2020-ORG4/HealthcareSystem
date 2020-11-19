using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model.Doctor;
using Controller.ExaminationAndPatientCard;
using Model.Users;
using Controller.DrugAndTherapy;
using Model.Manager;
using System.Configuration;
using Controller.UsersAndWorkingTime;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private int FIRST_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["first_shift_start"]);
        private int SECOND_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["second_shift_start"]);
        private int THIRD_SHIFT_TIME = Int32.Parse(ConfigurationManager.AppSettings["third_shift_start"]);
        private string patientName = "";
        private Doctor d;
        private int id = 0;
        private static DrugController dc = new DrugController();
        private static ExaminationController ser = new ExaminationController();
        private static WorkingTimeController wc = new WorkingTimeController();
        public HomePage(Doctor doctor)
        {
            
            InitializeComponent();

            logInDoctor.Text = doctor.Username;
            d = doctor;

            data1.SelectedDate = DateTime.Today.Date;
            data1.DisplayDateStart = DateTime.Today;
            data1.DisplayDateEnd = (new DateTime(2020, 12, 31));
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            IInputElement focusedControl = FocusManager.GetFocusedElement(activeWindow);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str);
            }
            else
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)activeWindow);
                HelpProvider.ShowHelp(str);
            }
        }

       
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {     
            List<Drug> drugs = dc.ViewUnconfirmedDrugs();
            if (drugs.Count > 0)
            {
                var s = new CheckingDrug();
                s.Show();
            }
            else
            {
                var s = new NotNewDrug();
                s.Show();
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(patientName))
            {
                MessageBox.Show("Morate selektovati red u tabeli koji pregled zelite da obavite.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                
                return;
            }

            PatienFileTab s = new PatienFileTab(patientName, d, id);
            s.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var s = new GenerateReport(d);
            s.Show();
        }    

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var s = new EditProfile(d);
            s.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new AboutAplication();
            s.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            HelpViewer help = new HelpViewer("index");
            help.ShowDialog();
        }

        private void Navigation_Graphical_Editor_Click(object sender, RoutedEventArgs e)
        {
            GraphicalEditor.MainWindow graphicalEditorMainWindow = new GraphicalEditor.MainWindow();
            graphicalEditorMainWindow.ShowDialog();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            
            string s = data1.SelectedDate.ToString();
            string[] lines = s.Split(' ');
            string datum = lines[0];

            if (String.IsNullOrEmpty(datum))
            {
                MessageBox.Show("Morate izdabrati datum za koji želite pregledati raspored.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                data1.Focus();
                return;
            }

            //WorkingTime wt = wc.ViewWorkingTime(d.Jmbg);
            WorkShifts ws = wc.getShiftForDoctor(d.Jmbg);
            TimeSpan ts;
            DateTime dt = (DateTime)data1.SelectedDate;

            if (ws == WorkShifts.FIRST)
            {
                ts = new TimeSpan(FIRST_SHIFT_TIME, 0, 0);
                dt = dt + ts;
            }
            else if(ws == WorkShifts.SECOND)
            {
                ts = new TimeSpan(SECOND_SHIFT_TIME, 0, 0);
                dt = dt + ts;
            }
            else
            {
                ts = new TimeSpan(THIRD_SHIFT_TIME, 0, 0);
                dt = dt + ts;
                dt = dt.AddDays(-1);
            }

            //TimeSpan ts = new TimeSpan(8, 0, 0);         
            List<Examination> exams = new List<Examination>();
            List<Examination> exams2 = new List<Examination>();

            double hours = 0;
            for (int i = 0; i < 16; i++)
            {
                
                exams = ser.ViewExaminationsByDoctorAndDate(d, dt.AddHours(hours));
                hours += 0.5;
                if (exams.Count != 0)
                {
                    foreach (Examination e1 in exams) {
                        exams2.Add(e1);
                    }
                }

            }
                      
            List<ExaminationDTO> examDTO = new List<ExaminationDTO>();
            foreach(Examination ee in exams2)
            {
                ExaminationDTO e1 = new ExaminationDTO();
                e1.IdExamination = ee.IdExamination;
                e1.doctor = ee.doctor.Name + " " + ee.doctor.Surname;
                e1.DateAndTime = ee.DateAndTime.ToShortTimeString();
                e1.IdExamination = ee.IdExamination;
                e1.patientCard = ee.patientCard.patient.Name + " " + ee.patientCard.patient.Surname + " " + ee.patientCard.patient.Jmbg;
                e1.Type = ee.Type.ToString();
                e1.room = ee.room.Number.ToString();

                examDTO.Add(e1);

            }
            if (examDTO.Count == 0)
            {
                MessageBox.Show("Nema zakazanih pregelda za odabrani datum.", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);

                data1.Focus();
                return;
            }

            DataGrid2.ItemsSource = examDTO;
        }

        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid2.SelectedItem != null)
            {

                var cellC = DataGrid2.Columns[5].GetCellContent(DataGrid2.SelectedItem);
                var row = cellC.Parent;
                string s = row.ToString();

                string[] lines = s.Split(':');
                patientName = lines[1];

                var cellC1 = DataGrid2.Columns[0].GetCellContent(DataGrid2.SelectedItem);
                var row1 = cellC1.Parent;
                string s1 = row1.ToString();

                string[] lines1 = s1.Split(':');
                string idStr = lines1[1];
                id = Int32.Parse(idStr);

            }
        }
    }
}
