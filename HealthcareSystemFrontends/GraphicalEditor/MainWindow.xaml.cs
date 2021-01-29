using GraphicalEditor.Controllers;
using GraphicalEditor.DTO;
using GraphicalEditor.DTOForView;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
using GraphicalEditor.Service;
using GraphicalEditor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static Canvas _canvas;
        private MapObjectController _mapObjectController;
        public static List<MapObject> _allMapObjects;
        public static List<MapObjectType> _allMapObjectTypes;
        private string _currentUserRole;
        public static string _currentUsername = "";

        private IRepository _fileRepository;

        private Boolean _editMode = false;
        public Boolean EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                OnPropertyChanged("EditMode");
            }
        }

        private Boolean _isMenuOpened = false;
        public Boolean IsMenuOpened
        {
            get { return _isMenuOpened; }
            set
            {
                _isMenuOpened = value;
                OnPropertyChanged("IsMenuOpened");
            }
        }

        private int _selectedMenuOptionIndex = 0;
        public int SelectedMenuOptionIndex
        {
            get { return _selectedMenuOptionIndex; }
            set
            {
                PreviousSelectedMenuOptionIndex = _selectedMenuOptionIndex;
                _selectedMenuOptionIndex = value;
                OnPropertyChanged("SelectedMenuOptionIndex");
            }
        }

        private int? _previousSelectedMenuOptionIndex;
        public int? PreviousSelectedMenuOptionIndex
        {
            get { return _previousSelectedMenuOptionIndex; }
            set
            {
                _previousSelectedMenuOptionIndex = value;
                OnPropertyChanged("PreviousSelectedMenuOptionIndex");
                OnPropertyChanged("IsBackButtonEnabled");
            }
        }

        public Boolean IsBackButtonEnabled
        {
            get
            {
                return PreviousSelectedMenuOptionIndex.HasValue;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler _handler = this.PropertyChanged;
            if (_handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                _handler(this, e);
            }
        }

        private MapObjectEntity _displayMapObject;

        public MapObjectEntity DisplayMapObject
        {
            get
            {
                return _displayMapObject;
            }
            set
            {
                _displayMapObject = value;
                OnPropertyChanged("DisplayMapObject");
            }
        }

        private MapObject _selectedMapObject;

        public MapObject SelectedMapObject
        {
            get
            {
                return _selectedMapObject;
            }
            set
            {
                _selectedMapObject = value;
                OnPropertyChanged("SelectedMapObject");
            }
        }

        public List<MapObjectType> AllMapObjectTypes
        {
            get
            {
                return _allMapObjectTypes;
            }
            set
            {
                _allMapObjectTypes = value;
                OnPropertyChanged("AllMapObjectTypes");
            }
        }

        public ObservableCollection<EquipmentTypeForViewDTO> AllEquipmentTypes { get; set; }

        public List<ExaminationDTO> ExaminationSearchResults { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            AllMapObjectTypes = MapObjectType.AllMapObjectTypesAvailableForSearch;
            _canvas = this.Canvas;
            _fileRepository = new FileRepository("test.json");
            _mapObjectController = new MapObjectController(new MapObjectServices(_fileRepository));
            _allMapObjects = new List<MapObject>();
            this.DataContext = this;
            MockupObjects mockupObjects = new MockupObjects();
            _allMapObjects = mockupObjects.AllMapObjects;
            ChangeEditButtonVisibility();

            // uncomment only when you want to save the map for the first time
            saveMap();

            LoadInitialMapOnCanvas();
            SetDataToUIControls();


            /*
            ExaminationForReschedulingDTO examinationForReschedulingDTO = new ExaminationForReschedulingDTO(new DateTime(), new DateTime(), 9);
            List<ExaminationForReschedulingDTO> examinationsForReschedunling = new List<ExaminationForReschedulingDTO>();

            RenovatonService ren = new RenovatonService();
            AppointmentService aps = new AppointmentService();
            EquipmentService es = new EquipmentService();
            aps.DeleteAppointment(1);
            es.DeleteEquipmentTransfer(1);
            ren.DeleteRenovation(1);
            BaseRenovationDTO dto = new BaseRenovationDTO(50, new DateTime(2021, 3, 12, 13, 10, 0, DateTimeKind.Utc), new DateTime(2021, 3, 12, 16, 45, 0, DateTimeKind.Utc), "menjanje poda", TypeOfRenovation.REGULAR_RENOVATION);
            // DivideRenovationDTO mrg = new DivideRenovationDTO(dto, " soba za konsultacije"," soba za pregled", TypeOfMapObject.EXAMINATION_ROOM, TypeOfMapObject.HOSPITALIZATION_ROOM);
            //MergeRenovationDTO mrg = new MergeRenovationDTO(dto, 16, "soba za hospitalizovanje pacijenata", TypeOfMapObject.HOSPITALIZATION_ROOM);
            bool add = ren.ScheduleBaseRenovation(dto);
            if (!add)
            {
                foreach (RenovationPeriodDTO period in ren.GetBaseRenovationAlternativeAppointments(dto))
                {
                    Console.WriteLine(period.StartTime + " ------ " + period.EndTime);
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("else");
            }

            examinationsForReschedunling.Add(examinationForReschedulingDTO);
            EmergencyAppointmentSearchResultsDataGrid.ItemsSource = examinationsForReschedunling;

            /*
            AppointmentSearchWithPrioritiesDTO appointmentSearch = new AppointmentSearchWithPrioritiesDTO
            {
                InitialParameters = new BasicAppointmentSearchDTO(patientCardId: 2, doctorJmbg: "0909965768767", requiredEquipmentTypes: new List<int>(),
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0, DateTimeKind.Utc), latestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 0, 0, DateTimeKind.Utc)),
                Priority = SearchPriority.Date,
                SpecialtyId = 1
            };

            AppointmentService app = new AppointmentService();

            app.GetEmergencyAppointments(appointmentSearch);*/

            /* RenovatonService service = new RenovatonService();
             BaseRenovationDTO dto = new BaseRenovationDTO(9,new DateTime(2021, 3, 1, 13, 10, 0, DateTimeKind.Utc), new DateTime(2021, 3, 1, 16, 45, 0, DateTimeKind.Utc),"krecenje",TypeOfRenovation.REGULAR_RENOVATION);
             bool add = service.ScheduleBaseRenovation(dto);
             if (!add)
             {
                 foreach (RenovationPeriodDTO period in service.GetAlternativeAppointments(dto))
                 {
                     Console.WriteLine(period.StartTime + " ------ " + period.EndTime);
                     Console.WriteLine("");
                 }
             } else
             {
                 Console.WriteLine("else");
             }  */
        }


        public MainWindow(string currentUserRole, string currentUsername)
        {
            InitializeComponent();
            this.DataContext = this;
            AllMapObjectTypes = MapObjectType.AllMapObjectTypesAvailableForSearch;
            _currentUserRole = currentUserRole;
            _currentUsername = currentUsername;
            _canvas = this.Canvas;
            _fileRepository = new FileRepository("test.json");
            _mapObjectController = new MapObjectController(new MapObjectServices(_fileRepository));
            _allMapObjects = new List<MapObject>();
            this.DataContext = this;
            MockupObjects mockupObjects = new MockupObjects();
            _allMapObjects = mockupObjects.AllMapObjects;
            ChangeEditButtonVisibility();
            // uncomment only when you want to save the map for the first time
            saveMap();

            LoadInitialMapOnCanvas();

            RestrictUsersAccessBasedOnRole();

            SetDataToUIControls();
        }

        public void SetDataToUIControls()
        {
            SetValuesInDoctorSpecialtyComboBoxForAppointmentSearch();
            SetValuesInPatientComboBoxForAppointmentSearch();
            SetSelectableEquipmentForAppointmentSearch();
        }


        public void SetValuesInDoctorSpecialtyComboBoxForAppointmentSearch()
        {
            DoctorService doctorService = new DoctorService();
            List<SpecialtyDTO> allDoctorSpecialties = doctorService.GetAllSpecialties();

            AppointmentDoctorSpecializationComboBox.ItemsSource = allDoctorSpecialties;
        }

        public void SetValuesInPatientComboBoxForAppointmentSearch()
        {
            PatientService patientService = new PatientService();
            List<PatientBasicDTO> allPatients = patientService.GetAllPatients();

            AppointmentSearchPatientComboBox.ItemsSource = allPatients;
        }

        public void SetSelectableEquipmentForAppointmentSearch()
        {
            AllEquipmentTypes = new ObservableCollection<EquipmentTypeForViewDTO>();

            EquipmentTypeService equipmentTypeService = new EquipmentTypeService();
            List<EquipmentTypeDTO> equipmentTypes = equipmentTypeService.GetEquipmentTypes();
            if (equipmentTypes != null)
                foreach (EquipmentTypeDTO equipmentType in equipmentTypes)
                {
                    AllEquipmentTypes.Add(new EquipmentTypeForViewDTO(equipmentType, false));
                }
        }


        private List<ExaminationDTO> RemoveAppointmentsWithDuplicateTimes(List<ExaminationDTO> freeAppointments)
        {
            List<ExaminationDTO> appointmentsWithoutDuplicateTimes = new List<ExaminationDTO>();
            foreach (ExaminationDTO appointment in freeAppointments)
            {
                bool found = appointmentsWithoutDuplicateTimes.Any(a => a.DateTime.Equals(appointment.DateTime));
                if (!found)
                    appointmentsWithoutDuplicateTimes.Add(appointment);
            }

            return appointmentsWithoutDuplicateTimes;
        }

        private ICollection<int> GetFreeRoomsByAppointment(List<ExaminationDTO> freeAppointments, ExaminationDTO appointment)
        {
            if (appointment == null)
            {
                return new List<int>();
            }

            ICollection<int> freeRooms = new List<int>();

            foreach (var e in freeAppointments.Where(e => e.DateTime.Equals(appointment.DateTime)))
                freeRooms.Add(e.RoomId);

            return freeRooms;
        }


        private void RestrictUsersAccessBasedOnRole()
        {

            if (!String.IsNullOrEmpty(_currentUserRole))
            {
                if (_currentUserRole.Equals("Patient"))
                {
                    ObjectEquipmentAndMedicinePanel.Visibility = Visibility.Collapsed;
                    SearchEquipmentAndMedicineMenuItem.Visibility = Visibility.Collapsed;
                }

                if (!_currentUserRole.Equals("Manager"))
                {
                    EditObjectButton.Visibility = Visibility.Collapsed;
                }

                if (!_currentUserRole.Equals("Secretary"))
                {
                    SearchAppointmentsPanel.Visibility = Visibility.Collapsed;
                    SearchAppointmentsMenuItem.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void LoadInitialMapOnCanvas()
        {
            _allMapObjects = _fileRepository.LoadMap().ToList();

            foreach (MapObject mapObject in _allMapObjects)
            {
                LoadAllMapObjectsExceptRooms(mapObject);
                LoadGroundLevelFromBuildings(mapObject);
            }

        }

        private void LoadAllMapObjectsExceptRooms(MapObject mapObject)
        {
            if (mapObject.MapObjectEntity.GetType() != typeof(Room))
            {
                mapObject.AddToCanvas(_canvas);
            }
        }

        private void LoadGroundLevelFromBuildings(MapObject mapObject)
        {
            if (mapObject.MapObjectEntity.GetType() == typeof(Room) && ((Room)mapObject.MapObjectEntity).Floor == 0)
            {
                mapObject.AddToCanvas(_canvas);
            }
        }

        private void saveMap()
            => _fileRepository.SaveMap(_allMapObjects);

        private void Edit_Display_Information(object sender, RoutedEventArgs e)
        {
            this.ObjectTypeChooserComboBox.SelectedItem = DisplayMapObject.MapObjectType.ObjectTypeFullName;
            EditMode = !EditMode;
        }

        public void ChangeEditButtonVisibility()
        {
            EditObjectButton.Visibility = Visibility.Collapsed;
            EditMode = false;

            if (!String.IsNullOrEmpty(_currentUserRole) && _currentUserRole.Equals("Manager") && (_selectedMapObject != null))
            {
                if (_selectedMapObject.MapObjectEntity.MapObjectType.TypeOfMapObject != TypeOfMapObject.ROAD)
                {
                    EditObjectButton.Visibility = Visibility.Visible;
                }
            }
        }

        private MapObjectType SettingTypeOfEditedMapObject()
        {
            MapObjectType type;
            if (ObjectTypeChooserComboBox.SelectedItem != null)
            {
                type = (MapObjectType)ObjectTypeChooserComboBox.SelectedItem;
            }
            else
            {
                type = SelectedMapObject.MapObjectEntity.MapObjectType;
            }
            return type;
        }

        private MapObject SettingMapObject(MapObjectType type)
        {
            MapObject objectToEdit = SelectedMapObject;
            DisplayMapObject.MapObjectType = type;
            objectToEdit.MapObjectEntity = DisplayMapObject;
            objectToEdit.Rectangle.Fill = type.ObjectTypeColor;
            return objectToEdit;
        }

        private MapObject CreateEditedObject()
        {
            MapObjectType type = SettingTypeOfEditedMapObject();
            MapObject objectToEdit = SettingMapObject(type);
            _mapObjectController.UpdateMapObject(objectToEdit);
            return objectToEdit;
        }

        private void ReloadMap(MapObject objectToEdit)
        {
            EditMode = !EditMode;
            Canvas.Children.Clear();
            SelectedMapObject.MapObjectEntity.MapObjectType = objectToEdit.MapObjectEntity.MapObjectType;
            LoadInitialMapOnCanvas();
        }

        private void Change_Display_Information(object sender, RoutedEventArgs e)
        {
            ReloadMap(CreateEditedObject());
        }

        private void Cancel_Editing_Mode(object sender, RoutedEventArgs e)
        {
            Canvas.Children.Clear();
            LoadInitialMapOnCanvas();
            EditMode = false;
            SelectedMapObject = null;
            DisplayMapObject = null;
            ChangeEditButtonVisibility();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            MapObject hoverMapObject = FindSelectedMapObject(e.GetPosition(this.Canvas));
            ApplyHoverEffectToObject(hoverMapObject);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _selectedMapObject = FindSelectedMapObject(e.GetPosition(this.Canvas));

            MarkAndDisplaySelectedMapObject(_selectedMapObject);

            ChangeEditButtonVisibility();
        }

        private void MarkAndDisplaySelectedMapObject(MapObject selectedMapObjectForDisplay)
        {
            ApplySelectionEffectToObject(selectedMapObjectForDisplay);

            if (selectedMapObjectForDisplay != null)
            {
                DisplayMapObject = selectedMapObjectForDisplay.MapObjectEntity;
                SelectedMapObject = selectedMapObjectForDisplay;

                ObjectEquipmentDataGrid.ItemsSource = SelectedMapObject.GetEquipmentInObject();
                ObjectMedicineDataGrid.ItemsSource = SelectedMapObject.GetMedicineInObject();

                AddEventsForEventSourcing(selectedMapObjectForDisplay);
            }
            else
            {
                SelectedMapObject = null;
                DisplayMapObject = null;
            }
        }

        private void AddEventsForEventSourcing(MapObject selectedMapObject)
        {
            if (selectedMapObject.CheckIsRoom())
            {
                ((Room)selectedMapObject.MapObjectEntity).AddRoomSelectionEvent();
            }
            else if (selectedMapObject.MapObjectEntity.MapObjectType.TypeOfMapObject == TypeOfMapObject.BUILDING)
            {
                ((Building)selectedMapObject.MapObjectEntity).AddBuildingSelectionEvent();
            }
        }


        private void ApplyHoverEffectToObject(MapObject hoverMapObject)
        {
            foreach (MapObject mapObject in _allMapObjects)
            {
                if (!mapObject.Equals(_selectedMapObject))
                {
                    RemoveShadowFromObjectAndDecreaseZIndex(mapObject);
                }
            }

            if (hoverMapObject != null)
            {
                if (hoverMapObject.MapObjectEntity.MapObjectType.TypeOfMapObject != TypeOfMapObject.ROAD)
                {
                    ApplyShadowToObjectAndIncreaseZIndex(hoverMapObject);
                }
            }
        }


        private void ApplySelectionEffectToObject(MapObject selectedMapObject)
        {
            foreach (MapObject mapObject in _allMapObjects)
            {
                RemoveShadowFromObjectAndDecreaseZIndex(mapObject);

                mapObject.Rectangle.Fill = mapObject.MapObjectEntity.MapObjectType.ObjectTypeColor;
                mapObject.MapObjectNameTextBlock.Foreground = Brushes.Black;
            }

            if (selectedMapObject != null)
            {
                if (selectedMapObject.MapObjectEntity.MapObjectType.TypeOfMapObject != TypeOfMapObject.ROAD)
                {
                    ApplyShadowToObjectAndIncreaseZIndex(selectedMapObject);

                    selectedMapObject.Rectangle.Fill = Brushes.MediumPurple;
                    selectedMapObject.MapObjectNameTextBlock.Foreground = Brushes.White;
                }
            }
        }


        private void ApplyShadowToObjectAndIncreaseZIndex(MapObject mapObject)
        {
            if (mapObject.MapObjectEntity.MapObjectType.TypeOfMapObject != TypeOfMapObject.BUILDING)
            {
                Panel.SetZIndex(mapObject.Rectangle, 1);
                Panel.SetZIndex(mapObject.MapObjectNameTextBlock, 1);
            }

            mapObject.Rectangle.Effect = new DropShadowEffect { Direction = 0, ShadowDepth = 0, BlurRadius = 14, Opacity = 1, Color = Colors.MediumPurple };
        }

        private void RemoveShadowFromObjectAndDecreaseZIndex(MapObject mapObject)
        {
            Panel.SetZIndex(mapObject.Rectangle, 0);
            Panel.SetZIndex(mapObject.MapObjectNameTextBlock, 0);

            mapObject.Rectangle.Effect = null;
        }


        private MapObject FindSelectedMapObject(Point mouseCursorCurrentPosition)
        {
            List<MapObject> MapObjectsThatContainMouseCursor = FindAllMapObjectsThatContainMouseCursor(mouseCursorCurrentPosition);
            MapObject selectedMapObject = FindMapObjectWithMinimumArea(MapObjectsThatContainMouseCursor);
            return selectedMapObject;
        }

        private List<MapObject> FindAllMapObjectsThatContainMouseCursor(Point mouseCursorCurrentPosition)
        {
            List<MapObject> MapObjectsThatContainMouseCursor = new List<MapObject>();

            foreach (MapObject mapObject in _allMapObjects)
            {
                if (_canvas.Children.Contains(mapObject.Rectangle) && IsMouseCursorInsideRectangle(mouseCursorCurrentPosition, mapObject.Rectangle))
                {
                    MapObjectsThatContainMouseCursor.Add(mapObject);
                }
            }

            return MapObjectsThatContainMouseCursor;
        }

        private MapObject FindMapObjectWithMinimumArea(List<MapObject> MapObjects)
        {
            if (MapObjects.Count != 0)
            {
                MapObject mapObjectWithMinimumArea = MapObjects[0];

                foreach (MapObject mapObject in MapObjects)
                {
                    if ((mapObject.MapObjectArea) < (mapObjectWithMinimumArea.MapObjectArea))
                    {
                        mapObjectWithMinimumArea = mapObject;
                    }
                }

                return mapObjectWithMinimumArea;
            }
            else
            {
                return null;
            }

        }

        private bool IsMouseCursorInsideRectangle(Point cursorPosition, Rectangle objectRectangle)
        {
            if ((cursorPosition.X > Canvas.GetLeft(objectRectangle) && cursorPosition.X < Canvas.GetLeft(objectRectangle) + objectRectangle.Width) && ((cursorPosition.Y > Canvas.GetTop(objectRectangle) && cursorPosition.Y < Canvas.GetTop(objectRectangle) + objectRectangle.Height)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            IsMenuOpened = false;
        }

        private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            IsMenuOpened = true;
        }

        private void ListViewExtendMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SelectedMenuOptionIndex)
            {
                case 0:
                    IsMenuOpened = false;
                    break;
                case 1:
                    IsMenuOpened = false;
                    break;
                case 2:
                    IsMenuOpened = false;
                    break;
                case 3:
                    IsMenuOpened = false;
                    break;
                case 4:
                    {
                        IsMenuOpened = false;
                        SelectedMenuOptionIndex = 0;
                        ShowAboutAppDialog();
                    }
                    break;
            }
        }

        private void ShowAboutAppDialog()
        {
            AboutAppDialog aboutAppDialog = new AboutAppDialog();
            aboutAppDialog.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PreviousSelectedMenuOptionIndex.HasValue)
                return;

            SelectedMenuOptionIndex = PreviousSelectedMenuOptionIndex.Value;
            PreviousSelectedMenuOptionIndex = null;
        }



        private void SearchMapObjectsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchObjectTypeComboBox.SelectedItem != null)
            {
                MapObjectType searchedMapObjectType = (MapObjectType)SearchObjectTypeComboBox.SelectedItem;
                List<MapObject> searchResultMapObjects = _mapObjectController.SearchMapObjects(searchedMapObjectType);
                ObjectSearchResultsDataGrid.ItemsSource = searchResultMapObjects;
            }
            else
            {
                return;
            }
        }

        public MapObject GetMapObjectById(long mapObjectId)
        {
            return _mapObjectController.GetMapObjectById(mapObjectId);
        }

        public void ShowSelectedSearchResultObjectOnMap(MapObject selectedSearchResultObject)
        {
            if (selectedSearchResultObject.MapObjectEntity.GetType() == typeof(Room))
            {
                long buildingId = ((Room)selectedSearchResultObject.MapObjectEntity).BuildingId;
                MapObject building = _mapObjectController.GetMapObjectById(buildingId);
                ((Building)building.MapObjectEntity).ShowFloorForSpecificMapObject(selectedSearchResultObject);
            }

            MarkAndDisplaySelectedMapObject(selectedSearchResultObject);
            ListViewExtendMenu.SelectedIndex = 0;
        }



        private void ShowSearchResultObjectOnMapButton_Click(object sender, RoutedEventArgs e)
        {
            MapObject selectedSearchResultObject = (MapObject)ObjectSearchResultsDataGrid.SelectedItem;

            ShowSelectedSearchResultObjectOnMap(selectedSearchResultObject);
        }


        private void SearchEquimentAndMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            String equipmentOrMedicineNameUserInput = EquipmentOrMedicineNameTextBox.Text.Trim().ToLower();
            if (String.IsNullOrEmpty(equipmentOrMedicineNameUserInput))
            {
                return;
            }

            EquipmentService equipmentService = new EquipmentService();
            DrugService drugService = new DrugService();


            List<EquipmentWithRoomDTO> searchResultEquipment = equipmentService.GetEquipmentWithRoomForSearchTerm(equipmentOrMedicineNameUserInput);
            List<DrugWithRoomDTO> searchResultDrugs = drugService.GetDrugsWithRoomForSearchTerm(equipmentOrMedicineNameUserInput);

            EquipmentSearchResultsDataGrid.ItemsSource = searchResultEquipment;
            MedicineSearchResultsDataGrid.ItemsSource = searchResultDrugs;
        }

        private void ShowEquipmentSearchResultObjectOnMapButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWithRoomDTO selectedSearchResultEquipmentDTO = (EquipmentWithRoomDTO)EquipmentSearchResultsDataGrid.SelectedItem;

            MapObject selectedSearchResultMapObject = _mapObjectController.GetMapObjectById(selectedSearchResultEquipmentDTO.RoomNumber);

            ShowSelectedSearchResultObjectOnMap(selectedSearchResultMapObject);
        }

        private void ShowMedicineSearchResultObjectOnMapButton_Click(object sender, RoutedEventArgs e)
        {
            DrugWithRoomDTO selectedSearchResultDrugDTO = (DrugWithRoomDTO)MedicineSearchResultsDataGrid.SelectedItem;

            MapObject selectedSearchResultMapObject = _mapObjectController.GetMapObjectById(selectedSearchResultDrugDTO.RoomNumber);

            ShowSelectedSearchResultObjectOnMap(selectedSearchResultMapObject);
        }


        private void AppointmentDoctorSpecializationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorService doctorService = new DoctorService();

            SpecialtyDTO selectedSpecialty = (SpecialtyDTO)AppointmentDoctorSpecializationComboBox.SelectedItem;
            List<DoctorDTO> doctorsWithSelectedSpecialty = doctorService.GetDoctorsBySpecialty(selectedSpecialty.Id);
            AppointmentDoctorComboBox.ItemsSource = doctorsWithSelectedSpecialty;
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
            if (AppointmentSearchStartDatePicker.SelectedDate == null || AppointmentSearchEndDatePicker.SelectedDate == null)
            {
                return false;
            }
            else if (AppointmentSearchStartDatePicker.SelectedDate != null && AppointmentSearchEndDatePicker.SelectedDate != null)
            {
                if (AppointmentSearchEndDatePicker.SelectedDate < AppointmentSearchStartDatePicker.SelectedDate)
                {
                    ShowDateIntervalInvalidDialog();
                    return false;
                }
                else if (AppointmentSearchEndDatePicker.SelectedDate == AppointmentSearchStartDatePicker.SelectedDate)
                {
                    if (AppointmentSearchEndTimePicker.SelectedTime <= AppointmentSearchStartTimePicker.SelectedTime)
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



        private void SearchAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIsDateTimeIntervalValid())
            {
                return;
            }

            DateTime startDate = AppointmentSearchStartDatePicker.SelectedDate.Value.Date;
            DateTime startTime = AppointmentSearchStartTimePicker.SelectedTime.Value;
           
            DateTime endDate = AppointmentSearchEndDatePicker.SelectedDate.Value.Date;
            DateTime endTime = AppointmentSearchEndTimePicker.SelectedTime.Value;

            DateTime appointmentSearchStartDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second, DateTimeKind.Utc);
            DateTime appointmentSearchEndDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second, DateTimeKind.Utc);
            
            string doctorJmbg = ((DoctorDTO)AppointmentDoctorComboBox.SelectedItem).Jmbg;
            int patientCardId = ((PatientBasicDTO)AppointmentSearchPatientComboBox.SelectedItem).PatientCardId;

            int doctorSpecialtyId = ((SpecialtyDTO)AppointmentDoctorSpecializationComboBox.SelectedItem).Id;

            SearchPriority searchPriority;
            if ((bool)AppointmentSearchDoctorPriorityRadioButton.IsChecked)
            {
                searchPriority = SearchPriority.Doctor;
            }
            else
            {
                searchPriority = SearchPriority.Date;
            }
         

            List<int> appointmentRequiredEquipmentTypes = new List<int>();
            foreach (EquipmentTypeForViewDTO equipmentType in AllEquipmentTypes)
            {
                if(equipmentType.IsSelected)
                {
                    appointmentRequiredEquipmentTypes.Add(equipmentType.EquipmentType.Id);
                }
            }

            AppointmentService appointmentService = new AppointmentService();
            AppointmentSearchWithPrioritiesDTO appointmentSearchParametersDTO = new AppointmentSearchWithPrioritiesDTO(
                new BasicAppointmentSearchDTO(patientCardId, doctorJmbg, appointmentRequiredEquipmentTypes, 
                appointmentSearchStartDateTime, 
                appointmentSearchEndDateTime), searchPriority, doctorSpecialtyId);
            
            List<ExaminationDTO> examinationDTOs =  appointmentService.GetFreeAppointments(appointmentSearchParametersDTO);
            ExaminationSearchResults = examinationDTOs;
            List<ExaminationDTO>  examinationDTOsWithoutDuplicates = RemoveAppointmentsWithDuplicateTimes(examinationDTOs);
            
            AppointmentSearchResultsDataGrid.ItemsSource = examinationDTOsWithoutDuplicates;

            AppointmentSearchScrollViewer.ScrollToBottom();
        }

        private void AppointmentSearchResultsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExaminationDTO selectedExaminationAppointment = (ExaminationDTO)AppointmentSearchResultsDataGrid.SelectedItem;

            SelectedAppointmentRoomComboBox.ItemsSource =  GetFreeRoomsByAppointment(ExaminationSearchResults, selectedExaminationAppointment);
        }

        private void SelectedAppointmentRoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedAppointmentRoomComboBox.SelectedItem != null)
            {
                int selectedAppointmentRoomId = (int)SelectedAppointmentRoomComboBox.SelectedItem;
                MapObject selectedAppointmentRoomMapObject = _mapObjectController.GetMapObjectById(selectedAppointmentRoomId);
                ShowSelectedSearchResultObjectOnMap(selectedAppointmentRoomMapObject);
            }
        }

        private void ScheduleAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if(AppointmentSearchResultsDataGrid.SelectedItem == null || SelectedAppointmentRoomComboBox.SelectedItem == null)
                return;
            
           

            ExaminationDTO selectedExamination = (ExaminationDTO)AppointmentSearchResultsDataGrid.SelectedItem;
            List<EquipmentInExaminationDTO> equipmentInExaminationDTOs = new List<EquipmentInExaminationDTO>();
            List<int> appointmentRequiredEquipmentTypes = new List<int>();
            foreach (EquipmentTypeForViewDTO equipmentType in AllEquipmentTypes)
            {
                if (equipmentType.IsSelected)
                {
                    appointmentRequiredEquipmentTypes.Add(equipmentType.EquipmentType.Id);
                   // equipmentInExaminationDTOs.Add(new EquipmentInExaminationDTO(equipmentType.EquipmentType.Id,selectedExamination.))
                }
            }
            int selectedRoomForExaminationId = (int)SelectedAppointmentRoomComboBox.SelectedItem;
            selectedExamination.RoomId = selectedRoomForExaminationId;

            new AppointmentService().AddExamination(selectedExamination, appointmentRequiredEquipmentTypes);
            InfoDialog infoDialog = new InfoDialog("Uspešno ste zakazali pregled.");
            infoDialog.ShowDialog();

            ClearAppointmentSearchFields();
        }

        private void ClearAppointmentSearchFields()
        {
            AppointmentSearchStartDatePicker.SelectedDate = null;
            AppointmentSearchStartTimePicker.SelectedTime = null;
            AppointmentSearchEndDatePicker.SelectedDate = null;
            AppointmentSearchEndTimePicker.SelectedTime = null;
            
            AppointmentDoctorComboBox.SelectedItem = null;
            AppointmentSearchTimePriorityRadioButton.IsChecked = false;
            AppointmentSearchDoctorPriorityRadioButton.IsChecked = false;
            

            foreach(EquipmentTypeForViewDTO equipmentType in AllEquipmentTypes)
            {
                equipmentType.IsSelected = false;
            }

            AppointmentSearchResultsDataGrid.ItemsSource = new List<ExaminationDTO>();

        }


        private void AppointmentSectionBackToTopButton_Click(object sender, RoutedEventArgs e)
        {
            AppointmentSearchScrollViewer.ScrollToTop();
        }

        private void CreatePatientAccountButton_Click(object sender, RoutedEventArgs e)
        {
            CreatePatientGuestAccountDialog createPatientGuestAccountDialog = new CreatePatientGuestAccountDialog();
            createPatientGuestAccountDialog.ShowDialog();
            SetValuesInPatientComboBoxForAppointmentSearch();
        }

        private void EmergencyAppointmentSearchResultsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ScheduleEmergencyAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            int patientCardId = ((PatientBasicDTO)AppointmentSearchPatientComboBox.SelectedItem).PatientCardId;

            int doctorSpecialtyId = ((SpecialtyDTO)AppointmentDoctorSpecializationComboBox.SelectedItem).Id;

            List<int> appointmentRequiredEquipmentTypes = new List<int>();
            foreach (EquipmentTypeForViewDTO equipmentType in AllEquipmentTypes)
            {
                if (equipmentType.IsSelected)
                {
                    appointmentRequiredEquipmentTypes.Add(equipmentType.EquipmentType.Id);
                }
            }


            AppointmentSearchWithPrioritiesDTO appointmentSearchParametersDTO = new AppointmentSearchWithPrioritiesDTO
            {
                InitialParameters = new BasicAppointmentSearchDTO(patientCardId, doctorJmbg: "0909965768767", appointmentRequiredEquipmentTypes,
                earliestDateTime: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 29, 7, 0, 0, DateTimeKind.Utc), latestDateTime: new DateTime()),
                Priority = SearchPriority.Date,
                SpecialtyId = doctorSpecialtyId
            };

            AppointmentService appointmentService = new AppointmentService();


            
            if (EmergencyAppointmentSearchResultsDataGrid.SelectedItem == null)
            {
                List<EmergencyExaminationDTO> emergencyExaminationSearchResults = appointmentService.GetEmergencyAppointments(appointmentSearchParametersDTO);

                if (emergencyExaminationSearchResults.Count == 1 && emergencyExaminationSearchResults[0].UnchangedExamination.DateTime.Equals(emergencyExaminationSearchResults[0].ShiftedExamination.DateTime))
                {

                    appointmentService.AddExamination(emergencyExaminationSearchResults[0].UnchangedExamination, appointmentRequiredEquipmentTypes);

                    
                    InfoDialog infoDialog = new InfoDialog(String.Format("Uspešno ste zakazali pregled!{0}{0}Nakon zatvaranja ovog dijaloga, biće prikazano više informacija o zakazanom pregledu.", Environment.NewLine));
                    infoDialog.ShowDialog();
                    ShowDialogWithMoreDetailsAboutScheduledExamination(emergencyExaminationSearchResults[0].UnchangedExamination);

                    ClearAppointmentSearchFields();
                }
                else
                {
                    EmergencyAppointmentSearchResultsDataGrid.ItemsSource = emergencyExaminationSearchResults;

                    InfoDialog infoDialog = new InfoDialog("Nažalost u skorijem periodu nema slobodnih termina koji odgovaraju unetim parametrima. Možete pomeriti neki od postojećih termina. Termini su sortirani u tabeli po pogodnosti za pomeranje. Termin koji je najpogodniji za pomeranje je posebno naglašen.");
                    infoDialog.ShowDialog();
                }
            }
            else
            {
                EmergencyExaminationDTO selectedEmergencyExamination = (EmergencyExaminationDTO)EmergencyAppointmentSearchResultsDataGrid.SelectedItem;

                ExaminationDTO unchangedExamination = selectedEmergencyExamination.UnchangedExamination;

                ExaminationDTO examinationForScheduleDTO = new ExaminationDTO(unchangedExamination.DateTime, unchangedExamination.Doctor, unchangedExamination.RoomId, patientCardId);

                RescheduleExaminationDTO rescheduleExaminationDTO = new RescheduleExaminationDTO(examinationForScheduleDTO, selectedEmergencyExamination.UnchangedExamination, selectedEmergencyExamination.ShiftedExamination);

                appointmentService.RescheduleExamination(rescheduleExaminationDTO);

                
                InfoDialog infoDialog = new InfoDialog(String.Format("Uspešno ste zakazali pregled!{0}{0}Nakon zatvaranja ovog dijaloga, biće prikazano više informacija o zakazanom pregledu.", Environment.NewLine));
                infoDialog.ShowDialog();
                ShowDialogWithMoreDetailsAboutScheduledExamination(examinationForScheduleDTO);

                ClearAppointmentSearchFields();
            }
            
            
        }

        private void ShowDialogWithMoreDetailsAboutScheduledExamination(ExaminationDTO examinationDTO)
        {
            AppointmentInRoomMoreDetailsDialog appointmentInRoomMoreDetailsDialog = new AppointmentInRoomMoreDetailsDialog(examinationDTO);
            appointmentInRoomMoreDetailsDialog.ShowDialog();
        }



        private void OpenEquipmentRelocationDialogButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentDTO selectedEquipment = (EquipmentDTO)ObjectEquipmentDataGrid.SelectedItem;
            EquipmentWithRoomDTO equipmentWithRoomForRelocationDTO = new EquipmentWithRoomDTO(selectedEquipment.Id, (int)SelectedMapObject.MapObjectEntity.Id, selectedEquipment.Quantity, selectedEquipment.Type.Name);

            EquipmentRelocationSchedulingDialog equipmentRelocationSchedulingDialog = new EquipmentRelocationSchedulingDialog(equipmentWithRoomForRelocationDTO);
            equipmentRelocationSchedulingDialog.Owner = Window.GetWindow(this);
            equipmentRelocationSchedulingDialog.ShowDialog();
        }

        private void ShowEmergencyAppointmentSearchResultObjectOnMapButton_Click(object sender, RoutedEventArgs e)
        {
            EmergencyExaminationDTO selectedSearchEmergencyExaminationDTO = (EmergencyExaminationDTO)EmergencyAppointmentSearchResultsDataGrid.SelectedItem;

            MapObject selectedSearchResultMapObject = _mapObjectController.GetMapObjectById(selectedSearchEmergencyExaminationDTO.UnchangedExamination.RoomId);

            ShowSelectedSearchResultObjectOnMap(selectedSearchResultMapObject);

        }

        private void ShowRoomScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            ScheduleForRoomDialog scheduleForRoomDialog = new ScheduleForRoomDialog((int)SelectedMapObject.MapObjectEntity.Id);
            scheduleForRoomDialog.ShowDialog();
        }


        private void ShowRenovationSchedulingDialogButton_Click(object sender, RoutedEventArgs e)
        {
            RenovationSchedulingDialog renovationSchedulingDialog = new RenovationSchedulingDialog((int)SelectedMapObject.MapObjectEntity.Id);
            renovationSchedulingDialog.Owner = Window.GetWindow(this);
            renovationSchedulingDialog.ShowDialog();
        }

    }
}

