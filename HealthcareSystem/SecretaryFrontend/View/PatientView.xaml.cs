using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using ProjekatZdravoKorporacija.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class PatientView : Page
    {
        private static PatientController patientController = new PatientController();
        private UserController userController = new UserController(patientController);
        private PatientCardController patientCardController = new PatientCardController();
        List<Patient> patients = new List<Patient>();
        List<PatientCard> patientCards = new List<PatientCard>();
        public PatientView()
        {
            InitializeComponent();
            txtSearchPatients.Focus();

            List<User> users = userController.ViewAllUsers();
            foreach(User u in users)
            {
                patients.Add((Patient)u);           
            }
            for(int i = 0; i < patients.Count; i++)
            {
                patientCards.Add(patientCardController.ViewPatientCard(patients[i].Jmbg));
            }

            dgPatients.ItemsSource = patients.ToList();
        }


        private void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Main.Content = new Registration();
                }
            }
        }

        private void txtSearchPatients_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = patients.Where(patient => patient.Jmbg.Contains(txtSearchPatients.Text) || patient.Name.Contains(txtSearchPatients.Text) 
                                        || patient.Surname.Contains(txtSearchPatients.Text)
                                        || patient.DateOfBirth.ToString().Contains(txtSearchPatients.Text)
                                        || patient.Phone.Contains(txtSearchPatients.Text));

            dgPatients.ItemsSource = filtered;
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patientToDelete = (Patient)dgPatients.SelectedItem;
            if (patientToDelete == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Odaberite pacijenta koga želite obrisati!";
                okMb.ShowDialog();
            }
            else
            {
                var mb = new MyMessageBoxDelete(patientToDelete,this);
                mb.titleMsgBox.Text = "Brisanje pacijenta";
                mb.textMsgBox.Text = "Da li ste sigurni da želite obrisati pacijenta?";
                mb.ShowDialog();
            }
        }

        private void btnEditPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patientToEdit = (Patient)dgPatients.SelectedItem;
            if (patientToEdit == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Odaberite pacijenta čije podatke mijenjate!";
                okMb.ShowDialog();
            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new EditPatient(patientToEdit);
                    }
                }
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            Patient patientToShow = (Patient)dgPatients.SelectedItem;
            if (patientToShow == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Odaberite pacijenta čije podatke želite vidjeti!";
                okMb.ShowDialog();
            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new DetailsAboutPatient(patientToShow);
                    }
                }
            }
        }
    }
}
