# Migration SQL scripts for Agile Studio Server
These scripts are used to migrate the database schema and data for Agile Studio Server in non-development environments.

## Genearting a SQL Script
To generate a migration script for a specific version/release, from the `AgileStudioServer/` directory, execute the following command:

```powershell
# first script
dotnet ef migrations script -o Data/Scripts/migration-v0.1.sql

# all subsequent scripts (from and to)
dotnet ef migrations script -o Data/Scripts/migration-v0.2.sql 20230915001_MigrationX 20231001001_MigrationY
```

Note: scripts should always be added to version control.