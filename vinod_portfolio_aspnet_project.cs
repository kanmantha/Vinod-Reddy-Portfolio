# Portfolio ASP.NET Core MVC (Vinod Reddy Kanmanthareddy)

This repository contains a minimal, professional ASP.NET Core MVC portfolio website tailored to Vinod Reddy Kanmanthareddy's resume. It uses .NET 7 minimal hosting, Razor views, responsive layout (Tailwind-like utility classes replaced by custom CSS), and is ready to push to GitHub.

---

## Project structure
```
VinodPortfolio/
├── VinodPortfolio.sln
├── VinodPortfolio/
│   ├── VinodPortfolio.csproj
│   ├── Program.cs
│   ├── Controllers/
│   │   └── HomeController.cs
│   ├── Models/
│   │   └── ResumeModel.cs
│   ├── Views/
│   │   ├── Shared/_Layout.cshtml
│   │   ├── Home/Index.cshtml
│   │   ├── Home/About.cshtml
│   │   └── Shared/_ContactPartial.cshtml
│   ├── wwwroot/
│   │   ├── css/site.css
│   │   └── images/ (drop your photos/cert images here)
│   └── appsettings.json
└── README.md
```

---

## VinodPortfolio.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
</Project>
```

---

## Program.cs

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

---

## Controllers/HomeController.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using VinodPortfolio.Models;

namespace VinodPortfolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = ResumeModel.Sample();
            return View(model);
        }

        public IActionResult About()
        {
            var model = ResumeModel.Sample();
            return View(model);
        }
    }
}
```

---

## Models/ResumeModel.cs

```csharp
namespace VinodPortfolio.Models
{
    public class ResumeModel
    {
        public string FullName { get; set; } = "Vinod Reddy Kanmanthareddy";
        public string Title { get; set; } = "Technical Project Manager | Software Architect | Engineering Manager";
        public string Email { get; set; } = "vinod.kanmantha@gmail.com";
        public string Phone { get; set; } = "+91-9640598454";
        public string LinkedIn { get; set; } = "https://linkedin.com/in/vinod-reddy-kanmanthareddy";

        public string Summary { get; set; }
        public List<string> KeySkills { get; set; } = new();
        public List<Experience> Experiences { get; set; } = new();

        public static ResumeModel Sample()
        {
            return new ResumeModel
            {
                Summary = "Results-driven technology leader with 18+ years of experience in cloud-native enterprise architecture, AI-driven analytics, and DevOps modernization.",
                KeySkills = new List<string> { "Azure", "Kubernetes", "Docker", "Terraform", ".NET", "ASP.NET Core", "React", "CI/CD", "Python", "MLOps" },
                Experiences = new List<Experience>
                {
                    new Experience { Role = "Technical Project Lead - VGSPL (Client: Amerant Inc.)", Period = "Jun 2022 – Present", Details = "Led design and delivery of a cloud-native citizen benefits platform using AKS, Dockerized .NET 6 microservices, built CI/CD automation, and introduced observability." },
                    new Experience { Role = "Technical Project Lead - KSAP Inc", Period = "Mar 2021 – Jun 2022", Details = "Modernized transportation platform using FastAPI and Angular 17, added AI forecasting and CI/CD automation." },
                    new Experience { Role = "Technical Lead - 6D PropTech (Financial Markets)", Period = "Jun 2019 – Feb 2021", Details = "Architected microservices-based financial analytics platform integrating Bloomberg APIs and AI-driven reconciliation." }
                }
            };
        }
    }

    public class Experience
    {
        public string Role { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
```

---

## Views/Shared/_Layout.cshtml

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Vinod Reddy</title>
    <link rel="stylesheet" href="/css/site.css" />
</head>
<body>
    <header class="site-header">
        <div class="container">
            <h1 class="brand"><a href="/">@Model?.FullName ?? "Vinod Reddy"</a></h1>
            <nav class="nav">
                <a href="/">Home</a>
                <a href="/Home/About">About</a>
                <a href="#contact">Contact</a>
            </nav>
        </div>
    </header>

    <main class="container">
        @RenderBody()
    </main>

    <footer class="site-footer">
        <div class="container">
            <p>&copy; @DateTime.Now.Year Vinod Reddy Kanmanthareddy</p>
        </div>
    </footer>
</body>
</html>
```

---

## Views/Home/Index.cshtml

```html
@model VinodPortfolio.Models.ResumeModel
@{
    ViewData["Title"] = "Home";
}

<section class="hero">
    <div class="container">
        <h2>@Model.FullName</h2>
        <p class="title">@Model.Title</p>
        <p class="summary">@Model.Summary</p>
        <p class="contact-inline">Email: <a href="mailto:@Model.Email">@Model.Email</a> | Phone: @Model.Phone</p>
    </div>
