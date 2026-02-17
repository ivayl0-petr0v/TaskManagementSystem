# 🚀 Planly

> A modern, robust ASP.NET Core MVC application designed for efficient project tracking and team organization. Built with a focus on Clean Architecture, advanced Identity management, and a user-centric design.

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple?style=flat&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-blue?style=flat&logo=microsoft)
![Database](https://img.shields.io/badge/SQL_Server-Entity_Framework-red?style=flat&logo=microsoftsqlserver)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?style=flat&logo=bootstrap)
![License](https://img.shields.io/badge/license-MIT-green?style=flat)
<img width="1920" height="1080" alt="readme-preview" src="https://github.com/user-attachments/assets/a4bd3bd6-799d-4365-b813-9621c3dd5e6e" />
---

## 📋 Table of Contents

- [About the Project](#-about-the-project)
- [Key Features](#-key-features)
- [Technical Highlights & Challenges](#-technical-highlights--challenges-solved)
- [Technologies Used](#-technologies-used)
- [Project Architecture](#-project-structure)
- [Prerequisites](#-prerequisites)
- [Getting Started](#-getting-started)
- [Database Setup](#-database-setup)
- [Contributing](#-contributing)
- [Contact](#-contact)

---

## 📖 About the Project

**Planly** is a web application built to solve the problem of disorganized project tracking. It allows users to create projects, assign categories, track deadlines, and mark progress—all within a clean, "Apple-style" interface using the **Inter** font family, glassmorphism elements, and custom Hero sections.

The project was built not just as a CRUD application, but as a deep dive into **Clean Architecture**, **Service Layer patterns**, and **Custom Authentication flows**.

## ✨ Key Features

- [x] **Project Management:** Create, edit, delete, and view details of projects.
- [x] **Smart Authorization:** Role-based security ensuring only project owners can modify their data.
- [x] **Status Tracking:** Mark projects as "Completed" with visual feedback.
- [x] **Custom User Profile:** Extended registration with **First Name** and **Last Name**.
- [x] **Categorization:** Organize projects dynamically (Work, Personal, etc.).
- [x] **Responsive UI:** Fully responsive design using Bootstrap 5 and custom CSS (Inter font).
- [x] **Data Access:** All database logic is encapsulated in a dedicated Service Layer.

---

## 💡 Technical Highlights & Challenges Solved

This project required solving complex architectural and framework-specific challenges.

### 1. Extended Identity System (`ApplicationUser`)
**The Challenge:** The default `IdentityUser` provided by ASP.NET Core was insufficient as the business requirement demanded capturing specific user details like **First Name** and **Last Name**.

**The Solution:**
I implemented a custom `ApplicationUser` class inheriting from `IdentityUser`. This was a major architectural change that caused a cascade of issues:
* **Refactoring:** I had to manually replace `IdentityUser` with `ApplicationUser` throughout the entire solution.
* **Scaffolding Issues:** The default Login, Register, and Logout pages broke because they were tightly coupled to the base user class. I had to override and rewrite the backing logic for these pages (`Login.cshtml.cs`, `Register.cshtml.cs`) to accept the new model and successfully save the extra fields to the database.

### 2. Data Seeding & Authentication Loop
**The Challenge:** During the development phase, I implemented a Data Seeding strategy to populate the database with test users. However, I encountered a critical bug where I could not log in with these seeded accounts, despite the database records appearing correct.

**The Solution:**
After extensive debugging, I discovered a discrepancy in how ASP.NET Identity validates users during the login process versus how I was seeding them. The system required the `UserName` and `Email` fields to be strictly synchronized (normalized). I refactored the seeding logic to ensure consistency between these fields, permanently resolving the access issue.

---

## 🛠️ Technologies Used

| Technology | Version | Purpose |
| :--- | :--- | :--- |
| **ASP.NET Core** | 8.0 | Main Web Framework (MVC Pattern) |
| **Entity Framework Core** | 8.0 | ORM & Database Management |
| **SQL Server** | - | Relational Database |
| **Bootstrap** | 5.3 | Responsive Frontend Framework |
| **Inter Font** | - | Primary Typography for modern UI |
| **C#** | 12 | Backend Language |

---

## 📁 Project Architecture & Structure

The solution follows the **Service Layer Pattern** to ensure Separation of Concerns (SoC).

```text
TaskManagementSystem.sln
│
├── 🗄️ Data Layer
│   ├── TaskManagementSystem.Data          # EF Core DbContext, Configurations & Migrations
│   └── TaskManagementSystem.Data.Models   # Database Entities (Project, Category, ApplicationUser)
│
├── 🧠 Service Layer
│   └── TaskManagementSystem.Services.Core # Business Logic Implementation (IProjectService, ProjectService)
│
├── 🌐 Presentation Layer (Web)
│   ├── TaskManagementSystem.Web           # ASP.NET Core MVC (Controllers, Views, wwwroot, Areas)
│   └── TaskManagementSystem.ViewModels    # DTOs & ViewModels (Decoupled from Data Models)
│
└── 🔧 Common / Shared
    └── TaskManagementSystem.GCommon       # Global Constants, Validation Constants, Helpers
```
---

## ✅ Prerequisites

Make sure you have the following installed before running the project:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Visual Studio 2026](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)
- [Git](https://git-scm.com/)

---

## 🚀 Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
[https://github.com/ivayl0-petr0v/TaskManagementSystem.git]
cd TaskManagementSystem
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Apply database migrations

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```
---
## 🗄️ Database Setup

The project uses **Entity Framework Core** with a Code-First approach.

Connection string is configured in `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TaskManagement;Trusted_Connection=True;Encrypt=False"
}
```

To create and seed the database:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---
## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add some feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request
---
## 📄 License
This project is licensed under the MIT License. See the LICENSE file for details.

---

## 📬 Contact
**Ivaylo Petrov** – [GitHub Profile](https://github.com/ivayl0-petr0v)

Project Link: [https://github.com/ivayl0-petr0v/TaskManagementSystem](https://github.com/ivayl0-petr0v/TaskManagementSystem.git)

Built with ❤️ using ASP.NET Core.
