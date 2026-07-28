namespace EcrituresApi.Models
{
    public class EcriturePagineeResult
    {
        public List<Ecriture> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
