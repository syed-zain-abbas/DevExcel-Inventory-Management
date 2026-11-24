# AI Usage Notes

## Tool Used

OpenAI ChatGPT was used as a development assistant.

## How AI Was Used

- Assisting with entities, DTOs, repositories, and application services.
- Discussing SQL Server transactions and stock concurrency handling.
- Understanding MVC controllers, Razor Views, and model binding.
- Assisting with EF Core configurations and seed data.
- Improving Bootstrap-based UI design.
- Preparing project documentation.

## AI Suggestion Corrected

During the development of ProductService, AI suggested implementing a separate method to manually validate DTO Data Annotations.

I identified that this duplicated the validation already performed by ASP.NET Core MVC through ModelState.IsValid.

The approach was revised to keep form validation in the MVC controllers and business rules, such as duplicate SKU and insufficient-stock validation, in the Application layer.

This simplified the implementation for the current MVC workflow. Additional input validation would be required if other write interfaces were introduced.

## Development Approach

The application was developed incrementally, with individual components discussed and implemented step by step. AI was used to assist with implementation and evaluate different approaches.

I take responsibility for the submitted code and its technical decisions.