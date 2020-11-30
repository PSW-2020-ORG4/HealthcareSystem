using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    class ReportPdf : INotifyPropertyChanged
    {
        private Stream docStream;



        public event PropertyChangedEventHandler PropertyChanged;


        public Stream DocumentStream

        {

            get

            {

                return docStream;

            }

            set

            {

                docStream = value;

                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));

            }

        }

        public ReportPdf() {
            try
            {
                docStream = new FileStream(@"..\..\izvjestaj.pdf", FileMode.OpenOrCreate);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
    }

        public void OnPropertyChanged(PropertyChangedEventArgs e)

        {

            if (PropertyChanged != null)

                PropertyChanged(this, e);

        }
    }
}
