using EcritureComptable.Models;
using EcrituresApi.Data;
using EcrituresApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace EcrituresApi.Services
{
    public class EcritureService
    {
        private readonly ComptaDbContext _db;//le pont vers db

        public EcritureService(ComptaDbContext db)//mon constructeur
        {
            _db = db;
        }

        public EcriturePagineeResult GetAll(string? libelle = null, string? journal = null, DateTime? datedebut = null, DateTime? datefin = null, string? comptecomptable = null, string? refpiece = null, string? devise = null, int page = 1, int pageSize = 10)
        {
            var query = _db.Ecritures.AsQueryable();

            if (!string.IsNullOrEmpty(libelle))
                query = query.Where(e => e.Libelle_Ecriture.Contains(libelle));

            if (!string.IsNullOrEmpty(journal))
                query = query.Where(e => e.Journal == journal);

            if (datedebut.HasValue)
                query = query.Where(e => e.Date >= datedebut.Value);

            if (datefin.HasValue)
                query = query.Where(e => e.Date <= datefin.Value);

            if (!string.IsNullOrEmpty(comptecomptable))
                query = query.Where(e => e.Compte_comptable == comptecomptable);

            if (!string.IsNullOrEmpty(refpiece))
                query = query.Where(e => e.Reference_Piece.Contains(refpiece));

            if (!string.IsNullOrEmpty(devise))
                query = query.Where(e => e.DEVISE == devise);

            int total = query.Count();

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new EcriturePagineeResult
            {
                Items = items,
                TotalCount = total
            };
        }

        public object GetKpis()
        {
            var totalDebit = _db.Ecritures.Where(e => e.Sens == "D").Sum(e => e.Montant);
            var totalCredit = _db.Ecritures.Where(e => e.Sens == "C").Sum(e => e.Montant);

            return new
            {
                TotalDebit = totalDebit,
                TotalCredit = totalCredit,
                EnAttente = 0,
                SupprimeesCeMois = 0
            };
        }

        public void SupprimerEcritures(List<decimal> ids, string? motif)
        {
            var aSupprimer = _db.Ecritures.Where(e => ids.Contains(e.CleMvt)).ToList();

            foreach (var e in aSupprimer)
            {
                _db.AuditSuppressions.Add(new AuditSuppression
                {
                    NumeroEcriture = e.N_Ecriture,
                    Libelle = e.Libelle_Ecriture,
                    DateSuppression = DateTime.Now,
                    Motif = motif
                });
            }

            _db.Ecritures.RemoveRange(aSupprimer);   // suppression normale via EF Core, plus besoin de SQL brut

            _db.SaveChanges();
        }

        public List<AuditSuppression> GetHistorique()
        {
            return _db.AuditSuppressions.OrderByDescending(a => a.DateSuppression).ToList();
        }
    }
}