# 🗓️ My Scheduler App

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-13-316192?logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-3-FF6600?logo=rabbitmq)
![Redis](https://img.shields.io/badge/Redis-7-DC382D?logo=redis)
![Next.js](https://img.shields.io/badge/Next.js-Planned-000000?logo=nextdotjs)
![DDD](https://img.shields.io/badge/DDD-Strategic-blue)
![TDD](https://img.shields.io/badge/TDD-Test%20Driven-success)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-Yes-important)
![License](https://img.shields.io/badge/License-MIT-green)


An advanced study and showcase project focused on **modern backend architecture**, emphasizing **DDD, TDD, SOLID principles, Microservices, and real-world best practices**.

This repository is being developed incrementally, prioritizing **code quality, architectural clarity, and conscious technical decisions** before UI polish or framework-driven development.

---

## 🎯 Project Goal

Build a complete, scalable, and secure scheduling system that serves as:

- 📌 Deep architectural study
- 📌 Practical DDD + TDD reference
- 📌 GitHub portfolio showcase
- 📌 Solid foundation for future evolution (payments, notifications, admin panel)

---

## 🧠 Architectural Principles

- **Domain-Driven Design (DDD)**
  - Aggregate Roots
  - Value Objects
  - Ubiquitous Language
  - Rich domain model (no anemic domain)

- **Test-Driven Development (TDD)**
  - Unit tests at domain level
  - Integration tests with real database
  - Proper test isolation
  - High confidence in behavior

- **SOLID**
- **Clean Architecture**
- **Clear separation of responsibilities**

---

## 🧩 Overall Architecture

The project follows a **microservices-based architecture**, where each service owns its domain, database, and business rules.

### Planned Services

- **Auth Service** (in progress)
  - User registration
  - Email/password login
  - Social login (Google / Facebook)
  - JWT + Refresh Token

- **Appointments Service**
  - Scheduling
  - Availability rules

- **Payments Service**
  - Stripe integration

- **Notifications Service**
  - Emails
  - Asynchronous events

- **API Gateway**
  - Orchestration
  - Security
  - Rate limiting

---

## 🛠️ Tech Stack

### Backend
- **.NET 9**
- **FastEndpoints**
- **Entity Framework Core**
- **PostgreSQL**
- **Redis**
- **RabbitMQ**
- **JWT**
- **Custom Authentication (inspired by ASP.NET Core Identity)**

### Frontend
- **Next.js** (planned)

### Infrastructure
- **Docker**
- **Docker Compose**
- **AWS (planned)**

---

## 📁 Repository Structure

```text
my-scheduler-app/
│
├── infra/
│   └── docker-compose.yml
│
├── services/
│   └── auth/
│       ├── src/
│       │   ├── Auth.Api
│       │   ├── Auth.Application
│       │   ├── Auth.Domain
│       │   └── Auth.Infrastructure
│       │
│       └── tests/
│           ├── Auth.Tests
│           └── Auth.IntegrationTests
│
├── frontend/
│   └── nextjs-app
│
└── docs/
```
---

### 🧪 Testing Strategy
- **Unit Tests**
  - Domain (Value Objects, Aggregates)
  - Use Cases
- **Integration Tests**
  - Real repositories
  - PostgreSQL via Docker
  - Database-per-test isolation (best practice)
---
### 🚀 Running the Project (Auth Service)

#### Start Dependencies (Postgres, Redis, RabbitMQ)
``` docker-compose up -d ```

#### Run tests
```dotnet test```

---
### 📌 Current Status
- ✅ Authentication domain implemented
- ✅ User registration use case completed
- ✅ Persistence with EF Core + PostgreSQL
- ✅ Unit and integration tests in place
- 🚧 Login use case (design/implementation)
- 🚧 HTTP API
- 🚧 JWT / Refresh Token
- 🚧 Frontend (Next.js)
- 🚧 AWS deployment

---
### 🧠 Important Note
This project **intentionally prioritizes architectural correctness over speed**.

Some steps may seem slower than usual — this is by design.
The focus is on **deep understanding and long-term maintainability**, not just making things work.

---
> ### _Correctness outlives speed._

#### 📄 MIT License