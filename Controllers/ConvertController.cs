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
        private readonly IWebHostEnvironment _environment;

        public ConvertController(HtmlToImageService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

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

        [HttpPost("template-to-image")]
        [Produces("image/png", "image/jpeg")]
        public async Task<IActionResult> ConvertTemplateToImage([FromBody] TemplateInputModel model)
        {
            if (model is null || string.IsNullOrWhiteSpace(model.TemplateName))
                return BadRequest("Template name cannot be empty.");

            try
            {
                // อ่านไฟล์ template
                var templatePath = Path.Combine(_environment.ContentRootPath, "template", $"{model.TemplateName}.html");

                if (!System.IO.File.Exists(templatePath))
                    return NotFound($"Template '{model.TemplateName}' not found.");

                var htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);

                // Replace placeholders ถ้ามี
                if (model.Replacements != null)
                {
                    foreach (var replacement in model.Replacements)
                    {
                        htmlContent = htmlContent.Replace($"{{{{{replacement.Key}}}}}", replacement.Value);
                    }
                }

                var width = model.Width.GetValueOrDefault(800);
                var format = string.IsNullOrWhiteSpace(model.Format) ? "png" : model.Format.Trim().ToLowerInvariant();
                if (format != "png" && format != "jpg" && format != "jpeg") format = "png";

                var bytes = _service.ConvertHtmlToImage(htmlContent, width, format);
                var ext = format == "jpg" ? "jpg" : (format == "jpeg" ? "jpg" : "png");
                var fileName = string.IsNullOrWhiteSpace(model.FileName) ? model.TemplateName : model.FileName;

                return File(bytes, ext == "png" ? "image/png" : "image/jpeg", $"{fileName}.{ext}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing template: {ex.Message}");
            }
        }

        [HttpGet("templates")]
        public IActionResult GetAvailableTemplates()
        {
            try
            {
                var templatePath = Path.Combine(_environment.ContentRootPath, "template");

                if (!Directory.Exists(templatePath))
                    return Ok(new { templates = new string[0] });

                var templates = Directory.GetFiles(templatePath, "*.html")
                    .Select(f => Path.GetFileNameWithoutExtension(f))
                    .ToArray();

                return Ok(new { templates });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving templates: {ex.Message}");
            }
        }
    }
}