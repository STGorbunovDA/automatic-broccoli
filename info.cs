//https://www.youtube.com/watch?v=s6fHXqzFzDI&list=PLmmIuINqEtvwFrQ9tpYzGMauy4GVp6GuR&index=39
//2:25:55


// dotnet new tool-manifest
// dotnet tool install dotnet-ef


// dotnet ef dbcontext info --project .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef migrations add Init -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef migrations script 0 -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef migrations remove -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef database update -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\

// dotnet ef database update -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\ --connection "User ID=postgres;Password=123;Server=localhost;Port=15432;Database=testDb;"

// dotnet ef migrations add ChangeColumnUserId -s .\AutomaticBroccoli.API\ -p .\AutomaticBroccoli.DataAccess.Postgres\