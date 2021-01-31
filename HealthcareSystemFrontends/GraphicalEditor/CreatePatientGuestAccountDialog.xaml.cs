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
using GraphicalEditor.Service;

namespace GraphicalEditor
{
    /// <summary>
    /// Interaction logic for CreatePatientGuestAccountDialog.xaml
    /// </summary>
    public partial class CreatePatientGuestAccountDialog : Window
    {

        PatientService _patientService;

        public CreatePatientGuestAccountDialog()
        {
            InitializeComponent();

            _patientService = new PatientService();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private GuestPatientDTO CreateGuestPatientDTOFromUserInput()
        {
            string PatientName = PatientNameInputTextBox.Text;
            string PatientSurname = PatientSurnameInputTextBox.Text;
            string PatientJmbg = PatientJMBGInputTextBox.Text;

            return new GuestPatientDTO(PatientName, PatientSurname, PatientJmbg);
        }

        private void ShowCreatedPatientAccountInfoDialog()
        {
            InfoDialog infoDialog = new InfoDialog("Uspešno ste kreirali nalog pacijentu!");
            infoDialog.ShowDialog();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            GuestPatientDTO guestPatientDTO = CreateGuestPatientDTOFromUserInput();
            _patientService.CreatePatientGuestAccount(guestPatientDTO);
            this.Close();
            ShowCreatedPatientAccountInfoDialog();
        }
    }
}
