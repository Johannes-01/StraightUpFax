using RestSharp;

namespace FaxWebService
{
    public static class Worker
    {

        private static readonly string host = "https://ntfy.sh";

        public static List<string> Quotes { get; set; } = new List<string>();

        public static void Initiliaze()
        {
            Quotes.AddRange(new string[]
            {
                    "And as big as the clouds may look, every storm eventually passes.",
                    "You have no enemies.",
                    "It is a shame for a man to grow old without seeing the beauty and strength of which his body is capable.",
                    "Skip the villain arc.",
                    "Do not let yourself be guided by the feeling of lust.",
                    "The only thing that is constant is change. - Heraclitus.",
                    "Never let yourself be saddened by a separation - Miyamoto Musashi.",
            });

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

                if (currentHour >= 22 && currentHour < 8)
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