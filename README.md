# CleanArchitecture (.NET)

A Clean Architecture starter solution for ASP.NET Core, organized into **Core**, **Infrastructure**, **Services**, and **API** projects.  
The goal is to enforce **separation of concerns**, **dependency inversion**, and a domain-first structure that scales as the application grows.

## Why this repository exists

Many ASP.NET Core projects start clean but become tightly coupled over time. This solution is structured to:
- Keep the **domain and business rules** independent from frameworks and infrastructure.
- Make dependencies point **inward** (toward the Core).
- Enable testability and maintainability through clear boundaries.

References:
- Clean Architecture (Robert C. Martin): https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html
- Microsoft guidance on common architectures: https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

---

## Solution Structure

