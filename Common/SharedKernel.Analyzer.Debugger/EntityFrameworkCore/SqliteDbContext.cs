using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Analyzer.Debugger.EntityFrameworkCore;

public class SqliteDbContext(SqliteConnection connection) : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connection);
    }
}