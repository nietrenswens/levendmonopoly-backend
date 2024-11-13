using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using QRCoder;

namespace LevendMonopoly.Api.Services
{
    public class PDFService : IPDFService
    {
        private readonly IConfiguration _configuration;

        public PDFService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] ExportBuildingsToPdf(List<Building> buildings)
        {
            PdfDocument pdf = new PdfDocument();
            int count = 0;
            foreach (Building building in buildings)
            {
                count++;
                PdfPage page = pdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 32);
                XFont small = new XFont("Verdana", 8);
                gfx.DrawString(building.Name, font, XBrushes.Black, new XRect(0, 0, page.Width.Point, page.Height.Point / 4), XStringFormats.Center);
                gfx.DrawString($"{count}", small, XBrushes.Black, new XRect(50, page.Height.Point - 50, 0,0), XStringFormats.BaseLineLeft);
                gfx.DrawString("Levend Monopoly", small, XBrushes.Black, new XRect(page.Width.Point - 50, page.Height.Point - 50, 0,0), XStringFormats.BaseLineRight);

                if (!string.IsNullOrEmpty(building.Image))
                {
                    var base64Data = building.Image.Substring(building.Image.IndexOf(",") + 1);
                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                    using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length, publiclyVisible: true, writable: false))
                    {
                        XImage image = XImage.FromStream(ms); // Convert base64 image to stream compatible with PdfSharp
                        var imgWidth = page.Width.Point / 1.5;
                        var imgHeight = page.Height.Point / 1.5;
                        double aspectRatio = image.PixelWidth / (double)image.PixelHeight;

                        if (imgWidth / aspectRatio <= imgHeight)
                        {
                            imgHeight = imgWidth / aspectRatio;
                        }
                        else
                        {
                            imgWidth = imgHeight * aspectRatio;
                        }

                        var x = (page.Width.Point - imgWidth) / 2;
                        var y = (page.Height.Point - imgHeight) / 2 - 50;
                        gfx.DrawImage(image, x, y, imgWidth, imgHeight);
                    }
                }

                // Generate and draw QR code image
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                var url = _configuration.GetValue<string>("QRCode:Url");
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{url}{building.Id}", QRCodeGenerator.ECCLevel.Q);
                var bitmap = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeBytes = bitmap.GetGraphic(10);
                MemoryStream qrStream = new MemoryStream(qrCodeBytes, 0, qrCodeBytes.Length, publiclyVisible: true, writable: false);
                qrStream.Position = 0;
                XImage qrImage = XImage.FromStream(qrStream);
                gfx.DrawImage(qrImage, page.Width.Point / 2 - (100 / 2), page.Height.Point - 110 - 100, 100, 100);
            }

            byte[] pdfBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                pdf.Save(ms);
                pdfBytes = ms.ToArray();
            }
            return pdfBytes;
        }



    }
}
