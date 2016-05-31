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
using Projekt_WPF_Solution.DataBaseClasses;
using Projekt_WPF_Solution.Validators;
using Microsoft.Win32;
using System.IO;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for AddNewCarWindow.xaml
    /// </summary>
    public partial class AddNewCarWindow : Window
    {
        Car car;
        bool isEditable;


        public AddNewCarWindow(Car car, bool isEditable)
        {
            InitializeComponent();
            MarkaComboBox.ItemsSource = SqlDataGetters.Brands;
            TypComboBox.ItemsSource = SqlDataGetters.BodyTypes;

            List<string> carTypesToString = new List<string>();
            for(int i=0;i<SqlDataGetters.CarTypes.Count;i++)
            {
                carTypesToString.Add(SqlDataGetters.CarTypes.ElementAt(i).CarType);
            }
            KlasaCombotBox.ItemsSource = carTypesToString;

            this.car = car;
            this.isEditable = isEditable;
            MainAddCarGrid.DataContext = this.car;
            MainAddCarGrid.IsEnabled = isEditable;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            if(Validator.IsValid(this))
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Niepoprawne dane!");
            }
        }

        private void CancelCarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Image File(*.jpg; *.bmp; *.gif)| *.jpg; *.bmp; *.gif";
            op.Title = "Wybierz zdjęcie:";
            if(op.ShowDialog() == true)
            {
                string[] split = op.FileName.Split('.').ToArray();
                string filename = car.RegPlate + "." + split[split.Count() - 1]; ;
                string dir = GetDirectory() + "\\" + filename;
                File.Copy(op.FileName, dir, true);
                car.Image = "Cars\\" + filename;
            }            
        }

        private string GetDirectory()
        {
            string dir = null, imgdir;
            do
            {
                if (dir == null)
                    dir = Directory.GetCurrentDirectory();
                else
                    dir = Directory.GetParent(dir).ToString();
                imgdir = System.IO.Path.Combine(dir, "Images");
                imgdir = System.IO.Path.Combine(imgdir, "Cars");
            } while (!Directory.Exists(imgdir));
            return imgdir;
        }
    }
}
