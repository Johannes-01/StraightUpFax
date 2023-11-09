using RestSharp;

namespace FaxWebService
{
    public static class Worker
    {

        private static readonly string host = "https://ntfy.sh";

        public static List<string> Quotes { get; set; } = new List<string>();

        public static void Initiliaze()
        {
            TextFileReader.GetQuotes();
            ExecuteAsync();
        }

        public static void SendPushNotificationToNtfy(string message, string adress)
        {
            var client = new RestClient($"{host}");

            var request = new RestRequest($"/{adress}", Method.Post);

            request.AddBody(message);

            request.AddHeader("Title", "Fax");
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Priority", "default");
            request.AddHeader("Tags", "warning, speaking_head");

            client.Post(request);
        }

        public static async Task ExecuteAsync()
        {
            while (true)
            {
                var random = new Random();
                var index = random.Next(Quotes.Count);
                var quote = Quotes[index];

                SendPushNotificationToNtfy(quote, "straightupfax");

                DateTime currentTime = DateTime.Now;
                int currentHour = currentTime.Hour;

                if (currentHour >= 22 || currentHour < 8)
                {
                    await Task.Delay(TimeSpan.FromHours(8));
                }
                else
                {
                    await Task.Delay(TimeSpan.FromHours(2));
                }
            }
        }
    }
}
