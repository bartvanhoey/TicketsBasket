using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace TicketsBasket.Functions.ThumbGenerator
{
  public static class ThumbGenerator
    {
        // enter  `func start` in a command line to start AzureFunction ThumbGenerator 

        [FunctionName("ThumbGenerator")]
        public static void Run([BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")]Stream imageStream, 
                                [Blob("thumb-images/{name}", FileAccess.Write)]Stream thumbStream, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {imageStream.Length} Bytes");

            var allowedExtensions = new [] {".jpg", ".png", ".bmp"};
            string extension = Path.GetExtension(name);
            if (!allowedExtensions.Contains(extension))
            {
                log.LogError($"{name} is not a valid image");
                return;
            }

            var image = Image.FromStream(imageStream);
            int bitmapWidth = 200;
            double ratio = Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height);
            var bitmapHeight = Convert.ToInt32(Math.Round(bitmapWidth/ratio, 0)) ;

            var bitmap = new Bitmap(image);
            var thumbImage = bitmap.GetThumbnailImage(bitmapWidth, bitmapHeight, null, IntPtr.Zero);

            thumbImage.Save(thumbStream, image.RawFormat);
            log.LogInformation($"Thumbnail for {name} created. Dimensions {bitmapWidth}x{bitmapHeight}");
        }
    }
}
