using Controller.DrugAndTherapy;
using Controller.ExaminationAndPatientCard;
using Controller.PlacementInARoomAndRenovationPeriod;
using Controller.UsersAndWorkingTime;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
using ProjekatZdravoKorporacija.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for MyMessageBoxDelete.xaml
    /// </summary>
    public partial class MyMessageBoxDelete : Window
    {
        Patient patient = null;
        private static PatientController patientController = new PatientController();
        private UserController userController = new UserController(patientController);
        PatientCardController patientCardController = new PatientCardController();
        ExaminationController examinationController = new ExaminationController();
        TherapyController therapyController = new TherapyController();
        PlacementInSickRoomController placementInSickRoomController = new PlacementInSickRoomController();
        public MyMessageBoxDelete(Object o,Page p)
        {
            InitializeComponent();

            if(p.GetType() == typeof(PatientView))
            {
                patient = (Patient)o;
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (patient != null)
            {
                examinationController.DeletePatientExaminations(patient.Jmbg);
                patientCardController.DeletePatientCard(patient.Jmbg);
                userController.DeleteProfile(patient.Jmbg);
                therapyController.DeletePatientTherapies(patient.Jmbg);
                placementInSickRoomController.DeletePatientPlacements(patient.Jmbg);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new PatientView();
                    }
                }
                this.Close();
            }
        }
    }
}
