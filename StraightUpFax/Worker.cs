using RestSharp;

namespace StraightUpFax
{
    public class Worker : BackgroundService
    {

        private readonly string host = "https://ntfy.sh";

        public Worker()
        {
        }

        public void SendPushNotificationToNtfy(string message,string adress)
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
                var quotes = new string[]
                {
                    "And as big as the clouds may look, every storm eventually passes.",
                    "You have no enemies.",
                    "It is a shame for a man to grow old without seeing the beauty and strength of which his body is capable.",
                    "Skip the villain ark.",
                    "Do not let yourself be guided by the feeling of lust.",
                    "The only thing that is constant is change. - Heraclitus.",
                    "Never let yourself be saddened by a separation - Miyamoto Musashi.",
                };

                var random = new Random();
                var index = random.Next(quotes.Length);
                var quote = quotes[index];

                SendPushNotificationToNtfy(quote, "straightupfax");

                TimeSpan start = new TimeSpan(0, 0, 0); 
                TimeSpan end = new TimeSpan(8, 0, 0); 
                TimeSpan now = DateTime.Now.TimeOfDay;

                if ((now > start) && (now < end))
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