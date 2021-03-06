﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

namespace Projekt_WPF_Solution.DataBaseClasses
{
    public class Client : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Variables
        private int id;
        private string pesel;
        private string name;
        private string surname;
        private DateTime born;
        private bool isMale;
        private int phoneNumber;
        private string address;
        private string city;
        private string type;
        private BitmapImage image;
        #endregion

        #region Properties
        public int ID { get { return id; } set { id = value; } }                                //ID PRIMARY KEY
        public string Pesel { get { return pesel; } set { pesel = value; } }                    //PESEL (KLUCZ GLOWNY)
        public string Name { get { return name; } set { name = value; } }                       //Imię 
        public string Surname { get { return surname; } set { surname = value; } }              //Nazwisko
        public DateTime Born { get { return born; } set { born = value; } }                     //Data urodzenia
        public bool IsMale { get { return isMale; } set { isMale = value; } }                   //Płeć (true = mężczyzna, false = kobieta)
        public int PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }     //Numer telefonu
        public string Address { get { return address; } set { address = value; } }              //Adres
        public string City { get { return city; } set { city = value; } }                       //Miasto
        public string Type { get { return type; } set { type = value; } }                       //Typ klienta
        public BitmapImage Image { get { return image; } set { image = value; OnPropertyChanged("Image"); } }                    //zDjęcie
        public string NameSurname { get { return name + " " + surname; } }                     //Imię i nazwisko
        #endregion
        #region Constructors
        //Pesel, Name, Surname, Born, IsMale, PhoneNumber, Address, City, Type, Image
        public Client(int id, string pesel, string name, string surname, DateTime born, bool isMale, int phoneNumber, string address, string city, string type, BitmapImage image)
        {
            this.id = id;
            this.pesel = pesel;
            this.name = name;
            this.surname = surname;
            this.born = born;
            this.isMale = isMale;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.city = city;
            this.type = type;
            this.image = image;
        }

        public Client()
        {
            this.Pesel = string.Empty;
            this.Name = string.Empty;
            this.Surname = string.Empty;
            this.Address = string.Empty;
            this.City = string.Empty;
            this.Type = string.Empty;
            this.Image = Converters.ImageConverter.GetNoPhoto();
        }

        public Client(Client c)
        {
            this.id = c.id;
            this.pesel = c.pesel;
            this.name = c.name;
            this.surname = c.surname;
            this.born = c.born;
            this.isMale = c.isMale;
            this.phoneNumber = c.phoneNumber;
            this.address = c.address;
            this.city = c.city;
            this.type = c.type;
            this.image = c.image;
        }
        #endregion
        #region SQL
        public bool SqlInsert()
        {
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "INSERT INTO clients (Pesel, Name, Surname, Born, IsMale, PhoneNumber, Address, City, Type, Image) VALUES (@Pesel, @Name, @Surname, @Born, @IsMale, @PhoneNumber, @Address, @City, @Type, @Image)";
                    cmd.Parameters.AddWithValue("@Pesel", this.Pesel);
                    cmd.Parameters.AddWithValue("@Name", this.Name);
                    cmd.Parameters.AddWithValue("@Surname", this.Surname);
                    cmd.Parameters.AddWithValue("@Born", this.Born);
                    cmd.Parameters.AddWithValue("@IsMale", this.IsMale);
                    cmd.Parameters.AddWithValue("@PhoneNumber", this.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", this.Address);
                    cmd.Parameters.AddWithValue("@City", this.City);
                    cmd.Parameters.AddWithValue("@Type", this.Type);
                    cmd.Parameters.AddWithValue("@Image", Converters.ImageConverter.ImageToBytes(this.Image));
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool SqlUpdate()
        {
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "UPDATE clients SET Pesel = @Pesel, Name = @Name, Surname = @Surname, Born = @Born, IsMale = @IsMale, PhoneNumber = @PhoneNumber, Address = @Address, City = @City, Type = @Type, Image = @Image WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@Pesel", this.Pesel);
                    cmd.Parameters.AddWithValue("@Name", this.Name);
                    cmd.Parameters.AddWithValue("@Surname", this.Surname);
                    cmd.Parameters.AddWithValue("@Born", this.Born);
                    cmd.Parameters.AddWithValue("@IsMale", this.IsMale);
                    cmd.Parameters.AddWithValue("@PhoneNumber", this.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", this.Address);
                    cmd.Parameters.AddWithValue("@City", this.City);
                    cmd.Parameters.AddWithValue("@Type", this.Type);
                    cmd.Parameters.AddWithValue("@Image", Converters.ImageConverter.ImageToBytes(this.Image));
                    cmd.Parameters.AddWithValue("@ID", this.ID);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool SqlDelete()
        {
            IDBaccess db = new IDBaccess();
            if (db.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "DELETE FROM clients WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", this.ID);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region IDataErrorInfo
        public string Error
        {
            get
            {
                return null;
            }
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName.Equals("Name"))
                {
                    Regex regex = new Regex("^[a-zA-Z ]*$"); 

                    if (string.IsNullOrWhiteSpace(Name) || !regex.IsMatch(Name))
                    {
                        return "Podaj imię";
                    }
                }
                if (columnName.Equals("Surname"))
                {
                    Regex regex = new Regex("^[a-zA-Z ]*$");

                    if (string.IsNullOrWhiteSpace(Surname) || !regex.IsMatch(Surname))
                    {
                        return "Podaj nazwisko";
                    }
                }
                if (columnName.Equals("Pesel"))
                {
                    Regex regex = new Regex("[0-9]{11}");
                    if(!regex.IsMatch(Pesel))
                    {
                        return "Zły pesel";
                    }
                }
                if (columnName.Equals("Born"))
                {
                    if (Born.Year < 1920)
                    {
                        return "Zły rok urodzenia";
                    }
                }
                if (columnName.Equals("Address"))
                {
                    if (string.IsNullOrWhiteSpace(Address))
                    {
                        return "Podaj adres";
                    }
                }
                if (columnName.Equals("City"))
                {
                    if (string.IsNullOrWhiteSpace(City))
                    {
                        return "Podaj miasto";
                    }
                }
                return null;
            }
        }
        #endregion
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion
        public void PropertyUpdate(Client c)
        {
            this.id = c.id;
            this.pesel = c.pesel;
            this.name = c.name;
            this.surname = c.surname;
            this.born = c.born;
            this.isMale = c.isMale;
            this.phoneNumber = c.phoneNumber;
            this.address = c.address;
            this.city = c.city;
            this.type = c.type;
            this.image = c.image;
        }
        public override string ToString()
        {
            string toString = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                toString += name;
            }
            if (!string.IsNullOrEmpty(surname))
            {
                toString += " " + surname;
            }
            if (!string.IsNullOrEmpty(pesel))
            {
                toString += "; " + pesel;
            }
            return toString;
        }
    }
}
