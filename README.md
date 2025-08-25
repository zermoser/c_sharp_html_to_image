# HtmlToImageAPI (.NET 8, ASP.NET Core Web API + Swagger)

API แปลง HTML เป็นไฟล์ภาพด้วยไลบรารี `CoreHtmlToImage` (wkhtmltoimage) รองรับ PNG/JPG และกำหนดความกว้างได้

## โครงสร้าง
- Controllers/ConvertController.cs
- Services/HtmlToImageService.cs
- Models/HtmlInputModel.cs
- Program.cs
- HtmlToImageAPI.csproj

## การใช้งาน
1) ติดตั้ง .NET 8 SDK
2) เปิดโฟลเดอร์นี้แล้วรัน:
```bash
dotnet restore
dotnet run
```
3) เปิด Swagger:
```
https://localhost:5001/swagger
```
หรือ:
```
http://localhost:5000/swagger
```

### ตัวอย่าง Request (Swagger หรือ curl)
```bash
curl -X POST "https://localhost:5001/api/convert/html-to-image"   -H "Content-Type: application/json"   --data '{
    "htmlContent": "<div style=\"padding:20px;color:blue\">Hello Image</div>",
    "width": 900,
    "format": "png",
    "fileName": "result"
  }' --output result.png
```

> หมายเหตุ: หากรันใน Production ให้พิจารณาปิด Swagger หรือกำหนดเงื่อนไข environment ตามความปลอดภัยของคุณ


examplet req
{
  "templateName": "test",
  "width": 1200,
  "format": "png",
  "fileName": "my-output",
  "replacements": {
    "title": "Hello World",
    "content": "This is dynamic content"
  }
}