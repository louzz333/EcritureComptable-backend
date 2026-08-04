namespace EcritureComptable.Models
{
    public class AuditSuppression
    {
        public int Id { get; set; }
        public decimal? NumeroEcriture { get; set; }
        public DateTime? DateEcriture { get; set; }
        public string? JournalEcriture { get; set; } = "";
        public string? CompteEcriture { get; set; } = "";
        public decimal MontantEcriture { get; set; }
        public string? SensEcriture { get; set; } = "";

        public string? ReferenceEcriture { get; set; } = "";
        public string? DeviseEcriture { get; set; } = "";
        public string Libelle { get; set; } = "";
        public DateTime DateSuppression { get; set; }
        public string Motif { get; set; } = "";
    }
}
