using Controller.PlacementInARoomAndRenovationPeriod;
using Controller.RoomAndEquipment;
using Model.Manager;
using ProjekatZdravoKorporacija.ModelDTO;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for Rooms.xaml
    /// </summary>
    public partial class RoomView : Page
    {
        List<RoomDTO> roomsDTO = new List<RoomDTO>();
        RoomController roomController = new RoomController();
        RenovationPeriod renovationPeriod;
        RenovationPeriodController periodController = new RenovationPeriodController();
        List<Room> rooms = new List<Room>();
        public RoomView()
        {
            InitializeComponent();
            txtNameToSearch.Focus();

            rooms.AddRange(roomController.ViewRooms());

            foreach(Room r in rooms)
            {
                if (r.Usage != TypeOfUsage.Soba_za_lezanje)
                {
                    RoomDTO roomDTO = new RoomDTO();

                    roomDTO.NumberOfRoom = r.Number.ToString();

                    roomDTO.isRenovate = r.Renovation;

                    if (r.Usage == TypeOfUsage.Soba_za_pregled)
                    {
                        roomDTO.Purpose = "Soba za pregled";
                    }
                    else
                    {
                        roomDTO.Purpose = "Operaciona sala";
                    }

                    renovationPeriod = periodController.ViewRenovationByRoomNumber(r.Number);           

                    if (renovationPeriod != null)
                    {
                        roomDTO.StartRenovationDate = renovationPeriod.BeginDate.ToShortDateString();
                        roomDTO.EndRenovationDate = renovationPeriod.EndDate.ToShortDateString();
                    }
                    else
                    {
                        roomDTO.StartRenovationDate = "-";
                        roomDTO.EndRenovationDate = "-";
                    }
                    roomsDTO.Add(roomDTO);
                }
            }

            dgRooms.ItemsSource = roomsDTO;
        }

        private void txtNameToSearch_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = roomsDTO.Where(room => room.NumberOfRoom.Contains(txtNameToSearch.Text) || room.Purpose.Contains(txtNameToSearch.Text)
                                        || room.StartRenovationDate.Contains(txtNameToSearch.Text) || room.EndRenovationDate.Contains(txtNameToSearch.Text));

            dgRooms.ItemsSource = filtered;
        }

    }
}
