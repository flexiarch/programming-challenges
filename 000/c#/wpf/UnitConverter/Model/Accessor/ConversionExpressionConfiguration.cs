using System.Data.Entity.ModelConfiguration;
using UnitConverter.Model.Entities;

namespace UnitConverter.Model.Accessor
{
    class ConversionExpressionConfiguration : EntityTypeConfiguration<ConversionExpression>
    {
        public ConversionExpressionConfiguration()
        {
            const string tableName = "ConversionExpression";
            ToTable(tableName);

            HasKey(t => new { t.InUnitId, t.OutUnitId });

            Property(k => k.InUnitId).HasColumnName("InUnit");
            Property(k => k.OutUnitId).HasColumnName("OutUnit");

            HasRequired(r => r.InUnit).WithMany().HasForeignKey(k => k.InUnitId);
            HasRequired(r => r.OutUnit).WithMany().HasForeignKey(k => k.OutUnitId);
        }
    }
}
