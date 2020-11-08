using GraphicalEditor.Constants;
using GraphicalEditor.Controllers;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
using GraphicalEditor.Repository.Intefrace;
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
        private Canvas _canvas;
        private MapObjectController _mapObjectController;
        private IMapObjectServices _mapObjectServices;
        private IMapObjectRepository _mapObjectRepository;
        public List<MapObject> AllMapObjects { get; set; }

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
            _mapObjectRepository = new MapObjectRepository("test.json");
            _mapObjectServices = new MapObjectServices(_mapObjectRepository);
            _mapObjectController = new MapObjectController(_mapObjectServices);
            AllMapObjects = new List<MapObject>();
            this.DataContext = this;
            MockupObjects mockupObjects = new MockupObjects();
            AllMapObjects = mockupObjects.AllMapObjects;
            //uncomment when you dont have anything in file
            saveMap();
            LoadMapOnCanvas();
        }

        private void LoadMapOnCanvas()
        {
            AllMapObjects = _fileRepository.LoadMap().ToList();
            foreach (MapObject mapObject in AllMapObjects)
            {
                mapObject.AddToCanvas(_canvas);
            }
        }


        private void saveMap()
            => _fileRepository.SaveMap(AllMapObjects);

        private void Edit_Display_Information(object sender, RoutedEventArgs e)
        {
            EditMode = !EditMode;
        }

        private void Change_Display_Information(object sender, RoutedEventArgs e)
        {          
            string selectedItem = (string)chooser.SelectedItem;
            DisplayMapObject.MapObjectType.ObjectTypeFullName = selectedItem;
            MapObject objectToEdit = SelectedMapObject;
            objectToEdit.MapObjectEntity = DisplayMapObject;
            MapObjectType type = new MapObjectType();
            type.ObjectTypeFullName = DisplayMapObject.MapObjectType.ObjectTypeFullName;           
            MapObjectEntity newEntity = new MapObjectEntity(type.TypeOfMapObject, DisplayMapObject.Description);
            MapObject newObject = new MapObject(newEntity, SelectedMapObject.MapObjectMetrics, SelectedMapObject.MapObjectDoor);
            newObject.MapObjectEntity.Id = SelectedMapObject.MapObjectEntity.Id;
            _mapObjectController.UpdateMapObject(objectToEdit);                      
            EditMode = !EditMode;
        }

        private void Cancel_Editing_Mode(object sender, RoutedEventArgs e)
        {
            EditMode = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            MapObject selectedMapObject = FindSelectedMapObject(e.GetPosition(this.Canvas));

            ApplyShadowEffectToObject(selectedMapObject);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MapObject selectedMapObject = FindSelectedMapObject(e.GetPosition(this.Canvas));
            if (selectedMapObject != null)
            {
                DisplayMapObject = selectedMapObject.MapObjectEntity;
                SelectedMapObject = selectedMapObject;
            }
            else
            {
                DisplayMapObject = null;
            }
        }

        private void ApplyShadowEffectToObject(MapObject selectedMapObject)
        {
            if (selectedMapObject != null)
            {
                if (selectedMapObject.MapObjectEntity.MapObjectType.TypeOfMapObject != TypeOfMapObject.ROAD) 
                {
                    selectedMapObject.Rectangle.Effect = new DropShadowEffect { Direction = 0, ShadowDepth = 0, BlurRadius = 14, Opacity = 1, Color = Colors.MediumPurple }; 
                }
              

                foreach (MapObject mapObject in AllMapObjects)
                {
                    if (!mapObject.Equals(selectedMapObject))
                    {
                        mapObject.Rectangle.Effect = null;
                    }
                }
            }
            else
            {
                foreach (MapObject mapObject in AllMapObjects)
                {
                    mapObject.Rectangle.Effect = null;
                }
            }
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

            foreach (MapObject mapObject in AllMapObjects)
            {
                if (IsMouseCursorInsideRectangle(mouseCursorCurrentPosition, mapObject.Rectangle))
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
    }
}

