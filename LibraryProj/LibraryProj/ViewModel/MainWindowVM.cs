using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LibraryProj.Model;
using LibraryProj.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static LibraryProj.Model.Book;

namespace LibraryProj.ViewModel
{
    class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #region parameters
        private JasonReader js;
        public ICommand _addNewBookClicked { get; set; }
        public ICommand _deleteBookClicked { get; set; }
        public ICommand _saveList { get; set; }
        public ICommand _shareClicked { get; set; }
        public ObservableCollection<Book> booksListGridData { get; set; }
        private Book _deleteSelectedItem;
        public Book deleteSelectedItem
        {
            get
            {
                return _deleteSelectedItem;
            }
            set
            {
                _deleteSelectedItem = value;
                OnPropertyChanged("deleteSelectedItem");
            }
        }
        #endregion

        public MainWindowVM()
        {
            js = new JasonReader();
            updateDataGrid();
            _addNewBookClicked = new RelayCommand(addNewBookClicked);
            _deleteBookClicked = new RelayCommand<Object>(deleteBookClicked);
            Messenger.Default.Register<Book>(this, addToList);
            _saveList = new RelayCommand(saveList);
            _shareClicked = new RelayCommand(shareBookList);
        }

        private void shareBookList()
        {
            ConnectToServer bookService = new ConnectToServer();
            _ = bookService.SendEmail(js.readBooks());
        }

        private void saveList()
        {
            js.writeToJson(booksListGridData);
        }

        private void addToList(Book obj)
        {
            booksListGridData.Add(obj);
        }

        private void updateDataGrid()
        {
            List<Book> books =  js.readBooks();
            booksListGridData = new ObservableCollection<Book>();
            foreach(Book b in books)
            {
                booksListGridData.Add(b);
            }
        }

        private void deleteBookClicked(Object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected item/s?",
            "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var dataGrid = obj as DataGrid;
                if (dataGrid != null && dataGrid.SelectedItems != null && dataGrid.SelectedItems.Count > 0)
                {
                    var selectedItemsCopy = dataGrid.SelectedItems.Cast<Book>().ToList();
                    foreach (var selectedItem in selectedItemsCopy)
                    {
                        booksListGridData.Remove((Book)selectedItem);
                    }
                }

            }
            else if(result == MessageBoxResult.No)
            {
                return;
            }


        }

        private void addNewBookClicked()
        {
            AddNewBook addBookPage = new AddNewBook();
            addBookPage.Show();
        }
    }
}
