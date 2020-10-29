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
using System.Windows.Shapes;

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for MyMessageBox.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        public MyMessageBox()
        {
            InitializeComponent();
        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.Owner.GetType() == typeof(MainWindow))
            {
                if (this.titleMsgBox.Text.Equals("Odjava sa sistema"))
                {
                    var l = new LogIn();
                    l.Show();
                    this.Owner.Close();
                    GC.Collect();
                }
                else if(this.textMsgBox.Text.Equals("Da li ste sigurni da želite odustati od izmjene informacije o pacijentu?"))
                {
                    this.Close();
                    (this.Owner as MainWindow).Main.Content = new PatientView();
                }
                else
                {
                    this.Close();
                    (this.Owner as MainWindow).Main.Content = new Home();
                }
            }
            else if (this.Owner.GetType() == typeof(FeedbackView))
            {
                this.Owner.Close();
            }
            else if(this.Owner.GetType() == typeof(EditExamination))
            {
                this.Owner.Close();
            }
            else if(this.Owner.GetType() == typeof(ScheduleExamination))
            {
                this.Owner.Close();
            }
            else if (this.Owner.GetType() == typeof(ChangePassword))
            {
                this.Owner.Close();
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
