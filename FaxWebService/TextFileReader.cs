using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace FaxWebService
{
    public class TextFileReader
    {
        public static void GetQuotes()
        {
            var q = File.ReadAllText("quotes.txt");
            var quotes = JsonConvert.DeserializeObject<List<string>>(q);
            Worker.Quotes = quotes;
        }

        public static void SetQuote(string quote) 
        {
            Worker.Quotes.Add(quote);
            string updatedString = JsonConvert.SerializeObject(Worker.Quotes);
            File.WriteAllText("quotes.txt", updatedString);
        }

        public static void RemoveQuote(string quote)
        {
            Worker.Quotes.Remove(quote);
            string updatedString = JsonConvert.SerializeObject(Worker.Quotes);
            File.WriteAllText("quotes.txt", updatedString);
        }
    }
}
