using Clinic_Health.Model;
using Controller.ExaminationAndPatientCard;
using Controller.UsersAndWorkingTime;
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

namespace Clinic_Health.Views.Employees
{
    /// <summary>
    /// Interaction logic for AllEmployeesView.xaml
    /// </summary>
    public partial class AllEmployeesView : UserControl
    {
        public ObservableCollection<Zaposleni> Zaposlenici { get; set; }
        public ObservableCollection<Zaposleni> TempZaposlenici { get; set; }
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
        private Zaposleni _selektovaniZaposleni;
        public Zaposleni SelektovaniZaposleni
        {
            get { return _selektovaniZaposleni; }
            set
            {
                _selektovaniZaposleni = value;
                OnPropertyChanged("SelektovaniZaposleni");
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
        public AllEmployeesView()
        {
            InitializeComponent();
			this.DataContext = this;
            Zaposlenici = new ObservableCollection<Zaposleni>();
            TempZaposlenici = new ObservableCollection<Zaposleni>();
            Kriterijumi = new ObservableCollection<string>();
            Kriterijumi.Add("Ime");
            Kriterijumi.Add("Prezime");
            Kriterijumi.Add("JMBG");
            Kriterijumi.Add("Zaposlenje");

            DoctorController doctorController = new DoctorController();
            List<User> doctors = doctorController.ViewAllUsers();
            if (doctors != null)
            {
                foreach (User w in doctors)
                {
                    Zaposlenici.Add(new Zaposleni() { Jmbg = w.Jmbg, Ime = w.Name, Prezime = w.Surname, Telefon=w.Phone, Zaposlenje="doktor"});
                }
            }
            SecretaryController secretaryController = new SecretaryController();
            List<User> secr = secretaryController.ViewAllUsers();
            if (secr != null)
            {
                foreach (User w in secr)
                {
                    Zaposlenici.Add(new Zaposleni() { Jmbg = w.Jmbg, Ime = w.Name, Prezime = w.Surname, Telefon = w.Phone, Zaposlenje = "sekretar" });
                }
            }
            // Zaposlenici.Add(new Zaposleni() { Ime = "jelena", Prezime = "budisa",Jmbg="1234567",Telefon="0636218781",Zaposlenje= "lekar" });
            //Zaposlenici.Add(new Zaposleni() { Ime = "ana", Prezime = "budisa", Jmbg = "1234567", Telefon = "0636218781", Zaposlenje = "lekar" });
            //Zaposlenici.Add(new Zaposleni() { Ime = "milana", Prezime = "budisa", Jmbg = "1234567", Telefon = "0636218781", Zaposlenje = "lekar" });
            //Zaposlenici.Add(new Zaposleni() { Ime = "dragana", Prezime = "budisa", Jmbg = "1234567", Telefon = "0636218781", Zaposlenje = "lekar" });
            //Zaposlenici.Add(new Zaposleni() { Ime = "sladjana", Prezime = "budisa", Jmbg = "1234567", Telefon = "0636218781", Zaposlenje = "sekretar" });

            foreach (var z in Zaposlenici)
            {
                TempZaposlenici.Add(z);
            } 
            SelectedKriterijum = Kriterijumi[0];
            PretragaHint = "Unesi tekst za pretragu";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void buttonTraziZaposlene_Click(object sender, RoutedEventArgs e)
        {
            TempZaposlenici.Clear();
            foreach (var z in Zaposlenici)
            {
                switch (SelectedKriterijum)
                {
                    case "Ime":
                        if (z.Ime.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            TempZaposlenici.Add(z);
                        }
                        break;
                    case "Prezime":
                        if (z.Prezime.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            TempZaposlenici.Add(z);
                        }
                        break;
                    case "JMBG":
                        if (z.Jmbg.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            TempZaposlenici.Add(z);
                        }
                        break;
                    case "Zaposlenje":
                        if (z.Zaposlenje.IndexOf(Pretraga, StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            TempZaposlenici.Add(z);
                        }
                        break;
                    default:
                        TempZaposlenici.Add(z);
                        break;
                }
            }
        }
    }
}
