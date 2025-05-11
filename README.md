# 🗂️ Project Management API

A RESTful API built with ASP.NET Core for managing users, projects, and tasks with support for user registration, task assignment, and project-based task grouping.

## 🚀 Features

- User registration and listing
- Project creation, update, retrieval, and deletion
- Task management with assignment and status tracking
- Task filtering by project or user
- JWT-based authentication 
- Swagger UI documentation 

## 🛠 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Repository Pattern
- Swagger for API documentation
- Unit of work pattern
- Clean architecture
- XUNIT for unit testing

## 🔐 JWT Configuration

This project uses JWT for authentication. You need to configure secrets locally using [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets).

### Add User Secrets

1. Open the project directory in your terminal.
2. Run the following command:

```bash
dotnet user-secrets init
dotnet user-secrets set "JWT:Issure" "Issuer"
dotnet user-secrets set "JWT:Audience" "Issuer"
dotnet user-secrets set "JWT:Key" "Key"
dotnet user-secrets set "JWT:DurationInMinutes" "120"
```

---

## 🧰 How to Run

### 1. Clone the Repository

```bash
https://github.com/fares7elsadek/ProjectManagement
cd ProjectManagement
```

### 2. Set Connection String

Open `appsettings.json` and configure the connection string:

```json
"ConnectionStrings": {
  "cs": "Server=(localdb)\\mssqllocaldb;Database=ProjectManagementDb;Trusted_Connection=True;"
}
```

### 3. Run Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

API will be available at: `https://localhost:5001` or `http://localhost:5000`.

---

## 📬 API Endpoints

### Authentication
* `POST /api/auth/register` – Register a new user
* `POST /api/auth/login` – login

### User Management
* `GET /api/user` – List all users

### Project Management

* `POST /api/projects` – Create a new project
* `GET /api/projects` – List all projects
* `GET /api/projects/{id}` – Get project with tasks
* `PUT /api/projects/{id}` – Update a project
* `DELETE /api/projects/{id}` – Delete a project

### Task Management

* `POST /api/tasks` – Create task and assign to project (and optionally user)
* `PUT /api/tasks/{id}/assign` – Assign/Unassign task to user
* `PUT /api/tasks/{id}/status` – Update task status
* `GET /api/tasks?projectId=&userId=` – Filter tasks
* `DELETE /api/tasks/{id}` – Delete a task
