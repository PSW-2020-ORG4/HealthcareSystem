using Controller.ExaminationAndPatientCard;
using Model.Secretary;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
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

namespace ProjekatZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for DetailsAboutPatient.xaml
    /// </summary>
    public partial class DetailsAboutPatient : Page
    {
        PatientCardController patientCardController = new PatientCardController();
        public DetailsAboutPatient(Object o)
        {
            InitializeComponent();

            Patient patient = (Patient)o;

            InitializeWindowInformation(patient);
        }

        private void InitializeWindowInformation(Patient patient)
        {
            txtName.Text = patient.Name;
            txtSurname.Text = patient.Surname;
            txtJmbg.Text = patient.Jmbg;
            txtDateOfBirth.Text = patient.DateOfBirth.ToShortDateString();
            txtGender.Text = patient.Gender.ToString();
            txtStreet.Text = patient.HomeAddress;
            if (patient.City.ZipCode == 0)
            {
                txtCity.Text = "";
            }
            else
            {
                txtCity.Text = patient.City.Name + " " + patient.City.ZipCode;
            }
            txtEmail.Text = patient.Email;
            txtPhone.Text = patient.Phone;
            txtUsername.Text = patient.Username;
            pbPassword.Password = patient.Password;

            PatientCard patientCard = patientCardController.ViewPatientCard(patient.Jmbg);

            txtBloodType.Text = patientCard.BloodType.ToString();
            txtRh.Text = patientCard.RhFactor.ToString();
            txtAllergy.Text = patientCard.Alergies;
            if (patientCard.HasInsurance)
            {
                txtHasOccurance.Text = "Da";
            }
            else
            {
                txtHasOccurance.Text = "Ne";
            }
            txtLbo.Text = patientCard.Lbo;

            string[] parts = patientCard.MedicalHistory.Split(';');
            string dates = "";
            string desc = "";
            string therapy = "";

            if (!patientCard.MedicalHistory.Equals("") || !patientCard.MedicalHistory.Equals(";"))
            {
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    string[] paramParts = parts[i].Split(':');
                    dates += paramParts[0] + "\n";
                    desc += paramParts[1] + "\n";
                    therapy += paramParts[2] + "\n";
                }

                txtDate.Text = dates;
                txtDesc.Text = desc;
                txtTherapy.Text = therapy;
            }

        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Main.Content = new PatientView();
                }
            }
        }
    }
}
