namespace SafeShop.Service.Infrastructure
{
    public interface IResizerService
    {
        byte[] ResizeImage(byte[] image, int width = 480, int height = 480);
    }
}
