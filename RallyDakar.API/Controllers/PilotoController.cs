using Microsoft.AspNetCore.Mvc;
using RallyDakar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PilotoController : ControllerBase
  {
    IPilotoRepository _pilotoRepository;

    /* Leitura: O ASP.Net Core vai passar a instância do PilotoRepository.
     * Utilizando essa instância passada por parâmetro no construtor, eu a utilizo em qualquer método.
     * 
     * O mecanismo interno de injeção de dependência (lá no MapScope do Startup), fará a isso automaticamente.
     */
    public PilotoController(IPilotoRepository pilotoRepository)
    {
      _pilotoRepository = pilotoRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      return Ok("Retornou todos.");
    }
  }
}
