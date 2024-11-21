using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using QRCoder;
using System.Drawing;

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
                XFont font = new XFont("Arial", 16);
                XFont big = new XFont("Arial", 48);
                XFont small = new XFont("Arial", 8);
                gfx.DrawString(building.Name, big, XBrushes.Black, new XRect(0, 0, page.Width.Point, page.Height.Point / 4), XStringFormats.Center);
                gfx.DrawString($"€{building.Price}", font, XBrushes.Black, new XRect(0, 60, page.Width.Point, page.Height.Point / 4), XStringFormats.Center);
                gfx.DrawString($"{count}", small, XBrushes.Black, new XRect(50, page.Height.Point - 50, 0,0), XStringFormats.BaseLineLeft);
                gfx.DrawString("Levend Monopoly", small, XBrushes.Black, new XRect(page.Width.Point - 50, page.Height.Point - 50, 0,0), XStringFormats.BaseLineRight);

                if (!string.IsNullOrEmpty(building.Image))
                {
                    var base64Data = building.Image.Substring(building.Image.IndexOf(",") + 1);
                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                    try
                    {
                        DrawImageCentered(gfx, imageBytes, page.Width.Point / 1.5, page.Height.Point / 1.5, page.Width.Point, page.Height.Point);
                    } catch (Exception e)
                    {
                        Console.WriteLine($"An error occured while trying to draw the image for '{building.Name}({building.Id})': {e.Message}");
                        var backupImage = File.OpenRead("Assets/Images/NoImage.jpg");
                        byte[] backupImageBytes = new byte[backupImage.Length];
                        backupImage.Read(backupImageBytes, 0, (int)backupImage.Length);
                        DrawImageCentered(gfx, backupImageBytes, page.Width.Point / 1.5, page.Height.Point / 1.5, page.Width.Point, page.Height.Point);
                    }
                }

                // Generate and draw QR code image
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                var url = _configuration.GetValue<string>("QRCode:Url");
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{url}{building.Id}", QRCodeGenerator.ECCLevel.Q);
                var bitmap = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeBytes = bitmap.GetGraphic(10);
                try
                {
                    DrawImage(gfx, qrCodeBytes, page.Width.Point / 2 - (100 / 2), page.Height.Point - 110 - 100, 100, 100);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occured while trying to draw the QR image for '{building.Name}({building.Id})': {e.Message}");
                }
            }

            byte[] pdfBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                pdf.Save(ms);
                pdfBytes = ms.ToArray();
            }
            return pdfBytes;
        }


        private static void DrawImage(XGraphics gfx, byte[] imageData, double x, double y, double width, double height)
        {
            using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length, publiclyVisible: true, writable: false))
            {
                Stream PNGstream = ConvertToPNGStream(ms);
                XImage image = XImage.FromStream(PNGstream);

                var (imgWidth, imgHeight) = WidthAndHeightByAspectRatio(width, height, image.PixelWidth, image.PixelHeight);
                gfx.DrawImage(image, x, y, imgWidth, imgHeight);
            }
        }

        private static void DrawImageCentered(XGraphics gfx, byte[] imageData, double width, double height, double pageWidth, double pageHeight)
        {
            using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length, publiclyVisible: true, writable: false))
            {
                Stream PNGstream = ConvertToPNGStream(ms);
                XImage image = XImage.FromStream(PNGstream);
                var (imgWidth, imgHeight) = WidthAndHeightByAspectRatio(width, height, image.PixelWidth, image.PixelHeight);
                var x = pageWidth / 2 - imgWidth / 2;
                var y = pageHeight / 2 - imgHeight / 2;
                gfx.DrawImage(image, x, y, imgWidth, imgHeight);
            }
        }

        private static Tuple<double, double> WidthAndHeightByAspectRatio(double desiredWidth, double desiredHeight, double imageWidth, double imageHeight)
        {
            double aspectRatio = imageWidth / (double)imageHeight;

            if (desiredWidth / aspectRatio <= desiredHeight)
            {
                desiredHeight = desiredWidth / aspectRatio;
            }
            else
            {
                desiredWidth = desiredHeight * aspectRatio;
            }
            return new Tuple<double, double>(desiredWidth, desiredHeight);
        }

        private static Stream ConvertToPNGStream(Stream nonPDFStream)
        {
            MemoryStream strm = new MemoryStream();
            Image img = Image.FromStream(nonPDFStream);
            img.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
            return strm;
        }
    }
}
