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

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for SearchClientWindow.xaml
    /// </summary>
    public partial class SearchClientWindow : Window
    {
        public SearchClientWindow()
        {
            InitializeComponent();
        }

        private void DeletClientButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Czy napewno chcesz usunąć zaznaczony wpis?", "Czy jestes pewien?", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }

        private void CloseSearchClientWButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
