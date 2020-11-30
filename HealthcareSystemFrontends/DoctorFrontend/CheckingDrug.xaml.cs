using Controller.DrugAndTherapy;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for CheckingDrug.xaml
    /// </summary>
    public partial class CheckingDrug : Window
    {
        private Drug drug;
        private DrugController dc = new DrugController();
        public CheckingDrug()
        {
            InitializeComponent();
           
            List<Drug> drugs = dc.ViewUnconfirmedDrugs();
            
                drug = drugs[0];
                newDrug.Text = drugs[0].Name;
                List<Ingredient> ingredients = drugs[0].ingredient;
                List<string> names = new List<string>();
                foreach (Ingredient i in ingredients)
                {
                    names.Add(i.Name);
                }
                listBox1.DataContext = names;
               
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            var s = new MessageConfirmedDrug(drug);
            s.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new MessageUnconfirmedDrug();
            s.Show();
            this.Close();
        }

       
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Morate selektovati jedan sastojak.", "Upozorenje!",
                   MessageBoxButton.OK, MessageBoxImage.Warning);

                txtBoxNewDrug.Focus();
                return;
            }
            
            List<Ingredient> ingridients = drug.ingredient;
            foreach (Ingredient i in ingridients) {
                if (i.Name == listBox1.SelectedItem.ToString()) {
                    i.Name = txtBoxNewDrug.Text;
                }
            }
            drug.ingredient = ingridients;
            dc.EditUnconfirmedDrug(drug);

            txtBoxNewDrug.Clear();
            List<Drug> drugs = dc.ViewUnconfirmedDrugs();

            drug = drugs[0];
            newDrug.Text = drugs[0].Name;
            List<Ingredient> ingredients = drugs[0].ingredient;
            List<string> names = new List<string>();
            foreach (Ingredient i in ingredients)
            {
                names.Add(i.Name);
            }
            listBox1.DataContext = names;

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
                    str = "noviLijek";
                }
                HelpProvider.ShowHelp(str);
            }
            else
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)activeWindow);
                HelpProvider.ShowHelp(str);
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                txtBoxNewDrug.Text = listBox1.SelectedItem.ToString();
            }
        }
    }
}
