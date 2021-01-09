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

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for EquipmentRelocationSchedulingDialog.xaml
    /// </summary>
    public partial class EquipmentRelocationSchedulingDialog : Window
    {
        public EquipmentWithRoomDTO EquipmentForRelocationWithRoom { get; set; }


        public EquipmentRelocationSchedulingDialog(EquipmentWithRoomDTO equipmentWithRoomDTO)
        {
            InitializeComponent();

            EquipmentForRelocationWithRoom = equipmentWithRoomDTO;

            SetDataToUIControls();
        }

        private void SetDataToUIControls()
        {
            EquipmentForRelocationComboBox.Items.Add(EquipmentForRelocationWithRoom.EquipmentName);
            RelocationStartingPointRoomComboBox.Items.Add(EquipmentForRelocationWithRoom.RoomNumber);

            for (int i = 1; i <= EquipmentForRelocationWithRoom.Quantity; i++)
            {
                EquipmentQuantityForRelocationComboBox.Items.Add(i);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
