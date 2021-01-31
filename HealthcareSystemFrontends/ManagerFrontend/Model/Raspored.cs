using Model.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Health.Model
{
    public class Raspored : INotifyPropertyChanged
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
        private WorkShifts _smena;
        private string _sala;
        private DateTime _datump;
        private DateTime _datumk;

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

        public WorkShifts Smena
        {
            get
            {
                return _smena;
            }
            set
            {
                if (value != _smena)
                {
                    _smena = value;
                    OnPropertyChanged("Smena");
                }
            }
        }
        public string Sala
        {
            get
            {
                return _sala;
            }
            set
            {
                if (value != _sala)
                {
                    _sala = value;
                    OnPropertyChanged("Sala");
                }
            }
        }
        public DateTime DatumP
        {
            get
            {
                return _datump;
            }
            set
            {
                if (value != _datump)
                {
                    _datump = value;
                    OnPropertyChanged("DatumP");
                }
            }
        }

        public DateTime DatumK
        {
            get
            {
                return _datumk;
            }
            set
            {
                if (value != _datumk)
                {
                    _datumk = value;
                    OnPropertyChanged("DatumK");
                }
            }
        }

    }
}
