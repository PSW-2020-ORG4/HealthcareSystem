using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using MaterialDesignColors;
using Model.Secretary;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        Patient patient;
        PatientCard patientCard;
        City city;
        List<City> cities = new List<City>();
        private static PatientController patientController = new PatientController();
        private UserController userController = new UserController(patientController);
        PatientCardController patientCardController = new PatientCardController();
        CityController cityController = new CityController();
        public Registration()
        {
            InitializeComponent();

            cities = cityController.ViewCities();
            txtCity.DataContext = cities;
        }
        private void quickRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            if (quickRadioBtn.IsChecked == true)
            {
                adressExpander.Visibility = Visibility.Hidden;
                adressExpander.IsExpanded = false;
                userExpander.Visibility = Visibility.Hidden;
                userExpander.IsExpanded = false;
            }
            else
            {
                adressExpander.Visibility = Visibility.Visible;
                adressExpander.IsExpanded = true;
                userExpander.Visibility = Visibility.Visible;
                userExpander.IsExpanded = true;
            }
        }

        private void completeRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            if (completeRadioBtn.IsChecked == true)
            {
                adressExpander.Visibility = Visibility.Visible;
                adressExpander.IsExpanded = true;
                userExpander.Visibility = Visibility.Visible;
                userExpander.IsExpanded = true;
            }
            else
            {
                adressExpander.Visibility = Visibility.Hidden;
                adressExpander.IsExpanded = false;
                userExpander.Visibility = Visibility.Hidden;
                userExpander.IsExpanded = false;
            }
        }

        private void yesCheckBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (yesCheckBtn.IsChecked == true)
            {
                noCheckBtn.IsChecked = false;
                lboTextInput.Background = Brushes.White;
                lboTextInput.IsEnabled = true;
            }
        }

        private void noCheckBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (noCheckBtn.IsChecked == true)
            {
                yesCheckBtn.IsChecked = false;
                lboTextInput.Background = Brushes.LightGray;
                lboTextInput.Text = "";
                lboTextInput.IsEnabled = false;
            }
        }

        private void yesCheckBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            lboTextInput.Background = Brushes.LightGray;
            lboTextInput.Text = "";
            lboTextInput.IsEnabled = false;
        }
        private void noCheckBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            lboTextInput.Background = Brushes.LightGray;
            lboTextInput.Text = "";
            lboTextInput.IsEnabled = false;
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/close.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Zatvaranje prozora";
            mb.textMsgBox.Text = "Da li ste sigurni da želite odustati od registracije korisnika?";
            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Regex regexJmbg = new Regex(@"^[0-9]{13}$");
            Match match = regexJmbg.Match(txtJmbg.Text);
            Regex regexPhone = new Regex(@"^[0-9]+$");           
            Match match1 = regexPhone.Match(txtPhone.Text);
            Regex regexLbo = new Regex(@"^[a-z]{2}[0-9]{3}$");
            Match match2 = regexLbo.Match(lboTextInput.Text);
            Regex regexEmail = new Regex(@"^[a-z0-9\.\-_]{4,20}[@]{1}[a-z.]{4,10}");
            Match match3 = regexEmail.Match(txtEmail.Text);

            if (txtName.Text.Equals("") || txtSurname.Text.Equals("") || txtJmbg.Text.Equals("") || cmbGender.SelectedItem == null || datePicker.SelectedDate == null)
            {
                var okMbx = new OKMessageBox(this, 1);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Unos imena, prezimena, JMBG-a, pola i datuma rođenja je obavezan!";
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
            else if (!lboTextInput.Text.Equals("") && !match2.Success)
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
            else if((bool)completeRadioBtn.IsChecked && (txtUsername.Text.Equals("") || txtPassword.Password.Equals("")))
            {
                var okMbx = new OKMessageBox(this, 2);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Unos korisničkog imena i lozinke je obavezan za kompletnu registraciju!";
                okMbx.ShowDialog();
            }
            else
            {
                bool hi = false;
                if ((bool)yesCheckBtn.IsChecked)
                {
                    hi = true;
                }
                BloodType bloodType = BloodType.O;
                RhFactorType rhFactorType = RhFactorType.Negativna;
                GenderType gender = GenderType.Z;
                DateTime sd = DateTime.Today;
                if (cmbBlood.SelectedItem != null)
                {
                    if (cmbBlood.Text.Equals("A"))
                    {
                        bloodType = BloodType.A;
                    }else if (cmbBlood.Text.Equals("B"))
                    {
                        bloodType = BloodType.B;
                    }else if (cmbBlood.Text.Equals("AB"))
                    {
                        bloodType = BloodType.AB;
                    }
                }
                if(cmbRh.SelectedItem != null)
                {
                    if (cmbRh.Text.Equals("+"))
                    {
                        rhFactorType = RhFactorType.Pozitivna;
                    }
                }               
                if(datePicker.SelectedDate != null)
                {
                    sd = (DateTime)datePicker.SelectedDate;
                }
                if(cmbGender.SelectedItem != null)
                {
                    if (cmbGender.Text.Equals("M"))
                    {
                        gender = GenderType.M;
                    }
                }

                bool isGuest = false;
                if ((bool)quickRadioBtn.IsChecked)
                {
                    isGuest = true;
                }

                if(txtCity.SelectedItem != null)
                {
                    city = (City)txtCity.SelectedItem;
                }
                else
                {
                    city = new City();
                }

                string medicalHistory = "";
                string[] partsDate = txtDate.Text.Split('\n');
                string[] partsDesc = txtDesc.Text.Split('\n');
                string[] partsTherapy = txtTherapy.Text.Split('\n');


                if(partsDate.Length != partsDesc.Length || partsDate.Length != partsTherapy.Length || partsDate.Length != partsTherapy.Length)
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

                patient = new Patient(txtJmbg.Text,txtName.Text,txtSurname.Text,sd,gender,city,txtStreet.Text,txtPhone.Text,txtEmail.Text,txtUsername.Text,txtPassword.Password,DateTime.Today,isGuest);
                patientCard = new PatientCard(patient,bloodType,rhFactorType,txtAllergy.Text, medicalHistory, hi,lboTextInput.Text);

                if(userController.Register(patient) != null && patientCardController.CreatePatientCard(patientCard) != null)
                {
                    var okMb = new OKMessageBox(this, 3);
                    okMb.titleMsgBox.Text = "Obavještenje";
                    okMb.textMsgBox.Text = "Uspješno ste registrovali novog pacijenta.";
                    okMb.ShowDialog();
                }
                else
                {
                    var okMbx = new OKMessageBox(this, 2);
                    okMbx.titleMsgBox.Text = "Greška";
                    okMbx.textMsgBox.Text = "Pacijent je već registrovan ili ste unijeli nedovoljan broj karaktera za korisničko ime/lozinku!";
                    okMbx.ShowDialog();
                }              
                
            }
        }

        private void cmbGender_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            List<string> gender = new List<string>();
            gender.Add("M");
            gender.Add("Z");
            combo.ItemsSource = gender;
        }

        private void cmbBlood_Loaded(object sender, RoutedEventArgs e)
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
