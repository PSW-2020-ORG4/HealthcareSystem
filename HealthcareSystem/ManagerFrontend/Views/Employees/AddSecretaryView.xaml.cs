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

namespace Clinic_Health.Views.Employees
{
	/// <summary>
	/// Interaction logic for AddSecretaryView.xaml
	/// </summary>
	public partial class AddSecretaryView : UserControl
	{
	

		private static SecretaryController sc = new SecretaryController();
		private UserController uc = new UserController(sc);
		public AddSecretaryView()
		{
			InitializeComponent();
		}

		private void buttonSacuvajSekretara_Click(object sender, RoutedEventArgs e)
		{
			Secretary secretar = new Secretary();
			secretar.Jmbg = JmbgTb.Text as string;
			secretar.Name = ImeTb.Text as string;
			secretar.DateOfBirth = (DateTime)DatumRTb.SelectedDate;
			secretar.DateOfEmployment = (DateTime)DatumZTb.SelectedDate;
			secretar.Email = EmailTb.Text as string;
			secretar.Phone = TelTb.Text as string;
			secretar.HomeAddress = AdresaTb.Text as string;
			secretar.Surname = PrezimeTb.Text as string;
			secretar.Username = KorisnickoTb.Text as string;
			secretar.Password = SifraTb.Text as string;
			secretar.Qualifications = (TypeOfQualifications)TipCb.SelectedItem;
			secretar.Gender = (GenderType)PolCb.SelectedItem;
			int grad = Convert.ToInt32(GradTb.Text);

			CityController cityController = new CityController();
			City city = cityController.getCityByZipCode(grad);
			secretar.City = city;

			Secretary s = new Secretary();
			s = (Secretary)uc.ViewProfile(secretar.Jmbg);
			if (s==null)
			{
				uc.Register(secretar);
				Poruka.Text = "Uspesno ste sacuvali sekretara!";
			}
			else
			{
				uc.EditProfile(secretar);
				Poruka.Text = "Uspesno ste izmenili sekretara!";
			}

		}
	}
}
