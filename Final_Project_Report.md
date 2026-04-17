# Final Project Report

**Project Title:** Student Registration System using ASP.NET Core Razor Pages  
**Course:** Selected Topics in Software Engineering  
**Student Name:** Godwin Ofwono  
**Date:** April 17, 2026

## 1. Introduction

Student registration and academic record management are essential processes in higher education institutions. In many environments, these tasks are still handled manually or through loosely connected tools, resulting in frequent errors, data duplication, limited accountability, and delays in service delivery. Such limitations affect both students and administrators by reducing efficiency and trust in academic data.

This project presents a web-based Student Registration System designed to improve data quality, operational efficiency, and security. The system was implemented using ASP.NET Core Razor Pages, Entity Framework Core, and SQLite. The solution supports complete student record management, secure authentication, data validation, and streamlined workflows for common administrative tasks.

The final version of the project demonstrates meaningful progress from earlier stages (Test 1 and Test 2). The system now reflects a more complete and professional software product with improved usability, better architectural organization, stronger data integrity, and practical application of object-oriented software engineering principles.

## 2. System Overview

The Student Registration System is a role-aware web application that enables authorized users to manage student records through a browser interface. The main objective is to provide a centralized and reliable platform for student data operations while enforcing validation and access control.

### Core Functional Scope

- User authentication (login/logout) using cookie-based authentication.
- Student management through full CRUD operations (Create, Read, Update, Delete).
- Search, filtering, and sorting of student records for faster retrieval.
- Validation at both model and database levels to prevent invalid and duplicate entries.
- Feedback messaging to improve user interaction and task visibility.

### Main User Flow

1. A user accesses the system and signs in through the login page.
2. After successful authentication, the user is redirected to the student management area.
3. The user can create new records, view and search existing records, update student details, and remove records when necessary.
4. The system validates all key fields and provides clear success/error feedback.

This flow ensures controlled access, consistent data handling, and usability suitable for an academic administrative context.

## 3. Enhancements from Test 1 and Test 2

The project evolved substantially across the development milestones.

### Enhancements Introduced in Test 1

Test 1 established the baseline student registration functionality:

- Initial implementation of student CRUD operations.
- Introduction of model-level validation using Data Annotations.
- Database integration with Entity Framework Core and SQLite.
- Basic data integrity constraints for required fields.
- Structured project organization with models, data context, and Razor Pages.

These improvements transformed the project from a conceptual prototype into a functioning academic data management tool.

### Enhancements Introduced in Test 2

Test 2 focused on security and usability enhancement:

- Added account authentication workflow (login/logout).
- Integrated BCrypt password hashing for secure password storage.
- Added authorization attributes to protect student management pages.
- Improved form validation messages and user feedback handling.
- Added student search, filtering, and sorting functionality.

This stage made the system safer and more practical for real usage.

### Final Exam-Level Improvements (Consolidated)

For the final submission, the application was refined and stabilized to present a complete system:

- Improved validation coverage for names, email, phone number, year of study, and registration number formats.
- Implemented unique database indexes for critical fields (student email, registration number, user email, username).
- Added automatic admin account seeding for immediate first-time access.
- Improved maintainability through clearer page model logic and consistent asynchronous data access patterns.
- Strengthened project completeness with cleaner module integration and better operational flow.

These improvements provide clear evidence of progressive development and practical application of software engineering techniques across all stages.

## 4. System Architecture

The project uses a layered structure that separates interface, business flow, and persistence concerns.

### Architectural Layers

**Presentation Layer (UI):**  
Implemented using Razor Pages (`.cshtml` and PageModel classes). This layer handles user input, rendering, and interaction feedback.

**Application/Logic Layer:**  
Implemented mainly in PageModel classes where request handling, validation checks, and workflow control occur.

**Data Access Layer:**  
Implemented through `ApplicationDbContext` using Entity Framework Core. It maps models to database tables and configures constraints.

**Database Layer:**  
SQLite stores persistent data for students and users.

### Architecture Benefits

- Separation of concerns improves readability and maintainability.
- Validation is enforced at multiple levels (model and DB), reducing data corruption risk.
- EF Core abstraction reduces boilerplate SQL and accelerates development.
- Layered design supports future extension (for example, reporting modules and dashboard analytics).

## 5. Object-Oriented Concepts Used

The system applies core object-oriented concepts in practical implementation.

### Encapsulation

Model classes such as `Student` and `User` encapsulate data properties and validation rules. Related behavior is managed through dedicated page models and context configuration.

### Abstraction

Entity Framework Core abstracts database operations through `DbContext` and `DbSet`, allowing developers to focus on domain logic rather than low-level SQL operations.

### Inheritance

Razor PageModel classes inherit framework behavior from ASP.NET Core base classes, enabling reusable request lifecycle patterns and standardized page handling.

### Polymorphism and Dependency Injection

Dependency injection is used to inject `ApplicationDbContext` into page models. The runtime resolves dependencies, supporting flexible component interaction and improving testability.

### Additional Engineering Principles

- Separation of Concerns: Distinct responsibilities for UI, logic, and persistence.
- Single Responsibility: Individual page models focus on specific use cases (create, edit, delete, list).
- Reusability: Shared validation and model structures minimize duplicate logic.

## 6. Challenges and Solutions

### Challenge 1: Duplicate and Inconsistent Data

**Issue:** Repeated registration numbers and emails can break trust in records.  
**Solution:** Added unique indexes in database configuration and pre-save duplicate checks in create/edit workflows.

### Challenge 2: Security of Authentication Data

**Issue:** Storing plain-text passwords is insecure.  
**Solution:** Implemented BCrypt hashing and verification, plus cookie-based authentication for session management.

### Challenge 3: Input Validation and Data Quality

**Issue:** Invalid names, emails, or phone formats can reduce data quality.  
**Solution:** Introduced robust validation through Data Annotations and regular expressions with clear user-friendly error messages.

### Challenge 4: Usability with Growing Student Records

**Issue:** Large lists are hard to navigate without discovery tools.  
**Solution:** Added search, filter, and sort operations in the student list page to improve retrieval speed and usability.

### Challenge 5: Project Progression Across Assessment Stages

**Issue:** Demonstrating measurable improvement from Test 1 to final exam.  
**Solution:** Structured implementation in milestones, introducing features incrementally while improving architecture and completeness at each stage.

## 7. Conclusion

The Student Registration System has progressed from a simple CRUD-focused prototype into a more secure, structured, and complete web application suitable for final practical assessment. The final solution demonstrates framework proficiency (ASP.NET Core and EF Core), practical database design, and thoughtful user-focused functionality.

Through authentication, validation, unique constraints, and improved record management capabilities, the system addresses the core problems identified at the start of the project. The architecture and coding style also provide a solid foundation for further enhancements such as advanced reporting, role expansion, dashboard analytics, and API integration.

Overall, the project meets the expected outcomes of software engineering practice by combining technical implementation quality, object-oriented design application, and clear evidence of improvement across development stages.

## 8. References

1. Microsoft. (2026). *ASP.NET Core Razor Pages documentation*. https://learn.microsoft.com/aspnet/core/razor-pages/
2. Microsoft. (2026). *Entity Framework Core documentation*. https://learn.microsoft.com/ef/core/
3. Microsoft. (2026). *Authentication and authorization in ASP.NET Core*. https://learn.microsoft.com/aspnet/core/security/
4. SQLite Consortium. (2026). *SQLite Documentation*. https://www.sqlite.org/docs.html
5. BCrypt.Net. (2026). *BCrypt.Net-Next library documentation*. https://github.com/BcryptNet/bcrypt.net
