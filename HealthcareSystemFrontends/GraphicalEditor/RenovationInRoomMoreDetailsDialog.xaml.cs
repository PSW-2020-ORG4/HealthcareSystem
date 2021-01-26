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
using GraphicalEditor.DTO;
using GraphicalEditor.Service;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for RenovationInRoomMoreDetailsDialog.xaml
    /// </summary>
    public partial class RenovationInRoomMoreDetailsDialog : Window
    {
        public int RenovationId { get; set; }
        public BaseRenovationDTO RenovationForDisplay { get; set; }
       

        RenovationService _renovationService;
        

        public RenovationInRoomMoreDetailsDialog(int renovationId)
        {
            InitializeComponent();
            DataContext = this;

            RenovationId = renovationId;

            _renovationService = new RenovationService();

            RenovationForDisplay = _renovationService.GetBaseRenovationById(RenovationId);
        }

        private void ShowRenovationSuccessfullyCancelledDialog()
        {
            InfoDialog infoDialog = new InfoDialog("Uspešno ste otkazali renoviranje!");
            infoDialog.ShowDialog();
        }

        private void CancelRenovationButton_Click(object sender, RoutedEventArgs e)
        {
            _renovationService.DeleteRenovation(RenovationId);
            this.Close();
            ShowRenovationSuccessfullyCancelledDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
