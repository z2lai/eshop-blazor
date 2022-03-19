## Architecture
Following Clean Architecture loosely:
- The Core Business project is the Domain layer
- The Use Case project is the Application layer

[Architecture] Domain Layer:
- Core Business project contains the business logic
- Core Business project stores the Product and Order entities (POCO)
- Order entity (POCO) contains very basic behaviour such as adding and removing	products to the LineItems property so as to avoid an Anemic Domain Model
- OrderService class contains the business logic of ensuring an order is valid for creating, updating or processing using multiple entities:
	+ Validate Customer information
	+ Validate the Order information
	+ Validate the Order Line Items information

[Architecture] Application Layer:
- Use Cases project contains the use cases that orchestrate business logic involved in executing specific features
- [Code Organization] Uses cases are organized into screen/page folders that can be used by any type of client (e.g. web, mobile, desktop, etc.)
- [Code Organization] Within each screen folder, one class is created for each use case (e.g. SearchProduct.cs, ViewProduct.cs), which is good for unit testing. **Note**: Mediator pattern can be used here but we're keeping it simple for this project.
 + Also, each use case is abstracted by its own interface - *DI Principle: high-level module should depend on abstractions? What is a high-level module?*
- The IProductRepository interface in the Plugin Interfaces folder abstracts the data store which makes testing easier as the repository can easily be mocked using this interface.
 + Data stores are IO devices and any IO device should be implemented as a plugin (thus "plugin-based" architecture) because I/O devices should be accessible to programs without specifying the device in advance (I/O software)
- The IShoppingCart interface in the Plugin Interfaces folder abstracts the temporary storage of the shopping cart which is order before it is confirmed by the user - *Why is it stored in the UI folder?*

[Architecture] UI Layer:
- Organized into web modules folder, one module for each frontend client and the common components module
- What is the eShop.Web project for?

[Architecture] Persistence/Infrastructure Layer:
- Hard coded ProductRepository implementation is stored in the Datastore.HardCoded plugin project and abstracted by the IProductRepository interface in the Use Case layer under the Plugin Interfaces folder
- The IShoppingCart implementation is stored in the ShoppingCart.LocalStorage plugin project to store the shopping cart in local storage.

## Use Case Approach for Planning and Writing Code
1. Start by modeling the entity in the Core Business layer
2. Then implement the use case (screens/pages) in the Use Case layer
    1. With clean architecture, we can start with abstracting the user interaction with the entity using an interface without worrying about the implementation details (interface implementation)
    2. Then we can implement our use case by creating a concrete class which composes of specific methods of the interface
    3. At this point, the use case can be tested by mocking an implementation(s) of the interface(s)
3. Implement the UI for this use case
4. Lastly, fill in the implementation details by creating the concrete class of our interface

### Example Use Case Implementation - Adding a Product to the Shopping Cart:
0. Implement the View Product Details use case first
1. Create the Entity models (POCO) for an Order and OrderLineItem
2. Create Order service for validating the OrderLineItem object and the Order object
3. Create the IShoppingCart abstraction of interacting with the shopping cart
4. Implement the Add Product to Cart use case (and also abstract it out into an interface using VS shortcut) using the IShoppingCart interface methods
    - Optional: Create unit tests using a mock implementation of IShoppingCart
5. Implement the UI for adding a product to the shopping cart on the Product page **<--**
6. Implement IShoppingCart abstraction to use Local Storage
7. Test the use case manually via the UI and check Local Storage
Bonus: Finish writing unit and integration tests, or adding more validation methods, or adding more UI features/interactivity, etc.