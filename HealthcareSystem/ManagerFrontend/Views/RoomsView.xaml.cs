using Clinic_Health.Model;
using Controller.RoomAndEquipment;
using Model.Manager;
using Controller.PlacementInARoomAndRenovationPeriod;
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
using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
using Model.Users;

namespace Clinic_Health.Views
{
	/// <summary>
	/// Interaction logic for RoomsView.xaml
	/// </summary>
	public partial class RoomsView : UserControl
	{

		public ObservableCollection<Soba> Sobe { get; set; }
		public ObservableCollection<Soba> TempSobe { get; set; }
		public ObservableCollection<string> TipSale
		{
			get;
			set;
		}
		public ObservableCollection<string> Kriterijumi { get; set; }
		private string selectedKriterijum;
		public string SelectedKriterijum
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
		private Soba _selektovanaSoba;
		public Soba SelektovanaSoba
		{
			get { return _selektovanaSoba; }
			set
			{
				_selektovanaSoba = value;
				OnPropertyChanged("SelektovanaSoba");
			}
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
		#region Hintovi
		private string tipHint;
		public string TipHint
		{
			get { return tipHint; }
			set
			{
				if (tipHint != value)
				{
					tipHint = value;
					OnPropertyChanged(nameof(TipHint));
				}
			}
		}
		private string sifraHint;
		public string SifraHint
		{
			get { return sifraHint; }
			set
			{
				if (sifraHint != value)
				{
					sifraHint = value;
					OnPropertyChanged(nameof(SifraHint));
				}
			}
		}

		private string kapacitetHint;
		public string KapacitetHint
		{
			get { return kapacitetHint; }
			set
			{
				if (kapacitetHint != value)
				{
					kapacitetHint = value;
					OnPropertyChanged(nameof(KapacitetHint));
				}
			}
		}
		private string zauzetostHint;
		public string ZauzetostHint
		{
			get { return zauzetostHint; }
			set
			{
				if (zauzetostHint != value)
				{
					zauzetostHint = value;
					OnPropertyChanged(nameof(ZauzetostHint));
				}
			}
		}
		private string pocetakHint;
		public string PocetakHint
		{
			get { return pocetakHint; }
			set
			{
				if (pocetakHint != value)
				{
					pocetakHint = value;
					OnPropertyChanged(nameof(PocetakHint));
				}
			}
		}
		private string krajHint;
		public string KrajHint
		{
			get { return krajHint; }
			set
			{
				if (krajHint != value)
				{
					krajHint = value;
					OnPropertyChanged(nameof(KrajHint));
				}
			}
		}
		private string pretragaHint;
		public string PretragaHint
		{
			get { return pretragaHint; }
			set
			{
				if (pretragaHint != value)
				{
					pretragaHint = value;
					OnPropertyChanged(nameof(PretragaHint));
				}
			}
		}
		#endregion

		public RoomsView()
		{
			InitializeComponent();
			this.DataContext = this;
			this.izaberiTip();
			Sobe = new ObservableCollection<Soba>();
			TempSobe = new ObservableCollection<Soba>();
			Kriterijumi = new ObservableCollection<string>();
			Kriterijumi.Add("Sifra");
			SifraTb.IsEnabled = true;

			RoomController roomController = new RoomController();
			List<Room> rooms = roomController.ViewRooms();
			RenovationPeriodController renovationPeriodController = new RenovationPeriodController();
			List<RenovationPeriod> periods = renovationPeriodController.ViewRenovations();
			if (rooms != null)
			{
				foreach (Room r in rooms)
				{
					if (periods != null)
					{
						foreach (RenovationPeriod p in periods)
						{
							if (r.Id == p.room.Number)
							{
								Sobe.Add(new Soba() { Sifra = r.Id, Kapacitet = r.Capacity, Zauzetost = r.Occupation, Tip = r.Usage, Pocetak = p.BeginDate, Kraj = p.EndDate });
								r.Renovation = true;
							}
						}
					}
					if(r.Renovation==false)
						Sobe.Add(new Soba() { Sifra = r.Id, Kapacitet = r.Capacity, Zauzetost = r.Occupation, Tip = r.Usage});

				}
			}
			
			
			//Sobe.Add(new Soba() {  Kapacitet = "3", Sifra = "djna" , Tip="Operaciona sala",Zauzetost="0", Pocetak = Convert.ToDateTime("1/12/2020"), Kraj = Convert.ToDateTime("1/12/2020") });
			//Sobe.Add(new Soba() {  Kapacitet = "30", Sifra = "aadjna",Pocetak=Convert.ToDateTime("1/12/2020"),Kraj= Convert.ToDateTime("1/12/2020") });
			//Sobe.Add(new Soba() {  Kapacitet = "40", Sifra = "jdjna", Pocetak = Convert.ToDateTime("1/12/2020"), Kraj = Convert.ToDateTime("1/12/2020") });
			//Sobe.Add(new Soba() {  Kapacitet = "4000", Sifra = "ccdjna", Pocetak = Convert.ToDateTime(null), Kraj = Convert.ToDateTime(null) });
			//Sobe.Add(new Soba() {  Kapacitet = "50", Sifra = "aadjna", Pocetak = Convert.ToDateTime(null), Kraj = Convert.ToDateTime(null) });
			//Sobe.Add(new Soba() { Kapacitet = "3", Sifra = "djakda", Tip = "Operaciona sala", Zauzetost = "0", Pocetak = Convert.ToDateTime("1/12/2020"), Kraj = Convert.ToDateTime("1/12/2020") });
			//Sobe.Add(new Soba() { Kapacitet = "3", Sifra = "123", Tip = "Operaciona sala", Zauzetost = "0", Pocetak = Convert.ToDateTime("1/12/2020"), Kraj = Convert.ToDateTime("1/12/2020") });
			//Sobe.Add(new Soba() { Kapacitet = "3", Sifra = "987", Tip = "Operaciona sala", Zauzetost = "0", Pocetak = Convert.ToDateTime("1/12/2020"), Kraj = Convert.ToDateTime("1/12/2020") });
			//Sobe.Add(new Soba() { Kapacitet = "3", Sifra = "23456789", Tip = "Operaciona sala", Zauzetost = "0", Pocetak = Convert.ToDateTime("1/12/2020"), Kraj = Convert.ToDateTime("1/12/2020") });


			foreach (var soba in Sobe)
			{
				TempSobe.Add(soba);
			}
			SelectedKriterijum = Kriterijumi[0];
			SifraHint = "Unesi broj";
			PocetakHint = "Izaberi datum";
			KrajHint = "Izaberi datum";
			TipHint = "Unesi tekst";
			KapacitetHint = "Unesi broj";
			ZauzetostHint = "Unesi broj";
			PretragaHint = "Unesi tekst za pretragu";
			SelektovanaSoba = null;
		}


		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void izaberiTip()
		{

			TipSale = new ObservableCollection<string>();
			TipSale.Add("Operaciona sala");
			TipSale.Add("Magacin");
			TipSale.Add("Soba za pregled");
			TipSale.Add("Soba za lezanje");
		}

		private void buttonObrisiSalu_Click(object sender, RoutedEventArgs e)
		{
			if (SelektovanaSoba == null || string.IsNullOrWhiteSpace(SelektovanaSoba.Sifra.ToString()))
			{
				return;
			}
			foreach (Soba soba in TempSobe)
			{
				if (soba.Sifra == SelektovanaSoba.Sifra)
				{
					RoomController roomController = new RoomController();
					RenovationPeriodController renovationPeriodController = new RenovationPeriodController();
					renovationPeriodController.CancelRenovation(soba.Sifra);
					roomController.DeleteRoom(soba.Sifra);
					//EquipmentInRoomsController ec = new EquipmentInRoomsController();
					//ec.deleteLeftEquipmentInRooms(soba.Sifra);
					ExaminationController exc = new ExaminationController();
					exc.DeleteRoomExaminations(soba.Sifra);
					PlacementInSickRoomController pc = new PlacementInSickRoomController();
					pc.DeleteRoomPlacements(soba.Sifra);
					DoctorController dc = new DoctorController();
					UserController uc = new UserController(dc);
					List<User> lista = uc.ViewAllUsers();
					foreach (Doctor d in lista) {
						Doctor n=(Doctor)d;
						n.DoctorsOffice = new Room();
						List<Room>rooms=roomController.ViewRooms();
						n.DoctorsOffice=rooms.First();
					}
					SelektovanaSoba = null;
					ClearFields();
					UpdateTempSoba();

					break;
				}
			}
		}

		private void buttonTraziSalu_Click(object sender, RoutedEventArgs e)
		{
			TempSobe.Clear();
			foreach (var soba in Sobe)
			{
				switch (SelectedKriterijum)
				{
					case "Sifra":
						//if (soba.Sifra.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
						//{
						TempSobe.Add(soba);
						//}
						break;
					default:
						TempSobe.Add(soba);
						break;
				}
			}
		}

		private void buttonSacuvajSalu_Click(object sender, RoutedEventArgs e)
		{
			bool isAdd = false;
			if (SelektovanaSoba == null || string.IsNullOrWhiteSpace(SelektovanaSoba.Sifra.ToString()))
			{
				isAdd = true;
			}

			// ovaj kod se izvrsi nakon validacije

			if (isAdd)
			{
				AddSoba();
			}
			else
			{
				UpdateSoba();
			}
		}
		private void AddSoba()
		{
			Soba novaSoba = new Soba();
			novaSoba.Sifra = Convert.ToInt32(SifraTb.Text);
			novaSoba.Kapacitet = Convert.ToInt32(KapacitetTb.Text);
			novaSoba.Zauzetost = Convert.ToInt32(ZauzetostTb.Text);
			novaSoba.Tip = (TypeOfUsage) TipCb.SelectedItem  ;
			novaSoba.Pocetak = (DateTime)PocetakTb.SelectedDate;
			novaSoba.Kraj = (DateTime)KrajTb.SelectedDate;
			Sobe.Add(novaSoba);
			RoomController roomController = new RoomController();
			RenovationPeriodController renovationPeriodController = new RenovationPeriodController();
			RenovationPeriod p = new RenovationPeriod();
			Room room = new Room();
			room.Capacity = novaSoba.Kapacitet;
			room.Id = novaSoba.Sifra;
			room.Occupation = novaSoba.Zauzetost;
			room.Usage = novaSoba.Tip;
			if (novaSoba.Pocetak!= DateTime.Today && novaSoba.Kraj != DateTime.Today) {
				
					room.Renovation = true;
					p.room = room;
					p.BeginDate = novaSoba.Pocetak;
					p.EndDate = novaSoba.Kraj;
					renovationPeriodController.ScheduleRenovation(p);
				
			}
			
			roomController.AddRoom(room);
			// Resetujemo polja
			ClearFields();

			UpdateTempSoba();
		}
		private void UpdateSoba()
		{
			SelektovanaSoba.Sifra = Convert.ToInt32(SifraTb.Text);
			SelektovanaSoba.Kapacitet = Convert.ToInt32(KapacitetTb.Text);
			SelektovanaSoba.Zauzetost = Convert.ToInt32(ZauzetostTb.Text);
			SelektovanaSoba.Pocetak= (DateTime)PocetakTb.SelectedDate;
			SelektovanaSoba.Tip =(TypeOfUsage) TipCb.SelectedItem;
			SelektovanaSoba.Kraj= (DateTime)KrajTb.SelectedDate;
			RoomController roomController = new RoomController();
			RenovationPeriodController renovationPeriodController = new RenovationPeriodController();
			RenovationPeriod p = new RenovationPeriod();
			Room room = new Room();
			room.Capacity = SelektovanaSoba.Kapacitet;
			room.Id = SelektovanaSoba.Sifra;
			room.Occupation = SelektovanaSoba.Zauzetost;
			if (SelektovanaSoba.Pocetak == DateTime.Today && SelektovanaSoba.Kraj == DateTime.Today)
			{
				room.Renovation = false;
				renovationPeriodController.CancelRenovation(room.Id);

			}
			else if (SelektovanaSoba.Pocetak == DateTime.MinValue && SelektovanaSoba.Kraj == DateTime.MinValue) {
				room.Renovation = false;
				renovationPeriodController.CancelRenovation(room.Id);
			}
			else {
				if (renovationPeriodController.ViewRenovationByRoomNumber(room.Id) != null)
				{
					room.Renovation = true;
					p.room = room;
					p.BeginDate = SelektovanaSoba.Pocetak;
					p.EndDate = SelektovanaSoba.Kraj;
					renovationPeriodController.EditRenovation(p);
				}
				else {
					room.Renovation = true;
					p.room = room;
					p.BeginDate = SelektovanaSoba.Pocetak;
					p.EndDate = SelektovanaSoba.Kraj;
					renovationPeriodController.ScheduleRenovation(p);
				}
			}
			
			roomController.EditRoom(room);
		}

		private void ClearFields()
		{
			SifraTb.Text = "";
			SifraTb.IsEnabled = true;
			KapacitetTb.Text = "";
			ZauzetostTb.Text = "";
			TipCb.SelectedIndex = -1;
			KrajTb.SelectedDate = DateTime.Today;
			PocetakTb.SelectedDate = DateTime.Today;
		}
		private void UpdateTempSoba()
		{
			Sobe.Clear();
			TempSobe.Clear();
			RoomController roomController = new RoomController();
			RenovationPeriodController renovationPeriodController = new RenovationPeriodController();
			List<Room> rooms = roomController.ViewRooms();
			List<RenovationPeriod> periods = renovationPeriodController.ViewRenovations();
			if (rooms != null)
			{
				foreach (Room r in rooms)
				{
					if (periods != null)
					{
						foreach (RenovationPeriod p in periods)
						{
							if (r.Id == p.room.Number)
							{
								Sobe.Add(new Soba() { Sifra = r.Id, Kapacitet = r.Capacity, Zauzetost = r.Occupation, Tip = r.Usage, Pocetak = p.BeginDate, Kraj = p.EndDate });
								r.Renovation = true;
							}
						}
					}
					if (r.Renovation == false)
						Sobe.Add(new Soba() { Sifra = r.Id, Kapacitet = r.Capacity, Zauzetost = r.Occupation, Tip = r.Usage });

				}
			}
			foreach (var soba in Sobe)
			{
				TempSobe.Add(soba);
			}
		}
		private void DataGridSoba_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SelektovanaSoba != null)
			{
				SifraTb.Text = SelektovanaSoba.Sifra.ToString();
				  KapacitetTb.Text = SelektovanaSoba.Kapacitet.ToString();
				ZauzetostTb.Text = SelektovanaSoba.Zauzetost.ToString();
				 PocetakTb.SelectedDate = SelektovanaSoba.Pocetak;
				 TipCb.SelectedItem = SelektovanaSoba.Tip;
				 KrajTb.SelectedDate = SelektovanaSoba.Kraj;
				SifraTb.IsEnabled = false;
			}
		}
		private void Otkazi_Click(object sender, RoutedEventArgs e)
		{
			ClearFields();
			SelektovanaSoba = null;
		}


	}
}


