using System.Collections.Generic;
using System.IO;
using MimeKit;
using MimeKit.Utils;

namespace OASP4Net.Business.Common.EmailManagement.DataType
{
    public class EmailMessage
    {
        private string From { get; set; }
        private string SenderName { get; set; }
        private List<string> To { get; set; }
        private string Subject { get; set; }
        //private string Body { get; set; }
        private List<MimePart> AttachmentList { get; set; }
        private BodyBuilder MessageBuilder { get; set; }
        private MimeMessage MailMessage { get; set; }
        private EmailBodyType EmailBodyType { get; set; }

        public EmailMessage(string subject, string senderName, string emailFrom, List<string> emailDestinationList)
        {
            MailMessage = new MimeMessage();
            EmailBodyType = EmailBodyType.PlainText;
            Subject = subject;
            From = emailFrom;
            SenderName = senderName;
            To = emailDestinationList;
            AttachmentList = new List<MimePart>();
            MessageBuilder = new BodyBuilder();
        }


        /// <summary>
        /// 
        ///        multipart/alternative
        ///       text/plain
        ///       multipart/related
        ///       text/html
        ///      image/jpeg
        ///      video/mp4
        ///       image/png
        /// 
        /// 
        /// </summary>
        /// <param name="mediaType"></param>
        /// <param name="mediaSubtype"></param>
        /// <param name="fullPath"></param>
        /// <param name="contentTransferEncoding"></param>
        /// <param name="contentObjectEncoding"></param>
        public void AddAttachment(string mediaType, string mediaSubtype, string fullPath, ContentEncoding contentTransferEncoding = ContentEncoding.Base64, ContentEncoding contentObjectEncoding = ContentEncoding.Default)
        {
            var attachment = new MimePart(mediaType, mediaSubtype)
            {
                ContentObject = new ContentObject(File.OpenRead(fullPath), contentObjectEncoding),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = contentTransferEncoding,
                FileName = Path.GetFileName(fullPath)
            };


            AttachmentList.Add(attachment);
        }

        public void AddBody(string stringBody, List<string> linkedResourcesPathList, EmailBodyType emailBodyType)
        {
            EmailBodyType = emailBodyType;
            foreach (var item in linkedResourcesPathList)
            {
                var resouce = MessageBuilder.LinkedResources.Add(item);
                resouce.ContentId = MimeUtils.GenerateMessageId();
            }

            switch (emailBodyType)
            {
                case EmailBodyType.HtmlText:
                    MessageBuilder.HtmlBody = stringBody;
                    foreach (var res in linkedResourcesPathList)
                    {
                        if (File.Exists(res)) MessageBuilder.LinkedResources.Add(res);
                    }

                    //linkedResourcesPathList.Any() ? string.Format(stringBody, MessageBuilder.LinkedResources.Select(l => l.ContentId).ToList()) : stringBody;
                    break;
                case EmailBodyType.PlainText:
                    MessageBuilder.TextBody = stringBody;
                    break;
                default:
                    MessageBuilder.TextBody = stringBody;
                    break;
            }


        }


        private void CreateMessage()
        {
            MailMessage.From.Add(new MailboxAddress(SenderName, From));
            foreach (var destinationMail in To)
            {
                MailMessage.To.Add(new MailboxAddress(destinationMail));
            }

            MailMessage.Subject = Subject;
            MailMessage.Body = AddAttachments();
        }

        private Multipart AddAttachments()
        {
            var multipart = new Multipart("mixed") { MessageBuilder.ToMessageBody() };

            foreach (var item in AttachmentList)
            {
                multipart.Add(item);
            }
            return multipart;
        }

        public MimeMessage GetEmailMessage()
        {
            CreateMessage();
            return MailMessage;
        }

    }
}
