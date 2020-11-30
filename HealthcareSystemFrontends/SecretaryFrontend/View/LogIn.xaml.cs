using Controller.UsersAndWorkingTime;
using Model.Users;
using ProjekatZdravoKorporacija.ModelDTO;
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

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public static Secretary secretary;
        private static SecretaryController secretaryController = new SecretaryController();
        private UserController userController = new UserController(secretaryController);
        public LogIn()
        {
            InitializeComponent();

            NameTextBox.Focus();
        }
        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            secretary = (Secretary)userController.SignIn(NameTextBox.Text,PasswordBox.Password);

            if(NameTextBox.Text.Equals("") || PasswordBox.Password.Equals(""))
            {
                var okMbx = new OKMessageBox(this, 1);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Unos korisničkog imena i lozinke je obavezan!";
                okMbx.ShowDialog();
            }
            else if (secretary == null)
            {
                var okMbx = new OKMessageBox(this, 1);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Unijeli ste pogrešno korisničko ime ili lozinku!";
                okMbx.ShowDialog();
            }
            else
            {
                var s = new MainWindow();
                s.Main.Content = new Home();
                s.Show();
                this.Close();
            }        
        }
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
