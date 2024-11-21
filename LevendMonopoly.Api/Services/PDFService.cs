using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using QRCoder;
using SixLabors.ImageSharp;
using System.Drawing;

namespace LevendMonopoly.Api.Services
{
    public class PDFService : IPDFService
    {
        private readonly IConfiguration _configuration;
        private Mutex mutex;
        private Mutex stringMutex;
        private Mutex imageMutex;
        private int count;

        private readonly XFont font = new XFont("Arial", 16);
        private readonly XFont big = new XFont("Arial", 48);
        private readonly XFont small = new XFont("Arial", 8);

        public PDFService(IConfiguration configuration)
        {
            _configuration = configuration;
            mutex = new Mutex();
            stringMutex = new();
            imageMutex = new();
            count = 0;
        }

        public byte[] ExportBuildingsToPdf(List<Building> buildings)
        {
            count = 0;
            PdfDocument pdf = new PdfDocument();

            // Pre-create pages to ensure order is maintained
            var pages = buildings.Select(_ => pdf.AddPage()).ToArray();

            // Render each page concurrently
            var tasks = buildings
                .Select((building, index) => Task.Run(() => RenderPage(pages[index], building)))
                .ToArray();

            // Wait for all tasks to complete
            Task.WaitAll(tasks);

            byte[] pdfBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                pdf.Save(ms);
                pdfBytes = ms.ToArray();
            }
            return pdfBytes;
        }

        private void RenderPage(PdfPage page, Building building)
        {
            int ownCount;
            mutex.WaitOne();
            count++;
            ownCount = count;
            mutex.ReleaseMutex();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            stringMutex.WaitOne();
            gfx.DrawString(building.Name, big, XBrushes.Black, new XRect(0, 0, page.Width.Point, page.Height.Point / 4), XStringFormats.Center);
            gfx.DrawString($"€{building.Price}", font, XBrushes.Black, new XRect(0, 60, page.Width.Point, page.Height.Point / 4), XStringFormats.Center);
            gfx.DrawString($"{ownCount}", small, XBrushes.Black, new XRect(50, page.Height.Point - 50, 0, 0), XStringFormats.BaseLineLeft);
            gfx.DrawString("Levend Monopoly", small, XBrushes.Black, new XRect(page.Width.Point - 50, page.Height.Point - 50, 0, 0), XStringFormats.BaseLineRight);
            stringMutex.ReleaseMutex();

            if (!string.IsNullOrEmpty(building.Image))
            {
                var base64Data = building.Image.Substring(building.Image.IndexOf(",") + 1);
                byte[] imageBytes = Convert.FromBase64String(base64Data);

                try
                {
                    DrawImageCentered(gfx, imageBytes, page.Width.Point / 1.5, page.Height.Point / 1.5, page.Width.Point, page.Height.Point);
                }
                catch (Exception e)
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


        private void DrawImage(XGraphics gfx, byte[] imageData, double x, double y, double width, double height)
        {
            using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length, publiclyVisible: true, writable: false))
            {
                Stream PNGstream = ConvertToPNGStream(ms);
                XImage image = XImage.FromStream(PNGstream);

                var (imgWidth, imgHeight) = WidthAndHeightByAspectRatio(width, height, image.PixelWidth, image.PixelHeight);
                imageMutex.WaitOne();
                gfx.DrawImage(image, x, y, imgWidth, imgHeight);
                imageMutex.ReleaseMutex();
            }
        }

        private void DrawImageCentered(XGraphics gfx, byte[] imageData, double width, double height, double pageWidth, double pageHeight)
        {
            using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length, publiclyVisible: true, writable: false))
            {
                Stream PNGstream = ConvertToPNGStream(ms);
                XImage image = XImage.FromStream(PNGstream);
                var (imgWidth, imgHeight) = WidthAndHeightByAspectRatio(width, height, image.PixelWidth, image.PixelHeight);
                var x = pageWidth / 2 - imgWidth / 2;
                var y = pageHeight / 2 - imgHeight / 2;
                imageMutex.WaitOne();
                gfx.DrawImage(image, x, y, imgWidth, imgHeight);
                imageMutex.ReleaseMutex();
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
            MemoryStream ms = new MemoryStream();
            SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(nonPDFStream);
            image.SaveAsPng(ms);
            ms.Position = 0;
            return ms;
        }
    }
}
