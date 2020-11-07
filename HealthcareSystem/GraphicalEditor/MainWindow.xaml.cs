using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditor.Repository;
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
        public List<MapObject> AllMapObjects { get; set; }

        private IRepository repository;

        private Boolean _editMode = false;

        private string objectName;
        private string objectDescription;
        private string objectSpecification;

        public long Id
        {
            get { return 1; }
        }
        public string ObjectName
        {
            get { return "Examination room"; }
            set { this.objectName = value; }
        }
        public string ObjectDescription
        {
            get { return "Main doctor is Dr Davison"; }
            set { this.objectDescription = value; }
        }
        public string ObjectSpecification
        {
            get { return "215"; }
            set { this.objectSpecification = value; }
        }
        public Boolean EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public MainWindow()
        {            
            InitializeComponent();
            this.DataContext = this;
            _canvas = this.Canvas;
            repository = new FileRepository("test.json");
            AllMapObjects = new List<MapObject>();
            DataContext = this;

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
            string name = this.name.Text;
            string specification = this.specification.Text;
            string description = this.specification.Text;

            MapObject mapObject = AllMapObjects.FirstOrDefault(x => x.MapObjectEntity.Id == Id);
            if (mapObject != null)
            {
                
                //mapObject.Update(name, specification, description);
            }
            EditMode = !EditMode;
        }

        private void Cancel_Editing_Mode(object sender, RoutedEventArgs e)
        {

            EditMode = false;
        }

    }

}
