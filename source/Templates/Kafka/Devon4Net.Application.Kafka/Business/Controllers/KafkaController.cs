using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Devon4Net.Application.Kafka.Business.KafkaManagement.Handlers;
using Devon4Net.Infrastructure.Kafka.Handlers;
using Devon4Net.Infrastructure.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.Kafka.Business.Controllers
{
    /// <summary>
    /// KafkaController sample
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private MessageProducerHandler MessageProducer { get; set; }
        private IKakfkaHandler KafkaHandler { get; set; }

        /// <summary>
        /// KafkaController constructor
        /// </summary>
        /// <param name="messageProducer"></param>
        /// <param name="kafkaHandler"></param>
        public KafkaController(MessageProducerHandler messageProducer, IKakfkaHandler kafkaHandler)
        {
            MessageProducer = messageProducer;
            KafkaHandler = kafkaHandler;
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
            Devon4NetLogger.Debug("Executing DeliverMessage from controller KafkaController");
            var result = await MessageProducer.SendMessage(key, value);
            return Ok(result);
        }

        /// <summary>
        /// Delivers a Kafka message
        /// </summary>
        /// <param name="topicName"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/kafka/topic")]
        public async Task<IActionResult> CreateTopic(string topicName)
        {
            Devon4NetLogger.Debug("Executing CreateTopic from controller KafkaController");
            return Ok(await KafkaHandler.CreateTopic("Admin1", topicName));

        }

        /// <summary>
        /// Delivers a Kafka message
        /// </summary>
        /// <param name="topicName"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/kafka/topic")]
        public async Task<IActionResult> DeleteTopicMessage(string topicName)
        {
            Devon4NetLogger.Debug("Executing DeleteTopicMessage from controller KafkaController");
            return Ok(await KafkaHandler.DeleteTopic("Admin1", new List<string>{ topicName }));

        }
    }
}
