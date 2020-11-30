using Clinic_Health.Model;
using Controller.RoomAndEquipment;
using Model.Manager;
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

namespace Clinic_Health.Views.Equipment
{
    /// <summary>
    /// Interaction logic for AllEquipmentView.xaml
    /// </summary>
    public partial class AllEquipmentView : UserControl
    {
		private static EquipmentController ec = new EquipmentController();
		private static EquipmentInRoomsController erc = new EquipmentInRoomsController();
		public ObservableCollection<Oprema> Oprema { get; set; }
		public ObservableCollection<Oprema> TempOprema { get; set; }
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
		private Oprema _selektovanaOprema;
		public Oprema SelektovanaOprema
		{
			get { return _selektovanaOprema; }
			set
			{
				_selektovanaOprema = value;
				OnPropertyChanged("SelektovanaOprema");
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
		private string nazivHint;
		public string NazivHint
		{
			get { return nazivHint; }
			set
			{
				if (nazivHint != value)
				{
					nazivHint = value;
					OnPropertyChanged(nameof(NazivHint));
				}
			}
		}
		private string kolicinaHint;
		public string KolicinaHint
		{
			get { return kolicinaHint; }
			set
			{
				if (kolicinaHint != value)
				{
					kolicinaHint = value;
					OnPropertyChanged(nameof(KolicinaHint));
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
		private string salaHint;
		public string SalaHint
		{
			get { return salaHint; }
			set
			{
				if (salaHint != value)
				{
					salaHint = value;
					OnPropertyChanged(nameof(SalaHint));
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

		public ObservableCollection<string> TipOpreme {
            get;
            set;
        }
		public ObservableCollection<string> VrstaOpreme
		{
			get;
			set;
		}
		public AllEquipmentView()
		{
			InitializeComponent();
			this.DataContext = this;
			this.izaberiTip();
			this.izaberiVrstu();
			SifraTb.IsEnabled = true;
			KolicinaTb.IsEnabled = true;
			Oprema = new ObservableCollection<Oprema>();
			TempOprema = new ObservableCollection<Oprema>();
			Kriterijumi = new ObservableCollection<string>();
			Kriterijumi.Add("Naziv");
			Kriterijumi.Add("Sifra");
			Kriterijumi.Add("Sala");

			List<ConsumableEquipment> cons = ec.ViewConsumableEquipment();

			if (cons != null)
			{
				foreach (ConsumableEquipment e in cons) {
					Oprema oprema = new Oprema();
					oprema.Kolicina = e.Quantity;
					oprema.Tip = TypeOfEquipment.CONSUMABLE;
					oprema.VrstaP = e.Type;
					oprema.Sifra = e.Id;
					oprema.Sala=erc.viewEquipmentInRooms(e.Id);

					Oprema.Add(oprema);
				}

			}
			List<NonConsumableEquipment> nCons = ec.ViewNonConsumableEquipment();

			if (nCons != null)
			{
				foreach (NonConsumableEquipment e in nCons)
				{
					Oprema oprema = new Oprema();;
					oprema.Kolicina = 0;
					oprema.Sala = erc.viewEquipmentInRooms(e.Id);
					oprema.Tip = TypeOfEquipment.NON_CONSUMABLE;
					oprema.VrstaN = e.Type;
					oprema.Sifra = e.Id;

					Oprema.Add(oprema);
				}

			}

			//Oprema.Add(new Oprema() {  Sifra = "djna", Tip = "Potrosna", Kolicina="2180", Vrsta="igla" });
			//Oprema.Add(new Oprema() {  Sifra = "aadjna" });
			//Oprema.Add(new Oprema() {  Sifra = "jdjna" });
			//Oprema.Add(new Oprema() {  Sifra = "ccdjna" });
			//Oprema.Add(new Oprema() {  Sifra = "aadjna" });
			//Oprema.Add(new Oprema() {  Sifra = "djakda", Tip = "Potrosna" });
			//Oprema.Add(new Oprema() {  Sifra = "123", Tip = "Potrosna" });
			//Oprema.Add(new Oprema() {  Sifra = "987", Tip = "Potrosna" });
			//Oprema.Add(new Oprema() {  Sifra = "23456789", Tip = "Potrosna" });


			foreach (var oprema in Oprema)
			{
				TempOprema.Add(oprema);
			}
			SelectedKriterijum = Kriterijumi[0];
			NazivHint = "Unesi tekst";
			KolicinaHint = "Unesi broj";
			SalaHint = "Unesi sifru sale";
			SifraHint = "Unesi broj";
			PretragaHint = "Unesi tekst za pretragu";
			SelektovanaOprema = null;
		}
		public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void izaberiTip() {

            TipOpreme = new ObservableCollection<string>();
            TipOpreme.Add("Potrosna");
            TipOpreme.Add("Nepotrosna");
        }
		private void izaberiVrstu()
		{
			VrstaOpreme = new ObservableCollection<string>();
			VrstaOpreme.Add("Igla");
			VrstaOpreme.Add("Krevet");
			VrstaOpreme.Add("Stolica");
			VrstaOpreme.Add("Sto");
			VrstaOpreme.Add("Racunar");
			VrstaOpreme.Add("Maska");
			VrstaOpreme.Add("Dezinfekciono sredstvo");
		}

		private void buttonObrisiOpremu_Click(object sender, RoutedEventArgs e)
        {
			if (SelektovanaOprema == null || string.IsNullOrWhiteSpace(SelektovanaOprema.Sifra.ToString()))
			{
				return;
			}
			foreach (Oprema oprema in TempOprema)
			{
				if (oprema.Sifra == SelektovanaOprema.Sifra)
				{
					if (!ec.DeleteConsumableEquipment(oprema.Sifra)) {
						ec.DeleteNonConsumableEquipment(oprema.Sifra);
					}
					erc.deleteEquipmentInRooms(oprema.Sifra);
					
					SelektovanaOprema = null;
					ClearFields();
					UpdateTempOprema();
					break;
				}
			}
		}

		private void ButtonSacuvajOpremu_Click(object sender, RoutedEventArgs e)
		{
			bool isAdd = false;
			if(SelektovanaOprema == null || string.IsNullOrWhiteSpace(SelektovanaOprema.Sifra.ToString()))
			{
				isAdd = true;
			}

			// ovaj kod se izvrsi nakon validacije

			if (isAdd)
			{
				AddOprema();
			}
			else
			{
				UpdateOprema();
			}
		}

		private void AddOprema()
		{
			Oprema novaOprema = new Oprema();
			novaOprema.Sifra = Convert.ToInt32(SifraTb.Text);
			novaOprema.Kolicina = Convert.ToInt32(KolicinaTb.Text);
			if (VrstaCb.SelectedItem != null)
			{
				novaOprema.VrstaP = (TypeOfConsumable)VrstaCb.SelectedItem;
			}
			if (VrstaNeCb.SelectedItem != null)
			{
				novaOprema.VrstaN = (TypeOfNonConsumable)VrstaNeCb.SelectedItem;
			}
			novaOprema.Tip = (TypeOfEquipment)TipCb.SelectedItem ;
			novaOprema.Sala = Convert.ToInt32(SalaTb.Text);

			Oprema.Add(novaOprema);
			if (novaOprema.Tip == TypeOfEquipment.CONSUMABLE)
			{
				ConsumableEquipment ce = new ConsumableEquipment();
				ce.Id = novaOprema.Sifra;
				ce.Quantity = novaOprema.Kolicina;
				ce.Type = novaOprema.VrstaP;
			
				ec.NewEquipment(novaOprema.Tip, ce);
			}
			if (novaOprema.Tip == TypeOfEquipment.NON_CONSUMABLE)
			{
				NonConsumableEquipment ne = new NonConsumableEquipment();
				ne.Id = novaOprema.Sifra;
				ne.Type = novaOprema.VrstaN;
				ec.NewEquipment(novaOprema.Tip,ne);
			}
			if (novaOprema.Sala!=0) {
				EquipmentInRooms er = new EquipmentInRooms();
				er.IdEquipment = novaOprema.Sifra;
				er.Quantity = novaOprema.Kolicina;
				er.RoomNumber = novaOprema.Sala;

				erc.addEquipmentInRoom(er);
			}
			
			// Resetujemo polja
			ClearFields();

			UpdateTempOprema();
		}

		private void UpdateOprema()
		{
			SelektovanaOprema.Sifra = Convert.ToInt32(SifraTb.Text);
			SelektovanaOprema.Kolicina = Convert.ToInt32(KolicinaTb.Text);
			if (VrstaCb.SelectedItem != null)
			{
				SelektovanaOprema.VrstaP = (TypeOfConsumable)VrstaCb.SelectedItem;

			}
			
			if (VrstaNeCb.SelectedItem!=null)
			{
				SelektovanaOprema.VrstaN = (TypeOfNonConsumable)VrstaNeCb.SelectedItem;

			}
			SelektovanaOprema.Tip = (TypeOfEquipment)TipCb.SelectedItem;
			SelektovanaOprema.Sala = Convert.ToInt32(SalaTb.Text);

			if (SelektovanaOprema.Tip == TypeOfEquipment.CONSUMABLE)
			{
				ConsumableEquipment ce = new ConsumableEquipment();
				ce.Id = SelektovanaOprema.Sifra;
				ce.Quantity = SelektovanaOprema.Kolicina;
				ce.Type = SelektovanaOprema.VrstaP;
				ec.EditConsumableEquipment(ce);
			}
			if (SelektovanaOprema.Tip == TypeOfEquipment.NON_CONSUMABLE)
			{
				NonConsumableEquipment ne = new NonConsumableEquipment();
				ne.Id = SelektovanaOprema.Sifra;
				ne.Type = SelektovanaOprema.VrstaN;
				ec.EditNonConsumableEquipment(ne);
			}
			if (SelektovanaOprema.Sala != 0)
			{
				if (erc.viewEquipmentInRooms(SelektovanaOprema.Sifra) != 0)
				{
					EquipmentInRooms er = new EquipmentInRooms();
					er.IdEquipment = SelektovanaOprema.Sifra;
					er.Quantity = SelektovanaOprema.Kolicina;
					er.RoomNumber = SelektovanaOprema.Sala;

					erc.editEquipmentInRooms(er);
				}
				else {
					EquipmentInRooms er = new EquipmentInRooms();
					er.IdEquipment = SelektovanaOprema.Sifra;
					er.Quantity = SelektovanaOprema.Kolicina;
					er.RoomNumber = SelektovanaOprema.Sala;

					erc.addEquipmentInRoom(er);
				}
			}
			if (SelektovanaOprema.Sala == 0)
			{
				EquipmentInRooms er = new EquipmentInRooms();
				er.IdEquipment = SelektovanaOprema.Sifra;
				er.Quantity = SelektovanaOprema.Kolicina;
				er.RoomNumber = SelektovanaOprema.Sala;
				erc.deleteEquipmentInRooms(er.IdEquipment);
			}

		}

		private void ClearFields()
		{
			SifraTb.Text = "";
			SifraTb.IsEnabled = true;
			KolicinaTb.IsEnabled=true;
			KolicinaTb.Text = "";
			VrstaCb.SelectedIndex= -1;
			VrstaNeCb.SelectedIndex = -1;
			TipCb.SelectedIndex = -1;
			SalaTb.Text = "";
		}

		private void UpdateTempOprema()
		{
			Oprema.Clear();
			TempOprema.Clear();
			List<ConsumableEquipment> cons = ec.ViewConsumableEquipment();

			if (cons != null)
			{
				foreach (ConsumableEquipment e in cons)
				{
					Oprema oprema = new Oprema();
					//oprema.Naziv = e.Name;
					oprema.Kolicina = e.Quantity;
					//oprema.Sala = e.Id;
					oprema.Tip = TypeOfEquipment.CONSUMABLE;
					oprema.VrstaP = e.Type;
					oprema.Sifra = e.Id;
					oprema.Sala = erc.viewEquipmentInRooms(e.Id);

					Oprema.Add(oprema);
				}

			}
			List<NonConsumableEquipment> nCons = ec.ViewNonConsumableEquipment();

			if (nCons != null)
			{
				foreach (NonConsumableEquipment e in nCons)
				{
					Oprema oprema = new Oprema();
					//oprema.Naziv = e.Name;
					//oprema.Kolicina = e.Quantity;
					//oprema.Sala = e.Id;
					oprema.Tip = TypeOfEquipment.NON_CONSUMABLE;
					oprema.VrstaN = e.Type;
					oprema.Sifra = e.Id;
					oprema.Sala = erc.viewEquipmentInRooms(e.Id);

					Oprema.Add(oprema);
				}

			}
			foreach (var oprema in Oprema)
			{
				TempOprema.Add(oprema);
			}			
		}

		private void DataGridOprema_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(SelektovanaOprema != null)
			{
				if (SelektovanaOprema.Tip==TypeOfEquipment.CONSUMABLE)
				{
					VrstaCb.SelectedItem = SelektovanaOprema.VrstaP;
					VrstaNeCb.SelectedIndex = -1;
					KolicinaTb.IsEnabled = true;
				}
				else {
					VrstaNeCb.SelectedItem = SelektovanaOprema.VrstaN;
					VrstaCb.SelectedIndex = -1;
					KolicinaTb.IsEnabled = false;
				}
				SifraTb.Text = SelektovanaOprema.Sifra.ToString();
				KolicinaTb.Text = SelektovanaOprema.Kolicina.ToString();
				TipCb.SelectedItem = SelektovanaOprema.Tip;
				SalaTb.Text = SelektovanaOprema.Sala.ToString();
				SifraTb.IsEnabled = false;
			}
		}

		private void Otkazi_Click(object sender, RoutedEventArgs e)
		{
			ClearFields();
			SelektovanaOprema = null;			
		}

		private void buttonTraziOpremu_Click(object sender, RoutedEventArgs e)
		{
			TempOprema.Clear();
			foreach (var oprema in Oprema)
			{
				switch (SelectedKriterijum)
				{
					case "Sifra":
						//if (oprema.Sifra.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
						//{
							TempOprema.Add(oprema);
						//}
						break;
					case "Sala":
						//if (oprema.Sifra.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
						//{
							TempOprema.Add(oprema);
						//}
						break;
					default:
						TempOprema.Add(oprema);
						break;
				}
			}
		}







		/*
			private void ButtonTraziLekove_Click(object sender, RoutedEventArgs e)
			{
				TempLekovi.Clear();
				foreach (var lek in Lekovi)
				{
					switch (SelectedKriterijum)
					{
						case "Naziv":
							if (lek.Naziv.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
							{
								TempLekovi.Add(lek);
							}
							break;
						case "Sifra":
							if (lek.Sifra.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
							{
								TempLekovi.Add(lek);
							}
							break;
						default:
							TempLekovi.Add(lek);
							break;
					}
				}
			}*/


	}
}
   