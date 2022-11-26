using SafeShop.Service.Infrastructure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Implementation
{
    public class ResizerService : IResizerService
    {
        public byte[] ResizeImage(byte[] img, int width = 240, int height = 240)
        {
            var image = Image.Load<Rgba32>(img);
            image.Mutate(x => x.Resize(width, height));
            using (var ms = new MemoryStream())
            {
                image.Save(ms, new PngEncoder());
                return ms.ToArray();
            }
        }
    }
}
