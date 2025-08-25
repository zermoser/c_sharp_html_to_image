using HtmlToImageAPI.Models;
using HtmlToImageAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToImageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvertController : ControllerBase
    {
        private readonly HtmlToImageService _service;

        public ConvertController(HtmlToImageService service)
        {
            _service = service;
        }

        /// <summary>
        /// Convert raw HTML to image (PNG/JPG).
        /// </summary>
        [HttpPost("html-to-image")]
        [Produces("image/png", "image/jpeg")]
        public IActionResult ConvertHtmlToImage([FromBody] HtmlInputModel model)
        {
            if (model is null || string.IsNullOrWhiteSpace(model.HtmlContent))
                return BadRequest("HTML content cannot be empty.");

            var width = model.Width.GetValueOrDefault(800);
            var format = string.IsNullOrWhiteSpace(model.Format) ? "png" : model.Format.Trim().ToLowerInvariant();
            if (format != "png" && format != "jpg" && format != "jpeg") format = "png";

            var bytes = _service.ConvertHtmlToImage(model.HtmlContent, width, format);

            var ext = format == "jpg" ? "jpg" : (format == "jpeg" ? "jpg" : "png");
            var fileName = string.IsNullOrWhiteSpace(model.FileName) ? "output" : model.FileName;
            return File(bytes, ext == "png" ? "image/png" : "image/jpeg", $"{fileName}.{ext}");
        }
    }
}
