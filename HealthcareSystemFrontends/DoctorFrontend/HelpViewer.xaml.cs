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
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
  
    public partial class HelpViewer : Window
    {
        
        public HelpViewer(string key)
        {
            InitializeComponent();
            int index = key.IndexOf('#');
            string anchor = index != -1 ? key.Substring(index) : "";
            key = index != -1 ? key.Remove(index) : key;

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = String.Format("{0}/Help/{1}.html", projectDirectory, key);
            if (!File.Exists(path))
            {
                key = "error";
            }
            Uri u = new Uri(String.Format("file:///{0}/Help/{1}.html", projectDirectory, key));
            wbHelp.Navigate(u + anchor);

        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbHelp != null) && (wbHelp.CanGoBack));
            // osvezava toolbar da bi dugme Back bilo omoguceno/onemoguceno
            CommandManager.InvalidateRequerySuggested();
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbHelp != null) && (wbHelp.CanGoForward));
            // osvezava toolbar da bi dugme Forward bilo omoguceno/onemoguceno
            CommandManager.InvalidateRequerySuggested();
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoForward();
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}
