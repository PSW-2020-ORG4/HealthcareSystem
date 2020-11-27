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
using System.Windows.Shapes;

namespace ZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for MakeSelfAppointment.xaml
    /// </summary>
    public partial class MakeSelfAppointment : Window
    {
        public MakeSelfAppointment()
        {
            InitializeComponent();
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

        }

        private void examinationDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                examinationDate.IsDropDownOpen = true;
            }
        }

        private void appointemtTime_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                appointemtTime.IsDropDownOpen = true;
            }
        }

        private void doctor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                doctor.IsDropDownOpen = true;
            }
        }
    }
}
