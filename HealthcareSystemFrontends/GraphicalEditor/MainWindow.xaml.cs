using GraphicalEditor.Constants;
using GraphicalEditor.Controllers;
using GraphicalEditor.DTO;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.Equipments;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
using GraphicalEditor.Service;
using GraphicalEditor.Services;
using GraphicalEditor.Services.Interface;
using GraphicalEditorServer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
            //saveMap();

            LoadInitialMapOnCanvas();
            // uncomment only the first time you start the project in order
            // to populate DB with start data
            InitializeDatabaseData initializeDatabaseData = new InitializeDatabaseData();
            //initializeDatabaseData.InitiliazeData();


            EquipementService equipementService = new EquipementService();
            /*List<EquipmentWithRoomDTO> result = equipementService.GetEquipmentWithRoomForSearchTerm("bed");
            foreach(EquipmentWithRoomDTO res in result)
            {
                Console.WriteLine(res.IdEquipment);
                Console.WriteLine(res.RoomNumber);
                Console.WriteLine("---");
            }*/

        }

        public MainWindow(string currentUserRole)
        {
            InitializeComponent();
            this.DataContext = this;
            AllMapObjectTypes = MapObjectType.AllMapObjectTypesAvailableForSearch;
            _currentUserRole = currentUserRole;
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

            }
            else
            {
                SelectedMapObject = null;
                DisplayMapObject = null;
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
                    IsMenuOpened = false;
                    break;
            }
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

        private void ShowSelectedSearchResultObjectOnMap(MapObject selectedSearchResultObject)
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

            EquipementService equipmentService = new EquipementService();
            DrugService drugService = new DrugService();


            List<EquipmentWithRoomDTO> searchResultEquipment = equipmentService.GetEquipmentWithRoomForSearchTerm(equipmentOrMedicineNameUserInput);
            List<DrugWithRoomDTO> searchResultDrugs = drugService.GetDrugsWithRoomForSearchTerm(equipmentOrMedicineNameUserInput);

            EquipmentSearchResultsDataGrid.ItemsSource = searchResultEquipment;
            MedicineSearchResultsDataGrid.ItemsSource = searchResultDrugs;
        }

        private void ShowEquipmentSearchResultObjectOnMapButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowMedicineSearchResultObjectOnMapButton_Click(object sender, RoutedEventArgs e)
        {

        }

      
    }
}

