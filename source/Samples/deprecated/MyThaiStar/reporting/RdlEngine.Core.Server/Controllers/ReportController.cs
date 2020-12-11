using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using fyiReporting.RDL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RdlEngine.Core.Server.Dto;
using RdlEngine.Core.Server.Utils;
using ReportTests.Utils;

namespace RdlEngine.Core.Server.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IConfiguration Configuration { get; set; }
        public ReportController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Generates the report from the template name
        /// </summary>
        /// <param name="templateName">The template name to generate the report.</param>
        /// <param name="paramList">Contains a pair key-Value</param>
        /// <returns>A string containing a pair key-Value</returns>
        /// <response code="201">Account created</response>
        /// <response code="400">Username already in use</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/report/GetReport")]
        public async Task<IActionResult> GetReport([FromBody]ReportingDto reportingData)
        {
            var reportingUtils = new ReportingUtils();
            var reportType = reportingData.ReportType != null ? (OutputPresentationType)reportingData.ReportType.Value : OutputPresentationType.PDF;
            var reportStream = reportingUtils.GetReport(reportingData.TemplateName, reportingData.ParamList, reportType);
            
            switch (reportType)
            {
                case OutputPresentationType.HTML: //html 0
                case OutputPresentationType.XML: //xml 4
                case OutputPresentationType.ASPHTML: //ASPHTML 5
                case OutputPresentationType.Internal: //Internal 6
                case OutputPresentationType.MHTML: //MHTML 7
                case OutputPresentationType.CSV: //CSV 8
                    return new ObjectResult(reportStream);
                case OutputPresentationType.RenderPdf_iTextSharp: //pdf_itext 1
                case OutputPresentationType.PDF: //pdf 2
                case OutputPresentationType.PDFOldStyle: //PDFOldStyle 3
                    return new FileStreamResult(reportStream, "application/pdf");
                case OutputPresentationType.RTF: //RTF 9
                    return new FileStreamResult(reportStream, "application/rtf");
                case OutputPresentationType.Word: //word 10
                    return new FileStreamResult(reportStream, "application/vnd.ms-word");
                case OutputPresentationType.Excel: //Excel 11
                    return new FileStreamResult(reportStream, "application/vnd.ms-excel");
                case OutputPresentationType.TIF: //TIF 12
                case OutputPresentationType.TIFBW: //TIFBW 13
                    return new FileStreamResult(reportStream, "image/tiff");
                case OutputPresentationType.Excel2003: //Excel2003 14
                    return new FileStreamResult(reportStream, "application/vnd.ms-excel");
            }

            return new FileStreamResult(reportStream, "application/pdf");
        }


        ///// <summary>
        ///// Generates the report from the template name
        ///// </summary>
        ///// <param name="templateName">The template name to generate the report.</param>
        ///// <param name="paramList">Contains a pair key-Value</param>
        ///// <returns>A string containing a pair key-Value</returns>
        ///// <response code="201">Account created</response>
        ///// <response code="400">Username already in use</response>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("/api/v1/report/GetPdf")]
        //public async Task<IActionResult> GetPdf([FromBody]ReportingDto reportingData)
        //{
        //    var reportingUtils = new ReportingUtils();
        //    var reportStream = reportingUtils.GetPdfReport(reportingData.TemplateName, reportingData.ParamList);

        //    return new FileStreamResult(reportStream, "application/pdf");

        //}

        ///// <summary>
        ///// Generates the report from the template name
        ///// </summary>
        ///// <param name="templateName">The template name to generate the report.</param>
        ///// <param name="paramList">Contains a pair key-Value</param>
        ///// <returns>A string containing a pair key-Value</returns>
        ///// <response code="201">Account created</response>
        ///// <response code="400">Username already in use</response>
        //[HttpGet]
        //[AllowAnonymous]
        //[Route("/api/v1/report/GetHtml")]
        //public async Task<IActionResult> GetHtml(ReportingDto reportingData)
        //{
        //    var reportingUtils = new ReportingUtils();
        //    var reportStream = reportingUtils.GetPdfHtml(reportingData.TemplateName, reportingData.ParamList);

        //    return new ObjectResult(reportStream);

        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="templateName"></param>
        ///// <param name="paramList"></param>
        ///// <returns>A string containing a pair key-Value</returns>
        ///// <response code="201">Account created</response>
        ///// <response code="400">Username already in use</response>
        //[HttpGet]
        //public void Generate(string templateName, List<Param> paramList)
        //{
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="templateName"></param>
        ///// <param name="connectionString"></param>
        ///// <param name="paramList"></param>
        ///// <returns>A string containing a pair key-Value</returns>
        ///// <response code="201">Account created</response>
        ///// <response code="400">Username already in use</response>
        //[HttpGet]
        //public void Generate(string templateName, string connectionString, List<Param> paramList)
        //{
        //}


    }
}
