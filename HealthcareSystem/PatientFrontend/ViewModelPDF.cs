using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija
{
    class ViewModelPDF : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private static Stream documentStream = null;

        public Stream DocumentStraem
        {
            get
            {
                return documentStream;
            }
            set
            {
                documentStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public ViewModelPDF()
        {
            if(documentStream != null)
            {
                documentStream.Close();
            }

            documentStream = new FileStream(@"../../Resources/Report/izvestaj.pdf", FileMode.OpenOrCreate);
       
        }

    }
}
