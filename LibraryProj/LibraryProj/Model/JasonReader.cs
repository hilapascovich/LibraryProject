using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace LibraryProj.Model
{
    class JasonReader
    {
        public List<Book>readBooks()
        {
            using (StreamReader r = new StreamReader("../../../bookList.json"))
            {
                string json = r.ReadToEnd();
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(json);
                return books;
            }
        }
        public void writeToJson(ObservableCollection<Book> bookslist)
        {
            try
            {
                string json = JsonConvert.SerializeObject(bookslist);
                File.WriteAllText(@"../../../bookList.json", json);
                MessageBox.Show("Successfully saved.");
            }
            catch
            {
                MessageBox.Show("Could not successfully saved the list./nPlease try again.");
            }
        }

    }
}
