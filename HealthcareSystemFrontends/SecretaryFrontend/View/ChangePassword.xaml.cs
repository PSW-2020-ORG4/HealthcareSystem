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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private static SecretaryController secretaryController = new SecretaryController();
        private UserController userController = new UserController(secretaryController);
        public ChangePassword()
        {
            InitializeComponent();
            pbOldPass.Focus();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/close.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Zatvaranje prozora";
            mb.textMsgBox.Text = "Da li ste sigurni da želite odustati od promjene lozinke?";

            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            if(pbOldPass.Password.Equals("") || newPass.Password.Equals("") || newPassOk.Password.Equals(""))
            {
                var mbOk = new OKMessageBox(this, 1);
                mbOk.titleMsgBox.Text = "Greška";
                mbOk.textMsgBox.Text = "Polja za unos lozinke ne mogu ostati prazna!";
                mbOk.ShowDialog();
            }
            else if (!pbOldPass.Password.Equals(userController.ViewProfile(LogIn.secretary.Jmbg).Password))
            {
                var mbOk = new OKMessageBox(this,1);
                mbOk.titleMsgBox.Text = "Greška";
                mbOk.textMsgBox.Text = "Unijeli ste neispravnu staru lozinku!";
                mbOk.ShowDialog();
            }
            else if (!newPass.Password.Equals(newPassOk.Password))
            {
                var mbOk = new OKMessageBox(this,2);
                mbOk.titleMsgBox.Text = "Greška";
                mbOk.textMsgBox.Text = "Unesene nove lozinke se ne poklapaju!";
                mbOk.ShowDialog();
            }
            else
            {
                Secretary s = LogIn.secretary;
                s.Password = newPass.Password;
                if (userController.EditProfile(s) == null)
                {
                    var mbOk = new OKMessageBox(this, 2);
                    mbOk.titleMsgBox.Text = "Greška";
                    mbOk.textMsgBox.Text = "Nova lozinka mora da ima makar 8 dozvoljenih karaktera (a-z A-Z 0-9 . - _) !";
                    mbOk.ShowDialog();
                }
                else
                {
                    var okMb = new OKMessageBox(this, 3);
                    okMb.titleMsgBox.Text = "Obavještenje";
                    okMb.textMsgBox.Text = "Uspješno ste promijenili lozinku.";
                    okMb.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
