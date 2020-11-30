using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjekatZdravoKorporacija.ModelDTO;
using Controller.UsersAndWorkingTime;
using Model.Users;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for Doctors.xaml
    /// </summary>
    public partial class DoctorView : Page
    {
        private static DoctorController doctorController = new DoctorController();
        private UserController userController = new UserController(doctorController);
        List<Doctor> doctors = new List<Doctor>();

        public DoctorView()
        {
            InitializeComponent();
            txtNameToSearch.Focus();

            List<User> users = userController.ViewAllUsers();
            foreach (User u in users)
            {
                doctors.Add((Doctor)u);
            }

            dgDoctors.ItemsSource = doctors;
        }

        private void txtNameToSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var filtered = doctors.Where(doctor => doctor.Jmbg.Contains(txtNameToSearch.Text) || doctor.Name.Contains(txtNameToSearch.Text) || doctor.Surname.Contains(txtNameToSearch.Text)
                                        || doctor.Type.ToString().Contains(txtNameToSearch.Text) || doctor.Phone.Contains(txtNameToSearch.Text)
                                        || doctor.Email.Contains(txtNameToSearch.Text));

            dgDoctors.ItemsSource = filtered;
        }

        private void btnExmByDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (dgDoctors.SelectedItem != null)
            {
                Doctor selectedDoctor = (Doctor)dgDoctors.SelectedItem;
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).Main.Content = new ExaminationViewByDoctor(selectedDoctor.Jmbg);
                    }
                }
            }
            else
            {
                var okMbx = new OKMessageBox(this, 2);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Izaberite doktora za kojeg želite prikazati raspored pregleda!";
                okMbx.ShowDialog();
            }
        }
    }
}
