using Model.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Health.Model
{
    public class Soba : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private int _sifra;
        private DateTime _pocetak;
        private DateTime _kraj;
        private int _kapacitet;
        private TypeOfUsage _tip;
        private int _zauzetost;



        public TypeOfUsage Tip
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

       /* public bool Renoviranje
        {
            get
            {
                return _renoviranje;
            }
            set
            {
                if (value != _renoviranje)
                {
                    _renoviranje = value;
                    OnPropertyChanged("Renoviranje");
                }
            }
        }*/

        public DateTime Pocetak
        {
            get
            {
                return _pocetak;
            }
            set
            {
                if (value != _pocetak)
                {
                    _pocetak = value;
                    OnPropertyChanged("Pocetak");
                }
            }
        }

        public DateTime Kraj
        {
            get
            {
                return _kraj;
            }
            set
            {
                if (value != _kraj)
                {
                    _kraj = value;
                    OnPropertyChanged("Kraj");
                }
            }
        }

        public int Kapacitet
        {
            get
            {
                return _kapacitet;
            }
            set
            {
                if (value != _kapacitet)
                {
                    _kapacitet = value;
                    OnPropertyChanged("Kapacitet");
                }
            }
        }
        public int Zauzetost
        {
            get
            {
                return _zauzetost;
            }
            set
            {
                if (value != _zauzetost)
                {
                    _zauzetost = value;
                    OnPropertyChanged("Zauzetost");
                }
            }
        }

    }

    }
