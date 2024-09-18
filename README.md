# Lemax_Test_Assignment


## Overview

This project is a JSON REST web service for managing and searching hotels. It provides two main functionalities:

1. **CRUD Operations for Hotel Data**
2. **Search Interface for Hotels**

The service is built using ASP.NET Core with C#, leveraging AutoMapper for object-to-object mapping. It includes logging with `Serilog` and authentication via ASP.NET Core's `Authorize` attribute.

## API Endpoints

### 1. CRUD Interface

#### Get Hotel by ID

- **Endpoint:** `GET /api/hotel/{id}`
- **Description:** Retrieves a hotel by its unique identifier.
- **Parameters:** 
  - `id` (Guid): The unique identifier of the hotel.
- **Responses:**
  - `200 OK`: Returns the hotel details.
  - `404 Not Found`: Hotel with the specified ID does not exist.
  - `500 Internal Server Error`: An error occurred while processing the request.

#### Create Hotel

- **Endpoint:** `POST /api/hotel`
- **Description:** Creates a new hotel entry.
- **Request Body:**
  - `HotelCreateDto`: Contains the hotel name, price, and geo-location.
- **Responses:**
  - `201 Created`: Hotel created successfully.
  - `400 Bad Request`: Invalid request data.
  - `500 Internal Server Error`: An error occurred while creating the hotel.

#### Update Hotel

- **Endpoint:** `PUT /api/hotel/{id}`
- **Description:** Updates an existing hotel entry.
- **Parameters:**
  - `id` (Guid): The unique identifier of the hotel.
- **Request Body:**
  - `HotelUpdateDto`: Contains updated hotel details.
- **Responses:**
  - `204 No Content`: Hotel updated successfully.
  - `400 Bad Request`: Invalid request data or ID mismatch.
  - `404 Not Found`: Hotel with the specified ID does not exist.
  - `500 Internal Server Error`: An error occurred while updating the hotel.

#### Delete Hotel

- **Endpoint:** `DELETE /api/hotel/{id}`
- **Description:** Deletes a hotel entry.
- **Parameters:**
  - `id` (Guid): The unique identifier of the hotel.
- **Responses:**
  - `204 No Content`: Hotel deleted successfully.
  - `404 Not Found`: Hotel with the specified ID does not exist.
  - `500 Internal Server Error`: An error occurred while deleting the hotel.

### 2. Search Interface

#### Search Hotels

- **Endpoint:** `GET /api/hotel/search`
- **Description:** Searches for hotels based on the current geo-location.
- **Parameters:**
  - `currentLocation` (GeoLocationDto): The user's current geo-location.
  - `pageNumber` (int, optional): The page number for pagination (default is 1).
  - `pageSize` (int, optional): The number of hotels per page (default is 10).
- **Responses:**
  - `200 OK`: Returns a list of hotels sorted by proximity and price.
  - `400 Bad Request`: Invalid geo-location data.
  - `500 Internal Server Error`: An error occurred while searching for hotels.

## Technical Design

- **Architecture:** The service uses ASP.NET Core with a RESTful API design.
- **Data Management:** In-memory data storage is used. Future integration with a persistent storage layer is designed to be straightforward.
- **Pagination:** Supported in the search interface to handle large sets of data efficiently.
- **Logging:** Detailed logging with `Serilog` for tracking requests and handling errors.
- **Validation:** Includes input validation and error handling to manage invalid requests and data inconsistencies.
- **AutoMapper:** Utilized for mapping between DTOs and data models.
- **GeoLocationHelper:** GeoLocationHelper class provides utility methods for calculating the distance between two geographical locations based on their latitude and longitude. It uses  Haversine formula to compute the great-circle distance between two points on the Earth's surface.

## Security

- **Authentication:** The API requires authentication using the `Authorize` attribute.
- **Data Validation:** Defensive programming practices and input validation are implemented to ensure data integrity and security.

## HealthChecks

In Program.cs, health checks are configured to monitor the application's health status
-**Self Check:** A basic health check to ensure the service is running.
-**Additional Checks:** Other health checks can be added as needed, such as database connectivity checks.

## Middleware Configuration
-**DummyAuthMiddleware::** Simulates authentication for testing purposes.
-**GlobalExceptionHandlingMiddleware::** Handles unhandled exceptions, logs errors, and provides a standardized JSON error response.

## Testing
- **TestData.cs:**  This file includes sample hotel data for use in unit tests.
- **Unit Tests:** The codebase includes unit tests to verify the functionality of the service.
- **Test Automation:** Test cases are automated for continuous integration and delivery.


## Installation and Setup

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/your-repo-url.git
