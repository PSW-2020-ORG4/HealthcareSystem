using Controller.DrugAndTherapy;
using Model.Manager;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for IngredientsOfDrug.xaml
    /// </summary>
    public partial class IngredientsOfDrug : Window
    {
        private static DrugController dc = new DrugController();
        public IngredientsOfDrug(Drug drug)
        {
            InitializeComponent();
            txtDrug.Text = drug.Name;
            List<Ingredient> ingredients = drug.ingredient;
            List<string> names = new List<string>();
            foreach (Ingredient i in ingredients)
            {
                names.Add(i.Name);
            }
            listBox2.DataContext = names;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
