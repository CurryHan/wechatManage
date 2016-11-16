using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using SensingCloud.Helpers;
using SensingCloud.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Routing;

namespace SensingCloud.Handlers
{

    public class QrcodeHandler //: IHttpHandler
    {
        public bool IsReusable { get { return true; } }
        protected RequestContext RequestContext { get; set; }

        public QrcodeHandler() : base() { }

        public QrcodeHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        //public void ProcessRequest(HttpContext context)
        //{
        //    var ticket = context.Request["ticket"];
        //    if (!string.IsNullOrEmpty(ticket))
        //    {
        //        // Retrieve the parameters from the QueryString
        //        var qrcodeText = string.Format(WeChatService.Direct_QrCode_Query, ConstConfig.PlatformHost, ticket);
        //        var codeParams = CodeDescriptor.Init(qrcodeText);

        //        // Encode the content
        //        if (codeParams == null || !codeParams.TryEncode())
        //        {
        //            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            return;
        //        }

        //        // Render the QR code as an image
        //        using (var ms = new MemoryStream())
        //        {
        //            codeParams.Render(ms);
        //            context.Response.ContentType = codeParams.ContentType;
        //            context.Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
        //        }
        //    }
        //}



        /// <summary>
        /// Class containing the description of the QR code and wrapping encoding and rendering.
        /// </summary>
        internal class CodeDescriptor
        {
            public ErrorCorrectionLevel Ecl;
            public string Content;
            public QuietZoneModules QuietZones;
            public int ModuleSize;
            public BitMatrix Matrix;
            public string ContentType;

            /// <summary>
            /// Parse QueryString that define the QR code properties
            /// </summary>
            /// <param name="request">HttpRequest containing HTTP GET data</param>
            /// <returns>A QR code descriptor object</returns>
            public static CodeDescriptor Init(string content)
            {
                var cp = new CodeDescriptor();
                // Error correction level
                cp.Ecl = ErrorCorrectionLevel.L;
                // Code content to encode
                cp.Content = content;

                // Size of the quiet zone

                cp.QuietZones = QuietZoneModules.Two;

                // Module size
                cp.ModuleSize = 12;

                return cp;
            }

            /// <summary>
            /// Encode the content with desired parameters and save the generated Matrix
            /// </summary>
            /// <returns>True if the encoding succeeded, false if the content is empty or too large to fit in a QR code</returns>
            public bool TryEncode()
            {
                var encoder = new QrEncoder(Ecl);
                QrCode qr;
                if (!encoder.TryEncode(Content, out qr))
                    return false;

                Matrix = qr.Matrix;
                return true;
            }

            /// <summary>
            /// Render the Matrix as a PNG image
            /// </summary>
            /// <param name="ms">MemoryStream to store the image bytes into</param>
            internal void Render(MemoryStream ms)
            {
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
                render.WriteToStream(Matrix, ImageFormat.Png, ms);
                ContentType = "image/png";
            }
        }
    }
}