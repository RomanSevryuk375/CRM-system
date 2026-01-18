CRM system for AutoService

REST API for managing auto repair shop business processes: from customer registration to inventory management

![Status](https://img.shields.io/badge/Status-MVP-orange)
![Framework](https://img.shields.io/badge/ASP.NET-blueviolet)
![Database](https://img.shields.io/badge/PostgreSQL-18-blue)

---

Tech Stack:

*   **Framework:** ASP.NET Core 9 (C#)
*   **Database:** PostgreSQL + Entity Framework Core (Fluent API)
*   **Caching:** Redis (Distributed Cache)
*   **Object Storage:** MinIO (S3-compatible storage)
*   **Architecture:** Clean Architecture
*   **DevOps:** Docker & Docker Compose
*   **Other:** JWT Authentication (via secure Cookies), AutoMapper, Serilog, Npgsql.

---

Key Features:

- **Order Management:** Flexible lifecycle management for repair orders and statuses.
- **Inventory System:** Multi-layer warehouse accounting (Parts → Storage Cells → Supplies/Purchases).
- **Staff Scheduling:** Shift planning and absence tracking (sick leaves, vacations).
- **Automated Billing:** Real-time cost calculation based on standard labor hours and part prices.

---

Architecture & Design Highlights

The project is built with a focus on maintainability, scalability, and data integrity:

*   **Unit of Work & Transactions:** Ensures ACID properties for complex business processes involving multiple repositories (e.g., atomic creation of a User profile and a Worker record).
*   **Decorator Pattern:** Implements transparent caching via Redis. The core business logic remains "clean," while caching logic is injected via decorators at the DI level.
*   **S3 Storage Integration:** Vehicle defect images are stored in MinIO. Includes "compensating action" logic to clean up storage if a database write fails.
*   **Global Exception Handling:** Centralized Middleware to catch exceptions and return standardized JSON responses.
*   **Domain Validation:** Business rules and integrity checks are encapsulated directly within the Domain Models.

---

Project Structure:

*   **Core:** Domain models, Repository interfaces, Projection models (DTOs), Enums, and Custom Exceptions.
*   **Business:** Business logic implementation (Services), Service interfaces, and Caching decorators.
*   **DataAccess:** Repository implementations, Unit of Work, DB Context, and Fluent API configurations.
*   **API:** Controllers, Request/Response contracts, and AutoMapper profiles.

---

Database Schema:

<details>
  <summary>Click to expand the ER Diagram</summary>
  <br>
  <p align="center">
    <img src="./docs/ERD.jpg" alt="Database Schema">
  </p>
</details>

## 🧪 Testing Strategy

The project implements a multi-layered testing approach to ensure both algorithmic correctness and system-wide data integrity.

### Unit Testing
*Focus: Isolated business logic and domain rules.*

- **Frameworks:** `xUnit`, `Moq`, `FluentAssertions`.
- **Domain Logic:** Verified "Smart Domain Models" (e.g., complex date overlap algorithms in the `Absence` model) ensuring that the core logic is correct without any infrastructure overhead.
- **Service Layer:** Tested the coordination between components, ensuring correct handling of business exceptions (`ConflictException`, `NotFoundException`) and transaction flows using isolated mocks for repositories.

### Integration Testing
*Focus: Database integrity and cross-component interaction.*

- **Infrastructure:** `Testcontainers` (PostgreSQL), `WebApplicationFactory`.
- **Data Cleanup:** `Respawn` — used to reset the database state between tests instantly using high-performance `TRUNCATE` operations, ensuring complete test isolation without the overhead of recreating the schema.
- **Real-World Scenarios:** 
    - Full lifecycle testing from Service layer to a real **PostgreSQL** instance running in a Docker container.
    - Verification of complex SQL queries and aggregations (e.g., automated billing calculations based on labor hours and part prices).
    - Automated execution of migrations against a temporary container to ensure the schema and Fluent API configurations are always up to date.

### Tools & Methodology
- **AAA Pattern:** All tests follow the *Arrange-Act-Assert* structure for maximum readability and maintainability.
- **Containerization:** Leveraging Docker to provide a "disposable" and consistent environment for every test run, eliminating the "works on my machine" problem.
- **State-Based Testing:** Integration tests verify the final state of the database rather than just method calls, ensuring reliable data persistence and relational integrity.

Quick Start:
1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
2. **Starting the infrastructure and application (Postgres, Redis, MinIO):**
   ```bash
   docker-compose up -d 
3. Access API Documentation: Navigate to http://localhost:5066/swagger to test endpoints via Swagger UI.

