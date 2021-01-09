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
using GraphicalEditor.Service;
using GraphicalEditor.DTO;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for EquipmentRelocationSchedulingDialog.xaml
    /// </summary>
    public partial class EquipmentRelocationSchedulingDialog : Window
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentQuantityInStartingRoom { get; set; }
        public int StartingRoomNumber { get; set; }

        public EquipmentRelocationSchedulingDialog(EquipmentWithRoomDTO equipmentWithRoomDTO)
        {
            InitializeComponent();

            EquipmentId = equipmentWithRoomDTO.IdEquipment;
            EquipmentName = equipmentWithRoomDTO.EquipmentName;
            EquipmentQuantityInStartingRoom = equipmentWithRoomDTO.Quantity;
            StartingRoomNumber = equipmentWithRoomDTO.RoomNumber;

            SetDataToUIControls();

            AlternativeRelocationAppointmentsTextBlock.Visibility = Visibility.Collapsed;
            AlternativeRelocationAppointmentsDataGrid.Visibility = Visibility.Collapsed;
            EquipmentRelocationBackToTopButton.Visibility = Visibility.Collapsed;

           
        }

        private void SetDataToUIControls()
        {
            EquipmentForRelocationComboBox.Items.Add(EquipmentName);
            RelocationStartingPointRoomComboBox.Items.Add(StartingRoomNumber);

            SetDataToEquipmentQuantityComboBox();
            SetDataToDestinationRoomComboBox();
        }

        private void SetDataToEquipmentQuantityComboBox()
        {
            for (int i = 1; i <= EquipmentQuantityInStartingRoom; i++)
            {
                EquipmentQuantityForRelocationComboBox.Items.Add(i);
            }
        }

        private void SetDataToDestinationRoomComboBox()
        {
            RoomService roomService = new RoomService();
            List<RoomDTO> possibleDestinationRooms = new List<RoomDTO>();
            foreach (RoomDTO roomDTO in roomService.GetAllRooms())
            {
                if (roomDTO.Id != StartingRoomNumber)
                {
                    possibleDestinationRooms.Add(roomDTO);
                }
            }

            RelocationDestinationRoomComboBox.ItemsSource = possibleDestinationRooms;
        }

        private void AlternativeRelocationAppointmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EquipmentRelocationBackToTopButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentRelocationScrollViewer.ScrollToTop();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ScheduleEquipmentRelocationButton_Click(object sender, RoutedEventArgs e)
        {
            AlternativeRelocationAppointmentsTextBlock.Visibility = Visibility.Visible;
            AlternativeRelocationAppointmentsDataGrid.Visibility = Visibility.Visible;
            EquipmentRelocationBackToTopButton.Visibility = Visibility.Visible;

            EquipmentRelocationScrollViewer.ScrollToBottom();
        }
    }
}
