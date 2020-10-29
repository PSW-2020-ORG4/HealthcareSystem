using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PatientFile
    {
        private string txtData1;
        private string txtData2;
        private string txtData3;

        public string Datum
        {
            get { return txtData1; }
            set { txtData1 = value; }
        }

        public string Bolest
        {
            get { return txtData2; }
            set { txtData2 = value; }
        }

        public string Terapija
        {
            get { return txtData3; }
            set { txtData3 = value; }
           
        }
    }
}
