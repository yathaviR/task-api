# Task Management REST API

A production-ready REST API for managing tasks, built with .NET 9 and Entity Framework Core.

## Features

- ✅ Full CRUD operations (Create, Read, Update, Delete)
- ✅ SQL Server database with Entity Framework ORM
- ✅ RESTful API design with proper HTTP status codes
- ✅ Async/await patterns for performance
- ✅ Comprehensive error handling
- ✅ Swagger/OpenAPI documentation
- ✅ Logging and monitoring ready

## Tech Stack

- **C# / .NET 9** — Backend framework
- **ASP.NET Core** — Web API framework
- **Entity Framework Core 9.0** — ORM
- **SQL Server** — Database
- **Swagger/OpenAPI** — API documentation

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB)
- Visual Studio 2022 or VS Code

### Installation
```bash
# Clone the repository
git clone https://github.com/YOUR-USERNAME/task-api.git
cd task-api/TaskApi

# Restore dependencies
dotnet restore

# Create database
dotnet ef migrations add InitialCreate
dotnet ef database update

# Run the application
dotnet run
```

API will be available at `https://localhost:7022`

## API Endpoints

### Get All Tasks
```
GET /api/tasks
```

### Get Task by ID
```
GET /api/tasks/{id}
```

### Create Task
```
POST /api/tasks
Content-Type: application/json

{
  "title": "Task Title",
  "description": "Task description"
}
```

### Update Task
```
PUT /api/tasks/{id}
Content-Type: application/json

{
  "title": "Updated title",
  "isCompleted": true
}
```

### Delete Task
```
DELETE /api/tasks/{id}
```

### Filter by Status
```
GET /api/tasks/completed/true
GET /api/tasks/completed/false
```

## Project Structure
```
TaskApi/
├── Models/           # Database entities
├── Data/             # Entity Framework context
├── DTOs/             # Data transfer objects
├── Controllers/      # API endpoints
├── Program.cs        # Application startup
└── appsettings.json  # Configuration
```

## Architecture & Design Patterns

- **Dependency Injection** — Loose coupling, testability
- **Repository Pattern** — Via Entity Framework DbContext
- **DTO Pattern** — Separation of API contract from database model
- **Async/Await** — Non-blocking I/O operations
- **Error Handling** — Proper HTTP status codes and messages

## What I Learned

- REST API design principles
- Entity Framework Core ORM and migrations
- Database schema design with indexes
- Async programming in .NET
- Swagger/OpenAPI documentation
- SOLID principles and clean code

## Future Enhancements

- [ ] User authentication (JWT)
- [ ] Role-based access control
- [ ] Unit tests (xUnit)
- [ ] Pagination
- [ ] Search/filtering
- [ ] Logging (Serilog)
- [ ] Caching (Redis)

## How to Test

1. Run the application
2. Go to `https://localhost:7022`
3. Use Swagger UI to test endpoints
4. Or use Postman/cURL

## Author

**Your Name**  
Backend Developer | C# | .NET | SQL

[LinkedIn](https://linkedin.com/in/yathavi-ramesh) | [GitHub](https://github.com/yathaviR)

## License

Open source for portfolio purposes.
```

4. Click **Commit new file**

---

## **Update Your LinkedIn**

Add this to your LinkedIn About section:
```
Backend developer with 1.5 years production .NET experience. 
Just built a Task Management REST API from scratch using .NET 9, 
Entity Framework Core, and SQL Server. 

Full code on GitHub: [link to your repo]

Tech stack: C#, .NET, SQL Server, REST APIs, Entity Framework

Open to: Remote, full-time backend/full-stack roles
