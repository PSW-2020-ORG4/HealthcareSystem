using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class DrugFile
    {
        private string datum;
        private string vrijeme;
        private string pacijent;
        public string Datum
        {
            get { return datum; }
            set { datum = value; }
        }

        public string Vrijeme
        {
            get { return vrijeme; }
            set { vrijeme = value; }
        }

        public string Pacijent
        {
            get { return pacijent; }
            set { pacijent = value; }

        }
    }
}
