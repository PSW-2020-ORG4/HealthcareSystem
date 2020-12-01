using Model.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Clinic_Health.Model
{
    public class Lek : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _naziv;
        private int _kolicina;
        private int _sifra;
        private DateTime _roktrajanja;
        private string _proizvodjac;
        private string _tip;
        private List<string> _sastav;
        

        public string Naziv
        {
            get
            {
                return _naziv;
            }
            set
            {
                if (value != _naziv)
                {
                    _naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public string Tip
        {
            get
            {
                return _tip;
            }
            set
            {
                if (value != _tip)
                {
                    _tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }

       

        public int Sifra
        {
            get
            {
                return _sifra;
            }
            set
            {
                if (value != _sifra)
                {
                    _sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }

        public int Kolicina
        {
            get
            {
                return _kolicina;
            }
            set
            {
                if (value != _kolicina)
                {
                    _kolicina = value;
                    OnPropertyChanged("Kolicina");
                }
            }
        }

        public DateTime RokTrajanja
        {
            get
            {
                return _roktrajanja;
            }
            set
            {
                if (value != _roktrajanja)
                {
                    _roktrajanja = value;
                    OnPropertyChanged("RokTrajanja");
                }
            }
        }

        public List<string> Sastav
        {
            get
            {
                return _sastav;
            }
            set
            {
                if (value != _sastav)
                {
                    _sastav = value;
                    OnPropertyChanged("Sastav");
                }
            }
        }

        public string Proizvodjac
        {
            get
            {
                return _proizvodjac;
            }
            set
            {
                if (value != _proizvodjac)
                {
                    _proizvodjac = value;
                    OnPropertyChanged("Proizvodjac");
                }
            }
        }

    }
}
