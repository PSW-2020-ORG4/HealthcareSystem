using GraphicalEditor.Constants;
using GraphicalEditor.Controllers;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
using GraphicalEditor.Service;
using GraphicalEditor.Services;
using GraphicalEditor.Services.Interface;
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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _canvas = this.Canvas;
            _fileRepository = new FileRepository("test.json");
            _mapObjectController = new MapObjectController(new MapObjectServices(_fileRepository));
            _allMapObjects = new List<MapObject>();
            this.DataContext = this;
            MockupObjects mockupObjects = new MockupObjects();
            _allMapObjects = mockupObjects.AllMapObjects;
            //uncomment when you dont have anything in file
            saveMap();
            LoadInitialMapOnCanvas();
            ChangeEditButtonVisibility();
            //uncomment if your database is empty to fill database tables
            //ServerService server = new ServerService();
            //server.AddAllRooms();
            //server.AddEquipment();
            //server.AddEquipmentInRooms();
        }

        public MainWindow(string currentUserRole)
        {
            InitializeComponent();
            this.DataContext = this;
            _currentUserRole = currentUserRole;
            _canvas = this.Canvas;
            _fileRepository = new FileRepository("test.json");
            _mapObjectController = new MapObjectController(new MapObjectServices(_fileRepository));
            _allMapObjects = new List<MapObject>();
            this.DataContext = this;
            MockupObjects mockupObjects = new MockupObjects();
            _allMapObjects = mockupObjects.AllMapObjects;
            //uncomment when you dont have anything in file
            saveMap();
            LoadInitialMapOnCanvas();
            ChangeEditButtonVisibility();
            // uncomment if your database is empty to fill database tables
            /*ServerService server = new ServerService();
            server.AddAllRooms();
            server.AddEquipment();
            server.AddEquipmentInRooms();*/
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
            EditObjectButton.Visibility = Visibility.Hidden;
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
            ApplySelectionEffectToObject(_selectedMapObject);

            if (_selectedMapObject != null)
            {
                DisplayMapObject = _selectedMapObject.MapObjectEntity;
                SelectedMapObject = _selectedMapObject;
                var s = SelectedMapObject.GetConsumableEquipmentByRoomNumber();
                var k = SelectedMapObject.GetNonConsumableEquipmentByRoomNumber();
            }
            else
            {
                SelectedMapObject = null;
                DisplayMapObject = null;
            }

            ChangeEditButtonVisibility();
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
            int selectedMenuOptionIndex = ListViewExtendMenu.SelectedIndex;

            switch (selectedMenuOptionIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
    }
}

