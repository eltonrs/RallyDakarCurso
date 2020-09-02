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
    private readonly IEquipeRepository _equipeRepository;
    private readonly ILogger<TelemetriaController> _logger;
    private readonly IMapper _mapper;

    public TelemetriaController(ITelemetriaRepository telemetriaRepository, IEquipeRepository equipeRepository, IMapper mapper, ILogger<TelemetriaController> logger)
    {
      _telemetriaRepository = telemetriaRepository;
      _equipeRepository = equipeRepository;
      _mapper = mapper;
      _logger = logger;
    }

    /* Melhoria com base no PilotoController
     * O tipo de retorno do método não é mais uma interface e sim a classe concreta, porém, tipada com o modelo (mais fácil a "leitura")
     * Dessa forma, ferramentas de documentação de APIs, conseguem identificar melhor os métodos.
     * 
     * Leitura do método:
     * Como são retornados vários registros de telemetria por equipe...
     */
    [HttpGet]
    public ActionResult<IEnumerable<TelemetriaModel>> GetByEquipe(int equipeId)
    {
      /* CUIDADO!!! o tipo do retorno não é validado, então se colocar, por exemplo, a classe concreta como retorno, irá passar pelo compilador. Exemplo:
       */

      //Telemetria telemetria = new Telemetria();
      //return Ok(telemetria);

      try
      {
        _logger.LogInformation("Iniciando método: GetById");

        if (!_equipeRepository.ExistByID(equipeId))
        {
          _logger.LogInformation($"Equipe com ID { equipeId } não localizada.");
          return StatusCode(StatusCodes.Status404NotFound, $"Equipe com ID { equipeId } não localizada.");
        }

        var listaTelemetria = _telemetriaRepository.GetAllByEquipe(equipeId);

        if (!listaTelemetria.Any())
          return StatusCode(StatusCodes.Status404NotFound, $"Nenhuma telemetria foi localizada para a equipe com ID { equipeId }.");

        /* Leitura:
         * A conversão da entidade para o modelo, nesse caso, é em lista, então é possível fazer isso...
         */
        var dadosTelemetriaModel =_mapper.Map<IEnumerable<TelemetriaModel>>(listaTelemetria);

        return StatusCode(StatusCodes.Status200OK, dadosTelemetriaModel);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Erro: { ex }");

        return StatusCode(StatusCodes.Status500InternalServerError, "Entrar em contato com o suporte!");
      }
      finally
      {
        _logger.LogInformation($"Término do método: GetById");
      }
    }
  }
}
