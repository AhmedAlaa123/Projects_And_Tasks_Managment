# Task & Project Management System

A full-stack application built with ASP.NET Core Web API (backend) and ASP.NET Core MVC (frontend), following Clean Architecture principles with CQRS pattern using MediatR.

---

## Table of Contents

- [Architecture Overview](#architecture-overview)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)

---

## Architecture Overview

```
┌─────────────────────────────────────────────────────┐
│                   MVC (UI Layer)                    │
│         ASP.NET Core MVC + Bootstrap 5              │
│   Cookie Auth │ HttpClient │ JWT Handler            │
└────────────────────────┬────────────────────────────┘
                         │ HTTP Requests
┌────────────────────────▼────────────────────────────┐
│                  API Layer                          │
│            ASP.NET Core Web API                     │
│    Controllers │ Versioning │ Swagger │ JWT Auth    │
└────────────────────────┬────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────┐
│              Application Layer                      │
│        CQRS │ MediatR │ AutoMapper                  │
│     Commands │ Queries │ Validators │ DTOs           │
└────────────────────────┬────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────┐
│              Infrastructure Layer                   │
│     Entity Framework Core                           │
│         Generic Repository │ Unit of Work           │
└────────────────────────┬────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────┐
│                  Database                           │
│                  SQL Server                         │
└─────────────────────────────────────────────────────┘
```

### Design Patterns

- **Clean Architecture** — separation of concerns across layers
- **CQRS** — Commands and Queries separated via MediatR
- **Repository Pattern** — Generic repository with Unit of Work
- **Mediator Pattern** — decoupled request handling via MediatR
- **Decorator Pattern** — `DelegatingHandler` for JWT injection

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend API | ASP.NET Core 9 Web API |
| Frontend | ASP.NET Core 9 MVC |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Auth | JWT Bearer + Cookie Authentication |
| CQRS | MediatR |
| Mapping | AutoMapper |
| Validation | FluentValidation |
| Logging | Serilog |
| API Docs | Swagger / OpenAPI |
| API Versioning | Asp.Versioning.Mvc 8.1.0 |

---

## Project Structure

```
Solution/
├── Api/                          # Web API project
│   ├── Controllers/              # API controllers (v1, v2)
│   ├── Middlewares/              # Global exception handler
│   └── Extensions/              # Service registration extensions
│
├── Application/                  # Application layer
│   ├── Features/
│   │   ├── Projects/
│   │   │   ├── Commands/         # Create, Update, Delete
│   │   │   └── Queries/          # GetAll, GetById
│   │   └── Tasks/
│   │       ├── Commands/
│   │       └── Queries/
│   ├── Contracts/                # Interfaces (IUserService, etc.)
│   ├── DTOs/                     # Data transfer objects
│   ├── Mappings/                 # AutoMapper profiles
│   └── Validators/               # FluentValidation validators
│
├── Domain/                       # Domain entities and enums
│   ├── Entities/                 # Project, Task, User, Role
│   └── Enums/                    # TaskStatus, TaskPriority
│
├── Infrastructure/               # Data access layer
│   ├── Data/                     # AppDbContext, Migrations
│   ├── Repositories/             # Generic repository
│   └── Services/                 # TokenService, CacheService
│
└── Ui/                           # MVC frontend project
    ├── Controllers/              # ProjectController, TaskController
    ├── Services/                 # IProjectService, ITaskService
    ├── Handlers/                 # JwtCookieHandler
    ├── Middlewares/              # JwtCookieAuthMiddleware
    ├── Extensions/              # HttpClientExtensions
    └── Views/
        ├── Project/             # Index, Create, Edit, Details
        ├── Task/                # Index, Create, Edit
        └── Account/             # Login
```

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/AhmedAlaa123/Projects_And_Tasks_Managment.git
cd task-project-management
```

### 2. Restore NuGet Packages

```bash
dotnet restore
```

### 3. Configure the Database

Update `appsettings.json` in the `Api` project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 4. Apply Migrations

```bash
cd Api
dotnet ef database update
```

### 5. Configure JWT

Update `appsettings.json` in the `Api` project:

```json
{
  "JwtSettings": {
    "Secret":   "YourSuperSecretKeyHere_AtLeast32Chars",
    "Issuer":   "TaskAndProjectManagmentSystem",
    "Audience": "WebApp"
  }
}
```

### 6. Configure API Base URL in MVC

Update `appsettings.json` in the `Ui` project:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001/"
  }
}
```

---

## Configuration

### `Api/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Secret":   "YourSuperSecretKeyHere_AtLeast32Chars",
    "Issuer":   "TaskAndProjectManagmentSystem",
    "Audience": "WebApp"
  },
  "Serilog": {
    "MinimumLevel": "Information"
  }
}
```

### `Ui/appsettings.json`

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7001/"
  }
}
```

---

## Running the Application

### Run API

```bash
cd Api
dotnet run
```

API runs at: `https://localhost:7001`
Swagger UI: `https://localhost:7001/swagger`

### Run MVC UI

```bash
cd Ui
dotnet run
```

UI runs at: `https://localhost:7002`

### Run Both (Visual Studio)

Set multiple startup projects:
1. Right-click Solution → Properties
2. Select **Multiple startup projects**
3. Set both `Api` and `Ui` to **Start**

---

## API Endpoints

### Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/v1/Auth/Login` | Login and get JWT token |
| POST | `/api/v1/Auth/Create` | Register new user |
| GET  | `/api/v1/Auth/Roles` | Get all roles |

### Projects

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/v1/Projects/GetAll?PageNumber=1&PageSize=10` | Get all projects (paginated) |
| GET    | `/api/v1/Projects/Get/{id}` | Get project by ID |
| GET    | `/api/v1/Projects/get-tasks?id={id}` | Get tasks by project ID |
| POST   | `/api/v1/Projects/Create` | Create new project |
| PUT    | `/api/v1/Projects/Update` | Update project |
| DELETE | `/api/v1/Projects/delete/{id}` | Delete project |

### Tasks

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST   | `/api/v1/Task/Create` | Create new task |
| PUT    | `/api/v1/Task/Update` | Update task |
| PUT    | `/api/v1/Task/Update-Status` | Update task status |
| DELETE | `/api/v1/Task/Delete/{id}` | Delete task |

---

## Authentication

The system uses **JWT Bearer** authentication on the API and **Cookie Authentication** on the MVC UI.

### Flow

```
User Login (MVC)
     │
     ▼
POST /api/v1/Auth/login
     │
     ▼
JWT Token returned
     │
     ▼
Token stored in Claims Cookie
     │
     ▼
JwtCookieHandler attaches token
to every API request automatically
```

### Roles

| Role | Permissions |
|------|-------------|
| `admin` | Full access to all resources |
| `manager` | Read/Write access to projects and tasks |
| `user` | Read access only |

---

## Error Responses

All errors follow a consistent format:

```json
{
  "statusCode": 404,
  "message": "Project with id 1 not found",
  "timestamp": "2026-05-24T10:00:00Z"
}
```

Validation errors include field-level details:

```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": [
    { "field": "Name",    "message": "Name is required" },
    { "field": "DueDate", "message": "DueDate must be in the future" }
  ],
  "timestamp": "2026-05-24T10:00:00Z"
}
```
