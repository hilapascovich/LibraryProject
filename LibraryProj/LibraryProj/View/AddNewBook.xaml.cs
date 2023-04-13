using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using LibraryProj.ViewModel;

namespace LibraryProj.View
{
    /// <summary>
    /// Interaction logic for AddNewBook.xaml
    /// </summary>
    public partial class AddNewBook : Window
    {
        public AddNewBook()
        {
            InitializeComponent();
            this.DataContext = new AddNewBookVm();
            Messenger.Default.Register < CloseMessage > (this, close);
        }

        private void close(CloseMessage obj)
        {
           Close();
        }
    }
}
