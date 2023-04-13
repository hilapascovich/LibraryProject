using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProj.Model
{
    class ConnectToServer
    {
        private readonly HttpClient client = new HttpClient();

        public async Task SendEmail(IEnumerable<Book> books)
        {
            var json = JsonConvert.SerializeObject(new { books });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:3000/send-email", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Email sent successfully");
            }
            else
            {
                Console.WriteLine("Error sending email");
            }
        }
    }
}
