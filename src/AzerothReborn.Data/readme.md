## Creating Migrations

```bash
dotnet ef migrations add [InitialCreate] --context Auth.Context --output-dir Auth/Migrations
dotnet ef migrations add [InitialCreate] --context AzerothReborn.Data.Character.Context --output-dir Character/Migrations
```

> If something wierd happens use the ef tool to rollback the migration and clear whatever cache is causing the issue 

```bash
dotnet ef migrations remove --context AzerothReborn.Data.Character.Context
```