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

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for RenovationSchedulingDialog.xaml
    /// </summary>
    public partial class RenovationSchedulingDialog : Window
    {
        private IRepository _fileRepository;
        private MapObjectServices _mapObjectService;
        private MapObjectController _mapObjectController;
        public int InitialRoomId { get; set; }


        public RenovationSchedulingDialog(int roomId)
        {
            InitializeComponent();

            _mapObjectController = new MapObjectController(new MapObjectServices(_fileRepository));

            

            InitialRoomId = roomId;

            SetDataToUIControls();
        }

        private void SetDataToUIControls()
        {
            RenovationRoomComboBox.Items.Add(InitialRoomId);
        }

        private void SetDataToSecondRoomForMergingComboBox()
        {
            MapObject initialRoomForRenovation = ((MainWindow)this.Owner).GetMapObjectById(InitialRoomId);
            List<MapObject> potentialRoomsForMergingWithSelectedRoom= _mapObjectController.GetNeighboringRoomsForRoom(initialRoomForRenovation);

            SecondRoomForMergingComboBox.ItemsSource = potentialRoomsForMergingWithSelectedRoom;
        }

        private void IsComplexRenovationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetDataToSecondRoomForMergingComboBox();
        }

        private void ScheduleRenovationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RenovationBackToTopButton_Click(object sender, RoutedEventArgs e)
        {
            RenovationScrollViewer.ScrollToTop();
        }

        
    }
}
