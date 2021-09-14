using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    public class ConsumerWrapper : IConsumerWrapper
    {

        private readonly ConsumerConfig _config;
        private readonly IOptions<TopicSettings> _topicSetting;

        public ConsumerWrapper(ConsumerConfig config, IOptions<TopicSettings> topicSetting)
        {
            _config = config;
            _topicSetting = topicSetting;
        }

        public Task<string> ReadMessage()
        {
            var topic = _topicSetting.Value.Filing;
            //var conf = new ConsumerConfig
            //{
            //    GroupId = "st_consumer_group",
            //    BootstrapServers = "localhost:9092",
            //    AutoOffsetReset = AutoOffsetReset.Earliest
            //};
            using (var builder = new ConsumerBuilder<Ignore,
                string>(_config).Build())
            {
                builder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                while (true)
                {
                    var consumer = builder.Consume(cancelToken.Token);
                    //Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
                    var messgae = consumer.Message.Value;
                    return Task.FromResult(messgae);
                }
            }
        }
    }

}
