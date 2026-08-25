using EcritureComptable.Models;
using EcrituresApi.Models;
using EcrituresApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcritureComptable.Controllers
{
    // Controller "pur" : délègue uniquement au Service, aucune logique métier ici.
    [ApiController]
    [Route("api/Ecritures")]
    public class EcrituresController : ControllerBase
    {
        private readonly EcritureService _service;

        public EcrituresController(EcritureService service)
        {
            _service = service;
        }

        [HttpGet("journaux")]
        public IActionResult GetJournaux()
        {
            return Ok(_service.GetJournaux());
        }

        [HttpGet("comptes")]
        public IActionResult GetComptesComptables()
        {
            return Ok(_service.GetComptesComptables());
        }

        [HttpGet("comptes-historique")]
        public IActionResult GetComptesComptablesHistorique()
        {
            return Ok(_service.GetComptesComptablesHistorique());
        }

        [HttpGet("kpis")]
        public IActionResult GetKpis()
        {
            var result = _service.GetKpis();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] string? libelle,
            [FromQuery] string? journal,
            [FromQuery] DateTime? datedebut,
            [FromQuery] DateTime? datefin,
            [FromQuery] string? comptecomptable,
            [FromQuery] string? refpiece,
            [FromQuery] string? devise,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = _service.GetAll(libelle, journal, datedebut, datefin, comptecomptable, refpiece, devise, page, pageSize);
            return Ok(result);
        }

        [HttpGet("historique")]
        public IActionResult GetHistorique(
            [FromQuery] string? libelle,
            [FromQuery] string? journal,
            [FromQuery] DateTime? datedebut,
            [FromQuery] DateTime? datefin,
            [FromQuery] string? comptecomptable,
            [FromQuery] string? refpiece,
            [FromQuery] string? devise,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = _service.GetHistorique(libelle, journal, datedebut, datefin, comptecomptable, refpiece, devise, page, pageSize);
            return Ok(result);
        }

        [HttpGet("ids")]
        public IActionResult GetAllIds(
            [FromQuery] string? libelle,
            [FromQuery] string? journal,
            [FromQuery] DateTime? datedebut,
            [FromQuery] DateTime? datefin,
            [FromQuery] string? comptecomptable,
            [FromQuery] string? refpiece,
            [FromQuery] string? devise)
        {
            var result = _service.GetAllIds(libelle, journal, datedebut, datefin, comptecomptable, refpiece, devise);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult SupprimerEcritures([FromBody] SuppressionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Motif))
                return BadRequest("Le motif est obligatoire.");

            _service.SupprimerEcritures(request.Ids, request.Motif);
            return Ok();
        }
    }
}

