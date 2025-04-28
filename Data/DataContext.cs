namespace dotnet_rpg.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options)
    {
        Characters = Set<Character>();
        Users = Set<User>();
    }

    public DbSet<Character> Characters { get; }
    public DbSet<User> Users { get; }
}