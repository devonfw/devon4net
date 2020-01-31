using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RdlEngine.Core.Render
{
    public class RenderDoc
    {
        public void HtmlToWord(string html, string destinationFileName)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(
                memoryStream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.MainDocumentPart;
                if (mainPart == null)
                {
                    mainPart = wordDocument.AddMainDocumentPart();
                    new DocumentFormat.OpenXml.Wordprocessing.Document(new Body()).Save(mainPart);
                }

                HtmlConverter converter = new HtmlConverter(mainPart);
                converter.ImageProcessing = ImageProcessing.AutomaticDownload;
                Body body = mainPart.Document.Body;

                IList<OpenXmlCompositeElement> paragraphs = converter.Parse(html);
                body.Append(paragraphs);

                mainPart.Document.Save();                
                File.WriteAllBytes(destinationFileName, memoryStream.ToArray());
            }
        }


        public  void HtmlToWordPartial(string html, string destinationFileName)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(destinationFileName, WordprocessingDocumentType.Document))
            {
                string altChunkId = "myId";
                
                MainDocumentPart mainPart = wordDocument.MainDocumentPart;
                if (mainPart == null)
                {
                    mainPart = wordDocument.AddMainDocumentPart();
                    new DocumentFormat.OpenXml.Wordprocessing.Document(new Body()).Save(mainPart);
                }
                MemoryStream ms = new MemoryStream(new UTF8Encoding(true).GetPreamble().Concat(Encoding.UTF8.GetBytes(html)).ToArray());

                // Uncomment the following line to create an invalid word document.
                // MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("<h1>HELLO</h1>"));

                // Create alternative format import part.
                AlternativeFormatImportPart formatImportPart = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Html, altChunkId);
                //ms.Seek(0, SeekOrigin.Begin);

                // Feed HTML data into format import part (chunk).
                formatImportPart.FeedData(ms);
                AltChunk altChunk = new AltChunk();
                altChunk.Id = altChunkId;

                mainPart.Document.Body.Append(altChunk);
                wordDocument.Save();
                wordDocument.Close();
            }
        }
    }
}
