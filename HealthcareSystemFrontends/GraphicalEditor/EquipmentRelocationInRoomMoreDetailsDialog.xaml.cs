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
    /// Interaction logic for EquipmentRelocationInRoomMoreDetailsDialog.xaml
    /// </summary>
    public partial class EquipmentRelocationInRoomMoreDetailsDialog : Window
    {
        public int RelocationId { get; set; }
        public TransferEquipmentDTO RelocationForDisplay { get; set; }
        EquipmentService _equipmentService;

        public EquipmentRelocationInRoomMoreDetailsDialog(int relocationId)
        {
            InitializeComponent();
            DataContext = this;

            RelocationId = relocationId;
            _equipmentService = new EquipmentService();
            //RelocationForDisplay = _equipmentService.GetEquipmentTransferById(RelocationId);
        }

        private void ShowRelocationSuccessfullyCancelledDialog()
        {
            InfoDialog infoDialog = new InfoDialog("Uspešno ste otkazali premeštanje opreme!");
            infoDialog.ShowDialog();
        }

        private void CancelRelocationButton_Click(object sender, RoutedEventArgs e)
        {
            _equipmentService.DeleteEquipmentTransfer(RelocationId);
            this.Close();
            ShowRelocationSuccessfullyCancelledDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
