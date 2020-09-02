using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RallyDakar.Domain.DbContextDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Model;

namespace RallyDakar.API.Controllers
{
  // vai ficar diferente, mais complexa (melhores) com relação à classe PilotoController

  [ApiController]
  [Route("api/equipes/{equipeId}/telemetria")] // para acessar: http://<host:port>/equipes/<id da equipe>/telemetria
  public class TelemetriaController : ControllerBase
  {
    private readonly ITelemetriaRepository _telemetriaRepository;
    private readonly ILogger<TelemetriaController> _logger;
    private readonly IMapper _mapper;

    public TelemetriaController(ITelemetriaRepository telemetriaRepository, IMapper mapper, ILogger<TelemetriaController> logger)
    {
      _telemetriaRepository = telemetriaRepository;
      _mapper = mapper;
      _logger = logger;
    }

    /* Melhoria com base no PilotoController
     * O tipo de retorno do método não é mais uma interface e sim a classe concreta, porém, tipada com o modelo (mais fácil a "leitura")
     * Dessa forma, ferramentas de documentação de APIs, conseguem identificar melhor os métodos.
     */
    [HttpGet]
    public ActionResult<TelemetriaModel> GetById(int equipeId)
    {
      /* CUIDADO!!! o tipo do retorno não é validado, então se colocar, por exemplo, a classe concreta como retorno, irá passar pelo compilador. Exemplo:
       */

      //Telemetria telemetria = new Telemetria();
      //return Ok(telemetria);
      
      if (equipeId <= 0)
        return StatusCode(StatusCodes.Status404NotFound, "ID não localizado.");

      return StatusCode(StatusCodes.Status200OK);
    }
  }
}
