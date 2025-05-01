# Customer List Full Stack App

This is a full-stack web application built with **Angular** (frontend) and **.NET 8 Web API** (backend) to manage a list of customers and their contact details.

## Purpose

This project was created as a first full-stack app to:
- Learn how frontend and backend connect via HTTP
- Practice CRUD operations (Create, Read, Delete)
- Use Entity Framework Core with SQL Server
- Build a responsive UI using PrimeNG in Angular
- Understand API consumption from Angular services

---

## Tech Stack

- **Frontend:** Angular 16, PrimeNG, TypeScript
- **Backend:** .NET 8 Web API, Entity Framework Core
- **Database:** SQL Server (local)
- **Communication:** HTTPClient (Angular) with CORS enabled

---

## Features

- Add, list, and delete customers
- CustomerInfo sub-record (1:1 relationship)
- CreatedDate auto-recorded in DB
- Responsive customer table with PrimeNG
- Table reset and animations for smooth UX

---

## How to Run Locally

### Backend (WebAPI-Customers)

1. Open terminal:
   ```bash
   cd WebAPI-Customers
   dotnet restore
   dotnet ef database update
   dotnet run

2. Ensure it runs on https://localhost:7224

### Frontend (Front-End-Customers)

1. Open new terminal:
   ```bash
   cd Front-End-Customers
   npm install
   ng serve

2. Access app at: http://localhost:4200

-Backend must be running before frontend can fetch customer data.

## Notes

*appsettings.Development.json is ignored via .gitignore for safety.

*Connection string must be configured locally in appsettings.json.

*CORS is enabled for http://localhost:4200.



