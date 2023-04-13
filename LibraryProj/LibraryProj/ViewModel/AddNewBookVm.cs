using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LibraryProj.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace LibraryProj.ViewModel
{
    class AddNewBookVm: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #region parameters
        public ICommand _addToTheList { get; set; }
        private string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("title");
            }
        }

        private string _isbn;
        public string isbn
        {
            get
            {
                return _isbn;
            }
            set
            {
                _isbn = value;
                OnPropertyChanged("isbn");
            }
        }
        private DateTime _publicationDate;
        public DateTime publicationDate
        {
            get
            {
                return _publicationDate;
            }
            set
            {
                _publicationDate = value;
                OnPropertyChanged("publicationDate");
            }
        }
        private string _genre;
        public string genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged("genre");
            }
        }
        private string _author;
        public string author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
                OnPropertyChanged("author");
            }
        }
        #endregion
        public AddNewBookVm()
        {
            _addToTheList = new RelayCommand(addBookToTheList);
        }

        private void addBookToTheList()
        {
            if (checkValues() == false)
                return;
            else
            {
                string[] splitedString = publicationDate.ToString().Split(" ")[0].Split('/');
                Book newBook = new Book { title = title, authors = author, categories = genre, publishedDate = splitedString[2]+"-"+splitedString[1]+"-"+splitedString[0], isbn = isbn };
                Messenger.Default.Send(newBook);
                Messenger.Default.Send( new CloseMessage());
            }
        }
        private bool checkValues()
        {
            if( publicationDate>DateTime.Now)
            {
                MessageBox.Show("Time not set correctly please fixed it");
                return false;
            }
            if (Regex.Matches(isbn, @"[a-zA-Z]").Count>0)
            {
                MessageBox.Show("ISBN should not contain any values");
                return false;
            }
            return true;
        }
    }
}
