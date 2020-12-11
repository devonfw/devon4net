using fyiReporting.RDL;
using RdlEngine.Core.Render;
using RdlEngine.Core.Server.Controllers;
using ReportTests.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RdlEngine.Core.Server.Utils
{
    public class ReportingUtils
    {        
        private string RootFolder { get; set; }
        private string TempFolder { get; set; }
        private string TemplateFolder { get; set; }
        private string ReportFileName { get; set; }

        public ReportingUtils()
        {
            Setup();
        }

        public Stream GetReport(string templateName, List<Param> paramList, OutputPresentationType reportType)
        {
            
            var outputFile = BuildReport(templateName, paramList, reportType);
            var result = GetMemoryStreamFromFileName(outputFile);
            DeleteFile(outputFile);
            return result;
        }

        #region private methods
        private void Setup()
        {
            RootFolder = Directory.GetCurrentDirectory();
            TempFolder = ServerVariables.TempFolder;
            TemplateFolder = Path.Combine(RootFolder, ServerVariables.TemplatesFolder);
        }

        private string GetReportFileName(string reportTemplateName)
        {
            ReportFileName = $"{Path.Combine(TemplateFolder, reportTemplateName)}.rdl";
            return ReportFileName;
        }

        private string GetOutputFileName(string templateName, OutputPresentationType outputPresentationType)
        {
            var now = System.DateTime.UtcNow;
            return Path.Combine(RootFolder, TempFolder) + @"/" + $"{templateName}{GeneralUtils.GetDateTimeMark()}_{now.Millisecond}.{GetOutputFileNameExtension(outputPresentationType)}";
            
        }

        private string GetOutputFileNameExtension(OutputPresentationType outputPresentationType)
        {
            switch (outputPresentationType)
            {
                case OutputPresentationType.PDF:
                    return "pdf";
                case OutputPresentationType.XML:
                    return "xml";
                case OutputPresentationType.CSV:
                    return "csv";
                case OutputPresentationType.Excel:
                    return "xlsx";
                case OutputPresentationType.Excel2003:
                    return "xls";
                case OutputPresentationType.RenderPdf_iTextSharp:
                    return "pdf";
                case OutputPresentationType.Word:
                    return "docx";
                case OutputPresentationType.HTML:
                    return "html";
                case OutputPresentationType.PDFOldStyle:
                    return "pdf";
                case OutputPresentationType.ASPHTML:
                    return "ASPHTML";
                case OutputPresentationType.Internal:
                    return "int";
                case OutputPresentationType.MHTML:
                    return "MHTML";
                case OutputPresentationType.RTF:
                    return "rtf";
                case OutputPresentationType.TIF:
                    return "tif";
                case OutputPresentationType.TIFBW:
                    return "tif";
            }
            
            return string.Empty;
        }


        private Report GetReport(string templateName, List<Param> paramList)
        {
            var sqlParams = paramList.Where(p => p.Type == ParamType.SQL).ToList();
            var source = File.ReadAllText(GetReportFileName(templateName));
            var rdlp = new RDLParser(source) { Folder = Path.Combine(RootFolder, TemplateFolder) };
            
            return rdlp.Parse(sqlParams);
        }

        /// <summary>
        /// Changes the embebed images from base 64 or set the values for report params
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="report"></param>
        private Dictionary<string, string> GetReportParams(List<Param> paramList, ref Report report)
        {
            var dictionary = new Dictionary<string, string>();
            if (paramList != null && paramList.Any())
            {
                

                foreach (var item in paramList)
                {
                    if (item.Type == ParamType.Image)
                        report.SetEmbebedImageItem(item.Key, item.Value);

                    else dictionary.Add(item.Key, item.Value);
                }

                
                
                //report.RunGetData(dictionary);
            }
            return dictionary;


        }

        private MemoryStream GetMemoryStreamFromFileName(string filename)
        {
            var theFile = new MemoryStream(System.IO.File.ReadAllBytes(filename));            
            return theFile;
        }

        private string BuildReport(string templateName, List<Param> paramList, OutputPresentationType output)
        {
            var outputFileName = GetOutputFileName(templateName, output);
            GeneralUtils.ChangeCurrentCultrue("es-ES");
            var report = GetReport(templateName, paramList);            
            var paramsList = GetReportParams(paramList, ref report);
            report.RunGetData(paramsList);
            var pages = report.BuildPages();

            var sg = new OneFileStreamGen(outputFileName, true);
            

            switch (output)
            {
                case OutputPresentationType.Word:
                    report.RunRender(sg, OutputPresentationType.HTML, pages);
                    outputFileName = GetWordDocumentFromHtml(outputFileName);
                    break;
                case OutputPresentationType.RenderPdf_iTextSharp:
                case OutputPresentationType.PDF:
                    report.ItextPDF = true;
                    report.RunRenderPdf(sg, pages);
                    break;
                case OutputPresentationType.PDFOldStyle:
                    report.ItextPDF = false;
                    report.RunRenderPdf(sg, pages);
                    break;
                default:
                    report.RunRender(sg, output, pages);
                    break;
            }
            
            sg.CloseMainStream();
            sg.Dispose();            

            return outputFileName;
        }

        private string GetWordDocumentFromHtml(string outputFileName)
        {
            var renderDoc = new RenderDoc();
            var html = File.ReadAllText(outputFileName);
            var docFileName = Path.Combine(RootFolder, TempFolder) + @"\" + $"{Path.GetFileNameWithoutExtension(outputFileName)}_mod.{GetOutputFileNameExtension(OutputPresentationType.Word)}";
            renderDoc.HtmlToWordPartial(html, docFileName);
            DeleteFile(outputFileName);


            return docFileName;
        }


        public void DeleteFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message} : {ex.InnerException}");
            }
        }
        #endregion


    }
}
