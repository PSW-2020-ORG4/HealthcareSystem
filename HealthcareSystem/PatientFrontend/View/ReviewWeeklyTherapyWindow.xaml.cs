using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Schema;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for ReviewWeeklyTherapyWindow.xaml
    /// </summary>
    public partial class ReviewWeeklyTherapyWindow : Window
    {
        public ObservableCollection<WeeklyTherapy> WeeklyTherapies
        {
            get;
            set;
        }

        public ReviewWeeklyTherapyWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            WeeklyTherapies = new ObservableCollection<WeeklyTherapy>();
            WeeklyTherapies.Add(new WeeklyTherapy { NameDrug = "Aerius", One = "na 8 sati", Two = "na 8 sati", Three = "na 8 sati", Four = "na 8 sati", Five = "na 8 sati", Six = "na 8 sati", Seven = "na 8 sati" });
            WeeklyTherapies.Add(new WeeklyTherapy { NameDrug = "Marisol", One = "na 6 sati", Two = "na 6 sati", Three = "na 6 sati", Four = "na 6 sati", Five = "na 6 sati", Six = "na 6 sati", Seven = "na 6 sati" });
            WeeklyTherapies.Add(new WeeklyTherapy { NameDrug = "Nasonex", One = "na 6 sati", Two = "na 6 sati", Three = "na 6 sati", Four = "na 6 sati", Five = "na 6 sati", Six = "na 6 sati", Seven = "na 6 sati" });
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

    public class WeeklyTherapy
    {
        public string NameDrug { get; set; }
        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }
        public string Seven { get; set; }
    }
}
