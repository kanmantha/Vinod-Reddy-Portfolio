--- FILE: README.md ---
# Vinod Reddy Kanmanthareddy - Personal Portfolio (ASP.NET Core MVC)

This repository contains a professional personal portfolio website built with **ASP.NET Core MVC** (.NET 7) pre-populated from Vinod Reddy Kanmanthareddy's resume.

Features
- Clean, professional layout using Bootstrap CDN
- Home / About / Projects / Experience / Contact sections
- Resume data pre-filled from uploaded CV
- Responsive design
- GitHub Actions workflow to build on push

How to run locally
1. Install .NET 7 SDK: https://dotnet.microsoft.com/download
2. Clone the repo:
```bash
git clone https://github.com/<your-username>/vinod-portfolio-aspnet.git
cd vinod-portfolio-aspnet
```
3. Run the app:
```bash
dotnet run --project Portfolio.Web
# open https://localhost:5001
```

Publish to GitHub (push code)
```bash
git init
git add .
git commit -m "Initial: ASP.NET Core portfolio"
# create GitHub repo then
git remote add origin https://github.com/<your-username>/vinod-portfolio-aspnet.git
git branch -M main
git push -u origin main
```

Optional: Deploy to Azure App Service (brief steps)
1. Create App Service on Azure portal (Windows, .NET 7)
2. In Azure, set up Deployment Center to pull from GitHub repo and deploy on push.
3. Or use `az webapp deploy` from Azure CLI.

--- FILE: .gitignore ---
bin/
obj/
.vs/
*.user
*.suo
*.db
appsettings.Secret.json

--- FILE: Portfolio.Web/Portfolio.Web.csproj ---
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation" Version="7.0.0" />
  </ItemGroup>
</Project>

--- FILE: Portfolio.Web/Program.cs ---
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Register resume data as singleton so controllers/views can use it
builder.Services.AddSingleton(Resume.Sample());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

--- FILE: Portfolio.Web/Models/Resume.cs ---
using System.Collections.Generic;

namespace Portfolio.Web.Models
{
    public record Experience(string Title, string Company, string Location, string Period, string Details);
    public record Education(string Degree, string Institution, string Year);
    public record Project(string Title, string Summary, string TechStack, string Link);

    public class Resume
n    {
        public string FullName { get; init; }
        public string Title { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public string LinkedIn { get; init; }
        public string Summary { get; init; }
        public List<string> Skills { get; init; }
        public List<Experience> Experiences { get; init; }
        public List<Education> Educations { get; init; }
        public List<Project> Projects { get; init; }

        public static Resume Sample() => new Resume
        {
            FullName = "Vinod Reddy Kanmanthareddy",
            Title = "Technical Project Manager | Software Architect | Engineering Manager",
            Email = "vinod.kanmantha@gmail.com",
            Phone = "+91-9640598454",
            LinkedIn = "https://linkedin.com/in/vinod-reddy-kanmanthareddy",
            Summary = "Results-driven technology leader with 18+ years of experience in cloud-native enterprise architecture, AI-driven analytics, and DevOps modernization.",
            Skills = new List<string>{ "Azure (AKS, Functions, Logic Apps)", "Docker, Kubernetes, Terraform, Helm", ".NET 6/7, ASP.NET Core", "Python, AI/ML, Azure ML", "React, Angular, TypeScript", "CI/CD (Azure DevOps, GitHub Actions, Jenkins)" },
            Experiences = new List<Experience>
            {
                new Experience("Technical Project Lead - VGSPL (Client: Amerant Inc.)","VGSPL","Hyderabad, India","Jun 2022 - Present","Led design and delivery of a cloud-native citizen benefits platform using Azure Kubernetes Service (AKS), Dockerized .NET 6 microservices, and Python-based AI analytics. Built CI/CD automation and observability."),
                new Experience("Technical Project Lead - KSAP Inc (Client: 3GTMS Inc.)","KSAP Inc","Khammam, India","Mar 2021 - Jun 2022","Modernized transportation management platform using Python (FastAPI) and Angular 17. Integrated AI forecasting and CI/CD automation."),
                new Experience("Technical Lead - 6D PropTech (Financial Markets)","6D PropTech","Hyderabad, India","Jun 2019 - Feb 2021","Architected microservices-based financial analytics platform integrating Bloomberg APIs and AI-driven reconciliation.")
            },
            Educations = new List<Education>
            {
                new Education("M.S., Computer Science","University of Northern Virginia, USA","Year: —"),
                new Education("B.Tech, Computer Science","Madras University, Chennai, India","2003")
            },
            Projects = new List<Project>
            {
                new Project("Cloud-native Citizen Benefits Platform","Microservices, AKS, .NET 6, Python, Prometheus & Grafana","Azure, AKS, .NET 6, Python, React",""),
                new Project("Transportation Management Modernization","FastAPI backend, Angular frontend, CI/CD automation","Python, Angular","")
            }
        };
    }
}

--- FILE: Portfolio.Web/Controllers/HomeController.cs ---
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Resume _resume;
        public HomeController(Resume resume)
        {
            _resume = resume;
        }

