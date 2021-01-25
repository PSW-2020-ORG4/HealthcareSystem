﻿using GraphicalEditor.Service;
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
using GraphicalEditor.DTO;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for ScheduleForRoomDialog.xaml
    /// </summary>
    public partial class ScheduleForRoomDialog : Window
    {
        RoomService _roomService;
        public int RoomId { get; set; }

        public ScheduleForRoomDialog(int roomId)
        {
            InitializeComponent();
            DataContext = this;

            _roomService = new RoomService();

            RoomId = roomId;
            GetDataAndDisplayItInScheduledActionsDataGrid();
        }

        private void GetDataAndDisplayItInScheduledActionsDataGrid()
        {
            List<RoomSchedulesDTO> roomSchedulesDTOs = _roomService.GetRoomSchedules(RoomId);
            RoomScheduledActionsDataGrid.ItemsSource = roomSchedulesDTOs;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowDetailsForScheduledActionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelScheduledActionButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
