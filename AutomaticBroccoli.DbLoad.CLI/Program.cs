using Npgsql;

var connString = "User ID=postgres;Password=123;Server=localhost;Port=15432;Database=AutomaticBroccoliDb;";
using var connection = new NpgsqlConnection(connString);

await connection.OpenAsync();

Console.WriteLine(await GetUsersTotal(connection));

var users = await GetUsers(connection);

Console.WriteLine(string.Join('\n', users.Select(x => x.ToString())));

var user = await GetUsers(connection, "test1000@mail.ru");

Console.WriteLine(user.FirstOrDefault());


async Task<long> GetUsersTotal(NpgsqlConnection connection)
{
    if (connection.State == System.Data.ConnectionState.Closed)
    {
        throw new ArgumentException("Connection cannot be closed");
    }

    var sql = @"SELECT COUNT(1) FROM ""Users""";

    using var command = new NpgsqlCommand(sql, connection);
    command.Parameters.Clear();

    return (long?)await command.ExecuteScalarAsync() ?? 0;
}

async Task<User[]> GetUsers(NpgsqlConnection connection, string? Login = null)
{
    if (connection.State == System.Data.ConnectionState.Closed)
    {
        throw new ArgumentException("Connection cannot be closed");
    }

    var sql = @"
        SELECT ""Id"",""Login"", ""CreatedDate"" 
        FROM ""Users""
        WHERE 1=1";

    using var command = new NpgsqlCommand(sql, connection);
    command.Parameters.Clear();

    if(Login != null)
    {
        command.CommandText += @" AND ""Login"" = @Login";
        command.Parameters.Add(new NpgsqlParameter("@Login", Login));
    }

    using var reader = await command.ExecuteReaderAsync();

    if (!reader.HasRows)
        return [];

    var users = new List<User>();
    while (await reader.ReadAsync())
    {
        var user = new User(
            Id: (int)reader.GetValue(0),
            Login: (string)reader[nameof(User.Login)],
            CreatedDate: reader.GetDateTime(2)
            );

        users.Add(user);
    }

    return users.ToArray();
}

record User(int Id, string Login, DateTimeOffset CreatedDate);