        public IActionResult Index() => View(_resume);
        public IActionResult About() => View(_resume);
        public IActionResult Projects() => View(_resume);
        public IActionResult Contact() => View(_resume);
    }
}

--- FILE: Portfolio.Web/Views/Shared/_Layout.cshtml ---
@using Portfolio.Web.Models
@inject Resume Resume
<!doctype html>
<html lang="en">
  <head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>@Resume.FullName - @Resume.Title</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="~/css/site.css" />
  </head>
  <body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container">
        <a class="navbar-brand" href="/">@Resume.FullName</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navMain">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navMain">
          <ul class="navbar-nav ms-auto">
            <li class="nav-item"><a class="nav-link" href="/">Home</a></li>
            <li class="nav-item"><a class="nav-link" href="/Home/About">About</a></li>
            <li class="nav-item"><a class="nav-link" href="/Home/Projects">Projects</a></li>
            <li class="nav-item"><a class="nav-link" href="/Home/Contact">Contact</a></li>
          </ul>
        </div>
      </div>
    </nav>

    <main class="container mt-4">
      @RenderBody()
    </main>

    <footer class="bg-light mt-5 py-4">
      <div class="container text-center small">
        © @DateTime.UtcNow.Year • @Resume.FullName • <a href="@Resume.LinkedIn">LinkedIn</a>
      </div>
    </footer>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
  </body>
</html>

--- FILE: Portfolio.Web/Views/Home/Index.cshtml ---
@model Portfolio.Web.Models.Resume

<div class="row gy-4">
  <div class="col-md-8">
    <h1>@Model.FullName</h1>
    <h4 class="text-muted">@Model.Title</h4>
    <p class="mt-3">@Model.Summary</p>

    <section class="mt-4">
      <h3>Experience</h3>
      @foreach(var e in Model.Experiences)
      {
        <div class="mb-3">
          <h5>@e.Title <small class="text-muted">@e.Company — @e.Period</small></h5>
          <p>@e.Details</p>
        </div>
      }
    </section>

    <section class="mt-4">
      <h3>Education</h3>
      <ul>
        @foreach(var ed in Model.Educations)
        {
          <li>@ed.Degree, @ed.Institution (@ed.Year)</li>
        }
      </ul>
    </section>
  </div>

  <aside class="col-md-4">
    <div class="card mb-3 p-3">
      <h5>Contact</h5>
      <p class="mb-0">Email: <a href="mailto:@Model.Email">@Model.Email</a></p>
      <p>Phone: @Model.Phone</p>
      <p>LinkedIn: <a href="@Model.LinkedIn">Profile</a></p>
    </div>

    <div class="card p-3">
      <h5>Skills</h5>
      <ul class="list-unstyled small">
        @foreach(var s in Model.Skills)
        {
          <li>• @s</li>
        }
      </ul>
    </div>
  </aside>
</div>

--- FILE: Portfolio.Web/Views/Home/About.cshtml ---
@model Portfolio.Web.Models.Resume

<h2>About</h2>
<p>@Model.Summary</p>

<h4 class="mt-4">Tech & Tools</h4>
<ul>
  @foreach(var s in Model.Skills)
  {
    <li>@s</li>
  }
</ul>

--- FILE: Portfolio.Web/Views/Home/Projects.cshtml ---
@model Portfolio.Web.Models.Resume

<h2>Selected Projects</h2>
<div class="row">
  @foreach(var p in Model.Projects)
  {
    <div class="col-md-6 mb-3">
      <div class="card h-100 p-3">
        <h5>@p.Title</h5>
        <p class="mb-1">@p.Summary</p>
        <small class="text-muted">Tech: @p.TechStack</small>
        @if(!string.IsNullOrEmpty(p.Link)){
          <div class="mt-2"><a href="@p.Link" target="_blank">View</a></div>
        }
      </div>
    </div>
  }
</div>

--- FILE: Portfolio.Web/Views/Home/Contact.cshtml ---
@model Portfolio.Web.Models.Resume

<h2>Contact</h2>
<p>If you'd like to reach out, email <a href="mailto:@Model.Email">@Model.Email</a> or call @Model.Phone.</p>

--- FILE: Portfolio.Web/wwwroot/css/site.css ---
body { background: #f8f9fa; }
h1 { font-weight:700; }
.card { border-radius: 12px; }

--- FILE: .github/workflows/dotnet.yml ---
name: Build
on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '7.0.x'
      - name: Restore
        run: dotnet restore Portfolio.Web/Portfolio.Web.csproj
      - name: Build
        run: dotnet build --no-restore Portfolio.Web/Portfolio.Web.csproj -c Release
      - name: Test
        run: echo "No tests configured"

--- FILE: Notes.md ---
Next steps / customization ideas:
- Add portfolio screenshots in wwwroot/images and reference them in Projects view.
- Add a downloadable resume PDF (place uploaded CV in wwwroot/files).
- Add contact form with SMTP or Azure Function-backed sending.
- Add Google Analytics / Plausible for traffic.
- Replace placeholder project links with GitHub/Azure links.



--- FILE: Portfolio.Web/Models/ContactForm.cs ---
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Models
{
    public class ContactForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }
    }
}

