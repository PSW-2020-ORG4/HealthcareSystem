using System;
using System.Collections;
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
using Clinic_Health.Model;
using Controller.DrugAndTherapy;
using Model.PerformingExamination;
using Model.Manager;

namespace Clinic_Health.Views
{
    /// <summary>
    /// Interaction logic for DrugsView.xaml
    /// </summary>
    public partial class DrugsView : UserControl, INotifyPropertyChanged
    {
		private static DrugTypeAndIngridentController dt = new DrugTypeAndIngridentController();
		private static DrugController dc = new DrugController();
		private static TherapyController tc = new TherapyController();
		private List<string> newIngredients = new List<string>();
		

		public ObservableCollection<Lek> Lekovi { get; set; }
		public ObservableCollection<Lek> TempLekovi { get; set; }
		public ObservableCollection<string> Kriterijumi { get; set; }
		private string selectedKriterijum;
		public string SelectedKriterijum
		{
			get { return selectedKriterijum; }
			set
			{
				if(selectedKriterijum != value)
				{
					selectedKriterijum = value;
					OnPropertyChanged(nameof(SelectedKriterijum));
				}
			}
		}
        private Lek _selektovaniLek;
        public Lek SelektovaniLek
        {
            get { return _selektovaniLek; }
            set
			{
				_selektovaniLek = value;
                OnPropertyChanged("SelektovaniLek");
            }
        }
		private string pretraga;
		public string Pretraga
		{
			get { return pretraga; }
			set
			{
				if(pretraga != value)
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
				if(nazivHint != value)
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
		private string namenaHint;
		public string NamenaHint
		{
			get { return namenaHint; }
			set
			{
				if (namenaHint != value)
				{
					namenaHint = value;
					OnPropertyChanged(nameof(NamenaHint));
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

		private string sastavHint;
		public string SastavHint
		{
			get { return sastavHint; }
			set
			{
				if (sastavHint != value)
				{
					sastavHint = value;
					OnPropertyChanged(nameof(SastavHint));
				}
			}
		}
		private string rokHint;
		public string RokHint
		{
			get { return rokHint; }
			set
			{
				if (rokHint != value)
				{
					rokHint = value;
					OnPropertyChanged(nameof(RokHint));
				}
			}
		}
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
		private string proizvodjacHint;
		public string ProizvodjacHint
		{
			get { return proizvodjacHint; }
			set
			{
				if (proizvodjacHint != value)
				{
					proizvodjacHint = value;
					OnPropertyChanged(nameof(ProizvodjacHint));
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


		public DrugsView()
        {
            InitializeComponent();
            this.DataContext = this;
			Lekovi = new ObservableCollection<Lek>();
			TempLekovi = new ObservableCollection<Lek>();
			Kriterijumi = new ObservableCollection<string>();
			Kriterijumi.Add("Naziv");
			Kriterijumi.Add("Sifra");
			SifraTb.IsEnabled = true;

			List<Drug> confirmedDrugs = dc.ViewConfirmedDrugs();
			List<Drug> unconfirmedDrugs = dc.ViewUnconfirmedDrugs();

			List<Ingredient> ingredients = new List<Ingredient>();
			ingredients = dt.ViewIngridients();
			List<string> ingStr = new List<string>();
			foreach (Ingredient i in ingredients)
			{
				ingStr.Add(i.Name);
			}
			listBox1.DataContext = ingStr;

			List<DrugType> drugTypes = new List<DrugType>();
			List<string> drugTypesStr = new List<string>();
			drugTypes = dt.ViewDrugTypes();
			foreach (DrugType d in drugTypes)
			{
				drugTypesStr.Add(d.Type);
			}
			TipTb.DataContext = drugTypesStr;

			if (confirmedDrugs != null)
			{
				foreach (Drug d in confirmedDrugs)
				{
					Lek lek = new Lek();
					lek.Naziv = d.Name;
					lek.Kolicina = d.Quantity;
					lek.Proizvodjac = d.Producer;
					lek.RokTrajanja = d.ExpirationDate;
					lek.Sifra = d.Id;
					lek.Sastav = new List<string>();
					foreach (Ingredient a in d.ingredient)
					{
							lek.Sastav.Add(a.Name);
					}
					
					lek.Tip = d.drugType.Type;
						
					
					Lekovi.Add(lek);
				}
			}
			if (unconfirmedDrugs != null)
			{
				foreach (Drug d in unconfirmedDrugs)
				{
					Lek lek = new Lek();
					lek.Naziv = d.Name;
					lek.Kolicina = d.Quantity;
					lek.Proizvodjac = d.Producer;
					lek.RokTrajanja = d.ExpirationDate;
					lek.Sifra = d.Id;
					lek.Sastav = new List<string>();
					foreach (Ingredient a in d.ingredient)
					{
						lek.Sastav.Add(a.Name);
					}
					
					lek.Tip = d.drugType.Type;
						
					
					Lekovi.Add(lek);
				}
			}

			//Lekovi.Add(new Lek() { Naziv = "Brufen\nBrufen\nBrufen\nBrufen\nBrufen\nBrufen\n", Kolicina = "3", Sifra = "djna" });
			//Lekovi.Add(new Lek() { Naziv = "Para", Kolicina = "30", Sifra = "aadjna" });
			//Lekovi.Add(new Lek() { Naziv = "AAAAA", Kolicina = "40", Sifra = "jdjna" });
			//Lekovi.Add(new Lek() { Naziv = "Wbdk", Kolicina = "4000", Sifra = "ccdjna" });
			//Lekovi.Add(new Lek() { Naziv = "Cjdla", Kolicina = "50", Sifra = "aadjna" });
			foreach (var lek in Lekovi)
			{
				TempLekovi.Add(lek);
			}
			SelectedKriterijum = Kriterijumi[0];
			NazivHint = "Unesi tekst";
			KolicinaHint = "Unesi broj";
			NamenaHint = "Unesi tekst";
			SifraHint = "Unesi broj";
			RokHint = "Izaberi datum";
			TipHint = "Unesi tekst";
			ProizvodjacHint = "Unesi tekst";
			SastavHint = "Unesi tekst";
			PretragaHint = "Unesi tekst za pretragu";
			SelektovaniLek = null;
		}

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
		private void Button_sastojci_Click(object sender, RoutedEventArgs e)
		{
			foreach (var list in listBox1.SelectedItems)
			{
				if (!newIngredients.Contains(list.ToString()))
				{
					newIngredients.Add(list.ToString());
					
				}
			}

			listBox2.DataContext = newIngredients;


		}
		private void buttonObrisiLek_Click(object sender, RoutedEventArgs e)
        {
			if(SelektovaniLek == null || string.IsNullOrWhiteSpace(SelektovaniLek.Naziv))
			{
				return;
			}

            foreach(Lek lek in TempLekovi)
            {
                if (lek.Sifra == SelektovaniLek.Sifra) {
					if (dc.DeleteUnconfirmedDrug(lek.Sifra) == false)
					{
						dc.DeleteConfirmedDrug(lek.Sifra);
						tc.DeleteDrugTherapies(lek.Sifra);
					}
					SelektovaniLek = null;
					ClearFields();
					UpdateTempLek();
					break;
                }
            }
			
		}

		private void ButtonTraziLekove_Click(object sender, RoutedEventArgs e)
		{
			TempLekovi.Clear();
			foreach(var lek in Lekovi)
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
						//if (lek.Sifra.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
						//{
							TempLekovi.Add(lek);
						//}
						break;
					default:
						TempLekovi.Add(lek);
						break;
				}
			}
		}

		private void ButtonSacuvajLek_Click(object sender, RoutedEventArgs e)
		{
			// 1. Resetovanje svih validacionih poruka
			// 2. Validacija
			// 3. Reagovanje na stanje validacije
			// Ako je stanje validno onda azuriraj podatke
			bool isAdd = false;
			if (SelektovaniLek == null || string.IsNullOrWhiteSpace(SelektovaniLek.Sifra.ToString()))
			{
				isAdd = true;
			}

			// ovaj kod se izvrsi nakon validacije

			if (isAdd)
			{
				AddLek();
			}
			else
			{
				UpdateLek();
			}
		}
		private void AddLek()
		{
			Lek noviLek = new Lek();
			Console.WriteLine(SifraTb.Text);
			noviLek.Sifra = Int32.Parse(SifraTb.Text);
			noviLek.Naziv = NazivTb.Text as string;
			noviLek.Kolicina=Convert.ToInt32(KolicinaTb.Text);
			noviLek.Tip = TipTb.SelectedItem as string;
			noviLek.RokTrajanja = (DateTime)RokTb.SelectedDate;
			noviLek.Sastav = (List<string>)listBox2.ItemsSource;
			noviLek.Proizvodjac = ProizvodjacTb.Text as string;
			
			Lekovi.Add(noviLek);
			
			Drug newDrug = new Drug();
			newDrug.Name = noviLek.Naziv;
			newDrug.Id = noviLek.Sifra;
			newDrug.Quantity = noviLek.Kolicina;
			newDrug.ExpirationDate = noviLek.RokTrajanja;
			newDrug.Producer = noviLek.Proizvodjac;
			List<DrugType> types = dt.ViewDrugTypes();
			foreach (DrugType d in types)
			{

				if (noviLek.Tip.Equals(d.Type))
				{
					newDrug.drugType = d;
				}
			}
			List<Ingredient> myIngredients = new List<Ingredient>();
			List<Ingredient> ingredients = dt.ViewIngridients();
			foreach (Ingredient i in ingredients)
			{
				foreach (string s in newIngredients)
				{
					if (s.Equals(i.Name))
					{
						myIngredients.Add(i);
					}
				}
			}

			newDrug.ingredient = myIngredients;
			dc.AddDrug(newDrug);

			// Resetujemo polja
			ClearFields();

			UpdateTempLek();
		}
		private void UpdateLek()
		{
			SelektovaniLek.Sifra = Convert.ToInt32(SifraTb.Text);
			SelektovaniLek.Naziv = NazivTb.Text as string;
			SelektovaniLek.Kolicina = Convert.ToInt32(KolicinaTb.Text);
			SelektovaniLek.RokTrajanja = (DateTime)RokTb.SelectedDate;
			SelektovaniLek.Tip = TipTb.SelectedItem as string;
			SelektovaniLek.Proizvodjac = ProizvodjacTb.Text as string;
			SelektovaniLek.Sastav = (List<string>)listBox2.ItemsSource;

			
			Drug newDrug = new Drug();
			newDrug.Name = SelektovaniLek.Naziv;
			newDrug.Id = SelektovaniLek.Sifra;
			newDrug.Quantity = SelektovaniLek.Kolicina;
			newDrug.ExpirationDate = SelektovaniLek.RokTrajanja;
			newDrug.Producer = SelektovaniLek.Proizvodjac;
			List<DrugType> types = dt.ViewDrugTypes();
			foreach (DrugType d in types)
			{

				if (SelektovaniLek.Tip.Equals(d.Type))
				{
					newDrug.drugType = d;
				}
			}
			List<Ingredient> myIngredients = new List<Ingredient>();
			List<Ingredient> ingredients = dt.ViewIngridients();
			foreach (Ingredient i in ingredients)
			{
				foreach (string s in newIngredients)
				{
					if (s.Equals(i.Name))
					{
						myIngredients.Add(i);
					}
				}
			}

			newDrug.ingredient = myIngredients;

			dc.EditConfirmedDrug(newDrug);
			List<Therapy> tt = new List<Therapy>();
			tt= tc.GetTherapyByDrug(newDrug.Id);
			foreach (Therapy t in tt) {
				t.Drug = newDrug;
				tc.UpdateTherapy(t);
			}
			dc.EditUnconfirmedDrug(newDrug);

		}
		private void ClearFields()
		{
			SifraTb.Text = "";
			SifraTb.IsEnabled = true;
			NazivTb.Text = "";
			KolicinaTb.Text = "";
			ProizvodjacTb.Text = "";
			TipTb.SelectedIndex = -1;
			RokTb.SelectedDate=DateTime.Today;
		}

		private void UpdateTempLek()
		{
			Lekovi.Clear();
			TempLekovi.Clear();
			
			List<Drug> confirmedDrugs = dc.ViewConfirmedDrugs();
			if (confirmedDrugs != null)
			{
				foreach (Drug d in confirmedDrugs)
				{
					Lek lek = new Lek();
					lek.Naziv = d.Name;
					lek.Kolicina = d.Quantity;
					lek.Proizvodjac = d.Producer;
					lek.RokTrajanja = d.ExpirationDate;
					lek.Sifra = d.Id;
					lek.Sastav = new List<string>();
					foreach (Ingredient a in d.ingredient)
					{
						lek.Sastav.Add(a.Name);
					}
					lek.Tip = d.drugType.Type;

					//lek.Sastav = d.ingredient.FirstOrDefault();

					Lekovi.Add(lek);
				}
			}
			List<Drug> unconfirmedDrugs = dc.ViewUnconfirmedDrugs();
			if (unconfirmedDrugs != null)
			{
				foreach (Drug d in unconfirmedDrugs)
				{
					Lek lek = new Lek();
					lek.Naziv = d.Name;
					lek.Kolicina = d.Quantity;
					lek.Proizvodjac = d.Producer;
					lek.RokTrajanja = d.ExpirationDate;
					lek.Sifra = d.Id;
					lek.Sastav = new List<string>();
					foreach (Ingredient a in d.ingredient)
					{
						lek.Sastav.Add(a.Name);
					}
					lek.Tip = d.drugType.Type;



					Lekovi.Add(lek);
				}
			}

			foreach (var lek in Lekovi)
			{
				TempLekovi.Add(lek);
			}
		}
		private void Button_Click_Drug(object sender, RoutedEventArgs e)
		{
			
			Drug newDrug = new Drug();
			newDrug.Name = SelektovaniLek.Naziv;
			newDrug.Id = SelektovaniLek.Sifra;
			newDrug.Quantity = SelektovaniLek.Kolicina;
			newDrug.ExpirationDate = SelektovaniLek.RokTrajanja;
			newDrug.Producer = SelektovaniLek.Proizvodjac;
			List<DrugType> types = dt.ViewDrugTypes();
			foreach (DrugType d in types)
			{

				if (SelektovaniLek.Tip.Equals(d.Type))
				{
					newDrug.drugType = d;
				}
			}
			List<Ingredient> myIngredients = new List<Ingredient>();
			List<Ingredient> ingredients = dt.ViewIngridients();
			foreach (Ingredient i in ingredients)
			{
				foreach (string a in SelektovaniLek.Sastav)
				{
					if (a.Equals(i.Name))
					{
						myIngredients.Add(i);
					}
				}
			}

			newDrug.ingredient = myIngredients;

			IngredientsOfDrug s = new IngredientsOfDrug(newDrug);
			s.Show();

		}
		private void DataGridLekovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SelektovaniLek != null)
			{
				SifraTb.Text = SelektovaniLek.Sifra.ToString();
				SifraTb.IsEnabled = false;
				NazivTb.Text = SelektovaniLek.Naziv;
				KolicinaTb.Text = SelektovaniLek.Kolicina.ToString();
				RokTb.SelectedDate = SelektovaniLek.RokTrajanja;
				TipTb.Text = SelektovaniLek.Tip;
				ProizvodjacTb.Text = SelektovaniLek.Proizvodjac;
				//SastavTb.Text = SelektovaniLek.Sastav;
			}
		}
		private void Otkazi_Click(object sender, RoutedEventArgs e)
		{
			ClearFields();
			SelektovaniLek = null;
		}

		private void buttonPrebaciLek_Click(object sender, RoutedEventArgs e)
		{
			List<Drug> unconfirmedDrugs = dc.ViewUnconfirmedDrugs();
			if (unconfirmedDrugs != null)
			{
				foreach (Drug d in unconfirmedDrugs)
				{
					if (SelektovaniLek != null)
					{
						if (SelektovaniLek.Sifra.Equals(d.Id))
						{
							dc.ConfirmDrug(d);
							dc.DeleteUnconfirmedDrug(d.Id);
						}
					}
				}
			}

		}
	}
}
