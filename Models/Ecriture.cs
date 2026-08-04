using System;

namespace EcrituresApi.Models
{
    public class Ecriture
    {
        public decimal CleMvt { get; set; }
        public decimal? N_Ecriture { get; set; }
        public string? Journal { get; set; } = "";
        public string? Compte_comptable { get; set; } = "";
        public string? Reference_Piece { get; set; } = "";
        public DateTime? Date { get; set; }
        public string? Libelle_Ecriture { get; set; } = "";
        public string? Tiers { get; set; }
        public string? Sens { get; set; } = "";
        public string? Type { get; set; } = "";
        public decimal Montant { get; set; }
        public decimal? MONTANT_DEVISE { get; set; }
        public string? DEVISE { get; set; } = "";
        public string? ETABLISSEMENT { get; set; } = "";
        public string? Section_Analytique { get; set; }
        public string? TYPE_PIECE { get; set; } = "";
        public string? Societe { get; set; } = "";
        public decimal? EtatComptabilisation { get; set; }
    }
}
