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

    public class TemplateInputModel
    {
        public string TemplateName { get; set; } = string.Empty; // ชื่อไฟล์ template (ไม่ต้องใส่ .html)
        public Dictionary<string, string>? Replacements { get; set; } // สำหรับแทนที่ placeholder
        public int? Width { get; set; } // default 800
        public string? Format { get; set; } // "png" or "jpg"
        public string? FileName { get; set; } // output file name (without extension)
    }
}