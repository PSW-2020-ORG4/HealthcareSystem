using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for SuccessfulSendSurvey.xaml
    /// </summary>
    public partial class SuccessfulSendSurvey : Window
    {
        public SuccessfulSendSurvey()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
         
            this.Close();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            { 
               
                this.Close();
         
            }
        }
    }
}
