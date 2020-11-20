using Controller.ExaminationAndPatientCard;
using Model.Enums;
using Model.PerformingExamination;
using ProjekatZdravoKorporacija.ModelDTO;
using ProjekatZdravoKorporacija.View;
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

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for CanceledExaminations.xaml
    /// </summary>
    public partial class CanceledExaminationsView : Page
    {
        List<ExaminationDTO> canceledExaminations = new List<ExaminationDTO>();
        List<Examination> examinations = new List<Examination>();
        ExaminationController examinationController = new ExaminationController();
        public CanceledExaminationsView()
        {
            InitializeComponent();

            txtSearchExaminations.Focus();

            examinations = examinationController.GetCanceledExaminations();
            string type;
            foreach(Examination e in examinations)
            {
                if (e.Type == TypeOfExamination.Opsti)
                {
                    type = "Opšti pregled";
                }
                else if (e.Type == TypeOfExamination.Specijalisticki)
                {
                    type = "Specijalistički pregled";
                }
                else
                {
                    type = "Operacija";
                }
                canceledExaminations.Add(new ExaminationDTO(e.IdExamination,e.Doctor.Name + " " + e.Doctor.Surname + " " + e.Doctor.Jmbg,e.PatientCard.Patient.Name + " " + 
                                                            e.PatientCard.Patient.Surname + " " + e.PatientCard.Patient.Jmbg,e.Room.Id.ToString(),type,e.DateAndTime.ToShortDateString(),e.DateAndTime.ToShortTimeString()));
            }

            dgCanceledExaminations.ItemsSource = canceledExaminations;
        }

        private void txtSearchExaminations_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = canceledExaminations.Where(examination => examination.Doctor.Contains(txtSearchExaminations.Text) || examination.Patient.Contains(txtSearchExaminations.Text)
                                        || examination.Room.Contains(txtSearchExaminations.Text) || examination.TypeOfExamination.Contains(txtSearchExaminations.Text)
                                        || examination.Time.Contains(txtSearchExaminations.Text));

            dgCanceledExaminations.ItemsSource = filtered;
        }

        private void btnEditExm_Click(object sender, RoutedEventArgs e)
        {
            ExaminationDTO examinationToEdit = (ExaminationDTO)dgCanceledExaminations.SelectedItem;
            if (examinationToEdit == null)
            {
                var okMb = new OKMessageBox(this,0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Prvo odaberite pregled koji mijenjate!";
                okMb.ShowDialog();
            }
            else
            {
                var ee = new EditExamination(examinationToEdit, this);

                ee.cmbDoctor.IsEnabled = false;
                ee.cmbTypeOfExamination.IsEnabled = false;
                ee.dpDate.IsEnabled = true;
                ee.tpTime.IsEnabled = true;
                ee.ShowDialog();
            }
        }

        private void btnDeleteExm_Click(object sender, RoutedEventArgs e)
        {
            ExaminationDTO examinationDelete = (ExaminationDTO)dgCanceledExaminations.SelectedItem;
            if (examinationDelete == null)
            {
                var okMb = new OKMessageBox(this, 0);
                okMb.titleMsgBox.Text = "Greška";
                okMb.textMsgBox.Text = "Prvo odaberite pregled koji brišete!";
                okMb.ShowDialog();
            }
            else
            {
                    examinationController.DeleteCanceledExamination(examinationDelete.Id);
                    var okMb = new OKMessageBox(this, 0);
                    okMb.titleMsgBox.Text = "Greška";
                    okMb.textMsgBox.Text = "Došlo je do greške prilikom otkazivanja pregleda!";
                    okMb.ShowDialog();
                    return;
                

                var okMb1 = new OKMessageBox(this, 0);
                okMb1.titleMsgBox.Text = "Obavještenje";
                okMb1.textMsgBox.Text = "Uspješno ste obrisali pregled.";
                okMb1.ShowDialog();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new CanceledExaminationsView();
                    }
                }
            }
        }
    }
}
