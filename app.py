#!/usr/bin/env python3
"""
app.py - Single-file professional portfolio web app (Flask).
Usage:
    pip install flask
    python app.py
Then open http://127.0.0.1:5000
"""

from flask import Flask, render_template_string, send_from_directory, request, redirect, url_for
import os

app = Flask(__name__)

# Basic data populated from the uploaded resume (Vinod Reddy Kanmanthareddy).
# Source: user's uploaded PDF resume. :contentReference[oaicite:2]{index=2}
PROFILE = {
    "name": "Vinod Reddy Kanmanthareddy",
    "title": "Technical Project Manager | Software Architect | Engineering Manager",
    "email": "vinod.kanmantha@gmail.com",
    "phone": "+91-9640598454",
    "linkedin": "https://www.linkedin.com/in/vinod-reddy-kanmanthareddy",
    "summary": (
        "Results-driven technology leader with 18+ years of experience in cloud-native "
        "enterprise architecture, AI-driven analytics, and DevOps modernization. "
        "Expert in Microsoft Azure, Kubernetes, Docker, and Python, with a proven record "
        "of leading global teams to deliver scalable, secure, and intelligent systems."
    ),
    "skills": [
        "Azure (AKS, Functions, Logic Apps)", "Kubernetes", "Docker", "Terraform",
        "Python (FastAPI, Flask)", ".NET Core / ASP.NET", "CI/CD (Azure DevOps, GitHub Actions, Jenkins)",
        "AI/ML: Scikit-learn, TensorFlow, Azure ML", "SQL Server / Azure SQL / PostgreSQL", "Grafana / Prometheus"
    ],
    # Short list of highlighted roles extracted from resume. :contentReference[oaicite:3]{index=3}
    "experience": [
        {
            "role": "Technical Project Lead - VGSPL (Client: Amerant Inc.)",
            "period": "June 2022 – Present",
            "bullets": [
                "Led design and delivery of a cloud-native citizen benefits platform using AKS and Dockerized .NET 6 microservices.",
                "Built CI/CD automation with Azure DevOps and introduced observability with Prometheus & Grafana."
            ]
        },
        {
            "role": "Technical Project Lead - KSAP Inc (Client: 3GTMS Inc.)",
            "period": "March 2021 – June 2022",
            "bullets": [
                "Modernized transportation platform using Python (FastAPI) and Angular 17.",
                "Implemented AI-powered logistics forecasting and automated CI/CD pipelines."
            ]
        },
        {
            "role": "Technical Lead - 6D PropTech (Financial Markets)",
            "period": "June 2019 – Feb 2021",
            "bullets": [
                "Architected microservices-based financial analytics platform and integrated AI-driven trade reconciliation."
            ]
        }
    ],
    "certifications": [
        "IIT Kanpur - E&ICT Academy: Advanced certification in AI, Cloud, and DevOps",
        "Microsoft Learn - Azure Developer Track",
        "Brainbench Certified Software Programmer"
    ]
}

# Path to resume PDF if included in same folder (optional)
RESUME_PDF = os.path.join(os.path.dirname(__file__), "Vinod_Reddy_Kanmanthareddy_Executive_CV_2025_Final_HQ.pdf")


