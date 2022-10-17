using Devon4Net.Application.Kafka.Producer.Business.FileManagement.Helpers;
using Devon4Net.Application.Kafka.Producer.Business.KafkaManagement.Handlers;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.Kafka.Producer.Business.FileManagement.Controllers
{
    /// <summary>
    /// File uploading controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    { 
        public FileProducerHandler Producer { get; set; }

        public FileController(FileProducerHandler messageProducer)
        {
            Producer = messageProducer;
        }

        /// <summary>
        /// Posts the file to the kafka by pieces
        /// </summary>
        /// <param name="file">File selected from the system</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/kafka/uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var dataPieces = DataPieceFileHelper.GetDataPieces(file);
                foreach(var piece in dataPieces)
                {
                    await Producer.SendMessage("jsonKey", piece);
                }
            }
            catch (Exception e)
            {
                Devon4NetLogger.Error(e.Message);
                return BadRequest();
            }
            
            return Ok();
        }

    }
}
