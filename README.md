[Clean Architecture] Core business Layer:
- Core Business layer contains the business domain logic so it's separate from Use Case (Application) Layer which contains application logic - Clean Architecture
- Core business layer stores the Product entity (POCO class)

[Clean Architecture] Use Case (Application) Layer:
- [Code Organization] Uses cases are organized into screen/page folders that can be used by any type of client (e.g. web, mobile, desktop, etc.)
- [Code Organization] Within each screen folder, one class is created for each use case (e.g. SearchProduct.cs, ViewProduct.cs), which is good for unit testing. **Note**: Mediator pattern can be used here but we're keeping it simple for this project.
- [Clean Architecture] The IProductRepository interface in the Use Case layer serves as an abstraction for the data store which makes testing easier as the repository can easily be mocked using this interface. **Note**: The Generic Repository pattern (e.g. IRepository interface and BaseRepository implementation) is not implemented here to keep things simple.
- Data stores are IO devices and any IO device should be implemented as a plugin - what does implemented as a plugin mean? 

[Clean Architecture] UI Layer:
- Contains the Blazor application