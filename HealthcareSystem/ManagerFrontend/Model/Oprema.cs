using Model.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Health.Model
{
    public class Oprema : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

 
        private int _kolicina;
        private int _sifra;
        private TypeOfEquipment _tip;
        private TypeOfConsumable _vrstaP;
        private TypeOfNonConsumable _vrstaN;
        private int _sala;


      
        public int Sala
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

        public TypeOfEquipment Tip
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

        public TypeOfConsumable VrstaP
        {
            get
            {
                return _vrstaP;
            }
            set
            {
                if (value != _vrstaP)
                {
                    _vrstaP = value;
                    OnPropertyChanged("VrstaP");
                }
            }
        }
        public TypeOfNonConsumable VrstaN
        {
            get
            {
                return _vrstaN;
            }
            set
            {
                if (value != _vrstaN)
                {
                    _vrstaN = value;
                    OnPropertyChanged("VrstaN");
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

       
    }
}
