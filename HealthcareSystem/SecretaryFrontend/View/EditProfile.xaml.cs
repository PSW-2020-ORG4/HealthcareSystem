using Controller.UsersAndWorkingTime;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Page
    {
        private Secretary secretary;
        private static SecretaryController secretaryController = new SecretaryController();
        private UserController userController = new UserController(secretaryController);
        private CityController cityController = new CityController();
        List<City> cities = new List<City>();

        public EditProfile()
        {
            InitializeComponent();

            secretary = (Secretary)userController.ViewProfile(LogIn.secretary.Jmbg);

            cities = cityController.ViewCities();
            txtCity.DataContext = cities;

            InitializeWindowInformation();
       
        }

        private void InitializeWindowInformation()
        {
            txtJmbg.Text = secretary.Jmbg;
            txtName.Text = secretary.Name;
            txtSurname.Text = secretary.Surname;
            dpDateOfBirth.SelectedDate = secretary.DateOfBirth;
            if (secretary.Gender == GenderType.Z)
            {
                female.IsSelected = true;
            }
            else if (secretary.Gender == GenderType.M)
            {
                male.IsSelected = true;
            }
            txtStreet.Text = secretary.HomeAddress;

            if (secretary.City != null)
            {
                City selectedCity = cityController.getCityByZipCode(secretary.City.ZipCode);
                for (int i = 0; i < cities.Count; i++)
                {
                    if (selectedCity.ZipCode == cities[i].ZipCode)
                    {
                        txtCity.SelectedItem = cities[i];
                    }
                }
            }

            txtPhone.Text = secretary.Phone;
            txtEmail.Text = secretary.Email;
            txtUsername.Text = secretary.Username;
            pbPassword.Password = secretary.Password;
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/close.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Zatvaranje prozora";
            mb.textMsgBox.Text = "Da li ste sigurni da želite odustati od izmjene profila?";
            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void btnChangePass_Click(object sender, RoutedEventArgs e)
        {
            var cp = new ChangePassword();
            cp.ShowDialog();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]{13}$");
            Match match = regex.Match(txtJmbg.Text);
            Regex regex1 = new Regex(@"^[0-9]+$");
            Match match1 = regex1.Match(txtPhone.Text);
            Regex regexEmail = new Regex(@"^[a-z0-9\.\-_]{4,20}[@]{1}[a-z.]{4,10}");
            Match match3 = regexEmail.Match(txtEmail.Text);

            if (txtName.Text.Equals("") || txtSurname.Text.Equals("") || txtJmbg.Text.Equals("") || cmbGender.SelectedItem == null || dpDateOfBirth.SelectedDate == null)
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
            else if (!txtEmail.Text.Equals("") && !match3.Success)
            {
                var okMbx = new OKMessageBox(this, 2);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Pogrešan format za email!";
                okMbx.ShowDialog();
            }
            else
            {
                DateTime date = (DateTime)dpDateOfBirth.SelectedDate;
                GenderType gender;
                City city;

                if(txtCity.SelectedItem != null)
                {
                    city = (City)txtCity.SelectedItem;
                }
                else
                {
                    city = new City();
                }

                if (cmbGender.Text.Equals("M"))
                {
                    gender = GenderType.M;
                }
                else
                {
                    gender = GenderType.Z;
                }
                
                Secretary s = new Secretary(txtJmbg.Text, txtName.Text, txtSurname.Text, date, gender, city, txtStreet.Text, txtPhone.Text, txtEmail.Text,
                                            LogIn.secretary.Username,LogIn.secretary.Password,LogIn.secretary.Qualifications,LogIn.secretary.DateOfEmployment);

                Secretary changedSecretary = (Secretary)userController.EditProfile(s);

                if (changedSecretary != null)
                {
                    LogIn.secretary = changedSecretary;

                    var okMb = new OKMessageBox(this, 3);
                    okMb.titleMsgBox.Text = "Obavještenje";
                    okMb.textMsgBox.Text = "Uspješno ste izmijenili svoje informacije.";
                    okMb.ShowDialog();
                }
                else
                {
                    var okMbx = new OKMessageBox(this, 2);
                    okMbx.titleMsgBox.Text = "Greška";
                    okMbx.textMsgBox.Text = "Došlo je do greške, provjerite da li je dobar format podataka!";
                    okMbx.ShowDialog();
                }
            }
        }
    }
}
