// dotnet new tool-manifest
// dotnet tool install dotnet-ef
// dotnet ef dbcontext info --project .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef migrations add Init -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef migrations script 0 -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef migrations remove -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef database update -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\