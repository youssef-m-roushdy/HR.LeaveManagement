## Description
The **HR Leave Management** system is a comprehensive solution designed to manage employee leave requests, approvals, and tracking within an organization. It provides a user-friendly interface for employees to request leave, managers to approve or reject requests, and administrators to manage leave types and allocations. The system is built using modern software development practices and follows the **Onion Architecture** and **CQRS (Command Query Responsibility Segregation)** patterns for scalability, maintainability, and separation of concerns.

This project was developed using **Ubuntu Linux** and **Visual Studio Code**, ensuring cross-platform compatibility and flexibility.

---

## Architecture Pattern
The project follows the **Onion Architecture**, which emphasizes separation of concerns and dependency inversion. The solution is organized into the following layers:
```
├── 📂 src
│ ├── 📂 Core (Business Logic)
│ │ ├── Domain (Entities, Value Objects, Domain Logic)
│ │ ├── Application (Use Cases, CQRS, Interfaces)
│ │
│ ├── 📂 Infrastructure (External Dependencies)
│ │ ├── Persistence (Database Repositories, EF Core)
│ │ ├── Identity (Authentication & Authorization)
│ │ ├── ExternalServices (Email, Logging, etc.)
│ │
│ ├── 📂 API (Presentation Layer)
│ │ ├── Controllers (REST API Endpoints)
│ │ ├── Middlewares (Custom Middleware for Global Error Handling, etc.)
| |
| ├── 📂 UI
│ │ ├── MVC (ASP.NET Core MVC for Web Interface)
│ │
│ ├── 📂 Tests (Unit & Integration Tests)
│ │ ├── XUnit (Unit Testing Framework)
│
├── 📄 HR.LeaveManagement.sln (Solution File)
```


---

### What is Onion Architecture?
The **Onion Architecture** is a software design pattern that organizes the application into concentric layers, with the core business logic at the center and external dependencies (e.g., databases, APIs) on the outer layers. Key principles include:
- **Separation of Concerns**: Each layer has a specific responsibility.
- **Dependency Inversion**: Inner layers do not depend on outer layers; instead, both depend on abstractions.
- **Testability**: The architecture promotes unit testing and integration testing.

---

### What is CQRS?
**CQRS (Command Query Responsibility Segregation)** is a pattern that separates the read and write operations of a system. In this project:
- **Commands**: Handle write operations (e.g., creating a leave request, updating leave allocations).
- **Queries**: Handle read operations (e.g., fetching leave requests, retrieving leave balances).

This separation improves scalability, performance, and maintainability.

---

## Technologies & Libraries
The project uses the following technologies, libraries, and packages, updated for **ASP.NET Core 8**:

### Backend
- **ASP.NET Core 8**: Framework for building the API and MVC application.
- **Entity Framework Core 8**: ORM for database interactions.
- **MediatR**: Library for implementing the CQRS pattern.
- **AutoMapper**: Object-to-object mapping for DTOs and entities.
- **FluentValidation**: Validation library for request models.
- **JWT (JSON Web Tokens)**: For authentication and authorization.
- **NSwag**: For generating API documentation, client code, and OpenAPI/Swagger specifications.
- **Swagger/OpenAPI**: API documentation and testing.

### Frontend (MVC)
- **Razor Pages**: For server-side rendering of views.
- **Bootstrap 5**: Frontend framework for responsive design.
- **jQuery**: JavaScript library for DOM manipulation and AJAX requests.

### Testing
- **xUnit**: Unit testing framework.
- **Moq**: Mocking library for unit tests.
- **FluentAssertions**: Library for expressive assertions in tests.

### Infrastructure
- **Microsoft Identity**: For user authentication and role management.
- **SendGrid**: For sending emails (e.g., leave request notifications).
- **Serilog**: For structured logging.

---

## Key Features
1. **Leave Request Management**:
   - Employees can submit leave requests.
   - Managers can approve or reject requests.
   - Administrators can manage leave types and allocations.

2. **Authentication & Authorization**:
   - Role-based access control (e.g., Employee, Manager, Administrator).
   - JWT-based authentication for secure API access.

3. **CQRS Implementation**:
   - Commands for write operations (e.g., `CreateLeaveRequestCommand`).
   - Queries for read operations (e.g., `GetLeaveRequestsQuery`).

4. **Validation**:
   - FluentValidation for validating request models.
   - Custom validation logic for business rules.

5. **Logging & Error Handling**:
   - Global error handling middleware for consistent error responses.
   - Serilog for structured logging.

6. **Email Notifications**:
   - SendGrid integration for sending email notifications (e.g., leave request updates).

7. **API Documentation**:
   - **NSwag** for generating OpenAPI/Swagger specifications and API documentation.
   - Interactive Swagger UI for testing and exploring API endpoints.

---

## Getting Started

### Prerequisites
- **.NET 8 SDK**: Download and install the latest .NET 8 SDK from [here](https://dotnet.microsoft.com/download/dotnet/8.0).
- **Visual Studio Code**: IDE for development on Ubuntu Linux.
- **SQL Server** (or another database supported by EF Core).

### Installation on Ubuntu Linux
1. Install the .NET 8 SDK:
```
   wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   sudo dpkg -i packages-microsoft-prod.deb
   rm packages-microsoft-prod.deb
   sudo apt-get update
   sudo apt-get install -y dotnet-sdk-8.0
```

2. Install Visual Studio Code:
```
sudo snap install --classic code
```

3. Clone the repository:
```
git clone https://github.com/your-repo/hr-leave-management.git
```

4. Navigate to the project directory:
```
cd hr-leave-management
```

5. Restore dependencies:
```
dotnet restore
```

6. Update the database:
Persistence:
```
dotnet ef database update --context LeaveManagementDbContext   --project ./src/Infrastructure/HR.LeaveManagement.Persistence/HR.LeaveManagement.Persistence.csproj   --startup-project ./src/API/HR.LeaveManagement.API/HR.LeaveManagement.API.csproj
```
Identity
```
dotnet ef database update --context LeaveManagementIdentityDbContext --project ./src/Infrastructure/HR.LeaveManagement.Identity/HR.LeaveManagement.Identity.csproj --startup-project ./src/API/HR.LeaveManagement.API/HR.LeaveManagement.API.csproj
```

7. Generate Client Code By Using Nswag:
```
cd ./src/HR.LeaveManagement/src/UI/HR.LeaveManagement.MVC
--
nswag openapi2csclient /input:swagger.json /output:MyClient.cs /generateClientInterfaces:true /generateClientClasses:true /generateResponseClasses:true /responseClass:ApiResponse /useBaseUrl:true /injectHttpClient:true /verbosity:Detailed
```

8. Run the application:
Run API:
```
cd ./src/HR.LeaveManagement/src/API/HR.LeaveManagement.API
dotnet run
```

Run UI:
```
cd ./src/HR.LeaveManagement/src/UI/HR.LeaveManagement.MVC
dotnet run
```

9. Testing
Run unit tests using xUnit:
```
dotnet test
```