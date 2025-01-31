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