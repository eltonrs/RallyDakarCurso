using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
  [ApiController]
  //[Route("api/[controller]")]
  [Route("api/pilotos")]
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
      // injetando a instância de PilotoRepository
      _pilotoRepository = pilotoRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      // Teste 1: retorno simples só para ver o conteúdo no browser
      //return Ok("Retornou todos.");

      // Teste 2: comunicação com o Postman
      //return Ok(_pilotoRepository.GetAll());

      // Teste 3: visualizando o conteúdo (JSON) da listagem de pilotos no Postaman
      List<Piloto> pilotos = new List<Piloto>();

      var piloto = new Piloto()
      {
        ID = 1,
        Nome = "Sangue frio"
      };
      pilotos.Add(piloto);

      piloto = new Piloto()
      {
        ID = 2,
        Nome = "Hot Wheels"
      };
      pilotos.Add(piloto);

      return Ok(pilotos); // indica o "Status Code" do HTTP (OK = 200).
    }

    [HttpPost]
    public IActionResult AddPiloto([FromBody]Piloto piloto)
    {
      /* Leitura:
       * Adiciono o piloto e retorno um ok, apenas.
       * Na parametrização: informo ao .NET Core que os dados do piloto, veem do "corpo" do HTML. No formato JSON.
       * 
       * Não é uma boa práica colocar a entidade direto aqui na requisição. O certo é utilizar o AutoMapper.
       */
      _pilotoRepository.Adicionar(piloto);
      return Ok();
    }

    [HttpPut]
    public IActionResult UpdateFullPiloto([FromBody] Piloto piloto)
    {
      return Ok();
    }

    [HttpPatch]
    public IActionResult UpdatePartialPiloto([FromBody] Piloto piloto)
    {
      return Ok();
    }

    [HttpDelete("{ID}")]
    public IActionResult UpdatePartialPiloto(int ID)
    {
      return Ok();
    }
  }
}