</section>

<section>
    <div class="container grid">
        <div class="card">
            <h3>Key Skills</h3>
            <ul>
                @foreach(var skill in Model.KeySkills){ <li>@skill</li> }
            </ul>
        </div>

        <div class="card">
            <h3>Experience</h3>
            @foreach(var e in Model.Experiences){
                <div class="exp">
                    <strong>@e.Role</strong>
                    <div class="period">@e.Period</div>
                    <p>@e.Details</p>
                </div>
            }
        </div>
    </div>
</section>

@await Html.PartialAsync("_ContactPartial", Model)
```

---

## Views/Home/About.cshtml

```html
@model VinodPortfolio.Models.ResumeModel
@{
    ViewData["Title"] = "About";
}

<h2>About</h2>
<p>@Model.Summary</p>

<h3>Education & Certifications</h3>
<ul>
    <li>Master of Science, Computer Science - University of Northern Virginia</li>
    <li>B.Tech, Computer Science - Madras University</li>
    <li>Certifications: Brainbench, Microsoft Learn - Azure, IIT Kanpur E&ICT Academy</li>
</ul>

<h3>Tools & Technologies</h3>
<p>.NET, ASP.NET Core, Azure, Kubernetes, Docker, Terraform, Python, React, Angular, CI/CD</p>
```

---

## Views/Shared/_ContactPartial.cshtml

```html
@model VinodPortfolio.Models.ResumeModel
<section id="contact" class="contact">
    <h3>Contact</h3>
    <p>Email: <a href="mailto:@Model.Email">@Model.Email</a></p>
    <p>Phone: @Model.Phone</p>
    <p>LinkedIn: <a href="@Model.LinkedIn" target="_blank">Profile</a></p>
</section>
```

---

## wwwroot/css/site.css

```css
:root{--container-width:1100px}
body{font-family:Inter,Segoe UI,Arial,sans-serif;margin:0;color:#222}
.container{max-width:var(--container-width);margin:0 auto;padding:20px}
.site-header{background:#0b3d91;color:#fff;padding:18px 0}
.site-header .brand a{color:#fff;text-decoration:none;font-size:20px}
.nav a{color:#fff;margin-left:18px;text-decoration:none}
.hero{background:#f6f8fb;padding:40px 0;border-bottom:1px solid #e6e9ef}
.hero h2{margin:0;font-size:26px}
.hero .title{color:#333;font-weight:600}
.grid{display:grid;grid-template-columns:1fr 1fr;gap:20px}
.card{background:#fff;padding:18px;border-radius:8px;box-shadow:0 6px 18px rgba(10,10,20,.06)}
.contact{margin-top:30px}
.site-footer{text-align:center;padding:18px 0;background:#fafafa;color:#666;margin-top:30px}
@media(max-width:800px){.grid{grid-template-columns:1fr}}
```

---

## appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

---

## .gitignore

```
bin/
obj/
.vs/
*.user
*.suo
.env

# Rider
.idea/
```

---

## README.md (summary & GitHub push + Azure deploy)

```markdown
# Vinod Reddy - Portfolio (ASP.NET Core MVC)

This repository contains a professional portfolio web app built with ASP.NET Core MVC (net7.0).

## Run locally
1. Install .NET 7 SDK
2. `dotnet restore`
3. `dotnet run --project VinodPortfolio/VinodPortfolio.csproj`
4. Open https://localhost:5001 or http://localhost:5000

## Push to GitHub
```bash
git init
git add .
git commit -m "Initial portfolio site"
# create repo on GitHub manually or using gh CLI
git remote add origin https://github.com/<your-username>/vinod-portfolio.git
git branch -M main
git push -u origin main
```

## Deploy to Azure App Service (recommended for ASP.NET hosting)
1. Create a resource in Azure: App Service (Windows) or Linux, choose .NET 7 runtime.
2. In GitHub: create a repository and push code.
3. In Azure App Service -> Deployment Center: choose GitHub Actions and authorize.
4. Select repository and branch. Azure will create a GitHub Action workflow that builds and deploys the app on push.

Alternatively use Azure CLI to deploy or configure GitHub Actions manually.
```

---

## Next steps and notes
- Replace placeholder content and expand Experiences list with your full resume bullets (the resume used to seed this content is included in the uploaded file).
- Add your profile photo and certification images into `wwwroot/images` and reference them from views.
- If you want a single-page look with sections (Home, Experience, Projects, Blog), I can convert to a single-page layout with smooth scrolling.
- To enable contact forms, add Email service (SendGrid or SMTP) and a simple API endpoint.

---

End of project boilerplate. Update styles and text as desired. Happy to refine design, add animations, projects page, or CI/CD workflow file for Azure/GitHub Actions.
