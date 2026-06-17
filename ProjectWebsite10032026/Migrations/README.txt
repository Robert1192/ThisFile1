To apply the migration locally:

1. Ensure dotnet-ef tools are installed:
   dotnet tool install --global dotnet-ef

2. From the project directory, run:
   dotnet ef migrations add AddUrlAndFileName
   dotnet ef database update

Note: I already added a migration file in Migrations/20250617_AddUrlAndFileName.cs. EF may refuse to use it if migration history differs; prefer running the dotnet ef commands to generate and apply the migration in your environment.