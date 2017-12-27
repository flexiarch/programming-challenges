namespace UnitConverter.Model.Entities
{
    public class ConversionExpression
    {
        public long InUnitId { get; set; }
        public long OutUnitId { get; set; }
        public virtual Unit InUnit { get; set; }
        public virtual Unit OutUnit { get; set; }
        public string Expression { get; set; }
        public string Description { get; set; }
    }
}
