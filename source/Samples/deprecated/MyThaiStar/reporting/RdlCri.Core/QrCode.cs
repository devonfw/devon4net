using System;
using System.Collections.Generic;
using System.Text;
using fyiReporting.RDL;
using System.DrawingCore;
using System.ComponentModel;
using System.Xml;
using ZXing.QrCode;
using ZXing.Rendering;
using System.IO;

namespace fyiReporting.CRI
{
    public class QrCode : ICustomReportItem
    {

        static public readonly float OptimalHeight = 35.91f;          // Optimal height at magnification 1    
        static public readonly float OptimalWidth = 35.91f;            // Optimal width at mag 1

        #region ICustomReportItem Members

        bool ICustomReportItem.IsDataRegion()
        {
            return false;
        }

        void ICustomReportItem.DrawImage(ref System.DrawingCore.Bitmap bm)
        {
            DrawImage(ref bm, _qrCode);
        }

        /// <summary>
        /// Does the actual drawing of the image.
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="qrcode"></param>
        internal void DrawImage(ref System.DrawingCore.Bitmap bm, string qrcode)
        {

            Graphics g = null;
            g = Graphics.FromImage(bm);
            float mag = PixelConversions.GetMagnification(g, bm.Width, bm.Height, OptimalHeight, OptimalWidth);

            int barHeight = PixelConversions.PixelXFromMm(g, OptimalHeight * mag);
            int barWidth = PixelConversions.PixelYFromMm(g, OptimalWidth * mag);
            var writer = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = barHeight,
                    Width = barWidth,
                    Margin = 0
                }
            };

			writer.Format = ZXing.BarcodeFormat.QR_CODE;
			writer.Options.Height = barHeight;
			writer.Options.Width = barWidth;

			bm = ConvertPixelDataToBitmap(writer.Write(qrcode));
         
        }

        internal void DrawImage(ref MemoryStream ms, string qrcode, int height, int weight)
        {
            Bitmap bm = new Bitmap(weight,height);
            Graphics g = null;
            g = Graphics.FromImage(bm);
            float mag = PixelConversions.GetMagnification(g, bm.Width, bm.Height, OptimalHeight, OptimalWidth);

            int barHeight = PixelConversions.PixelXFromMm(g, OptimalHeight * mag);
            int barWidth = PixelConversions.PixelYFromMm(g, OptimalWidth * mag);
            var writer = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = barHeight,
                    Width = barWidth,
                    Margin = 0
                }
            };

            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options.Height = barHeight;
            writer.Options.Width = barWidth;

            //bm = ConvertPixelDataToBitmap(writer.Write(qrcode));
            ms = ConvertPixelDataToMemoryStream(writer.Write(qrcode));

        }

        private MemoryStream ConvertPixelDataToMemoryStream(PixelData pixelData)
        {

            using (var bitmap = new System.DrawingCore.Bitmap(pixelData.Width, pixelData.Height, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.DrawingCore.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.DrawingCore.Imaging.ImageLockMode.WriteOnly, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG   
                bitmap.Save(ms, System.DrawingCore.Imaging.ImageFormat.Jpeg);

                return ms;
            }

        }

        private Bitmap ConvertPixelDataToBitmap(PixelData pixelData )
        {

            using (var bitmap = new System.DrawingCore.Bitmap(pixelData.Width, pixelData.Height, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb))
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.DrawingCore.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.DrawingCore.Imaging.ImageLockMode.WriteOnly, System.DrawingCore.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG   
                bitmap.Save(ms, System.DrawingCore.Imaging.ImageFormat.Jpeg);

                return bitmap;
            }
            
        }



        /// <summary>
        /// Design time: Draw a hard coded BarCode for design time;  Parameters can't be
        /// relied on since they aren't available.
        /// </summary>
        /// <param name="bm"></param>
        void ICustomReportItem.DrawDesignerImage(ref System.DrawingCore.Bitmap bm)
        {
            DrawImage(ref bm, "https://github.com/majorsilence/My-FyiReporting");
        }

        private string _qrCode = "";
        void ICustomReportItem.SetProperties(IDictionary<string, object> props)
        {
            try
            {
                _qrCode = props["QrCode"].ToString();
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("QrCode property must be specified");
            }
        }

        object ICustomReportItem.GetPropertiesInstance(System.Xml.XmlNode iNode)
        {
            BarCodePropertiesQR bcp = new BarCodePropertiesQR(this, iNode);
            foreach (XmlNode n in iNode.ChildNodes)
            {
                if (n.Name != "CustomProperty")
                    continue;
                string pname = XmlHelpers.GetNamedElementValue(n, "Name", "");
                switch (pname)
                {
                    case "QrCode":
                        bcp.SetQrCode(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;
                    default:
                        break;
                }
            }

            return bcp;
        }

        public void SetPropertiesInstance(System.Xml.XmlNode node, object inst)
        {
            node.RemoveAll();       // Get rid of all properties

            BarCodePropertiesQR bcp = inst as BarCodePropertiesQR;
            if (bcp == null)
                return;


            XmlHelpers.CreateChild(node, "QrCode", bcp.QrCode);

        }

        


        /// <summary>
        /// Design time call: return string with <CustomReportItem> ... </CustomReportItem> syntax for 
        /// the insert.  The string contains a variable {0} which will be substituted with the
        /// configuration name.  This allows the name to be completely controlled by
        /// the configuration file.
        /// </summary>
        /// <returns></returns>
        string ICustomReportItem.GetCustomReportItemXml()
        {
            return "<CustomReportItem><Type>{0}</Type>" +
                string.Format("<Height>{0}mm</Height><Width>{1}mm</Width>", OptimalHeight, OptimalWidth) +
                "<CustomProperties>" +
                "<CustomProperty>" +
                "<Name>QrCode</Name>" +
                "<Value>Enter Your Value</Value>" +
                "</CustomProperty>" +
                "</CustomProperties>" +
                "</CustomReportItem>";
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            return;
        }

        public void DrawImage(ref MemoryStream ms, int height, int weight)
        {
            DrawImage(ref ms, _qrCode, height,weight);
        }

        #endregion

        /// <summary>
        /// BarCodeProperties- All properties are type string to allow for definition of
        /// a runtime expression.
        /// </summary>
        public class BarCodePropertiesQR
        {
            string _QrCode;
            QrCode _bc;
            XmlNode _node;

            internal BarCodePropertiesQR(QrCode bc, XmlNode node)
            {
                _bc = bc;
                _node = node;
            }

            internal void SetQrCode(string ns)
            {
                _QrCode = ns;
            }
            [Category("QrCode"),
               Description("The text string to be encoded as a QR Code.")]
            public string QrCode
            {
                get { return _QrCode; }
                set { _QrCode = value; _bc.SetPropertiesInstance(_node, this); }
            }


        }
    }

    
}
