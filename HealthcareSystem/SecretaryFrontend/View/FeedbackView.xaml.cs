using Controller.NotificationSurveyAndFeedback;
using Model.Users;
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
    /// Interaction logic for Feedback.xaml
    /// </summary>
    public partial class FeedbackView : Window
    {
        FeedbackController feedbackController = new FeedbackController();
        public FeedbackView()
        {
            InitializeComponent();

            txtComment.Focus();
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            var mb = new MyMessageBox();
            mb.imageMsgBox.Source = new BitmapImage(new Uri(@"/Resources/Icons/close.png", UriKind.Relative));
            mb.titleMsgBox.Text = "Zatvaranje prozora";
            mb.textMsgBox.Text = "Da li ste sigurni da želite odustati od unosa povratne informacije?";
            mb.Owner = Window.GetWindow(this);
            mb.ShowDialog();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string comment = txtComment.Text;
            Stars grade = (Stars)BasicRatingBar.Value;
            Feedback feedback = new Feedback(grade,comment);

            if (feedbackController.AddFeedback(feedback) != null)
            {
                var okMb = new OKMessageBox(this, 0);
                okMb.titleMsgBox.Text = "Obavještenje";
                okMb.textMsgBox.Text = "Uspješno ste poslali povratnu informaciju.";
                okMb.ShowDialog();
                this.Close();
            }
            else
            {
                var okMbx = new OKMessageBox(this, 0);
                okMbx.titleMsgBox.Text = "Greška";
                okMbx.textMsgBox.Text = "Došlo je do greške, pokušajte ponovo!";
                okMbx.ShowDialog();
            }
        }
    }
}
