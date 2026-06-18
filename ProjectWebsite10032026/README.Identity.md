Identity setup notes
--------------------

1) After these code changes, you must add and run EF migrations to create Identity tables in the database:

   dotnet tool install --global dotnet-ef --version 8.0.10
   dotnet ef migrations add AddIdentityTables -p ProjectWebsite10032026 -s ProjectWebsite10032026
   dotnet ef database update -p ProjectWebsite10032026 -s ProjectWebsite10032026

2) The IdentitySeed creates a default admin user (admin@example.com / P@ssw0rd!). Change or remove this credential in production.

3) You can log in at /login and log out at /logout. The Upload and Delete controls on /downloads are restricted to users in the Admin role.
