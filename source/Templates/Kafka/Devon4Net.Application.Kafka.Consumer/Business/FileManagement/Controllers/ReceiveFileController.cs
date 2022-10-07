using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Helpers;
using Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.Kafka.Consumer.Business.FileManagement.Controllers
{
    public class ReceiveFileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IConfigurationSection _downloadOptions;

        public ReceiveFileController(IFileService fileService, IConfiguration configuration)
        {
            _fileService = fileService;
            _downloadOptions = configuration.GetSection("Downloads");
        }

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
                await ReaderHelper.ReadPiecesAndWriteToFile
                (
                    _fileService.GetPiecesByFileGuid(guid).OrderBy(o => o.Position), 
                    _downloadOptions.GetValue<string>("TargetDirectory"), 
                    _downloadOptions.GetValue<string>("DefaultFileName")
                );

                _fileService.DeleteFileByGuid(guid);
                return Ok();
            }
            return Ok("Not downloaded, file is not completed.");
        }
    }
}
