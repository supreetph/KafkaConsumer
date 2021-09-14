using Confluent.Kafka;
using KafkaConsumer.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    public class ConsumerHandler : BackgroundService
    {
        private readonly ILogger<ConsumerHandler> _logger;
        private readonly IConsumerWrapper _consumerWrapper;

        public ConsumerHandler(ILogger<ConsumerHandler> logger, IConsumerWrapper consumerWrapper)
        {
            _logger = logger;
            _consumerWrapper = consumerWrapper;
        }

        protected override async Task<Request> ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("started consuming messages");
            Request requestBody = new();
            while (!stoppingToken.IsCancellationRequested)
            {

                string message = await _consumerWrapper.ReadMessage();
                requestBody = JsonConvert.DeserializeObject<Request>(message);
            }
            return requestBody;
        }
    }
}
