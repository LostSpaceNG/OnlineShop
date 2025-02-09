/ *******************************************
/ Welcome to OnlineShopMVC Project
/ *******************************************

About this project
- Todo


Components Documentation


///
/// REPOSITORY and SERVICE Layers
///

# /Repositories
	
	Function: Handles database operations through direct interaction with DbContext and keeps them in one central location.
	Components:
		- IGenericRepository<T>: Defines general rules how a generic repository should interact with the database.
		- GenericRepository<T>: The actual repository that interacts with the database.
		- ICategoryRepository and CategoryRepository: Specialized repos to handle categories.
		- IProductRepository and ProductRepository: Specialized repos to handle products.
	
# /Services

	Function: Contains business logic and interacts with repos.
	Components:
		- ICategoryService and CategoryService: Handle logic for categories before calling repos.
		- IProductService and ProductService: Handle logic for products before calling repos.



///
/// Authentication & Authorization
///

# ASP.NET Core Identity

	- Implements secure authentication and role-based authorization.

# Custom User Model

	- ApplicationUser.cs: extends 'IdentityUser' to include additional custom fields (e.g. FullName, Address, etc.).

# Account Management

	- AccountController.cs: handles registration, login, logout and access-denied scenarios using 'UserManager' and 'SignInManager'.

# Role-Based Security

	- [Authorize(Roles = "Admin")] attribute: protects admin areas.
	- /Data/SeedRoles.cs: automatically creates and assigns roles on startup.

# Cookie & Session Management

	- Persistent authentication cookies (30-min lifespan) maintain user sessions.
	- Guest data (like shopping carts) is managed via session storage.

# Configuration

	- All services (Identity, Cookie Options, Session) are configured in Program.cs.



///
/// Shopping Cart Functionality
///

# Database Storage for logged-in users

	- ICartService and CartService: Handle database-backed cart operations.

# Session Storage for guest users

	- /Helpers/SessionCartHelper.cs: Manages cart items stored in the session.

# Controller and Model

	- CartController.cs: handles cart-related actions, selecting the appropriate storage mechanism based on user authentication.
	- CartItem.cs: represents items in the cart.
