using EcritureComptable.Controllers;
using EcrituresApi.Models;
using EcrituresApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EcritureComptable.Models;


namespace EcritureComptable.Controllers
{
    [ApiController]
    [Route("api/Ecritures")]
    public class EcrituresController : ControllerBase
    {
        private readonly EcritureService _service;
        public EcrituresController(EcritureService service)//mon constructeur
        {
            _service = service;
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
            [FromQuery] int pageSize=10)  //lit directement d'apres url apres lecture de ?
        {
            
            var result = _service.GetAll(libelle,journal,datedebut,datefin, comptecomptable,refpiece,devise,page,pageSize);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult SupprimerEcritures([FromBody] SuppressionRequest request)
        {
            _service.SupprimerEcritures(request.Ids, request.Motif);
            return Ok();
        }

        [HttpGet("historique")]
        public IActionResult GetHistorique()
        {
            return Ok(_service.GetHistorique());
        }
    }
}

