# LMS App MVC

**Library Management System**

A full-featured Library Management System built with **ASP.NET Core MVC**.  
It supports book catalog management, author & category administration, member/librarian registration, and a complete loan (borrow/return) workflow.

---

## Overview

LMS App helps libraries:

- Manage books (ISBN, copies, availability)
- Organize authors and categories
- Register members and librarians
- Handle loan requests, approvals, and returns
- Track overdue / pending loans
- Authenticate users with role-based access (Admin, Librarian, Member)

### Key Features

- Book catalog with author & category relationships
- Author and category management
- Member & Librarian registration
- Librarian registration codes
- Loan lifecycle (request → approve → borrow → return)
- Available copies tracking
- Cookie-based authentication
- Role-based authorization (Admin, Librarian, Member)
- Email support (invitations / notifications)
- FluentValidation
- PostgreSQL + Entity Framework Core
- Razor Views UI

---

## Tech Stack

| Layer              | Technology                              |
|--------------------|-----------------------------------------|
| Framework          | ASP.NET Core MVC (.NET 10)              |
| Database           | PostgreSQL + EF Core                    |
| Authentication     | Cookie Authentication                   |
| Authorization      | Role-based (Admin, Librarian, Member)   |
| Validation         | FluentValidation                        |
| Email              | Brevo / Mailchimp / MimeKit             |
| Architecture       | Layered (Controllers → Services → Repositories → DbContext) |
| UI                 | Razor Views + custom assets             |

---

## Solution Structure

```
LMSAppMVC/
├── Controllers/              # Auth, Book, Author, Category, Loan, User
├── Models/
│   ├── Entities/             # Book, Author, Category, Loan, User, Member, Librarian, Role
│   └── DTOs/                 # Request/Response models + validators
├── Interfaces/
│   ├── Services/
│   └── Repositories/
├── Implementation/
│   ├── Services/
│   ├── Repositories/
│   └── MailingService/
├── Identity/                 # UserStore, RoleStore, IdentityService
├── Contracts/                # BaseEntity, Enums, shared contracts
├── LMSDbContext/             # EF Core context
├── Views/                    # Razor views (Auth, Book, Author, Category, Loan, User)
├── Messaging/                # Email models
└── wwwroot/                  # CSS, JS, images
```

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL 15+
- Visual Studio 2022 / Rider / VS Code

### 1. Clone the repository

```bash
git clone <your-repo-url>
cd LMSAppMVC
```

### 2. Configure Database

Update connection string in `appsettings.Development.json` or `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=LMS_db;Username=postgres;Password=your_password;"
}
```

### 3. Apply Migrations

```bash
cd LMSAppMVC
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run --project LMSAppMVC
```

Navigate to: `https://localhost:<port>`

---

## Core Domain Entities

| Entity        | Description                                      |
|---------------|--------------------------------------------------|
| `Book`        | Title, ISBN, Author, Category, total/available copies |
| `Author`      | Full name, contact, gender, country              |
| `Category`    | Name & description                               |
| `Loan`        | Borrow/return record with status & dates         |
| `User`        | Base identity                                    |
| `Member`      | Library member profile                           |
| `Librarian`   | Librarian profile                                |
| `Role`        | Admin, Librarian, Member                         |

---

## Main Modules

### Authentication
- Member registration
- Librarian registration (with registration code)
- Login / Logout (Cookie auth)
- Role-based access control

### Catalog
- CRUD for Books, Authors, Categories
- Track total and available copies

### Loans
- Members request loans
- Librarians approve / manage pending loans
- Borrow & return workflow
- Due date tracking

---

## Documentation

- [Architecture & System Diagrams](docs/architecture.md) – System architecture, entity relationships, loan workflow, and module overview.

---

## Roles & Permissions (Typical)

| Role       | Capabilities                                      |
|------------|---------------------------------------------------|
| **Admin**  | Full access                                       |
| **Librarian** | Manage books, authors, categories, approve loans |
| **Member** | Browse books, request loans, view own loans       |

---

## Roadmap

- [x] Auth (Member / Librarian)
- [x] Book, Author, Category management
- [x] Loan request & approval flow
- [ ] Overdue fines / notifications
- [ ] Search & advanced filters
- [ ] Reports & dashboards
- [ ] Reservation system
- [ ] Digital / e-book support

---

## Contributing

1. Create a feature branch
2. Follow existing layered structure
3. Add FluentValidation for new requests
4. Submit a Pull Request

---

## License

Proprietary – All rights reserved.

---

**Built with** ASP.NET Core MVC, Entity Framework Core, PostgreSQL, and FluentValidation.
