using GraphicalEditor.Service;
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
using GraphicalEditor.Enumerations;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for ScheduleForRoomDialog.xaml
    /// </summary>
    public partial class ScheduleForRoomDialog : Window
    {
        RoomService _roomService;

        AppointmentService _appointmentService;
        EquipmentService _equipmentService;
        RenovatonService _renovationService;

        public int RoomId { get; set; }

        public ScheduleForRoomDialog(int roomId)
        {
            InitializeComponent();
            DataContext = this;

            _roomService = new RoomService();
            _appointmentService = new AppointmentService();
            _equipmentService = new EquipmentService();
            _renovationService = new RenovatonService();


            RoomId = roomId;
            GetDataAndDisplayItInScheduledActionsDataGrid();
        }

        private void ShowActionSuccessfullyCancelledDialog()
        {
            InfoDialog infoDialog = new InfoDialog("Uspešno ste otkazali zakazanu akciju!");
            infoDialog.ShowDialog();
        }

        private void GetDataAndDisplayItInScheduledActionsDataGrid()
        {
            List<RoomSchedulesDTO> roomSchedulesDTOs = _roomService.GetRoomSchedules(RoomId);
            RoomScheduledActionsDataGrid.ItemsSource = roomSchedulesDTOs;
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
            RoomSchedulesDTO selectedScheduledAction = (RoomSchedulesDTO)RoomScheduledActionsDataGrid.SelectedItem;
            

            if(selectedScheduledAction.ScheduleType == ScheduleType.Appointment)
            {
                _appointmentService.DeleteAppointment(selectedScheduledAction.Id);
            }
            else if(selectedScheduledAction.ScheduleType == ScheduleType.EquipmentTransfer)
            {
                _equipmentService.DeleteEquipmentTransfer(selectedScheduledAction.Id);
            }
            else if(selectedScheduledAction.ScheduleType == ScheduleType.Renovation)
            {
                _renovationService.DeleteRenovation(selectedScheduledAction.Id);
            }

            GetDataAndDisplayItInScheduledActionsDataGrid();
            ShowActionSuccessfullyCancelledDialog();
        }
    }
}
