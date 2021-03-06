﻿using System;
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
using GraphicalEditor.Service;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for AppointmentInRoomMoreDetailsDialog.xaml
    /// </summary>
    public partial class AppointmentInRoomMoreDetailsDialog : Window
    {
        public int ExaminationId { get; set; }
        public ExaminationDTO ExaminationForDisplay { get; set; }
        public PatientBasicDTO ExaminationPatient { get; set; }

        AppointmentService _appointmentService;
        PatientService _patientService;

        public AppointmentInRoomMoreDetailsDialog(int examinationId)
        {
            InitializeComponent();
            DataContext = this;

            ExaminationId = examinationId;

            _appointmentService = new AppointmentService();
            _patientService = new PatientService();

            ExaminationForDisplay = _appointmentService.GetExaminationById(ExaminationId);
            ExaminationPatient = _patientService.GetPatientByPatientCardId(ExaminationForDisplay.PatientCardId);
        }

        public AppointmentInRoomMoreDetailsDialog(ExaminationDTO examinationDTO)
        {
            InitializeComponent();
            DataContext = this;

            _patientService = new PatientService();

            ExaminationForDisplay = examinationDTO;
            ExaminationPatient = _patientService.GetPatientByPatientCardId(ExaminationForDisplay.PatientCardId);

            CancelExaminationButton.Visibility = Visibility.Collapsed;
        }

        private void ShowAppointmentSuccessfullyCancelledDialog()
        {
            InfoDialog infoDialog = new InfoDialog("Uspešno ste otkazali pregled!");
            infoDialog.ShowDialog();
        }

        private void CancelExaminationButton_Click(object sender, RoutedEventArgs e)
        {
            _appointmentService.DeleteAppointment(ExaminationId);
            this.Close();
            ShowAppointmentSuccessfullyCancelledDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
