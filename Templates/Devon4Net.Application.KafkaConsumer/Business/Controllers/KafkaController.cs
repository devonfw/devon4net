using System.Threading.Tasks;
using Confluent.Kafka;
using Devon4Net.Application.KafkaConsumer.Business.KafkaManagement.Handlers;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.KafkaConsumer.Business.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private MessageProducerHandler MessageProducer { get; set; }

        public KafkaController(MessageProducerHandler messageProducer)
        {
            MessageProducer = messageProducer;
        }

        /// <summary>
        /// Delivers a Kafka message
        /// </summary>
        /// <param name="key">message key</param>
        /// <param name="value">message value</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DeliveryResult<string,string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/kafka/deliver")]
        public async Task<IActionResult> DeliverMessage(string key, string value)
        {
            Devon4NetLogger.Debug("Executing CreateTodo from controller RabbitMqController");
            var result = await MessageProducer.SendMessage(key, value);
            return Ok(result);
        }
    }
}
