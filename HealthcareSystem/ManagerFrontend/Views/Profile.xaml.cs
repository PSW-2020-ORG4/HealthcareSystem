using Controller.UsersAndWorkingTime;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic_Health.Views
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
		private static ManagerController mc = new ManagerController();
		private UserController uc = new UserController(mc);
		public Profile()
        {
            InitializeComponent();
        }
		private void buttonSacuvajProfil_Click(object sender, RoutedEventArgs e)
		{

			Manager manager = new Manager();
			manager.Jmbg = JmbgTb.Text as string;
			manager.Name = ImeTb.Text as string;
			manager.DateOfBirth = (DateTime)DatumRTb.SelectedDate;
			manager.DateOfEmployment = (DateTime)DatumZTb.SelectedDate;
			manager.Email = EmailTb.Text as string;
			manager.Phone = TelTb.Text as string;
			manager.HomeAddress = AdresaTb.Text as string;
			manager.Surname = PrezimeTb.Text as string;
			manager.Username = KorisnickoTb.Text as string;
			manager.Password = SifraTb.Text as string;
			manager.Qualifications = (TypeOfQualifications)TipCb.SelectedItem;
			manager.Gender = (GenderType)PolCb.SelectedItem;
			int grad = Convert.ToInt32(GradTb.Text);

			CityController cityController = new CityController();
			City city = cityController.getCityByZipCode(grad);
			manager.City = city;
			
			uc.EditProfile(manager);
			
		}
		}
	}
