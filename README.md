# Budgetly API

Budgetly is a RESTful API to mange personal budget flow. This repository only contains the backend of this application. Frontend resides on a separate repository. API was built with `.NET 6` and `PostgreSQL`. You can use the `InMemory` database as well (controlled from the appsettings). Built according to the CQRS pattern and clean architecture. This was done as a part of a MSc module project.

## Diagrams and other screenshots

> Repository contains only the API module

## Dashboard

![dashboard](https://github.com/gayankanishka/budgetly-api/blob/main/docs/dashboard.png?raw=true)

## High-level architecture diagram

![alt text](https://github.com/gayankanishka/budgetly-api/blob/main/docs/budgetly-architecture.png?raw=true)

## Endpoints

![endpoints](https://github.com/gayankanishka/budgetly-api/blob/main/docs/endpoints.png?raw=true)

What's included:

- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MediatR](https://github.com/jbogard/MediatR)
- [Swagger](https://swagger.io/)
- [Serilog](https://serilog.net/)
- [Fluent Validation](https://fluentvalidation.net/)
- [Appinsights](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview#:~:text=Application%20Insights%20is%20a%20feature,by%20using%20powerful%20analytics%20tools.)
- [PostgreSQL](https://www.postgresql.org/)

## Table of Content

- [Quick Start](#quick-start)
  - [Prerequisites](#prerequisites)
  - [Development Environment Setup](#development-environment-setup)
  - [Build and run](#build-and-run-from-source)
- [License](#license)

## Quick Start

After setting up your local DEV environment, you can clone this repository and run the solution.

### Prerequisites

You'll need the following tools:

- [.NET](https://dotnet.microsoft.com/download), version `>=6`
- [Visual Studio](https://visualstudio.microsoft.com/), version `>=2022` or [JetBrains Rider](https://jetbrains.com/rider/), version `>=2021`

### Development Environment Setup

First clone this repository locally.

- Install all of the the prerequisite tools mentioned above.

### Build and run from source

With Visual studio:
Open up the solutions using Visual studio.

- Restore solution `nuget` packages.
- Rebuild solution once.
- Run the solution.
- Local swagger URL [here](https://localhost:7208/swagger).

## License

Licensed under the [MIT](LICENSE) license.
