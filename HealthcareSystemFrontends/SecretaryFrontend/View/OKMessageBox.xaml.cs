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
    /// Interaction logic for OKMessageBox.xaml
    /// </summary>
    public partial class OKMessageBox : Window
    {
        private Object typeOfParent;
        private int errorCode;
        public OKMessageBox(Object obj, int code)
        {
            InitializeComponent();
            typeOfParent = obj;
            errorCode = code;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    if (typeOfParent.GetType() == typeof(EditPatient))
                    {
                        if (this.errorCode == 1 || this.errorCode == 2)
                        {
                            this.Close(); //doslo je do greske
                        }
                        else if (this.errorCode == 3)
                        {
                            (window as MainWindow).Main.Content = new PatientView(); //uspjesna izmjena informacija o pacijentu
                        }
                    }
                    else if (typeOfParent.GetType() == typeof(Registration))
                    {
                        if (this.errorCode == 1 || this.errorCode == 2)
                        {
                            this.Close(); //doslo je do greske
                        }
                        else if (this.errorCode == 3)
                        {
                            (window as MainWindow).Main.Content = new PatientView(); //uspjesna registracija pacijenta
                        }
                    }
                    else if (typeOfParent.GetType() == typeof(ChangePassword))
                    {
                        if (this.errorCode == 1 || this.errorCode == 2)
                        {
                            this.Close(); //doslo je do greske
                        }
                        else if(this.errorCode == 3)
                        {
                            (window as MainWindow).Main.Content = new EditProfile(); //uspjesna promjena lozinke
                        }
                    }
                    else if (typeOfParent.GetType() == typeof(EditProfile))
                    {
                        if (this.errorCode == 1 || this.errorCode == 2)
                        {
                            this.Close(); //doslo je do greske
                        }
                        else if (this.errorCode == 3)
                        {
                            (window as MainWindow).Main.Content = new Home(); //uspjesna izmjena informacija na profilu
                        }
                    }
                    else if (typeOfParent.GetType() == typeof(LogIn))
                    {
                        if (this.errorCode == 1 || this.errorCode == 2 || this.errorCode == 3)
                        {
                            this.Close(); //doslo je do greske prilikom prijave na sistem
                        }
                    }
                    else if (typeOfParent.GetType() == typeof(ScheduleExamination))
                    {
                        if (this.errorCode == 1 || this.errorCode == 2 || this.errorCode == 3 || this.errorCode == 4)
                        {
                            this.Close(); //doslo je do greske
                        }
                        else if (this.errorCode == 0)
                        {
                            (window as MainWindow).Main.Content = new ExaminationViewByDoctor(""); //uspjesna izmjena informacija na profilu
                        }
                    }
                }
            }
        }
    }
}
