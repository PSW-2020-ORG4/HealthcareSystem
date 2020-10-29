using Controller.DrugAndTherapy;
using Model.Manager;
using System;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MessageConfirmedDrug.xaml
    /// </summary>
    public partial class MessageConfirmedDrug : Window
    {
        private Drug d;
        private static DrugController dc = new DrugController();
        public MessageConfirmedDrug(Drug drug)
        {
            InitializeComponent();
            d = drug;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            dc.ConfirmDrug(d);        
            this.Close();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new CheckingDrug();
            s.Show();
            this.Close();
        }
    }
}
