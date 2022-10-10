using Devon4Net.Application.Kafka.Producer.Business.KafkaManagement.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.Kafka.Producer.Business.KafkaManagement.Controllers
{
    /// <summary>
    /// Controller to put messages in the Kafka topic (defined in appsettings)
    /// </summary>
    public class MessageController : Controller
    {
        public MessageProducerHandler MessageProducer;

        public MessageController(MessageProducerHandler messageProducer)
        {
            MessageProducer = messageProducer;
        }

        /// <summary>
        /// Send a message with the specified key and value
        /// </summary>
        /// <param name="key">Key of the Kafka message</param>
        /// <param name="value">Value of the Kafka message</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/kafka/sendMessage")]
        public async Task<IActionResult> SendMessage(string key, string value)
        {
            await MessageProducer.SendMessage(key, value);
            return Ok();
        }

    }
}
