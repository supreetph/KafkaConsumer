using System.Threading.Tasks;

namespace KafkaConsumer
{
    public interface IConsumerWrapper
    {
        Task<string> ReadMessage();
    }
}