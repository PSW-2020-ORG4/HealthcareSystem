using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas _canvas;
        Point _mouseCurrentPosition;

        

        public List<MapObject> AllMapObjects { get; set; }

        private IRepository repository;

        public MainWindow()
        {
            InitializeComponent();
            _canvas = this.Canvas;
            repository = new FileRepository("test.json");
            AllMapObjects = new List<MapObject>();

            MocupObjects mockupObjects = new MocupObjects();
            AllMapObjects = mockupObjects.getAllMapObjects();
            //saveMap();
            LoadMapOnCanvas();
        }

        private void LoadMapOnCanvas()
        {
            AllMapObjects = repository.LoadMap().ToList();
            foreach (MapObject mapObject in AllMapObjects)
            {
                mapObject.AddToCanvas(_canvas);
            }
        }

        private void saveMap()
            => repository.SaveMap(AllMapObjects);



        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseCurrentPosition = e.GetPosition(this.Canvas);
           
            findSelectedMapObject();
        }
        
        private MapObject findSelectedMapObject()
        {
            List<MapObject> MapObjectsThatContainMouseCursor = findAllMapObjectsThatContainMouseCursor(AllMapObjects);

            MapObject selectedMapObject = findMapObjectWithMinimumArea(MapObjectsThatContainMouseCursor);

            if (selectedMapObject != null)
            {
                selectedMapObject.Rectangle.Effect = new DropShadowEffect { Direction = 0, ShadowDepth = 0, BlurRadius = 14, Opacity = 1, Color = Colors.MediumPurple };
            }

            return selectedMapObject;
        }

        private List<MapObject> findAllMapObjectsThatContainMouseCursor(List<MapObject> AllMapObjects)
        {
            List<MapObject> MapObjectsThatContainMouseCursor = new List<MapObject>();

            foreach (MapObject mapObject in AllMapObjects)
            {
                if (isMouseXCoordinateInsideRectangle(_mouseCurrentPosition.X, mapObject.Rectangle) && isMouseYCoordinateInsideRectangle(_mouseCurrentPosition.Y, mapObject.Rectangle))
                {
                    MapObjectsThatContainMouseCursor.Add(mapObject);
                }
                else
                {
                    mapObject.Rectangle.Effect = null;
                }
            }

            return MapObjectsThatContainMouseCursor;
        }

        private MapObject findMapObjectWithMinimumArea(List<MapObject> MapObjects)
        {
            if (MapObjects.Count != 0)
            {
                MapObject mapObjectWithMinimumArea = MapObjects[0];

                foreach (MapObject mapObject in MapObjects)
                {
                    if ((mapObject.MapObjectArea) < (mapObjectWithMinimumArea.MapObjectArea))
                    {
                        mapObjectWithMinimumArea = mapObject;
                        mapObjectWithMinimumArea.Rectangle.Effect = new DropShadowEffect { Direction = 0, ShadowDepth = 0, BlurRadius = 14, Opacity = 1, Color = Colors.MediumPurple };
                    }
                    else
                    {
                        mapObject.Rectangle.Effect = null;
                    }
                }

                return mapObjectWithMinimumArea;
            }
            else
            {
                return null;
            }
            
        }


        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseCurrentPosition = e.GetPosition(this.Canvas);

            MapObject selectedMapObject = findSelectedMapObject();

            if(selectedMapObject != null)
            {
                TestText.Text = selectedMapObject.MapObjectEntity.MapObjectType.ObjectTypeFullName;
            }
            else
            {
                TestText.Text = "";
            }
            
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }


        private bool isMouseXCoordinateInsideRectangle(double mouseXCoordinate, Rectangle objectRectangle)
        {
            if (mouseXCoordinate > Canvas.GetLeft(objectRectangle) && mouseXCoordinate < Canvas.GetLeft(objectRectangle) + objectRectangle.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isMouseYCoordinateInsideRectangle(double mouseYCoordinate, Rectangle objectRectangle)
        {
            if ((mouseYCoordinate > Canvas.GetTop(objectRectangle)) && (mouseYCoordinate < Canvas.GetTop(objectRectangle) + objectRectangle.Height))
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
