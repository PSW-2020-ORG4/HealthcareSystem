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

namespace Clinic_Health.Views
{
	/// <summary>
	/// Interaction logic for Survay.xaml
	/// </summary>
	public partial class Survay : UserControl
	{
		public Survay()
		{
			InitializeComponent();
		}
		private void buttonSacuvajAnketu_Click(object sender, RoutedEventArgs e)
		{

			Poruka.Text = "Hvala sto ucestvujete u poboljsanju nase aplikacije!";

			

		}
	}
}
