using RestSharp;

namespace StraightUpFax
{
    public class Worker : BackgroundService
    {

        private readonly string host = "https://ntfy.sh";

        public List<string> Quotes { get; set; } = new List<string>();

        public Worker()
        {
            this.Quotes.AddRange(new string[]
            {
                    "And as big as the clouds may look, every storm eventually passes.",
                    "You have no enemies.",
                    "It is a shame for a man to grow old without seeing the beauty and strength of which his body is capable.",
                    "Skip the villain ark.",
                    "Do not let yourself be guided by the feeling of lust.",
                    "The only thing that is constant is change. - Heraclitus.",
                    "Never let yourself be saddened by a separation - Miyamoto Musashi.",
            });
        }

        public void SendPushNotificationToNtfy(string message, string adress)
        {
            var client = new RestClient($"{host}");

            var request = new RestRequest($"/{adress}", Method.Post);

            request.AddBody(message);

            request.AddHeader("Title", "Fax");
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Priority", "urgent");
            request.AddHeader("Tags", "warning, speaking_head");

            client.Post(request);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var random = new Random();
                var index = random.Next(this.Quotes.Count);
                var quote = this.Quotes[index];

                SendPushNotificationToNtfy(quote, "straightupfax");

                DateTime currentTime = DateTime.Now;
                int currentHour = currentTime.Hour;

                if (currentHour >= 22 && currentHour < 8)
                {
                    await Task.Delay(TimeSpan.FromHours(8), stoppingToken);
                }
                else
                {
                    await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
                }
            }
        }
    }
}