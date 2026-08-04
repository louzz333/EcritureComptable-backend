using EcritureComptable.Models;

namespace EcrituresApi.Models
{
    public class EcriturePagineeResult
    {
        public List<Ecriture> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class HistoriquePagineeResult
    {
        public List<AuditSuppression> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
