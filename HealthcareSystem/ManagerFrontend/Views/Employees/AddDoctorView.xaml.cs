using Controller.ExaminationAndPatientCard;
using Controller.RoomAndEquipment;
using Controller.UsersAndWorkingTime;
using Model.Enums;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic_Health.Views.Employees
{
	/// <summary>
	/// Interaction logic for AddDoctorView.xaml
	/// </summary>
	public partial class AddDoctorView : UserControl
	{
		private static DoctorController dc = new DoctorController();
		private UserController uc = new UserController(dc);
		public AddDoctorView()
		{
			InitializeComponent();
		}

		private void ButtonSacuvajLekara_Click(object sender, RoutedEventArgs e)
		{

			Doctor doctor = new Doctor();
			doctor.Jmbg= JmbgTb.Text as string;
			doctor.Name= ImeTb.Text as string;
			doctor.DateOfBirth= (DateTime)DatumRTb.SelectedDate;
			doctor.DateOfEmployment= (DateTime)DatumZTb.SelectedDate;
			doctor.Email= EmailTb.Text as string;
			doctor.Phone= TelTb.Text as string;
			doctor.HomeAddress= AdresaTb.Text as string;
			doctor.Surname = PrezimeTb.Text as string;
			doctor.Username= KorisnikTb.Text as string;
			doctor.Password= SifraTb.Text as string;
			doctor.NumberOfLicence= LicencaTb.Text as string;
			doctor.Type= (TypeOfDoctor)TipTb.SelectedItem;
			doctor.Gender = (GenderType)PolCb.SelectedItem;
			int soba= Convert.ToInt32(SobaTb.Text);
			int grad = Convert.ToInt32(GradTb.Text);
			RoomController roomController = new RoomController();
			Room room=roomController.ViewRoomByNumber(soba);
			doctor.DoctorsOffice = room;

			CityController cityController = new CityController();
			City city = cityController.getCityByZipCode(grad);
			doctor.City = city;

			Doctor s = new Doctor();
				s=(Doctor)uc.ViewProfile(doctor.Jmbg);
			if (s==null) {

				uc.Register(doctor);
				Poruka.Text = "Uspesno ste sacuvali doktora!";
			} else {
				uc.EditProfile(doctor);
				ExaminationController ec = new ExaminationController();
				ec.DeleteDoctorExaminations(doctor.Jmbg);
				WorkingTimeController wtc = new WorkingTimeController();
				WorkingTime novo = new WorkingTime();
				novo= wtc.viewWorkingTimeDoctor(doctor.Jmbg);
				wtc.EditWorkingTime(novo);
				Poruka.Text = "Uspesno ste izmenili doktora!";
			}
			
			
			/* VALIDACIJA
			// 1. Resetovati sve validacione poruke
			ResetAllErrors();

			// 2. Izvrsiti validaciju
			bool isValid = true;

			if (String.IsNullOrWhiteSpace(ImeTb.Text))
			{
				ErrorImeTb.Text = "Morate uneti ime";
				ImeTb.BorderBrush = Brushes.Red;
				isValid = false;
			}
			//
			// Ostatak validacije
			//
			Poruka.Text = "Uspesno ste sacuvali lekara!";


			// 3. Reagovati na rezultat validacije
			if (isValid)
			{
				// Dodaj lekara
				// SuccessMessageTb.Text = "Uspesno ste dodali lekara.";
				// Resetovati sve textboxove, comboboxove, datepickere.
				// Za sve TB stavis .Text = ""
				// Za DatePicker.SelectedDate stavis DateTime.Now
				// Za CB stavis .SelectedItem = ListZaCB[0]
			}*/
		}

		private void ResetAllErrors()
		{
			// SuccessMessageTb.Text = "";
			ErrorImeTb.Text = "";
			ImeTb.BorderBrush = Brushes.Black;
		}
	}
}
