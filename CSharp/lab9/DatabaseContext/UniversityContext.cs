namespace DatabaseContext;
using Microsoft.EntityFrameworkCore;
using DatabaseModels;

public class UniversityContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Curator> Curators{ get; set; }
    public DbSet<Group> Groups { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .IsRequired();
        modelBuilder.Entity<Student>()
            .Property(s => s.Age)
            .IsRequired();
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId);

        modelBuilder.Entity<Curator>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Curator>()
            .Property(c => c.Name)
            .IsRequired();
        modelBuilder.Entity<Curator>()
            .Property(c => c.Email)
            .IsRequired();
        modelBuilder.Entity<Curator>()
            .HasOne(c => c.Group)
            .WithOne(g => g.Curator)
            .HasForeignKey<Curator>(c => c.GroupId)
            .IsRequired();

        modelBuilder.Entity<Group>()
            .HasKey(g => g.Id);
        modelBuilder.Entity<Group>()
            .Property(g => g.Name)
            .IsRequired();
        modelBuilder.Entity<Group>()
            .Property(g => g.CreationDate)
            .IsRequired();
        modelBuilder.Entity<Group>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId);
        
        base.OnModelCreating(modelBuilder);
    }

    public UniversityContext() : base(
        GetOptions("User ID=valerikanasha228;Password=postgres;Server=localhost;Port=5432;Database=valerikanasha228"))
    { }
    
    private static DbContextOptions GetOptions(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return optionsBuilder.Options;
    }
}