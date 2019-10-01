using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace MultivixTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "bb32e2f8";
            string baseUri = $"http://www.omdbapi.com/?i=tt3896198&apikey=bb32e2f8";

            Console.WriteLine("Digite o Nome do Filme: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            string type = "Movie";

            var sb = new StringBuilder(baseUri);
            sb.Append($"&s={name}");
            sb.Append($"&type={type}");

            var request = WebRequest.Create(sb.ToString());
            request.Timeout = 1000;
            request.Method = "GET";
            request.ContentType = "application/json";

            string result = string.Empty;

            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);

            result = reader.ReadToEnd();

            dynamic results = JsonConvert.DeserializeObject<dynamic>(result);
            
            if (results.Response == false)
            {
                Console.WriteLine("Desculpe não encontramos seu filme!");
            }
            else
            {
            
                foreach (dynamic x in results.Search)
                {
                    string Title = x.Title;
                    string Year = x.Year;
                    string id = x.imdbID;
                    Console.WriteLine("Nome do Filme: " + Title
                        + "\nAno de lançamento: " + Year
                        + "\nIdentificador: " + id
                        + "\nTipo: " + type);
                    Console.WriteLine();
                }
            }
        }
    }
}