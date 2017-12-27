using System.Data.Entity.ModelConfiguration;
using UnitConverter.Model.Entities;

namespace UnitConverter.Model.Accessor
{
    class UnitConfiguration : EntityTypeConfiguration<Unit>
    {
        public UnitConfiguration()
        {
            ToTable("Unit");
            HasKey(u => u.Identifier)
            .Property(p => p.Identifier).HasColumnName("UnitId");
        }
    }
}
