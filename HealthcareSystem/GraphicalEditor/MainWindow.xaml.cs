using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
using System;
using System.Collections.Generic;
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
        public List<MapObject> AllMapObjects { get; set; }

        private IRepository repository;

        public MainWindow()
        {
            InitializeComponent();
            _canvas = this.Canvas;
            repository = new FileRepository("test.json");
            AllMapObjects = new List<MapObject>();

            //MocupObjects mockupObjects = new MocupObjects();
            //AllMapObjects = mockupObjects.getAllMapObjects();
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

        private void Change_Display_Information(object sender, RoutedEventArgs e)
        {           
        }        

    }

}
