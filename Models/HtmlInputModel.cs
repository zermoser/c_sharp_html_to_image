namespace HtmlToImageAPI.Models
{
    public class HtmlInputModel
    {
        public string HtmlContent { get; set; } = string.Empty;

        // Optional parameters
        public int? Width { get; set; } // default 800
        public string? Format { get; set; } // "png" or "jpg"
        public string? FileName { get; set; } // output file name (without extension)
    }
}
