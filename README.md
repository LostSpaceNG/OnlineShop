-----------------------------------
------  Online Shop Project  ------
-----------------------------------

/// 
/// Overview
///

This project is a fully functional online shopping application built using ASP.NET Core. It includes both admin and customer-facing features. 
The application provides a seamless user experience for browsing products, managing a shopping cart, checking out, and managing orders. 
For administrators, it offers tools to manage products and categories.


/// 
/// Features
///

## Core Functionality

  Product and Category Management:
		- Admins can add, update, delete, and manage products and categories.
		- Categories and products are organized for easy navigation and filtering.

  Customer-Facing Pages:
		- Product catalog with filtering options (search by name and filter by category).
		- Product details pages with complete product information.

  Shopping Cart:
		- Supports logged-in users (database-backed cart) and guest users (session-based cart).
		- Unified cart functionality for adding, updating, and removing items.

  Checkout and Order Processing:
		- Customers can enter shipping details and choose between multiple payment methods (Card or PayPal).
		- Payment simulation with order confirmation.
		- Order details, including payment method, are displayed on the confirmation page.

## User Account Features

  Authentication:
		- Secure login, registration, and logout functionality using ASP.NET Core Identity.
		- Role-based security with "Admin" and "Customer" roles.

  My Account Section:
		- View and update profile details.
		- View order history with detailed order information.

## Admin-Specific Features

  Admin Dashboard:
		- Role-based access to manage categories and products securely.
		- CRUD operations for products and categories.

## Advanced Search and Filtering

  - Search functionality directly available via a search bar in the navigation menu.
	- Product filtering by category with a sticky sidebar for easy access.


/// 
/// Technology Stack
///

# Backend: ASP.NET Core with Entity Framework Core.
# Frontend: Basic Bootstrap for styling, responsive design.
# Database: SQL Server.
# Authentication: ASP.NET Core Identity.
# Tools: SSMS for database management, Visual Studio for development.


/// 
/// Setup Instructions
///

1. Clone the Repository:
	- git clone <repository-url>
	- cd <project-folder>

2. Configure the Database:
	- Open 'appsettings.json' and update the 'ConnectionStrings' section with your SQL Server details.

3. Apply Migrations:
	- dotnet ef database update

4. Run the Application:
	- dotnet run

5. Admin Role Setup:
	- Seed roles are automatically created via 'SeedRoles.cs' in /Data.
	- Default Admin credentials:
		  Email: onlineShop@gmail.com
		  Password: ichBin1Admin!
	- Register a new user and assign the "Admin" role manually in the database or extend the seed logic.


/// 
/// Security
///

- Role-based security protects the admin panel.
- Secure password storage and validation using ASP.NET Core Identity.
- Payment simulation to avoid handling sensitive financial data.


/// 
/// License
///

This project is licensed under the MIT License.
