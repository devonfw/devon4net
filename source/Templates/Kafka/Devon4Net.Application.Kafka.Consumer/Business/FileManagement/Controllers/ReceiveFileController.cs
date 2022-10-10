using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Helpers;
using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Controllers
{
    /// <summary>
    /// Controller for downloading the files received from Kafka
    /// </summary>
    public class ReceiveFileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IConfigurationSection _downloadOptions;

        public ReceiveFileController(IFileService fileService, IConfiguration configuration)
        {
            _fileService = fileService;
            _downloadOptions = configuration.GetSection("Downloads");
        }

        /// <summary>
        /// Gets all the files Guids available in the database. 
        /// </summary>
        /// <returns>List of guids from completed and incompleted files</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/kafka/getFilesGuids")]
        public IList<string> GetFilesGuids()
        {
            return _fileService.GetDistinctFileGuids();
        }

        /// <summary>
        /// Downloads a file to the directory specified in the appsettings and removes it from the database.
        /// If the file is incomplete, it shows a message and does nothing.
        /// </summary>
        /// <param name="guid">Guid of the file to download.</param>
        /// <returns>A message specifying what happened.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/kafka/downloadfile")]
        public async Task<IActionResult> DownloadFile(string guid)
        {
            if (_fileService.IsFileComplete(guid))
            {
                var targetDirectory = _downloadOptions.GetValue<string>("TargetDirectory");

                await ReaderHelper.ReadPiecesAndWriteToFile
                (
                    _fileService.GetPiecesByFileGuid(guid), 
                   targetDirectory, 
                    _downloadOptions.GetValue<string>("DefaultFileName")
                );

                _fileService.DeleteFileByGuid(guid);
                return Ok($"The file was downloaded correctly in the specified directory ({targetDirectory}).");
            }
            return Ok("Not downloaded, file is not completed.");
        }
    }
}
