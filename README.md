# Library Management API

A library management application built with **ASP.NET Core Web API, Entity Framework Core and an MVC client**.

This individual project was developed as part of a school assignment focused on building a RESTful API with authentication, authorization and a separate client application.

## Project Status

This project is completed and was developed as an individual school project.

The application allows users to browse books and manage loans, while administrators can manage books, authors and categories.

## My Contribution

As this was an individual project, I developed the application independently from start to finish.

My work included:

* Building the RESTful Web API
* Creating CRUD functionality for books, authors and categories
* Implementing book loans
* Creating the database with Entity Framework Core
* Implementing JWT authentication and role-based authorization
* Creating DTOs and service layers
* Adding seed data
* Building the MVC client
* Connecting the client to the API through HTTP
* Adding Swagger and a Postman collection

## Technologies

* **C#**
* **.NET**
* **ASP.NET Core Web API**
* **ASP.NET Core MVC**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Core Identity**
* **JWT**
* **Swagger / OpenAPI**
* **Postman**
* **Bootstrap**

## Key Functionality

### Library Management

The application supports:

* Managing books, authors and categories
* Searching and filtering books
* Borrowing and returning books
* Managing user accounts

### Authentication & Authorization

The application uses **ASP.NET Core Identity and JWT authentication** with two roles:

* **Admin**
* **User**

Administrators can manage library data, while authenticated users can use the loan functionality.

### Client Application

The project includes an **ASP.NET Core MVC client** that communicates with the API exclusively through HTTP requests.

The client provides pages for books, authors, categories, loans, login and registration.

## Project Structure

The solution is divided into three projects:

### `Pri.Ee.Core`

Contains entities, DTOs, database context, services, migrations and seed data.

### `Pri.Ee.Api`

Contains the RESTful API, authentication, authorization and Swagger configuration.

### `Pri.Ee.Client`

Contains the MVC client and services used to communicate with the API.

## Project Context

I chose to build a **library management application** because it provided a clear way to combine these requirements. The relationships between books, authors, categories, users and loans also provided a practical use case for Entity Framework Core and RESTful API design.

A separate MVC client was created to consume the API and demonstrate how an application can communicate with a backend through HTTP while keeping the database and backend logic separated from the client.

## Running the Project

The project uses **SQL Server Express** with Windows Authentication.

To run the project:

1. Open `Pri.Ee.sln` in Visual Studio.
2. Make sure SQL Server Express is running.
3. Restore NuGet packages.
4. Build the solution.
5. Start the API and client.
6. The database is created and seeded automatically.

The API can also be tested through Swagger and the included Postman collection.

