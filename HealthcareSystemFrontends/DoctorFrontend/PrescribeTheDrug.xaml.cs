using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for PrescribeTheDrug.xaml
    /// </summary>
    public partial class PrescribeTheDrug : Window
    {
     

        public PrescribeTheDrug()
        {
            InitializeComponent();
 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
