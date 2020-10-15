::Tax Management API Application::

1. Implemented swagger documentation for the API project (http://localhost:52923/swagger/ui/index.html)
2. Impemented ApiKey authentication to secure the application to for testing please key in "1234abcd" as Api key in the authorization section of swagger.
3. Implemented and registered ErrorHandlingMiddleware in StartUp to catch and process exceptions.
4. Implemented CQRS pattern using MediatR for business logic to adhere to SOLID principles.
5. Implemented Generic Repository pattern in the repository layer.
6. Implemented dependency injection where ever needed.
7. Used service based local database and corresponding connection string has been configured in appsettings.json.
8. Created tables with proper foreignkeys and unique indexes.
9. Followed multi layered architecture.

To run the application please make "TaxManagement.API" as start-up project

Endpoint details:
Please use, 
a. "/Tax/municipalities" POST endpoint to add municipality names
b. "/Tax" PUT endpoint to add or update a daily/monthly/yearly tax rate for a given municiplaity. Enter starting date as date input.
c. "/Tax" GET endpoint can be used to fetch the tax rate for a given municipality and date.