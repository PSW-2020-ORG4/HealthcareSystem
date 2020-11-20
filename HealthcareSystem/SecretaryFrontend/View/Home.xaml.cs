using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using OxyPlot;
using OxyPlot.Axes;
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
using System.Windows.Threading;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private static PatientController patientController = new PatientController();
        private UserController userController = new UserController(patientController);
        private ExaminationController examinationController = new ExaminationController();
        public Home()
        {
            InitializeComponent();

            regPatients.Text = userController.ViewAllUsers().Count.ToString();
            allExm.Text = examinationController.ViewScheduledExaminations().Count.ToString();
            cancExm.Text = examinationController.ViewCanceledExaminations().Count.ToString();
            todayExm.Text = examinationController.ViewScheduledExaminationsByDate(DateTime.Today).Count.ToString();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal,delegate
                {
                    this.txtTime.Text = DateTime.Now.ToString("HH:mm:ss");
                }, this.Dispatcher);

            txtDate.Text = DateTime.Now.ToShortDateString();

            ChartViewModel mw = new ChartViewModel(){ Title = "Nedeljni broj zakazanih pregleda", Points = new List<DataPoint>{
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-7)), 15),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-6)), 36),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-5)), 18),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-4)), 26),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-3)), 27),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-2)), 45),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-1)), 37),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now ), 40),
                              }
            };
            ChartViewModel mw1 = new ChartViewModel()
            {
                Title = "Nedeljni broj registrovanih pacijenata",
                Points = new List<DataPoint>{
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-7)), 12),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-6)), 9),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-5)), 17),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-4)), 5),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-3)), 10),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-2)), 4),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now.AddDays(-1)), 6),
                                  new DataPoint(DateTimeAxis.ToDouble( DateTime.Now ), 8),
                              }
            };

            chart.DataContext = mw;
            chart1.DataContext = mw1;
            

        }

        private void registredPatients_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Main.Content = new PatientView();
                }
            }
        }

        private void scheduledExaminations_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Main.Content = new SearchExaminations();
                }
            }
        }

        private void canceledExaminations_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Main.Content = new CanceledExaminationsView();
                }
            }
        }

        private void todayExamination_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Main.Content = new ExaminationViewByDoctor("");
                }
            }
        }
    }
    public class ChartViewModel
    {
        public string Title { get; set; }

        public IList<DataPoint> Points { get; set; }
    }
}
