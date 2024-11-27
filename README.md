
1. Steps to Run
2. Testing
3. API

///////////////////////////////////////////////////////////
*Disclaimer* In the spirit of complete transparency, I would like to note that I am new to Docker, and was able to containerise my project by investing a bit more time upon completing the mandatory aspects of the assignment.
///////////////////////////////////////////////////////////

1. Steps to Run:
Please pull the image from my repository - https://hub.docker.com/r/damiankfdocker/productmanagement-api
docker pull damiankfdocker/productmanagement-api:latest

Then run with the below command, ensuring that ports 8080 and 5000 are bound
docker run -d -p 8080:5000 damiankfdocker/productmanagement-api:latest

The Service will now run and you can navigate to the front end via http://localhost:8080/. This will also serve as the base of the endpoints for the API, e.g http://localhost:8080/products


If not using Docker:
I have also included the Published files of the app (please see the Publish folder) in the event of being unable to use Docker. 
Providing the necessary dependencies are available on the host machine, run the ProductManagement.Server.exe in the 'Publish' folder to launch the Service, and navigate to http://localhost:5000 in browser for the front end. This will also serve as the base of the endpoints for the API, e.g http://localhost:5000/products




2 Testing:

There are a series of Unit Tests available in the ProductManagement.Tests project. 
These tests have been written with the ProductService in mind, which is relied upon by the Product and Category controllers.



3. API:

The API consists of 2 controllers corresponding to Product and Category.

// Product - When creating a new Product, please ensure the CategoryID references an existing Category
// Category - Category names must be unique

Summary of PRODUCT Endpoints
Method	Endpoint	Description
GET	/products	Get all products
GET	/products/{id}	Get a product by ID
POST	/products	Add a new product
DELETE	/products/{id}	Delete a product by ID



1. Get all Products

Endpoint: GET /products
Description: Retrieves a list of all products in the system.

Parameters: None

Responses:
200 OK: Returns a list of products.
500 Internal Server Error: If the service fails to retrieve the products.

Example Response:
[
  {
    "id": 1,
    "productName": "Test Product",
    "productCode": "TEST0000",
    "price": 9.99,
    "stockQty": 15,
    "dateAdded": "2024-11-20T12:00:00",
    "categoryId": 1
  }
]




2. Get Product by ID
Endpoint: GET /products/{id}
Description: Retrieves a product by its unique ID.

Parameters:
id: The unique identifier of the product.

Responses:
200 OK: Returns the requested product details.
404 Not Found: If the product with the specified ID does not exist.
500 Internal Server Error: If there was an issue retrieving the product.

Example Response:
{
  "id": 1,
  "productName": "Test Product",
  "productCode": "TEST0000",
  "price": 9.99,
  "stockQty": 15,
  "dateAdded": "2024-11-20T12:00:00",
  "categoryId": 1
}




3. Add Product
Endpoint: POST /products
Description: Adds a new product to the system.

ProductDTO: A JSON object containing the details of the product to be added.

Responses:
201 Created: Returns the newly created product object.
500 Internal Server Error: If there was an issue creating the product.

Example Request:
Content-Type: application/json
{
  "productName": "New Product",
  "productCode": "NEW0001",
  "price": 19.99,
  "stockQty": 100,
  "dateAdded": "2024-11-20T12:00:00",
  "categoryId": 2
}

Example Response:
{
  "id": 2,
  "productName": "New Product",
  "productCode": "NEW0001",
  "price": 19.99,
  "stockQty": 100,
  "dateAdded": "2024-11-20T12:00:00",
  "categoryId": 2
}




4. Delete Product
Endpoint: DELETE /products/{id}
Description: Deletes a product from the system by its unique ID.

Parameters:
id: The unique identifier of the product.

Responses:
204 No Content: If the product is successfully deleted.
500 Internal Server Error: If there was an issue deleting the product.

Example Request:
DELETE /products/1

Example Response:
{
  "message": "Product deleted"
}




Summary of CATEGORIES Endpoints
Method	Endpoint	Description
GET	/categories	Get all categories
GET	/categories/{id}	Get a category by ID
POST	/categories	Add a new category
DELETE	/categories/{id}	Delete a category by ID

1. Get all Categories
Endpoint: GET /categories
Description: Retrieves a list of all categories in the system.

Parameters: None

Responses:

200 OK: Returns a list of categories.
500 Internal Server Error: If the service fails to retrieve the categories.

Example Request:
GET /categories

Example Response:
[
  {
    "id": 1,
    "categoryName": "Electronics"
  },
  {
    "id": 2,
    "categoryName": "Clothing"
  }
]




2. Get Category by ID
Endpoint: GET /categories/{id}
Description: Retrieves a category by its unique ID.

Parameters:
id: The unique identifier of the category.

Responses:
200 OK: Returns the requested category details.
404 Not Found: If the category with the specified ID does not exist.
500 Internal Server Error: If there was an issue fetching the category.


Example Request:
GET /categories/1

Example Response:
{
  "id": 1,
  "categoryName": "Electronics"
}




3. Add Category
Endpoint: POST /categories
Description: Adds a new category to the system.

Parameters:
CategoryDTO: A JSON object containing the details of the category to be added.

Responses:
201 Created: Returns the newly created category object.
500 Internal Server Error: If there was an issue creating the category.

Example Request:
Content-Type: application/json

{
  "categoryName": "Home & Kitchen"
}

Example Response:
{
  "id": 3,
  "categoryName": "Home & Kitchen"
}




4. Delete Category
Endpoint: DELETE /categories/{id}
Description: Deletes a category from the system by its unique ID.

Parameters:
id: The unique identifier of the category to be deleted.

Responses:
204 No Content: If the category is successfully deleted.
500 Internal Server Error: If there was an issue deleting the category.

Example Request:
DELETE /categories/1

Example Response:
{
  "message": "Category deleted"
}











