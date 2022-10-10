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
        /// <param name="kafkaHandler">Administration handler injection</param>
        public KafkaController(IKafkaAdministrationHandler kafkaHandler)
        {
            KafkaHandler = kafkaHandler;
        }

        /// <summary>
        /// Creates a topic
        /// </summary>
        /// <param name="topicName">Name of the topic to create</param>
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
        /// Deletes a topic given its name
        /// </summary>
        /// <param name="topicName">Name of the topic to delete</param>
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