BASE_HTML = """
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width,initial-scale=1">
  <title>{{profile.name}} — {{profile.title}}</title>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
  <style>
    :root{--accent:#0d6efd}
    body{font-family:Inter, system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", Arial; background:#f6f8fb;}
    .hero{background:linear-gradient(90deg, rgba(13,110,253,0.07), rgba(13,110,253,0.02)); padding:48px 0;}
    .card-quiet{background:#ffffff; border:0; box-shadow:0 6px 20px rgba(34,50,80,0.06); border-radius:12px;}
    .skill-badge{margin:4px 6px 6px 0; padding:6px 10px; background:#f1f6ff; border-radius:999px; font-size:0.875rem;}
    .nav-link.active{font-weight:600; color:var(--accent) !important;}
    footer{padding:28px 0; font-size:0.9rem; color:#6b7280;}
    .contact-card{background:linear-gradient(180deg,#ffffff,#fcfdff); padding:18px; border-radius:10px;}
    .mono{font-family:ui-monospace, SFMono-Regular, Menlo, Monaco, "Roboto Mono", monospace;}
  </style>
</head>
<body>
<nav class="navbar navbar-expand-lg navbar-light bg-white border-bottom">
  <div class="container">
    <a class="navbar-brand mono" href="/">{{profile.name}}</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#nav">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="nav">
      <ul class="navbar-nav ms-auto">
        <li class="nav-item"><a class="nav-link" href="#about">About</a></li>
        <li class="nav-item"><a class="nav-link" href="#experience">Experience</a></li>
        <li class="nav-item"><a class="nav-link" href="#skills">Skills</a></li>
        <li class="nav-item"><a class="nav-link" href="#contact">Contact</a></li>
        <li class="nav-item"><a class="btn btn-outline-primary ms-2" href="{{ url_for('download_resume') }}">Download Resume</a></li>
      </ul>
    </div>
  </div>
</nav>

<section class="hero">
  <div class="container">
    <div class="row align-items-center">
      <div class="col-lg-8">
        <div class="card-quiet p-5">
          <h1 class="mb-2">{{profile.name}}</h1>
          <h5 class="text-muted mb-3">{{profile.title}}</h5>
          <p class="lead text-muted">{{profile.summary}}</p>

          <div class="mt-4">
            <a class="btn btn-primary me-2" href="#contact">Hire / Contact</a>
            <a class="btn btn-outline-secondary" href="{{ profile.linkedin }}" target="_blank">LinkedIn</a>
          </div>
        </div>
      </div>
      <div class="col-lg-4 mt-4 mt-lg-0">
        <div class="contact-card">
          <h6>Contact</h6>
          <p class="mb-1"><strong>Email:</strong> <a href="mailto:{{profile.email}}">{{profile.email}}</a></p>
          <p class="mb-1"><strong>Phone:</strong> {{profile.phone}}</p>
          <p class="mb-1"><strong>Location:</strong> Hyderabad, India</p>
          <hr/>
          <h6>Certifications</h6>
          <ul class="mb-0">
            {% for c in profile.certifications %}
              <li>{{c}}</li>
            {% endfor %}
          </ul>
        </div>
      </div>
    </div>
  </div>
</section>

<section class="container my-5" id="experience">
  <div class="row">
    <div class="col-lg-8">
      <h3 class="mb-3">Experience</h3>
      {% for e in profile.experience %}
        <div class="mb-4 card p-3 card-quiet">
          <div class="d-flex justify-content-between">
            <div>
              <h5 class="mb-1">{{e.role}}</h5>
              <div class="text-muted">{{e.period}}</div>
            </div>
          </div>
          <ul class="mt-2">
            {% for b in e.bullets %}
              <li>{{b}}</li>
            {% endfor %}
          </ul>
        </div>
      {% endfor %}
    </div>

    <div class="col-lg-4">
      <h3 id="skills">Skills</h3>
      <div class="card p-3 card-quiet">
        {% for s in profile.skills %}
          <span class="skill-badge">{{s}}</span>
        {% endfor %}
      </div>

      <div class="mt-4">
        <h6>Quick links</h6>
        <div class="list-group">
          <a class="list-group-item list-group-item-action" href="{{ profile.linkedin }}" target="_blank">LinkedIn Profile</a>
          <a class="list-group-item list-group-item-action" href="{{ url_for('download_resume') }}">Download Resume (PDF)</a>
        </div>
      </div>
    </div>
  </div>
</section>

<section class="container mb-5" id="contact">
  <div class="row">
    <div class="col-lg-8">
      <h3>Contact / Hire</h3>
      <div class="card p-4 card-quiet">
        <form method="post" action="{{ url_for('contact') }}">
          <div class="mb-3">
            <label class="form-label">Your name</label>
            <input class="form-control" name="name" required>
          </div>
          <div class="mb-3">
            <label class="form-label">Your email</label>
            <input class="form-control" name="email" type="email" required>
          </div>
          <div class="mb-3">
            <label class="form-label">Message</label>
            <textarea class="form-control" name="message" rows="4" required></textarea>
          </div>
          <button class="btn btn-primary" type="submit">Send message</button>
        </form>
      </div>
    </div>
    <div class="col-lg-4">
      <h6>Availability</h6>
      <p class="text-muted">Open to leadership roles in cloud-native engineering, architecture, and delivery (full-time / contractual).</p>
      <h6>Stack</h6>
      <p class="text-muted">Python, .NET, Azure, Kubernetes, Terraform, Docker, React/Angular</p>
    </div>
  </div>
</section>

<footer class="bg-white border-top">
  <div class="container text-center py-3">
    <div>© {{profile.name}} • Built with Flask • <a href="{{ profile.linkedin }}">LinkedIn</a></div>
  </div>
</footer>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
"""

@app.route("/")
def home():
    return render_template_string(BASE_HTML, profile=PROFILE)

@app.route("/download-resume")
def download_resume():
    # if resume PDF is present in same folder, serve it; otherwise give 404 with helpful message.
    if os.path.isfile(RESUME_PDF):
        folder = os.path.dirname(RESUME_PDF)
        filename = os.path.basename(RESUME_PDF)
        return send_from_directory(folder, filename, as_attachment=True)
    else:
        return redirect(url_for('home'))

@app.route("/contact", methods=["POST"])
def contact():
    # This demo does not send emails by default — it stores the message to a simple local file (messages.txt).
    name = request.form.get("name")
    email = request.form.get("email")
    message = request.form.get("message")
    entry = f"Name: {name}\\nEmail: {email}\\nMessage: {message}\\n---\\n"
    with open("messages.txt", "a", encoding="utf-8") as f:
        f.write(entry)
    return render_template_string("""
    <!doctype html><html><head>
    <meta http-equiv="refresh" content="2;url={{ url_for('home') }}">
    <style>body{font-family:sans-serif;display:flex;align-items:center;justify-content:center;height:100vh}</style></head>
    <body><div class="card" style="padding:20px;border-radius:10px;box-shadow:0 8px 20px rgba(0,0,0,0.06)">
    <h3>Thanks — message saved.</h3><p>You will be redirected shortly.</p></div></body></html>""")

if __name__ == "__main__":
    port = int(os.environ.get("PORT", 5000))
    app.run(host="0.0.0.0", port=port, debug=False)
