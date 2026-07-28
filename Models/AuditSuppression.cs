namespace EcritureComptable.Models
{
    public class AuditSuppression
    {
        public int Id { get; set; }
        public decimal? NumeroEcriture { get; set; }
        public string Libelle { get; set; } = "";
        public DateTime DateSuppression { get; set; }
        public string? Motif { get; set; } = "";
    }
}
