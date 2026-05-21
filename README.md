# Project & Task Management API

A scalable backend API built with **ASP.NET Core Web API**, **.NET 9**, **Entity Framework Core**, **SQL Server**, **JWT Authentication**, and **Clean Architecture**.

This project was developed as a Backend .NET Developer technical assessment.  
It allows authenticated users to manage their own projects and tasks securely.

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture Overview](#architecture-overview)
- [Project Structure](#project-structure)
- [Database Setup with Docker](#database-setup-with-docker)
- [Application Configuration](#application-configuration)
- [How to Run the Project](#how-to-run-the-project)
- [Database Migrations](#database-migrations)
- [Authentication](#authentication)
- [API Endpoints](#api-endpoints)
- [Swagger Documentation](#swagger-documentation)
- [Postman Collection](#postman-collection)
- [Validation](#validation)
- [Error Handling](#error-handling)
- [Security Notes](#security-notes)
- [Future Improvements](#future-improvements)
- [Author](#author)

---

## Overview

The system is a simple **Project & Task Management API**.

Each authenticated user can:

- Create and manage projects
- Create and manage tasks inside projects
- Update task status
- Access only their own data

The project focuses on clean code, maintainability, proper API design, and scalable architecture.

---

## Features

### Authentication

- User registration
- User login
- JWT token generation
- Protected endpoints using Bearer tokens

### Projects Module

- Create project
- Get all projects for the authenticated user
- Get project by ID
- Update project
- Delete project

### Tasks Module

- Create task inside a project
- Get tasks by project
- Update task status
- Delete task

### Technical Features

- Clean Architecture
- Dependency Injection
- SOLID principles
- Entity Framework Core
- SQL Server database
- Dockerized database using Azure SQL Edge
- JWT Authentication
- DTO usage
- Validation
- Global exception handling
- Swagger documentation
- Database migrations
- User-based data isolation

---

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / Azure SQL Edge
- Docker
- JWT Authentication
- Swagger / OpenAPI
- C#

---

## Architecture Overview

The project follows **Clean Architecture** principles to separate business logic from infrastructure and presentation concerns.

### Domain Layer

Contains the core business entities and enums.

Examples:

- `Project`
- `ProjectTask`
- `TaskStatus`
- `TaskPriority`

The Domain layer does not depend on any other layer.

---

### Application Layer

Contains application logic, DTOs, service contracts, and validation rules.

Responsibilities:

- Define request and response models
- Define service interfaces
- Handle application use cases
- Apply validation rules
- Keep business workflows independent from infrastructure

---

### Infrastructure Layer

Contains external implementations and database-related code.

Responsibilities:

- Entity Framework Core configuration
- Database context
- Repository implementations
- Identity implementation
- JWT token generation
- Database migrations

---

### API Layer

The entry point of the application.

Responsibilities:

- Controllers
- Middleware
- Dependency injection setup
- Authentication setup
- Swagger setup
- HTTP request/response handling

---

## Project Structure

```txt
ProjectTaskManagement/
│
├── src/
│   ├── ProjectTaskManagement.Api/
│   │   ├── Controllers/
│   │   ├── Extensions/
│   │   ├── Middlewares/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── ProjectTaskManagement.Application/
│   │   ├── Common/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   └── Validators/
│   │
│   ├── ProjectTaskManagement.Domain/
│   │   ├── Entities/
│   │   └── Enums/
│   │
│   └── ProjectTaskManagement.Infrastructure/
│       ├── Identity/
│       ├── Persistence/
│       ├── Repositories/
│       └── Migrations/
│
├── docker-compose.yml
├── README.md
├── ProjectTaskManagement.postman_collection.json
└── ProjectTaskManagement.sln