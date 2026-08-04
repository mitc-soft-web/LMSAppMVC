# LMS App MVC – Architecture & System Diagrams

This document contains the system architecture and key flow diagrams for the Library Management System.

---

## 1. High-Level Architecture

```mermaid
flowchart TB
    subgraph Clients["Clients"]
        Browser["Web Browser<br/>(Members & Librarians)"]
    end

    subgraph Presentation["LMSAppMVC (ASP.NET Core MVC)"]
        Controllers["Controllers<br/>Auth · Book · Author<br/>Category · Loan · User"]
        Views["Razor Views"]
        Static["wwwroot Assets"]
    end

    subgraph Business["Business Layer"]
        Services["Services<br/>BookService, LoanService,<br/>AuthorService, CategoryService,<br/>UserService, IdentityService"]
        Validators["FluentValidation"]
    end

    subgraph Data["Data Access"]
        Repos["Repositories + BaseRepository"]
        DbContext["LMS DbContext (EF Core)"]
        Identity["UserStore / RoleStore"]
    end

    subgraph External["External"]
        Postgres[(PostgreSQL)]
        Email["Email Provider<br/>(Brevo / SMTP)"]
    end

    Clients --> Controllers
    Controllers --> Views
    Controllers --> Services
    Services --> Validators
    Services --> Repos
    Services --> Identity
    Repos --> DbContext
    DbContext --> Postgres
    Services --> Email
```

---

## 2. Layered Architecture

```mermaid
graph TD
    A[Controllers] --> B[Services]
    B --> C[Repositories]
    C --> D[LMS DbContext]
    D --> E[(PostgreSQL)]

    B --> F[Identity Service]
    B --> G[Mail Service]
    A --> H[Razor Views]

    style A fill:#bfb,stroke:#333
    style B fill:#bbf,stroke:#333
    style C fill:#fbb,stroke:#333
    style D fill:#f9f,stroke:#333
```

---

## 3. Domain Entity Relationships

```mermaid
erDiagram
    AUTHOR ||--o{ BOOK : writes
    CATEGORY ||--o{ BOOK : categorizes
    BOOK ||--o{ LOAN : borrowed_as
    MEMBER ||--o{ LOAN : borrows
    LIBRARIAN ||--o{ LOAN : approves
    USER ||--o| MEMBER : is
    USER ||--o| LIBRARIAN : is
    USER }o--|| ROLE : has

    BOOK {
        string Title
        string ISBN
        int PublishedYear
        int TotalCopies
        int AvailableCopies
        Guid AuthorId
        Guid CategoryId
    }

    LOAN {
        Guid BookId
        Guid MemberId
        Guid LibrarianId
        DateTime BorrowDate
        DateTime DueDate
        DateTime ReturnDate
        LoanStatus LoanStatus
        bool IsReturned
    }

    AUTHOR {
        string FullName
        string Email
        string PhoneNumber
        string Country
    }

    CATEGORY {
        string Name
        string Description
    }
```

---

## 4. Loan Workflow

```mermaid
stateDiagram-v2
    [*] --> Pending : Member requests loan
    Pending --> Approved : Librarian approves
    Pending --> Rejected : Librarian rejects
    Approved --> Borrowed : Book issued
    Borrowed --> Returned : Member returns book
    Returned --> [*]
    Rejected --> [*]

    note right of Borrowed
        AvailableCopies decreased
        DueDate set
    end note

    note right of Returned
        AvailableCopies increased
        IsReturned = true
    end note
```

### Loan Sequence

```mermaid
sequenceDiagram
    participant Member
    participant LoanController
    participant LoanService
    participant BookService
    participant DB

    Member->>LoanController: Request Loan (BookId)
    LoanController->>LoanService: Create pending loan
    LoanService->>DB: Save Loan (Pending)
    DB-->>Member: Request submitted

    Note over Member,DB: Librarian reviews

    participant Librarian
    Librarian->>LoanController: Approve Loan
    LoanController->>LoanService: Approve
    LoanService->>BookService: Decrement AvailableCopies
    LoanService->>DB: Update Loan (Approved/Borrowed)
    DB-->>Librarian: Success

    Member->>LoanController: Return Book
    LoanController->>LoanService: Mark returned
    LoanService->>BookService: Increment AvailableCopies
    LoanService->>DB: Update Loan (Returned)
```

---

## 5. Authentication Flow

```mermaid
sequenceDiagram
    participant User
    participant AuthController
    participant UserService
    participant IdentityService
    participant DB

    User->>AuthController: Register (Member / Librarian)
    AuthController->>UserService: RegisterAsync
    UserService->>IdentityService: Hash password
    UserService->>DB: Create User + Role
    DB-->>User: Registration success

    User->>AuthController: Login
    AuthController->>UserService: Validate credentials
    AuthController->>AuthController: SignIn (Cookie)
    AuthController-->>User: Redirect to Dashboard
```

---

## 6. Module Overview

```mermaid
flowchart TB
    subgraph Auth["Authentication"]
        Login[Login / Logout]
        RegMember[Member Registration]
        RegLib[Librarian Registration + Code]
        Roles[Role-based Access]
    end

    subgraph Catalog["Catalog"]
        Books[Books]
        Authors[Authors]
        Categories[Categories]
    end

    subgraph Circulation["Circulation"]
        Request[Loan Request]
        Approve[Approve / Reject]
        Return[Return Book]
        Pending[Pending Loans]
    end

    Login --> Roles
    Roles --> Books
    Roles --> Request
    Books --> Authors
    Books --> Categories
    Request --> Approve
    Approve --> Return
```

---

## 7. Future Enhancements

```mermaid
mindmap
  root((LMS))
    Current
      Auth & Roles
      Books / Authors / Categories
      Loan Workflow
      Email Support
    Next
      Overdue Fines
      Search & Filters
      Reports Dashboard
      Notifications
    Later
      Reservations
      E-books
      Mobile App
      Barcode / RFID
```

---

**Notes**

- Diagrams are written in Mermaid and render on GitHub, GitLab, Notion, and VS Code.
- Update this file as new features are added.
