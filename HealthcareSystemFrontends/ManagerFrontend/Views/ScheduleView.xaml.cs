using Clinic_Health.Model;
using Controller.UsersAndWorkingTime;
using Model.Manager;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : UserControl
    {
		public ObservableCollection<Raspored> Raspored { get; set; }
		public ObservableCollection<Raspored> TempRaspored { get; set; }
		public ObservableCollection<string> TipSmene
		{
			get;
			set;
		}
		
		
		private Raspored _selektovanRaspored;
		public Raspored SelektovanRaspored
		{
			get { return _selektovanRaspored; }
			set
			{
				_selektovanRaspored = value;
				OnPropertyChanged("SelektovanRaspored");
			}
		}
		private DateTime? selectedKriterijum;
		public DateTime? SelectedKriterijum
		{
			get { return selectedKriterijum; }
			set
			{
				if (selectedKriterijum != value)
				{
					selectedKriterijum = value;
					OnPropertyChanged(nameof(SelectedKriterijum));
				}
			}
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		private string pretraga;
		public string Pretraga
		{
			get { return pretraga; }
			set
			{
				if (pretraga != value)
				{
					pretraga = value;
					OnPropertyChanged(nameof(Pretraga));
				}
			}
		}
		private void izaberiTip()
		{

			TipSmene = new ObservableCollection<string>();
			TipSmene.Add("Prva");
			TipSmene.Add("Druga");
			TipSmene.Add("Treca");
			
		}
		public ScheduleView()
        {
            InitializeComponent();
			this.DataContext = this;
			this.izaberiTip();
			JmbgTb.IsEnabled = true;
			Raspored = new ObservableCollection<Raspored>();
			TempRaspored = new ObservableCollection<Raspored>();

			WorkingTimeController workingTimeController = new WorkingTimeController();
			List<WorkingTime> times = workingTimeController.ViewWorkingTimes();
			if (times!=null) {
				foreach (WorkingTime w in times) {
					Raspored.Add(new Raspored() { Jmbg = w.doctor.Jmbg, DatumP=w.StartDate, DatumK=w.EndDate,Ime= w.doctor.Name,Prezime=w.doctor.Surname,Smena=w.WorkShift });
				}
			}
			//Raspored.Add(new Raspored() { Ime = "jelena", Prezime = "budisa", Jmbg = "1234567", Datum = Convert.ToDateTime("12/1/2020") });
			//Raspored.Add(new Raspored() { Ime = "jelena", Prezime = "budisa", Jmbg = "1234567",Smena="Prva", Datum = Convert.ToDateTime("1/10/2020") });
			//Raspored.Add(new Raspored() { Ime = "jelena", Prezime = "budisa", Jmbg = "1234567", Datum = Convert.ToDateTime("1/12/2020") });


			foreach (var r in Raspored)
			{
				TempRaspored.Add(r);
			}
			
			SelektovanRaspored = null;
		}

        private void buttonTraziRaspored_Click(object sender, RoutedEventArgs e)
        {
			TempRaspored.Clear();
			foreach (var r in Raspored)
			{
				if (r.DatumP.Equals(SelectedKriterijum)) {

					
						TempRaspored.Add(r);
					
				}
					
				}
			}
		

        private void buttonObrisiRaspored_Click(object sender, RoutedEventArgs e)
        {
			if (SelektovanRaspored == null || string.IsNullOrWhiteSpace(SelektovanRaspored.Jmbg))
			{
				return;
			}
			foreach (Raspored r in TempRaspored)
			{
				if (r.Jmbg == SelektovanRaspored.Jmbg)
				{

					WorkingTimeController workingTimeController = new WorkingTimeController();
					workingTimeController.DeleteWorkingTime(r.Jmbg);
					TempRaspored.Remove(r);
					Raspored.Remove(r);
					SelektovanRaspored = null;
					ClearFields();
					break;
				}
			}
		}

        private void buttonSacuvajRaspored_Click(object sender, RoutedEventArgs e)
        {
			bool isAdd = false;
			if (SelektovanRaspored == null || string.IsNullOrWhiteSpace(SelektovanRaspored.Jmbg))
			{
				isAdd = true;
			}

			// ovaj kod se izvrsi nakon validacije

			if (isAdd)
			{
				AddRaspored();
			}
			else
			{
				UpdateRaspored();
			}
		}
		private void AddRaspored()
		{
			Raspored r = new Raspored();
			//r.Ime = ImeTb.Text as string;
			//r.Prezime = PrezimeTb.Text as string;
			r.Jmbg = JmbgTb.Text as string;
			r.Smena = (WorkShifts)TipCb.SelectedItem;
			//r.Sala = SalaTb.Text as string;
			r.DatumP = (DateTime)DatumPTb.SelectedDate;
			r.DatumK = (DateTime)DatumKTb.SelectedDate;
			Raspored.Add(r);

			WorkingTimeController workingTimeController = new WorkingTimeController();
			WorkingTime wt = new WorkingTime();
			DoctorController dc = new DoctorController();
			UserController uc = new UserController(dc);
			Doctor doctor = (Doctor)uc.ViewProfile(r.Jmbg);
			if (doctor==null) {
				Poruka.Text = "Uneli ste nepostojeci jmbg!";
			} 
			else {
				wt.doctor = doctor;
				wt.WorkShift = r.Smena;
				wt.StartDate = r.DatumP;
				wt.EndDate = r.DatumK;
				workingTimeController.DefineWorkingTime(wt);
			}
			
			// Resetujemo polja
			ClearFields();

			UpdateTempRaspored();
		}
		private void UpdateRaspored()
		{
			SelektovanRaspored.Jmbg = JmbgTb.Text as string;
			//SelektovanRaspored.Ime= ImeTb.Text as string;
			//SelektovanRaspored.Prezime= PrezimeTb.Text as string;
			SelektovanRaspored.Smena= (WorkShifts)TipCb.SelectedItem;
			//SelektovanRaspored.Sala= SalaTb.Text as string;
			SelektovanRaspored.DatumP= (DateTime)DatumPTb.SelectedDate;
			SelektovanRaspored.DatumK = (DateTime)DatumKTb.SelectedDate;
			WorkingTimeController workingTimeController = new WorkingTimeController();
			WorkingTime wt = new WorkingTime();
			DoctorController dc = new DoctorController();
			UserController uc = new UserController(dc);
			Doctor doctor = (Doctor)uc.ViewProfile(SelektovanRaspored.Jmbg);
			wt.doctor = doctor;
			wt.WorkShift = SelektovanRaspored.Smena;
			wt.StartDate = SelektovanRaspored.DatumP;
			wt.EndDate = SelektovanRaspored.DatumK;
			workingTimeController.EditWorkingTime(wt);
		}
		private void ClearFields()
		{
			JmbgTb.Text = "";
			JmbgTb.IsEnabled = true;
			//ImeTb.Text = "";
			//PrezimeTb.Text = "";
			TipCb.SelectedIndex = -1;
			DatumPTb.SelectedDate = DateTime.Today;
			DatumKTb.SelectedDate = DateTime.Today;
		
			//SalaTb.Text = "";
		}
		private void UpdateTempRaspored()
		{
			Raspored.Clear();
			TempRaspored.Clear();
			WorkingTimeController workingTimeController = new WorkingTimeController();
			List<WorkingTime> times = workingTimeController.ViewWorkingTimes();
			if (times != null)
			{
				foreach (WorkingTime w in times)
				{
					Raspored.Add(new Raspored() { Jmbg = w.doctor.Jmbg, DatumP = w.StartDate, DatumK = w.EndDate, Ime = w.doctor.Name, Prezime = w.doctor.Surname, Smena = w.WorkShift });
				}
			}
			foreach (var r in Raspored)
			{
				TempRaspored.Add(r);
			}
		}
		private void DataGridSoba_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SelektovanRaspored != null)
			{
				 JmbgTb.Text  =SelektovanRaspored.Jmbg;
				JmbgTb.IsEnabled = false;
				//ImeTb.Text  =SelektovanRaspored.Ime;
				//PrezimeTb.Text= SelektovanRaspored.Prezime;
				TipCb.SelectedItem = SelektovanRaspored.Smena;
				// SalaTb.Text = SelektovanRaspored.Sala;
				 DatumPTb.SelectedDate = SelektovanRaspored.DatumP;
				DatumKTb.SelectedDate = SelektovanRaspored.DatumK;
				Poruka.Text = "";
			}
		}
		private void Otkazi_Click(object sender, RoutedEventArgs e)
		{
			ClearFields();
			SelektovanRaspored = null;
		}

		
	}
}
