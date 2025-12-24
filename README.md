# FunDooApplication

FunDooApplication is a multi-layered ASP.NET Core Web API project designed using clean architecture / N-tier architecture principles.
The application separates concerns into distinct layers to improve maintainability, scalability, and testability.

## Layer Responsibilities
## Presentation Layer (FunDooAPP)

Exposes REST APIs

Handles HTTP requests & responses

No business logic

Calls Business Logic Layer

## Business Logic Layer

Contains core application rules

Implements validations & workflows

Uses interfaces for loose coupling

Calls Database Logic Layer

## Database Logic Layer

Handles all database operations

Implements Repository pattern

Interacts with SQL Server / EF Core

No business rules

## Model Layer

Contains entity classes

DTOs for request/response

Shared across all layers

No logic inside models