--- FILE: Portfolio.Web/Controllers/ContactController.cs ---
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Web.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Portfolio.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IConfiguration _config;

        public ContactController(ILogger<ContactController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IActionResult Index() => View(new ContactForm());

        [HttpPost]
        public async Task<IActionResult> Index(ContactForm model)
        {
            if (!ModelState.IsValid) return View(model);

            // Try to send email via SMTP if configured, otherwise fall back to writing to a local file
            var smtpHost = _config["Contact:Smtp:Host"];
            if (!string.IsNullOrEmpty(smtpHost))
            {
                try
                {
                    var mail = new MailMessage();
                    mail.From = new MailAddress(_config["Contact:FromEmail"] ?? model.Email);
                    mail.To.Add(_config["Contact:ToEmail"] ?? _config["Contact:FromEmail"]);
                    mail.Subject = $"Portfolio contact from {model.Name}";
                    mail.Body = $"Name: {model.Name}
Email: {model.Email}
Phone: {model.Phone}

Message:
{model.Message}";

                    using var client = new SmtpClient(smtpHost, int.Parse(_config["Contact:Smtp:Port"] ?? "25"));
                    client.EnableSsl = bool.Parse(_config["Contact:Smtp:EnableSsl"] ?? "false");
                    var user = _config["Contact:Smtp:User"];
                    var pass = _config["Contact:Smtp:Pass"];
                    if (!string.IsNullOrEmpty(user)) client.Credentials = new System.Net.NetworkCredential(user, pass);

                    await client.SendMailAsync(mail);
                    TempData["Message"] = "Thanks — your message was sent successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send contact email via SMTP");
                }
            }

            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
                Directory.CreateDirectory(dir);
                var file = Path.Combine(dir, $"contact_{DateTime.UtcNow:yyyyMMddHHmmss}.txt");
                await System.IO.File.WriteAllTextAsync(file, $"Name: {model.Name}
Email: {model.Email}
Phone: {model.Phone}
Message:
{model.Message}");
                TempData["Message"] = "Thanks — your message was recorded.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to record contact message");
                TempData["Message"] = "Sorry, we couldn't process your message right now.";
            }

            return RedirectToAction("Index");
        }
    }
}

--- FILE: Portfolio.Web/Views/Contact/Index.cshtml ---
@model Portfolio.Web.Models.ContactForm

@{
    ViewData["Title"] = "Contact";
}

<h2>Contact</h2>

@if(TempData["Message"] != null)
{
    <div class="alert alert-success">@TempData["Message"]</div>
}

<form method="post" asp-controller="Contact" asp-action="Index" novalidate>
  <div class="mb-3">
    <label class="form-label">Name</label>
    <input asp-for="Name" class="form-control" />
    <span asp-validation-for="Name" class="text-danger"></span>
  </div>
  <div class="mb-3">
    <label class="form-label">Email</label>
    <input asp-for="Email" class="form-control" />
    <span asp-validation-for="Email" class="text-danger"></span>
  </div>
  <div class="mb-3">
    <label class="form-label">Phone</label>
    <input asp-for="Phone" class="form-control" />
  </div>
  <div class="mb-3">
    <label class="form-label">Message</label>
    <textarea asp-for="Message" class="form-control" rows="5"></textarea>
    <span asp-validation-for="Message" class="text-danger"></span>
  </div>
  <button type="submit" class="btn btn-primary">Send Message</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}

--- FILE: Portfolio.Web/Views/Shared/_Layout.cshtml (update snippet) ---
@@
          <ul class="navbar-nav ms-auto">
            <li class="nav-item"><a class="nav-link" href="/">Home</a></li>
            <li class="nav-item"><a class="nav-link" href="/Home/About">About</a></li>
            <li class="nav-item"><a class="nav-link" href="/Home/Projects">Projects</a></li>
-            <li class="nav-item"><a class="nav-link" href="/Home/Contact">Contact</a></li>
+            <li class="nav-item"><a class="nav-link" href="/Home/Contact">Contact</a></li>
+            <li class="nav-item"><a class="nav-link" href="/Contact">Contact Form</a></li>
          </ul>
@@
-        <div class="container text-center small">
-        © @DateTime.UtcNow.Year • @Resume.FullName • <a href="@Resume.LinkedIn">LinkedIn</a>
-      </div>
+        <div class="container text-center small">
+        © @DateTime.UtcNow.Year • @Resume.FullName • <a href="@Resume.LinkedIn">LinkedIn</a> • <a href="/files/Vinod_Reddy_Kanmanthareddy_Executive_CV_2025_Final_HQ.pdf">Download Resume</a>
+      </div>
@@

