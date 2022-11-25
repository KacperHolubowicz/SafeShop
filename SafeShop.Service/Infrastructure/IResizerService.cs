using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Infrastructure
{
    public interface IResizerService
    {
        byte[] ResizeImage(byte[] image, int width = 480, int height = 480);
    }
}
