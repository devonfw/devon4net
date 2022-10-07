using Devon4Net.Infrastructure.Kafka.Handlers.Administration;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.Kafka.Business.KafkaManagement.Controllers
{
    /// <summary>
    /// KafkaController sample
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private IKafkaAdministrationHandler KafkaHandler { get; }

        /// <summary>
        /// KafkaController constructor
        /// </summary>
        /// <param name="messageProducer"></param>
        /// <param name="messageProducer2"></param>
        /// <param name="kafkaHandler"></param>
        public KafkaController(IKafkaAdministrationHandler kafkaHandler)
        {
            KafkaHandler = kafkaHandler;
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
            return Ok(await KafkaHandler.CreateTopic("MyAdmin", topicName).ConfigureAwait(false));
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
            return Ok(await KafkaHandler.DeleteTopic("MyAdmin", new List<string> { topicName }).ConfigureAwait(false));
        }
    }
}
