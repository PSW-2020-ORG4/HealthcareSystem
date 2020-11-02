using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //txtNameAndSurname.Text = "Marko Marković";
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Home_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Home();
        }

        private void Patients_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new PatientView();
        }

        private void Examinations_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ExaminationViewByDoctor("");
        }
        private void editProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new EditProfile();
        }

        private void Search_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new SearchExaminations();
        }

        private void Doctors_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new DoctorView();
        }

        private void Rooms_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new RoomView();
        }

        private void signOutBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/signout.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Odjava sa sistema";
            mb.textMsgBox.Text = "Da li ste sigurni da se želite odjaviti?";
            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void Report_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Report();
        }

        private void CanceledExaminations_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new CanceledExaminationsView();
        }

        private void HospitalMap_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GraphicalEditor.MainWindow graphicalEditorMainWindow = new GraphicalEditor.MainWindow();
            graphicalEditorMainWindow.ShowDialog();
        }
    }
}
