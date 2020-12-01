using Clinic_Health.Views;
using Clinic_Health.Views.Employees;
using Clinic_Health.Views.Equipment;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic_Health
{
	
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#region Tema
		private readonly PaletteHelper _paletteHelper = new PaletteHelper();
		private void ToggleBaseColour(bool isDark) {
			ITheme theme = _paletteHelper.GetTheme();
			IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
			theme.SetBaseTheme(baseTheme);
			_paletteHelper.SetTheme(theme);
		}
		private void ThemeButtonClick(object sender, RoutedEventArgs e) {

			if ((sender as ToggleButton).IsChecked ?? false)
			{
				ToggleBaseColour(true);
				ITheme theme = _paletteHelper.GetTheme();
				theme.PrimaryLight = Color.FromRgb(144, 238, 144);
				_paletteHelper.SetTheme(theme);


			}
			else
			{
				ToggleBaseColour(false);
				ITheme theme = _paletteHelper.GetTheme();
				theme.PrimaryLight = Color.FromRgb(204, 255, 204);
				
				_paletteHelper.SetTheme(theme);
			}


		}
		# endregion

        private Visibility employeesMenuVisibility;
		public Visibility EmployeesMenuVisibility
		{
			get { return employeesMenuVisibility; }
			set
			{
				if(employeesMenuVisibility != value)
				{
					employeesMenuVisibility = value;
					OnPropertyChanged("EmployeesMenuVisibility");
				}
			}
		}

		private Visibility profileMenuVisibility;
		public Visibility ProfileMenuVisibility
		{
			get { return profileMenuVisibility; }
			set
			{
				if (profileMenuVisibility != value)
				{
					profileMenuVisibility = value;
					OnPropertyChanged("ProfileMenuVisibility");
				}
			}
		}
		
		public MainWindow()
		{
			InitializeComponent();
			ITheme theme = _paletteHelper.GetTheme();
			theme.PrimaryLight = Color.FromRgb(144, 238, 144);
			_paletteHelper.SetTheme(theme);
			DataContext = this;
			EmployeesMenuVisibility = Visibility.Hidden;
			
			ProfileMenuVisibility = Visibility.Hidden;
			
		}

		
		#region Menu Togglers
		private void Toggle_Employees_Menu_Click(object sender, RoutedEventArgs e)
		{
			if(EmployeesMenuVisibility == Visibility.Hidden)
			{
				EmployeesMenuVisibility = Visibility.Visible;
			}
			else
			{
				EmployeesMenuVisibility = Visibility.Hidden;
			}
		}
		

		private void Toggle_Profile_Menu_Click(object sender, RoutedEventArgs e)
		{
			if (ProfileMenuVisibility == Visibility.Hidden)
			{
				ProfileMenuVisibility = Visibility.Visible;
			}
			else
			{
				ProfileMenuVisibility = Visibility.Hidden;
			}
		}

		#endregion


		#region Navigation

		// EMPLOYEES 
		private void Navigation_Add_Doctor_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Content = new AddDoctorView();
			Toggle_Employees_Menu_Click(null, null);
		}

		private void Navigation_Add_Secretary_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Content = new AddSecretaryView();
			Toggle_Employees_Menu_Click(null, null);
		}

		private void Navigation_Show_Employees_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Content = new AllEmployeesView();
			Toggle_Employees_Menu_Click(null, null);
		}
		//HOME
		private void Navigation_Home_Click(object sender, RoutedEventArgs e)
		{

			MainFrame.Content = new HomeView();
		}

		//PROFILE
		private void Navigation_Profile_Click(object sender, RoutedEventArgs e)
		{

			MainFrame.Content = new Profile();
		}

		//SURVAY
		private void Navigation_Survay_Click(object sender, RoutedEventArgs e)
		{

			MainFrame.Content = new Survay();
		}
		//PASSWORD
		private void Navigation_Change_Password_Click(object sender, RoutedEventArgs e)
		{

			MainFrame.Content = new ChangePassword();
		}

		//REPORTS
		private void Navigation_Reports_Click(object sender, RoutedEventArgs e)
		{
			
			MainFrame.Content = new ReportsView();
		}
		// EQUIPMENT 
		private void Navigation_Equipment_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Content = new AllEquipmentView();
		}
		// SCHEDULE 
		private void Navigation_Schedule_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Content = new ScheduleView();
		}
		// ROOMS 
		private void Navigation_Rooms_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Content = new RoomsView();
		}
		// DRUGS 
		private void Navigation_Drugs_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Content = new DrugsView();
		}

        #endregion

        private void Navigation_Graphical_Editor_Click(object sender, RoutedEventArgs e)
        {
			GraphicalEditor.MainWindow graphicalEditorMainWindow = new GraphicalEditor.MainWindow("Manager");
			graphicalEditorMainWindow.ShowDialog();
        }
    }
}
