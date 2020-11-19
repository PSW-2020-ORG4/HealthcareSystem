using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.PerformingExamination;
using Model.Manager;
using Model.Users;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Window
    {
       private Doctor doc;
       private static DoctorController dc = new DoctorController();
       private static UserController uc = new UserController(dc);
       private static CityController cc = new CityController();
       private ExaminationController ec = new ExaminationController();
        private WorkingTimeController wc = new WorkingTimeController();

        public EditProfile(Doctor d)
        {
            
            InitializeComponent();
            doc = d;     
            Doctor doctor = (Doctor)dc.ViewProfile(d.Jmbg);
            txtAdress.Text = doctor.HomeAddress;
            txtPhone.Text = doctor.Phone;
            txtEmail.Text = doctor.Email;

            List<string> specials = new List<string>();
            specials.Add(TypeOfDoctor.Dermatolog.ToString());
            specials.Add(TypeOfDoctor.drOpstePrakse.ToString());
            specials.Add(TypeOfDoctor.Endokrinolog.ToString());
            specials.Add(TypeOfDoctor.Ginekolog.ToString());
            specials.Add(TypeOfDoctor.KardioVaskularniHirurg.ToString());
            specials.Add(TypeOfDoctor.Neurolog.ToString());
            specials.Add(TypeOfDoctor.Oftamolog.ToString());
            specials.Add(TypeOfDoctor.Onkolog.ToString());
            specials.Add(TypeOfDoctor.Ortoped.ToString());
            specials.Add(TypeOfDoctor.Pulmolog.ToString());
            specials.Add(TypeOfDoctor.Radiolog.ToString());

            comboSpecial.DataContext = specials;   
            comboSpecial.SelectedItem = doctor.Type.ToString();

            List<City> cities = cc.ViewCities();
            List<string> citiesStr = new List<string>();
            foreach(City c in cities)
            {
                citiesStr.Add(c.Name);
            }

            comboCity.DataContext = citiesStr;
            comboCity.SelectedItem = doctor.City.Name;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (IsDigitsOnly(txtPhone.Text) == false)
            {
                MessageBox.Show("Broj telefona mora sadržati samo cifre.", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);
                
                txtPhone.Focus();
                return;
            }
            else if (!txtEmail.Text.Contains("@"))
            {
                
                    MessageBox.Show("E-mail mora biti u formatu xxx@yyy.com", "Upozorenje!",
                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                    txtEmail.Focus();
                    return;
                
            }else if (!txtEmail.Text.Contains(".com"))
            {
                MessageBox.Show("E-mail mora biti u formatu xxx@yyy.com", "Upozorenje!",
              MessageBoxButton.OK, MessageBoxImage.Warning);
               
                txtEmail.Focus();
                return;
            }  
 
            Doctor d1 = doc;
            d1.HomeAddress = txtAdress.Text;
            d1.Email = txtEmail.Text;
            d1.Phone = txtPhone.Text;
         
            if (comboSpecial.SelectedItem.ToString().Equals("Dermatolog"))
            {
                d1.Type = TypeOfDoctor.Dermatolog;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("drOpstePrakse"))
            {
                d1.Type = TypeOfDoctor.drOpstePrakse;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("Endokrinolog")) {
                d1.Type = TypeOfDoctor.Endokrinolog;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("Ginekolog"))
            {
                d1.Type = TypeOfDoctor.Ginekolog;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("KardioVaskularniHirurg"))
            {
                d1.Type = TypeOfDoctor.KardioVaskularniHirurg;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("Neurolog"))
            {
                d1.Type = TypeOfDoctor.Neurolog;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("Oftamolog"))
            {
                d1.Type = TypeOfDoctor.Oftamolog;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("Onkolog"))
            {
                d1.Type = TypeOfDoctor.Onkolog;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("Ortoped"))
            {
                d1.Type = TypeOfDoctor.Ortoped;
            }
            else if (comboSpecial.SelectedItem.ToString().Equals("Pulmolog"))
            {
                d1.Type = TypeOfDoctor.Pulmolog;
            }
            else
            {
                d1.Type = TypeOfDoctor.Radiolog;
            }

            List<City> cities = cc.ViewCities();
            foreach(City c in cities)
            {
                if (comboCity.SelectedItem.ToString().Equals(c.Name))
                {
                    d1.City = c;
                }
            }

            uc.EditProfile(d1);

            List<Examination> examinations = ec.GetExaminationsByDoctor(doc.Jmbg);
            foreach (Examination exm in examinations)
            {
                exm.Doctor = d1;
                ec.UpdateExamination(exm);

            }

            WorkingTime work = wc.viewWorkingTimeDoctor(doc.Jmbg);
            work.doctor = d1;
            wc.EditWorkingTime(work);

            var s = new MessageEditProfile();
            s.Show();

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((txtPass1.Password.Equals(txtPass2.Password) == false))
            {
                MessageBox.Show("Lozinke se ne poklapaju.", "Upozorenje!",
              MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPass2.Clear();
                txtPass2.Focus();
                return;
            } 
     
            Doctor d1 = doc;    
            d1.Password = txtPass2.Password;
            uc.EditProfile(d1);

            List<Examination> examinations = ec.GetExaminationsByDoctor(doc.Jmbg);
            foreach (Examination exm in examinations)
            {
                exm.Doctor = d1;
                ec.UpdateExamination(exm);

            }

            WorkingTime work = wc.viewWorkingTimeDoctor(doc.Jmbg);
            work.doctor = d1;
            wc.EditWorkingTime(work);

            if (txtPass1.Password.Length < 8)
            {
                MessageBox.Show("Lozinke se ne mora sadrzati bar 8 karaktera.", "Upozorenje!",
            MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPass2.Clear();
                txtPass2.Focus();
                txtPass1.Clear();
                return;
            }
            var s = new MessageEditPassword();
            s.Show();

            this.Close();
        }
    }
}
