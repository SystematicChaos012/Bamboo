using Microsoft.Data.Sqlite;
using SharedKernel.Analyzer.Debugger.EntityFrameworkCore;

namespace SharedKernel.Analyzer.Debugger;

class Program
{
    static void Main(string[] args)
    {
        using var connection = new SqliteConnection("datasource=:memory:");
        connection.Open();

        using var dbContext = new SqliteDbContext(connection);
        dbContext.Database.EnsureCreated();

        dbContext.SaveChanges();
    }
}