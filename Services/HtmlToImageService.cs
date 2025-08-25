using CoreHtmlToImage;

namespace HtmlToImageAPI.Services
{
    public class HtmlToImageService
    {
        public byte[] ConvertHtmlToImage(string html, int width = 800, string format = "png")
        {
            var converter = new HtmlConverter();
            var imgFormat = format?.ToLowerInvariant() == "jpg" || format?.ToLowerInvariant() == "jpeg"
                ? ImageFormat.Jpg
                : ImageFormat.Png;

            return converter.FromHtmlString(html, width, imgFormat);
        }
    }
}
