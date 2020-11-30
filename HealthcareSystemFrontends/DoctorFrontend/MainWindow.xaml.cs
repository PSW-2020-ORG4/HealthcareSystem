using Controller.UsersAndWorkingTime;
using Model.Users;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private String username;
        private String password;
        private static DoctorController dc = new DoctorController();

        public String Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public String Password
        {
            get { return password; }
            set
            {
                if (value != password)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            txtKorisnickoIme.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (String.IsNullOrEmpty(txtKorisnickoIme.Text))
            {
                MessageBox.Show("Morate unijeti korisničko ime.", "Upozorenje!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Clear();
                txtKorisnickoIme.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Morate unijeti lozinku.", "Upozorenje!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
          
                return;
            }
            
            Doctor doctor = (Doctor)dc.SignIn(txtKorisnickoIme.Text, txtPassword.Password);

            if (doctor != null)
            {
                HomePage s = new HomePage(doctor);
                s.Show();
                this.Close();
            }
            else {
                MessageBox.Show("Korisnicko ime ili lozinka ne postoje.", "Upozorenje!",
                       MessageBoxButton.OK, MessageBoxImage.Warning);
                txtKorisnickoIme.Clear();
                txtPassword.Clear();
                txtKorisnickoIme.Focus();
            }             

        }


        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            IInputElement focusedControl = FocusManager.GetFocusedElement(activeWindow);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                if (str.Equals("index"))
                {
                    str = "prijava";
                }
                HelpProvider.ShowHelp(str);
            }
            else
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)activeWindow);
                HelpProvider.ShowHelp(str);
            }
        }
       
    }
}
