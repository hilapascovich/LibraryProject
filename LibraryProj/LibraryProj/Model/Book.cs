using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProj.Model
{
    class Book
    {
        public string title { get; set; }
        public string isbn { get; set; }
        public string publishedDate { get; set; }
        public string authors { get; set; }
        public string categories { get; set; }

    }
}
