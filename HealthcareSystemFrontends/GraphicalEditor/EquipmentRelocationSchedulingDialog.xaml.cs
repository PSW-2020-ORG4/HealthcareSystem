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
using GraphicalEditor.Models;
using System.ComponentModel;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for EquipmentRelocationSchedulingDialog.xaml
    /// </summary>
    public partial class EquipmentRelocationSchedulingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private EquipmentService _equipmentService;
        private bool _isAlternativeAppointmentsSectionVisible;

        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentQuantityInStartingRoom { get; set; }
        public int StartingRoomNumber { get; set; }

        public bool IsAlternativeAppointmentsSectionVisible
        {
            get
            { 
                return _isAlternativeAppointmentsSectionVisible; 
            }
            set
            {
                _isAlternativeAppointmentsSectionVisible = value;
                OnPropertyChanged("IsAlternativeAppointmentsSectionVisible");
            }
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler _handler = this.PropertyChanged;
            if (_handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                _handler(this, e);
            }
        }



        public EquipmentRelocationSchedulingDialog(EquipmentWithRoomDTO equipmentWithRoomDTO)
        {
            InitializeComponent();
            DataContext = this;

            _equipmentService = new EquipmentService();

            EquipmentId = equipmentWithRoomDTO.IdEquipment;
            EquipmentName = equipmentWithRoomDTO.EquipmentName;
            EquipmentQuantityInStartingRoom = equipmentWithRoomDTO.Quantity;
            StartingRoomNumber = equipmentWithRoomDTO.RoomNumber;

            SetDataToUIControls();

            IsAlternativeAppointmentsSectionVisible = false;
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

       
        private void EquipmentRelocationBackToTopButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentRelocationScrollViewer.ScrollToTop();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private TransferEquipmentDTO CreateEquipmentRelocationDTOFromUserInput()
        {
            RoomDTO destinationRoom = (RoomDTO)RelocationDestinationRoomComboBox.SelectedItem;
            int destinationRoomNumber = (int)destinationRoom.Id;

            DateTime relocationDate = EquipmentRelocationDatePicker.SelectedDate.Value.Date;
            DateTime relocationTime = EquipmentRelocationTimePicker.SelectedTime.Value;

            DateTime equipmentRelocationDateTime = new DateTime(relocationDate.Year, relocationDate.Month, relocationDate.Day, relocationTime.Hour, relocationTime.Minute, relocationTime.Second, DateTimeKind.Utc);

            int quantityForRelocation = (int)EquipmentQuantityForRelocationComboBox.SelectedItem;

            return new TransferEquipmentDTO(EquipmentId, StartingRoomNumber, quantityForRelocation, equipmentRelocationDateTime, destinationRoomNumber);
        }



        private void ScheduleEquipmentRelocation(TransferEquipmentDTO relocationAppointmentDTO)
        {
            IsAlternativeAppointmentsSectionVisible = false;

            _equipmentService.ScheduleEquipmentTransfer(relocationAppointmentDTO);
            this.Close();
            InfoDialog infoDialog = new InfoDialog("Uspešno ste zakazali premeštanje opreme!");
            infoDialog.ShowDialog();
        }


        private void DisplayAlternativeRelocationAppointmentsSection()
        {
            IsAlternativeAppointmentsSectionVisible = true;
            EquipmentRelocationScrollViewer.ScrollToBottom();
        }


        private void MarkProblematicRelocationRoomOnMap(long problematicRoomId)
        {
            MapObject problematicRoomMapObject = ((MainWindow)this.Owner).GetMapObjectById(problematicRoomId);
            ((MainWindow)this.Owner).ShowSelectedSearchResultObjectOnMap(problematicRoomMapObject);
        }


        private void ShowRelocationUnavailableDialog(long problematicRoomId)
        {
            string relocationUnavailableMessage = String.Format("Premeštanje opreme nije moguće!{0}U sobi broj {1} izabrana oprema se koristi ili postoji neki zakazan pregled u navedenom terminu.{2}Problematična soba je označena na mapi.{3}Možete odabrati neki termin iz liste alternativnih termina za premeštanje željene opreme.",
                                                                            Environment.NewLine, problematicRoomId, Environment.NewLine, Environment.NewLine);
            InfoDialog infoDialog = new InfoDialog(relocationUnavailableMessage);
            infoDialog.ShowDialog();
        }


        private void GetAndDisplayAlternativeAppointmentsForRelocationAppointment(TransferEquipmentDTO unavailableRelocationAppointmentDTO)
        {
            List<DateTime> alternativeRelocationAppointments = _equipmentService.GetAlternativeAppointments(unavailableRelocationAppointmentDTO);
            AlternativeRelocationAppointmentsDataGrid.ItemsSource = alternativeRelocationAppointments;
        }


        private void ShowInfoDialogAndAlternativeAppointmentsForUnavailableRelocationAppointment(long problematicRoomId, TransferEquipmentDTO unavailableRelocationAppointmentDTO)
        {
            DisplayAlternativeRelocationAppointmentsSection();

            MarkProblematicRelocationRoomOnMap(problematicRoomId);
            ShowRelocationUnavailableDialog(problematicRoomId);

            GetAndDisplayAlternativeAppointmentsForRelocationAppointment(unavailableRelocationAppointmentDTO);
        }



        private void ScheduleEquipmentRelocationButton_Click(object sender, RoutedEventArgs e)
        {
            TransferEquipmentDTO relocationAppointmentDTO = CreateEquipmentRelocationDTOFromUserInput();

            if (AlternativeRelocationAppointmentsDataGrid.SelectedItem == null)
            {
                int relocationResult = _equipmentService.InitializeEquipmentTransfer(relocationAppointmentDTO);

                if (relocationResult == -1)
                {
                    ScheduleEquipmentRelocation(relocationAppointmentDTO);
                }
                else
                {
                    long problematicRoomId = relocationResult;
                    ShowInfoDialogAndAlternativeAppointmentsForUnavailableRelocationAppointment(problematicRoomId, relocationAppointmentDTO);
                }
            }
            else 
            {
                relocationAppointmentDTO.DateAndTimeOfTransfer = (DateTime)AlternativeRelocationAppointmentsDataGrid.SelectedItem;
                ScheduleEquipmentRelocation(relocationAppointmentDTO);
            }
        }
    }
}
