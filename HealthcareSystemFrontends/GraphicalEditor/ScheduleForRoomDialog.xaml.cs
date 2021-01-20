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

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for ScheduleForRoomDialog.xaml
    /// </summary>
    public partial class ScheduleForRoomDialog : Window
    {
        public ScheduleForRoomDialog()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowDetailsForScheduledActionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelScheduledActionButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
