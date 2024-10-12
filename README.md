# Movie Reservation System - Backend
## Overview
This project is a backend system for a Movie Reservation Service that allows users to sign up, log in, browse movies, reserve seats for specific showtimes, and manage their reservations. It includes user authentication, movie and showtime management, seat reservation functionality, and reporting capabilities. The project demonstrates the ability to implement complex business logic like seat reservation and scheduling, along with database design, relationships, and querying.

## Features
### User Authentication & Authorization
- User Sign-Up & Login: Users can register and log in to the system.
- Roles: The system supports two roles: Admin and Regular User.
    - Admin: Can manage movies, showtimes, and view system-wide reports.
    - Regular User: Can browse movies, reserve seats, and manage their own reservations.
- Initial Admin Setup: An initial admin is created via seed data, and only admins can promote users to the admin role.

### Movie Management (Admin Only)
- CRUD Operations for Movies: Admins can add, update, and delete movies.
- Movie Details: Each movie includes a title, description, and poster image.
- Genres: Movies are categorized by genre.
- Showtime Management: Admins can manage showtimes for each movie.

### Seat Reservation (Regular Users)
- Browse Movies: Users can view movies and their showtimes for a specific date.
- Seat Selection: Users can view available seats for a specific showtime and reserve their preferred seats.
- Reservation Management: Users can view their upcoming reservations and cancel them if needed.

### Reporting (Admin Only)
- Reservation Reporting: Admins can view all reservations, seat capacities, and total revenue.

## Tech Stack
- Backend Framework: ASP.NET Core (.NET)
- Database: SQL Server (or any SQL-based database supported by Entity Framework Core)
- Authentication: ASP.NET Core Identity
- API Design: RESTful APIs

## Future Enhancements
- Implement payment gateways for reservations.
- Introduce notifications for users regarding upcoming reservations.
- Enhance the seat selection interface with real-time availability updates.
