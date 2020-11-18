using Controller.PlacementInARoomAndRenovationPeriod;
using Model.Doctor;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ViewPlacements.xaml
    /// </summary>
    public partial class ViewPlacements : Window
    {
        private PlacementInSickRoomController pc = new PlacementInSickRoomController();
        public ViewPlacements(string name)
        {
            InitializeComponent();
            txtPatient.Text = name;

            string[] ss = name.Split(' ');         
            string jmbg = ss[ss.Length - 1];

            List<PlacemetnInARoom> placements = new List<PlacemetnInARoom>();
            placements = pc.ViewPatientPlacements(jmbg);

            List<PlacementDTO> pDTOs = new List<PlacementDTO>();
            foreach(PlacemetnInARoom p in placements)
            {
                PlacementDTO pDTO = new PlacementDTO();
                pDTO.Id = p.Id;
                pDTO.patientCard = p.patientCard.Patient.Name;
                pDTO.room = p.room.Number;
                pDTO.DateOfPlacement = p.DateOfPlacement.ToShortDateString();
                pDTO.DateOfDismison = p.DateOfDismison.ToShortDateString();

                pDTOs.Add(pDTO);
            }

            DataGrid1.ItemsSource = pDTOs;
           
        }
    }
}
