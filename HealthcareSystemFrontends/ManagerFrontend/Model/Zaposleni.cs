using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Health.Model
{
    public class Zaposleni : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _ime;
        private string _prezime;
        private string _jmbg;
        private string _telefon;
        private string _zaposlenje;


        public string Ime
        {
            get
            {
                return _ime;
            }
            set
            {
                if (value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        public string Prezime
        {
            get
            {
                return _prezime;
            }
            set
            {
                if (value != _prezime)
                {
                    _prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        public string Jmbg
        {
            get
            {
                return _jmbg;
            }
            set
            {
                if (value != _jmbg)
                {
                    _jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }

        public string Telefon
        {
            get
            {
                return _telefon;
            }
            set
            {
                if (value != _telefon)
                {
                    _telefon = value;
                    OnPropertyChanged("Telefon");
                }
            }
        }

        public string Zaposlenje
        {
            get
            {
                return _zaposlenje;
            }
            set
            {
                if (value != _zaposlenje)
                {
                    _zaposlenje = value;
                    OnPropertyChanged("Zaposlenje");
                }
            }
        }

       
    }
}


