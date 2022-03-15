[Architecture] Core business Layer:
- Core Business layer contains the business domain logic so it's separate from Use Case (Application) Layer which contains application logic - Clean Architecture
- Core business layer stores the Product and Order entities (POCO)
- Order entity (POCO) contains very basic behaviour such as adding and removing	products to the LineItems property so as to avoid an Anemic Domain Model
- OrderService class contains the business logic of ensuring an order is valid for creating, updating or processing using multiple entities:
	+ Validate Customer information
	+ Validate the Order information
	+ Validate the Order Line Items information

[Architecture] Use Case (Application) Layer:
- [Code Organization] Uses cases are organized into screen/page folders that can be used by any type of client (e.g. web, mobile, desktop, etc.)
- [Code Organization] Within each screen folder, one class is created for each use case (e.g. SearchProduct.cs, ViewProduct.cs), which is good for unit testing. **Note**: Mediator pattern can be used here but we're keeping it simple for this project.
- The IProductRepository interface in the Plugin Interfaces folder of the Use Case layer abstracts the data store which makes testing easier as the repository can easily be mocked using this interface.
- Data stores are IO devices and any IO device should be implemented as a plugin (thus "plugin-based" architecture)

[Architecture] UI Layer:
- Organized into web modules folder, one module for each frontend client and the common components module
- What is the eShop.Web project for?

[Persistence/Infrastructure Layer]:
- Hard coded ProductRepository implementation is stored in the Datastore.HardCoded plugin project and abstracted by the IProductRepository interface in the Use Case layer under the Plugin Interfaces folder