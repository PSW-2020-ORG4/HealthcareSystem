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
        public List<MapObject> allMapObjects { get; set; }

        private IRepository repository;

        public MainWindow()
        {
            InitializeComponent();
            _canvas = this.Canvas;
            repository = new FileRepository("map.json");
            allMapObjects = new List<MapObject>();

            //saveMap();
            LoadMapOnCanvas();
        }

        private void LoadMapOnCanvas()
        {
            allMapObjects = repository.LoadMap().ToList();
            foreach (MapObject mapObject in allMapObjects)
            {
                mapObject.AddToCanvas(_canvas);
            }
        }

        private void saveMap()
            => repository.SaveMap(allMapObjects);

    }

}
