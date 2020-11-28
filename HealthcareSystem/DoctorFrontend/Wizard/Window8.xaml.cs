﻿using System;
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

namespace WpfApp1.Wizard
{
    /// <summary>
    /// Interaction logic for Window8.xaml
    /// </summary>
    public partial class Window8 : Window
    {
        public Window8()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = new Window7();
            s.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new MainWindow();
            s.Show();this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var s = new Window9();
            s.Show();
            this.Close();
        }
    }
}
