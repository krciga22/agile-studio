# Agile Studio Server Test
This project contains tests for Agile Studio Server. 

Note: **TestContainers** is used to create a temporary database for testing. 
The database is automatically created before any tests are run and is stopped  
after all tests have completed. This ensures that tests are run in a separate 
database from your local development database.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker or Equivalent Container Runtime](https://www.docker.com/) (required for TestContainers)

## Quick Start
1. Clone the repository.
1. Configure environment settings as needed (see below).
1. Ensure Docker is installed and running.
1. Ensure no other services are using the required ports (e.g. 5432 for PostgreSQL).
1. Run tests as described below.

## Environment Settings
Environment specific settings, such as database name/user/port, can be configured in: `./AgileStudioServerTest/appsettings.Testing.json`. 

For all available settings, see: `./AgileStudioServer/appsettings-example.json`.

## Execute tests in Visual Studio
1. Open the **AgileStudioServer** solution in Visual Studio.
1. Right-click on the **AgileStudioServerTest** project in the Solution Explorer.
1. Select "Run Tests", "Debug Tests" or "Show in Test Explorer".

Note: the ASPNETCORE_ENVIRONMENT for testing is set to `Testing` and is defined in `.runsettings`.

## Execute tests via Powershell
```shell
cd .\AgileStudioServerTest
dotnet test -e ASPNETCORE_ENVIRONMENT=Testing
```