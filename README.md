# Save Most Popular Proxy Service
This is a .NET 8 minimal API that serves as a proxy service for interacting with an external RESTful API. The primary purpose is to process data received from the external service, identify the most popular dates for each country, and send the results via a POST request to a specified endpoint.

## Endpoints
### 1. Root Endpoint
**Method:** GET
**Path:** `/`
**Description:** Returns a simple "Hello World!" message.
**Example:** `GET http://localhost:3000/`
### 2. Save Most Popular Endpoint
**Method:** POST
**Path:** `/save-most-popular`
**Description:** Processes data from the provided dataUrl, calculates the most popular dates for each country, and sends the results to the specified postUrl.
**Request Body:**
```json
{
  "dataUrl": "https://api.filtered.ai/q/get-partner-availability",
  "postUrl": "https://example.com/post-endpoint"
}
```
**Response Body:**
```json
{
  "confirmationCode": "xxxxxxxxxxxxxxxxxxxx"
}
```

```
Example: POST http://localhost:3000/save-most-popular
```

## How to Run

1. Clone the repository.
```bash
git clone https://github.com/yourusername/your-repo.git
```

2. Navigate to the project directory.
bash
```bash
cd your-repo
```

3. Run the application.
```bash
dotnet run
```
4. Access the endpoints using a tool like Postman or your preferred method.

## Dependencies
* **Microsoft.AspNetCore:** Provides the necessary libraries for building web applications with ASP.NET Core.

* **System.Text.Json:** Used for JSON serialization and deserialization.

* **System.Net.Http.Json:** Simplifies HTTP requests and responses when working with JSON.

## Configuration
* The application listens on port 3000 by default. You can change this by modifying the `UseUrls` line in the `Program.cs` file.

## Additional Notes
* The application uses Swagger for API documentation. You can access the Swagger UI by navigating to `http://localhost:3000/swagger` in your browser during development.

* Ensure you have the necessary permissions to access the provided `dataUrl` and that the data format matches the expected JSON structure.

* The application is structured with readability and simplicity in mind. You may extend or modify the code based on your specific requirements.

Feel free to reach out if you have any questions or need further assistance!# SaveMostPopularService
