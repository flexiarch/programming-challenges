using System.Data.Entity;
using System.Data.Entity.Migrations;
using UnitConverter.Model.Entities;

namespace UnitConverter.Model.Accessor
{
    public class UnitDbContext : DbContext
    {
        static UnitDbContext()
        {
            Database.SetInitializer<UnitDbContext>(null); // no initializer;
        }

        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<ConversionExpression> ConversionExpression { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
            
        }
    }

    public class MigrationDefiniontion : DbMigrationsConfiguration<UnitDbContext>
    {
        public MigrationDefiniontion()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
