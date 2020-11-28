using Controller.DrugAndTherapy;
using Controller.ExaminationAndPatientCard;
using Controller.PlacementInARoomAndRenovationPeriod;
using Controller.UsersAndWorkingTime;
using Model.Doctor;
using Model.Secretary;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        private PatientCard p;
        private PatientCardController pc = new PatientCardController();
        private ExaminationController ec = new ExaminationController();
        private PlacementInSickRoomController prc = new PlacementInSickRoomController();
        private TherapyController tc = new TherapyController();

        public EditPatient(PatientCard patientCard)
        {
            InitializeComponent();
            p = patientCard;
            txtPatient.Text = p.patient.Name + " " + p.patient.Surname + " " + p.patient.Jmbg;
            txtLbo.Text = p.Lbo;
            txtAlergija.Text = p.Alergies;
            List<string> bloods = new List<string>();
            bloods.Add(BloodType.A.ToString());
            bloods.Add(BloodType.B.ToString());
            bloods.Add(BloodType.AB.ToString());
            bloods.Add(BloodType.O.ToString());

            comboKrvna.DataContext = bloods;
            comboKrvna.SelectedItem = p.BloodType.ToString();

            List<string> rhs = new List<string>();
            rhs.Add(RhFactorType.Pozitivna.ToString());
            rhs.Add(RhFactorType.Negativna.ToString());

            comboRh.DataContext = rhs;
            comboRh.SelectedItem = p.RhFactor.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"^[a-z]{2}[0-9]{3}$");
            Match match = regex.Match(txtLbo.Text);       
            if (match.Success == false) {
                MessageBox.Show("Broj osiguranja mora biti u formatu xxYYY (x-slovo, Y-cifra).", "Upozorenje!",
                      MessageBoxButton.OK, MessageBoxImage.Warning);

                txtLbo.Focus();
                return;
            }

            PatientCard patinetC = p;
            patinetC.Lbo = txtLbo.Text;
            patinetC.Alergies = txtAlergija.Text;
            if (comboKrvna.SelectedItem.ToString().Equals("A"))
            {
                patinetC.BloodType = BloodType.A;
            }else if (comboKrvna.SelectedItem.ToString().Equals("B"))
            {
                patinetC.BloodType = BloodType.B;
            }else if (comboKrvna.SelectedItem.ToString().Equals("AB"))
            {
                patinetC.BloodType = BloodType.AB;
            }
            else
            {
                patinetC.BloodType = BloodType.O;
            }

            if (comboRh.SelectedItem.ToString().Equals("Pozitivna"))
            {
                patinetC.RhFactor = RhFactorType.Pozitivna;
            }
            else
            {
                patinetC.RhFactor = RhFactorType.Negativna;
            }              

            pc.EditPatientCard(patinetC);
           
            List<Examination> examinations = ec.ViewExaminationsByPatient(patinetC.patient.Jmbg);
            foreach(Examination exm in examinations)
            {
                    exm.patientCard = patinetC;
                    ec.EditExamination(exm);

            }

            List<PlacemetnInARoom> placements = prc.ViewPatientPlacements(patinetC.patient.Jmbg);
            foreach (PlacemetnInARoom p in placements)
            {
                p.patientCard = patinetC;
                prc.EditPlacement(p);

            }

            List<Therapy> therapies = tc.ViewAllTherapyByPatient(patinetC.patient.Jmbg);
            foreach (Therapy t in therapies)
            {
                t.patientCard = patinetC;
                tc.EditTherapy(t);

            }


            this.Close();

        }
    }
}
