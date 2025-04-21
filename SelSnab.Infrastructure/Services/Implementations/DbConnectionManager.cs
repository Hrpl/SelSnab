
using SelSnab.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace SelSnab.Infrastructure.Services.Implementations;

public class DbConnectionManager : IDbConnectionManager
{ 
    private readonly ILogger<DbConnectionManager> _logger;

    public DbConnectionManager(ILogger<DbConnectionManager> logger)
    {
        _logger = logger;
    }
    private string NpgsqlConnectionString => $"Host=89.169.2.95;Port=5432;Database=SelSnab;Username=postgres;Password=2208;";


    public NpgsqlConnection PostgresDbConnection => new(NpgsqlConnectionString);

    public QueryFactory PostgresQueryFactory => new(PostgresDbConnection, new PostgresCompiler(), 60)
    {
        Logger = compiled => { _logger.LogInformation("Query = {@Query}", compiled.ToString()); }
    };
}
