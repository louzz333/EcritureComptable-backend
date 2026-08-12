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

        public HistoriquePagineeResult GetHistorique(
            string? libelle = null,
            string? journal = null,
            DateTime? datedebut = null,
            DateTime? datefin = null,
            string? comptecomptable = null,
            string? refpiece = null,
            string? devise = null,
            int page = 1,
            int pageSize = 10)
        {
            var query = _db.AuditSuppressions.AsQueryable();

            if (!string.IsNullOrEmpty(libelle))
                query = query.Where(a => a.Libelle.Contains(libelle));

            if (!string.IsNullOrEmpty(journal))
                query = query.Where(a => a.JournalEcriture == journal);

            if (datedebut.HasValue)
                query = query.Where(a => a.DateEcriture >= datedebut.Value);

            if (datefin.HasValue)
                query = query.Where(a => a.DateEcriture <= datefin.Value);

            if (!string.IsNullOrEmpty(comptecomptable))
                query = query.Where(a => a.CompteEcriture == comptecomptable);

            if (!string.IsNullOrEmpty(refpiece))
                query = query.Where(a => a.ReferenceEcriture.Contains(refpiece));

            if (!string.IsNullOrEmpty(devise))
                query = query.Where(a => a.DeviseEcriture == devise);

            query = query.OrderByDescending(a => a.DateSuppression);

            int total = query.Count();

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new HistoriquePagineeResult
            {
                Items = items,
                TotalCount = total
            };
        }

        public List<decimal> GetAllIds(string? libelle = null, string? journal = null, DateTime? datedebut = null, DateTime? datefin = null, string? comptecomptable = null, string? refpiece = null, string? devise = null)
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

            return query.Select(e => e.CleMvt).ToList();
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

        public List<string> GetComptesComptables()
        {
            return _db.Ecritures
                .Select(e => e.Compte_comptable)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        public List<string> GetComptesComptablesHistorique()
        {
            return _db.AuditSuppressions
                .Where(a => a.CompteEcriture != null)
                .Select(a => a.CompteEcriture!)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        public object GetKpis()
        {
            var totalDebit = _db.Ecritures.Where(e => e.Sens == "D").Sum(e => e.Montant);
            var totalCredit = _db.Ecritures.Where(e => e.Sens == "C").Sum(e => e.Montant);

            var maintenant = DateTime.Now;
            var supprimeesCeMois = _db.AuditSuppressions
                .Where(a => a.DateSuppression.Month == maintenant.Month && a.DateSuppression.Year == maintenant.Year)
                .Count();
            var enAttente = _db.Ecritures.Count(e => e.EtatComptabilisation == 0);

            return new
            {
                TotalDebit = totalDebit,
                TotalCredit = totalCredit,
                EnAttente = enAttente,
                SupprimeesCeMois = supprimeesCeMois
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
                    DateEcriture = e.Date,
                    JournalEcriture = e.Journal,
                    CompteEcriture = e.Compte_comptable,
                    MontantEcriture = e.Montant,
                    SensEcriture = e.Sens,
                    ReferenceEcriture = e.Reference_Piece,
                    DeviseEcriture = e.DEVISE,
                    Libelle = e.Libelle_Ecriture,
                    DateSuppression = DateTime.Now,
                    Motif = motif
                });
            }

            _db.Ecritures.RemoveRange(aSupprimer);   // suppression normale via EF Core, plus besoin de SQL brut

            _db.SaveChanges();
        }
    }
}