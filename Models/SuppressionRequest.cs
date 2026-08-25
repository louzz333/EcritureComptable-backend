namespace EcritureComptable.Models
{
    public class SuppressionRequest
    {
        public List<decimal> Ids { get; set; } = new();
        public string Motif { get; set; } = "";
    }
}
