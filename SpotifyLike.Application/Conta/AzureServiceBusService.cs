using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace SpotifyLike.Application.Conta
{
    public class AzureServiceBusService
    {
        private string ConnectionString { get; set; }

        public AzureServiceBusService(IConfiguration configuration)
        {
            ConnectionString = configuration["AzureServiceBus:ConnectionString"];
        }

        public async Task SendMessage(Notificacao notificacao)
        {
            var client = new ServiceBusClient(this.ConnectionString);
            var sender = client.CreateSender("notification");

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notificacao));
            var message = new ServiceBusMessage(body);
            
            await sender.SendMessageAsync(message);
        }
    }
}
