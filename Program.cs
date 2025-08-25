using HtmlToImageAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HtmlToImageService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HtmlToImageAPI V1");
        c.RoutePrefix = string.Empty; // �Դ Swagger UI ��� root (https://localhost:61216/)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
