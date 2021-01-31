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
using GraphicalEditor.Controllers;
using GraphicalEditor.Models;
using GraphicalEditor.Service;
using GraphicalEditor.Repository;
using System.ComponentModel;
using GraphicalEditor.DTO;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models.MapObjectRelated;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for RenovationSchedulingDialog.xaml
    /// </summary>
    public partial class RenovationSchedulingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private RenovationService _renovatonService;
        private bool _isAlternativeAppointmentsSectionVisible;

        private IRepository _fileRepository;
        private MapObjectServices _mapObjectService;
        private MapObjectController _mapObjectController;



        public int InitialRoomId { get; set; }

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


        public RenovationSchedulingDialog(int roomId)
        {
            InitializeComponent();
            DataContext = this;

            _renovatonService = new RenovationService();
            _mapObjectController = new MapObjectController(new MapObjectServices(_fileRepository));

            InitialRoomId = roomId;

            SetDataToUIControls();

            IsAlternativeAppointmentsSectionVisible = false;
        }

        private void SetDataToUIControls()
        {
            RenovationRoomComboBox.Items.Add(InitialRoomId);
            SetDataToRoomTypeComboboxes();
        }

        private void SetDataToRoomTypeComboboxes()
        {
            MergedRoomTypeComboBox.ItemsSource = MapObjectType.AllRoomTypes();
            RenovationFirstRoomTypeComboBox.ItemsSource = MapObjectType.AllRoomTypes();
            RenovationSecondRoomTypeComboBox.ItemsSource = MapObjectType.AllRoomTypes();
        }

        private void SetDataToSecondRoomForMergingComboBox()
        {
            MapObject initialRoomForRenovation = ((MainWindow)this.Owner).GetMapObjectById(InitialRoomId);
            List<MapObject> potentialRoomsForMergingWithSelectedRoom = _mapObjectController.GetNeighboringRoomsForRoom(initialRoomForRenovation);

            SecondRoomForMergingComboBox.ItemsSource = potentialRoomsForMergingWithSelectedRoom;
        }

        private void IsComplexRenovationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetDataToSecondRoomForMergingComboBox();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RenovationBackToTopButton_Click(object sender, RoutedEventArgs e)
        {
            RenovationScrollViewer.ScrollToTop();
        }


        private void ShowRenovationUnavailableDialog()
        {
            string relocationUnavailableMessage = String.Format("Renoviranje nije moguće!{0}Postoji neki zakazan pregled, premeštanje opreme ili renoviranje u navedenom terminu.{1}Možete odabrati neki termin iz liste alternativnih termina za renoviranje.",
                                                                            Environment.NewLine, Environment.NewLine);
            InfoDialog infoDialog = new InfoDialog(relocationUnavailableMessage);
            infoDialog.ShowDialog();
        }

        private void ShowRenovationSuccessfullyScheduledDialog()
        {
            IsAlternativeAppointmentsSectionVisible = false;

            this.Close();
            InfoDialog infoDialog = new InfoDialog("Uspešno ste zakazali renoviranje!");
            infoDialog.ShowDialog();
        }

        private void ShowDateIntervalInvalidDialog()
        {
            string dateIntervalInvalidMessage = String.Format("Neispravan unos vremenskog intervala!{0}Gornja granica vremenskog intervala mora biti veća od donje!",
                                                                Environment.NewLine);

            InfoDialog infoDialog = new InfoDialog(dateIntervalInvalidMessage);
            infoDialog.ShowDialog();
        }


        private bool CheckIsDateTimeIntervalValid()
        {
            if (RenovationStartDatePicker.SelectedDate == null || RenovationEndDatePicker.SelectedDate == null)
            {
                return false;
            }
            else if (RenovationStartDatePicker.SelectedDate != null && RenovationEndDatePicker.SelectedDate != null)
            {
                if (RenovationEndDatePicker.SelectedDate < RenovationStartDatePicker.SelectedDate)
                {
                    ShowDateIntervalInvalidDialog();
                    return false;
                }
                else if (RenovationEndDatePicker.SelectedDate == RenovationStartDatePicker.SelectedDate)
                {
                    if(RenovationEndTimePicker.SelectedTime <= RenovationStartTimePicker.SelectedTime)
                    {
                        ShowDateIntervalInvalidDialog();
                        return false;
                    }
                }
            }
            return true;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckIsDateTimeIntervalValid();
        }

        private void TimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            CheckIsDateTimeIntervalValid();
        }


        private void DisplayAlternativeRenovationAppointmentsSection()
        {
            IsAlternativeAppointmentsSectionVisible = true;
            RenovationScrollViewer.ScrollToBottom();
        }


        private BaseRenovationDTO CreateBaseRenovationDTOFromUserInput()
        {
            string renovationDescription = BasicRenovationDescriptionTextBox.Text;

            DateTime renovationStartDate = RenovationStartDatePicker.SelectedDate.Value.Date;
            DateTime renovationStartTime = RenovationStartTimePicker.SelectedTime.Value;
            DateTime renovationStartDateTime = new DateTime(renovationStartDate.Year, renovationStartDate.Month, renovationStartDate.Day, renovationStartTime.Hour, renovationStartTime.Minute, renovationStartTime.Second, DateTimeKind.Utc);

            DateTime renovationEndDate = RenovationEndDatePicker.SelectedDate.Value.Date;
            DateTime renovationEndTime = RenovationEndTimePicker.SelectedTime.Value;
            DateTime renovationEndDateTime = new DateTime(renovationEndDate.Year, renovationEndDate.Month, renovationEndDate.Day, renovationEndTime.Hour, renovationEndTime.Minute, renovationEndTime.Second, DateTimeKind.Utc);

            return new BaseRenovationDTO(InitialRoomId, renovationStartDateTime, renovationEndDateTime, renovationDescription, TypeOfRenovation.REGULAR_RENOVATION);
        }

        private DivideRenovationDTO CreateDivideRenovationDTOFromUserInput(BaseRenovationDTO baseRenovationDTO)
        {
            baseRenovationDTO.TypeOfRenovation = TypeOfRenovation.DIVIDE_RENOVATION;

            string firstRoomDescription = RenovationFirstRoomDescriptionTextBox.Text;
            string secondRoomDescription = RenovationSecondRoomDescriptionTextBox.Text;

            TypeOfMapObject firstRoomType = (TypeOfMapObject)RenovationFirstRoomTypeComboBox.SelectedItem;
            TypeOfMapObject secondRoomType = (TypeOfMapObject)RenovationSecondRoomTypeComboBox.SelectedItem;

            return new DivideRenovationDTO(baseRenovationDTO, firstRoomDescription, secondRoomDescription, firstRoomType, secondRoomType);
        }

        private MergeRenovationDTO CreateMergeRenovationDTOFromUserInput(BaseRenovationDTO baseRenovationDTO)
        {
            baseRenovationDTO.TypeOfRenovation = TypeOfRenovation.MERGE_RENOVATION;

            int secondRoomId = (int)((MapObject)SecondRoomForMergingComboBox.SelectedItem).MapObjectEntity.Id;
            string mergedRoomDescription = MergedRoomDescriptionTextBox.Text;
            TypeOfMapObject mergedRoomType = (TypeOfMapObject)MergedRoomTypeComboBox.SelectedItem;

            return new MergeRenovationDTO(baseRenovationDTO, secondRoomId, mergedRoomDescription, mergedRoomType);
        }
        
        private void TryToScheduleBaseRenovation(BaseRenovationDTO baseRenovationDTO)
        {
            bool isSuccessfullyScheduled = _renovatonService.ScheduleBaseRenovation(baseRenovationDTO);
            if (isSuccessfullyScheduled)
            {
                ShowRenovationSuccessfullyScheduledDialog();
            }
            else
            {
                List<RenovationPeriodDTO> alternativeRenovationAppointments = _renovatonService.GetBaseRenovationAlternativeAppointments(baseRenovationDTO);
                AlternativeRenovationAppointmentsDataGrid.ItemsSource = alternativeRenovationAppointments;
                DisplayAlternativeRenovationAppointmentsSection();
                ShowRenovationUnavailableDialog();
            }
        }

        private void TryToScheduleMergeRenovation(MergeRenovationDTO mergeRenovationDTO)
        {
            bool isSuccessfullyScheduled = _renovatonService.ScheduleMergeRenovation(mergeRenovationDTO);
            if (isSuccessfullyScheduled)
            {
                ShowRenovationSuccessfullyScheduledDialog();
            }
            else
            {
                List<RenovationPeriodDTO> alternativeRenovationAppointments = _renovatonService.GetMergeRenovationAlternativeAppointments(mergeRenovationDTO);
                AlternativeRenovationAppointmentsDataGrid.ItemsSource = alternativeRenovationAppointments;
                DisplayAlternativeRenovationAppointmentsSection();
                ShowRenovationUnavailableDialog();
            }
        }

        private void TryToScheduleDivideRenovation(DivideRenovationDTO divideRenovationDTO)
        {
            bool isSuccessfullyScheduled = _renovatonService.ScheduleDivideRenovation(divideRenovationDTO);
            if (isSuccessfullyScheduled)
            {
                ShowRenovationSuccessfullyScheduledDialog();
            }
            else
            {
                List<RenovationPeriodDTO> alternativeRenovationAppointments = _renovatonService.GetDivideRenovationAlternativeAppointments(divideRenovationDTO);
                AlternativeRenovationAppointmentsDataGrid.ItemsSource = alternativeRenovationAppointments;
                DisplayAlternativeRenovationAppointmentsSection();
                ShowRenovationUnavailableDialog();
            }
        }

        private void ScheduleRenovationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIsDateTimeIntervalValid())
            {
                return;
            }

            BaseRenovationDTO baseRenovationDTO = CreateBaseRenovationDTOFromUserInput();

            if (AlternativeRenovationAppointmentsDataGrid.SelectedItem != null)
            {
                baseRenovationDTO.StartTime = ((RenovationPeriodDTO)AlternativeRenovationAppointmentsDataGrid.SelectedItem).StartTime;
                baseRenovationDTO.EndTime = ((RenovationPeriodDTO)AlternativeRenovationAppointmentsDataGrid.SelectedItem).EndTime;
            }

            if (!(bool)IsComplexRenovationCheckBox.IsChecked)
            { 
                TryToScheduleBaseRenovation(baseRenovationDTO);
            }
            else
            {
                if (RoomMergingComboBoxItem.IsSelected)
                {
                    MergeRenovationDTO mergeRenovationDTO = CreateMergeRenovationDTOFromUserInput(baseRenovationDTO);
                    TryToScheduleMergeRenovation(mergeRenovationDTO);
                }
                else if (RoomDivisionComboBoxItem.IsSelected)
                {
                    DivideRenovationDTO divideRenovationDTO = CreateDivideRenovationDTOFromUserInput(baseRenovationDTO);
                    TryToScheduleDivideRenovation(divideRenovationDTO);
                }
            }
        }
    }
}
