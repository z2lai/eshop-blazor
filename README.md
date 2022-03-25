## General Requirements
We need an e-commerce system that can help us sell our products.
Customers can look through our product catalogue online and add one or more products to the shopping cart. Users can also change quantities or even remove products from
the shopping cart. Once ready, customers can check out from the shopping cart. After checking out, the shopping cart should be emptied and the customer who placed the customer should see an order confirmation page that displays the order number.
The orders should be placed in the order system allowing admin users to see the placed orders, so that they can process them. The admins can see a list of outstanding orders as well as all line items belong to any order. After processing the order, the admins can mark an order as processed. Any orders that are marked as processed should not be found on the outstanding orders screen, but should be found on the processed orders screen.

## Architecture
Following Clean Architecture loosely and the Use Case Approach:
- The Core Business project is the Domain layer
- The Use Case project is the Application layer
- The Web.Modules folder and the Web project is the presentation layer
- The Plugins folder is the persistence/infrastructure layer

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

[Architecture] Presentation Layer:
- Organized into web modules folder, one module for each frontend client and the common components module
- eShop.Web project is the startup Blazor project that includes the assemblies of the web modules

[Architecture] Persistence/Infrastructure Layer (Plugins):
- Hard coded ProductRepository implementation is stored in the Datastore.HardCoded plugin project (.net standard class library) and abstracted by the IProductRepository interface in the Use Case layer under the Plugin Interfaces folder
- The IShoppingCart implementation is stored in the ShoppingCart.LocalStorage plugin project to store the shopping cart in local storage
    + This project is a Razor class library unlike the other .net standard class libraries so that we can access local storage using JS interop

## Use Case Approach for Planning and Writing Code (Blazor Server App)
1. (Domain) Start with defining the domain model
2. (Application) Then create the "repo" interface for persisting the domain entities (DI Principle)
3. (Application) Then create the "use case" concrete class that consumes the "repo" interface to implement the use case
-  At this point, the use case can be unit tested by mocking an implementation of the "repo" interface (e.g. using a hardcoded data store)
4. (Application) Extract the interface from the "use case" concrete class using the VS shortcut
5. (Presentation) Then create the UI component and logic that consumes the "use case" interface
6. (Persistence) Lastly, implement the "repo" interface.
7. Then Test.

### Example Use Case Implementation - Adding a Product to the Shopping Cart stored in Local Storage:
0. Implement the View Product use case and screen first
1. Create the Entity models (POCO) for an Order and OrderLineItem
2. Create the IShoppingCart abstraction for persisting the order in the shopping cart
3. Create the concrete class for the Add Product to Cart use case by consuming the appropriate IShoppingCart interface method(s)
    - Optional: Create unit tests using a mock implementation of IShoppingCart
4. Extract the interface from the concrete class to be consumed by the UI
5. Implement the UI for adding a product to the shopping cart on the View Product page **<--**
6. Implement the IShoppingCart abstraction to use Local Storage for getting and setting the order
7. Test the use case manually via the UI and Local Storage
Bonus:
- Create an IOrderService abstraction for validating the the Order and OrderLineItem objects
- Implement the IOrderService abstraction
- Finish writing unit and integration tests
- Add more UI features/use cases for more interactivity

## Concerns/Questions:
- When implementing the UI component for the **View Shopping Cart use case**, since we are using local storage and JSInterop in the IShoppingCart implementation, we need to call it in a specific lifecycle method of the Blazor component to ensure that the JSInterop is available. Is this considered as the presentation layer (higher-level module) having a direct dependency on implementation details (instead of the abstraction) of the persistence layer (lower-level module), which goes against Dependency Inversion principle of SOLID?
    + If so, is this unavoidable because we are using Blazor which combines MVC all into one component? Or is this not considered a direct dependency since we don't actually have a code/project reference to the persistence layer?