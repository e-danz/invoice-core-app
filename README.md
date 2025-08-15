# Invoice Core Application

## Overview

Invoice processing system built with ASP.NET Core, implementing
secure API endpoints for invoice data management with RabbitMQ
integration.

## Key Features

1. Endpoints for invoice management.
2. JWT-based login for secure API access.
3. Razor Pages UI with cookie authentication.

## System Architecture

This solution consists of two applications:

1. **API Service (ASP.NET Core)**
   - Authenticated endpoint for invoice submission
   - SQLite persistence via Entity Framework
   - RabbitMQ queue producer

2. **Monitoring Application (.NET 5 Console)**
   - RabbitMQ queue consumer
   - Message processing and display functionality

## Data Model

```csharp
public class Invoice
{
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Supplier { get; set; }
    public List<InvoiceLine> Lines { get; set; }
}

public class InvoiceLine
{
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}
```
## REST API Endpoints


- GET /api/invoices � Returns all invoices.
- GET /api/invoices/{id} � Returns a specific invoice by ID.
- POST /api/invoices � Creates a new invoice.
- (not implemented) PUT /api/invoices/{id} � Updates an existing invoice.
- (not implemented) DELETE /api/invoices/{id} � Deletes an invoice by ID.

All invoice endpoints require valid JWT authentication.

- POST /api/auth/login � Authenticates user credentials and returns a JWT token.
 

## Development

### Requirements

- .NET 5 SDK or later
- Entity Framework Core tools
- Postman (for API testing)

### Getting Started

1. Clone the repository
2. Configure connection strings in appsettings.json
3. Run database migrations
4. Start the API service
5. Start the console processing service

### Testing

A Postman collection is provided for comprehensive workflow testing including:
- Authentication token acquisition
- Invoice submission
- End-to-end verification

## Project Status

In active development. See the [project board](https://github.com/users/e-danz/projects/2) for current priorities and progress tracking.