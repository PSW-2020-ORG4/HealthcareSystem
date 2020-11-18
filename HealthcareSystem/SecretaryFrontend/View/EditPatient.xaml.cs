using Controller.DrugAndTherapy;
using Controller.ExaminationAndPatientCard;
using Controller.PlacementInARoomAndRenovationPeriod;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Enums;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Page
    {
        private CityController cityController = new CityController();     
        private PatientCardController patientCardController = new PatientCardController();
        private static PatientController patientController = new PatientController();
        private UserController userController = new UserController(patientController);
        private ExaminationController examinationController = new ExaminationController();
        private TherapyController therapyController = new TherapyController();
        private PlacementInSickRoomController placementInSickRoomController = new PlacementInSickRoomController();
        List<City> cities = new List<City>();
        Patient patient;
        PatientCard patientCard;
        string dates = "";
        string desc = "";
        string therapy = "";
        public EditPatient(Object o)
        {
            InitializeComponent();

            patient = (Patient)o;
            patientCard = patientCardController.ViewPatientCard(patient.Jmbg);

            cities = cityController.ViewCities();
            txtCity.DataContext = cities;

            InitializeWindowIndormation(patient);
        }

        private void InitializeWindowIndormation(Patient patient)
        {
            txtName.Text = patient.Name;
            txtSurname.Text = patient.Surname;
            txtJmbg.Text = patient.Jmbg;
            if (!patient.DateOfBirth.Equals(""))
                dpDateOfBirth.SelectedDate = patient.DateOfBirth;

            if(patient.Gender == GenderType.M)
            {
                cmbGender.SelectedIndex = 0;
            }
            else
            {
                cmbGender.SelectedIndex = 1;
            }

            txtStreet.Text = patient.HomeAddress;

            if (patient.City != null)
            {
                City selectedCity = cityController.getCityByZipCode(patient.City.ZipCode);
                if (selectedCity != null)
                {
                    for (int i = 0; i < cities.Count; i++)
                    {
                        if (selectedCity.ZipCode == cities[i].ZipCode)
                        {
                            txtCity.SelectedItem = cities[i];
                        }
                    }
                }
            }

            txtPhone.Text = patient.Phone;
            txtEmail.Text = patient.Email;
            txtUsername.Text = patient.Username;
            txtPassword.Password = patient.Password;

            if(patientCard.BloodType == BloodType.A)
            {
                cmbBloodType.SelectedIndex = 0;
            }else if(patientCard.BloodType == BloodType.B)
            {
                cmbBloodType.SelectedIndex = 1;
            }else if (patientCard.BloodType == BloodType.AB)
            {
                cmbBloodType.SelectedIndex = 2;
            }
            else
            {
                cmbBloodType.SelectedIndex = 3;
            }

            if(patientCard.RhFactor == RhFactorType.Pozitivna)
            {
                cmbRh.SelectedIndex = 0;
            }
            else
            {
                cmbRh.SelectedIndex = 1;
            }
           
            txtAllergy.Text = patientCard.Alergies;
            if (patientCard.HasInsurance)
            {
                yesCheckBtn.IsChecked = true;
                txtLbo.Text = patientCard.Lbo;
            }
            else
            {
                noCheckBtn.IsChecked = true;
            }
            string[] parts = patientCard.MedicalHistory.Split(';');

            if (!patientCard.MedicalHistory.Equals("") || !patientCard.MedicalHistory.Equals(";"))
            {
                for (int i = 0; i < parts.Length-1; i++)
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

        private void noCheckBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (noCheckBtn.IsChecked == true)
            {
                yesCheckBtn.IsChecked = false;
                txtLbo.Background = Brushes.LightGray;
                txtLbo.Text = "";
                txtLbo.IsEnabled = false;
            }
        }

        private void noCheckBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLbo.Background = Brushes.LightGray;
            txtLbo.Text = "";
            txtLbo.IsEnabled = false;
        }

        private void yesCheckBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (yesCheckBtn.IsChecked == true)
            {
                noCheckBtn.IsChecked = false;
                txtLbo.Background = Brushes.White;
                txtLbo.IsEnabled = true;
            }
        }

        private void yesCheckBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLbo.Background = Brushes.LightGray;
            txtLbo.Text = "";
            txtLbo.IsEnabled = false;
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            Regex regexJmbg = new Regex(@"^[0-9]{13}$");
            Match match = regexJmbg.Match(txtJmbg.Text);
            Regex regexPhone = new Regex(@"^[0-9]+$");
            Match match1 = regexPhone.Match(txtPhone.Text);
            Regex regexLbo = new Regex(@"^[a-z]{2}[0-9]{3}$");
            Match match2 = regexLbo.Match(txtLbo.Text);
            Regex regexEmail = new Regex(@"^[a-z0-9\.\-_]{4,20}[@]{1}[a-z.]{4,10}");
            Match match3 = regexEmail.Match(txtEmail.Text);

            if (txtName.Text.Equals("") || txtSurname.Text.Equals("") || txtJmbg.Text.Equals(""))
            {
                var okMbx = new OKMessageBox(this, 1);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Unos imena, prezimena i jmbg-a je obavezan!";
                okMbx.ShowDialog();
            }
            else if (!match.Success)
            {
                var okMbx = new OKMessageBox(this, 2);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "JMBG mora da sadrži 13 cifara!";
                okMbx.ShowDialog();
            }
            else if (!txtPhone.Text.Equals("") && !match1.Success)
            {
                var okMbx = new OKMessageBox(this, 2);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Telefon može da sadrži samo cifre!";
                okMbx.ShowDialog();
            }
            else if (!txtLbo.Text.Equals("") && !match2.Success)
            {
                var okMbx = new OKMessageBox(this, 2);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Lbo mora da bude u formatu xx000!";
                okMbx.ShowDialog();
            }
            else if (!txtEmail.Text.Equals("") && !match3.Success)
            {
                var okMbx = new OKMessageBox(this, 2);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Pogrešan format za email!";
                okMbx.ShowDialog();
            }
            else
            {
                bool hi = false;
                if ((bool)yesCheckBtn.IsChecked)
                {
                    hi = true;
                }
                BloodType blood = BloodType.O;
                RhFactorType rh = RhFactorType.Negativna;
                DateTime date = DateTime.Today;
                GenderType gen = GenderType.Z;

                if (cmbBloodType.Text.Equals("A"))
                {
                    blood = BloodType.A;
                }
                else if (cmbBloodType.Text.Equals("B"))
                {
                    blood = BloodType.B;
                }
                else if (cmbBloodType.Text.Equals("AB"))
                {
                    blood = BloodType.AB;
                }

                if (cmbRh.Text.Equals("+"))
                {
                    rh = RhFactorType.Pozitivna;
                }
                if (dpDateOfBirth.SelectedDate != null)
                {
                    date = (DateTime)dpDateOfBirth.SelectedDate;
                } 
                   if (cmbGender.Text.Equals("M"))
                    {
                        gen = GenderType.M;
                    }

                City city = (City)txtCity.SelectedItem;

                    bool guest = false;
                    if (txtUsername.Text.Equals("") || txtUsername.Text == null)
                    {
                        guest = true;
                    }


                    Patient p = new Patient(txtJmbg.Text, txtName.Text, txtSurname.Text, date, gen, city, txtStreet.Text, txtPhone.Text, txtEmail.Text, txtUsername.Text, txtPassword.Password, DateTime.Today, guest);

                string medicalHistory = "";
                string[] partsDate = txtDate.Text.Split('\n');
                string[] partsDesc = txtDesc.Text.Split('\n');
                string[] partsTherapy = txtTherapy.Text.Split('\n');

                if (!txtDate.Text.Equals(dates) || !txtTherapy.Text.Equals(therapy) || !txtDesc.Text.Equals(desc))
                {
                    if (partsDate.Length != partsDesc.Length || partsDate.Length != partsTherapy.Length || partsDate.Length != partsTherapy.Length)
                    {
                        var okMbx = new OKMessageBox(this, 2);
                        okMbx.titleMsgBox.Text = "Greška";
                        okMbx.textMsgBox.Text = "Niste dobro unijeli istoriju bolesti!";
                        okMbx.ShowDialog();
                        return;
                    }
                    if (!txtDate.Text.Equals("") || !txtDesc.Text.Equals("") || !txtTherapy.Text.Equals(""))
                    {
                        for (int i = 0; i < partsDate.Length; i++)
                        {
                            if (partsDate[i].Equals("") || partsDesc[i].Equals("") || partsTherapy[i].Equals(""))
                            {
                                var okMbx = new OKMessageBox(this, 2);
                                okMbx.titleMsgBox.Text = "Greška";
                                okMbx.textMsgBox.Text = "Niste dobro unijeli istoriju bolesti!";
                                okMbx.ShowDialog();
                                return;
                            }
                            medicalHistory += partsDate[i] + ":" + partsDesc[i] + ":" + partsTherapy[i] + ";";
                        }
                    }
                }
                else
                {
                    medicalHistory = dates + desc + therapy;
                    medicalHistory = medicalHistory.Replace('\n',':');
                    if (medicalHistory.Length > 0)
                    {
                        medicalHistory = medicalHistory.Substring(0, medicalHistory.Length - 1);
                        medicalHistory += ";";
                    }
                }

                PatientCard pc = new PatientCard(p, blood, rh, txtAllergy.Text, medicalHistory, hi, txtLbo.Text);

                    if (patientController.EditProfile(p) != null)
                    {
                        List<Examination> examinations = examinationController.ViewExaminationsByPatient(p.Jmbg);
                        foreach(Examination exm in examinations)
                        {
                            exm.patientCard = pc;
                            examinationController.EditExamination(exm);
                        }
                        List<Therapy> therapies = therapyController.ViewAllTherapyByPatient(p.Jmbg);
                        foreach(Therapy t in therapies)
                        {
                            t.patientCard = pc;
                            therapyController.EditTherapy(t);
                        }
                         List<PlacemetnInARoom> placemetnInARooms = placementInSickRoomController.ViewPatientPlacements(p.Jmbg);
                        foreach(PlacemetnInARoom pr in placemetnInARooms)
                        {
                            pr.patientCard = pc;
                            placementInSickRoomController.EditPlacement(pr);
                        }

                        var okMb = new OKMessageBox(this, 3);
                        okMb.titleMsgBox.Text = "Obavještenje";
                        okMb.textMsgBox.Text = "Uspješno ste izmijenili informacije o pacijentu.";
                        okMb.ShowDialog();
                    }
                    else
                    {
                        var okMbx = new OKMessageBox(this, 2);
                        okMbx.titleMsgBox.Text = "Greška";
                        okMbx.textMsgBox.Text = "Došlo je do greške prilikom izmjene informacija! Provjerite da li su korisničko ime/lozinka validni.";
                        okMbx.ShowDialog();
                    }
                }
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/close.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Zatvaranje prozora";
            mb.textMsgBox.Text = "Da li ste sigurni da želite odustati od izmjene informacije o pacijentu?";
            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void cmbGender_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            List<string> gender = new List<string>();
            gender.Add("M");
            gender.Add("Z");
            combo.ItemsSource = gender;
        }

        private void cmbBloodType_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            List<string> blood = new List<string>();
            blood.Add("A");
            blood.Add("B");
            blood.Add("AB");
            blood.Add("0");
            combo.ItemsSource = blood;
        }

        private void cmbRh_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            List<string> rh = new List<string>();
            rh.Add("+");
            rh.Add("-");
            combo.ItemsSource = rh;
        }

    }
}
