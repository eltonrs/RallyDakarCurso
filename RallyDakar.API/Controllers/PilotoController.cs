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
      /*List<Piloto> pilotos = new List<Piloto>();

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

      return Ok(pilotos); // indica o "Status Code" do HTTP (OK = 200). */

      // Teste 4: 404 (Not found)
      /*var pilotos = _pilotoRepository.GetAll();
      if (!pilotos.Any())
        return NotFound();
      */

      // Teste 5: outros problemas (tratamento de erros)
      try
      {
        var pilotos = _pilotoRepository.GetAll();
        if (!pilotos.Any())
          return StatusCode(StatusCodes.Status404NotFound, "Não existe pilotos cadastrados!");

        //return Ok();
        // mais elegante:
        return StatusCode(StatusCodes.Status200OK, pilotos);
      }
      catch (Exception ex)
      {
        // Alternativa 1
        // retornando a mensagem de erro (analisar se pode enviar a mensagem, talvez tratá-la. Retornar uma genérica, por exemplo, "Entrar em contato com o suporte").
        // além disso, "logar" a mensagem, para futuramente fezer uma analisa (nesse pode ser a mensagem na integra).
        //return BadRequest(ex.ToString());

        // Alternativa 2 (mais elegante)
        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    /* Configurando a rota (para o Exemplo 2 do AddPiloto):
     * 1 - Adicionar o parâmetro que foi utilizado
     * 2 - Especificando o nome "personalizado" da rota (o mesmo passado no CreatedAtRoute)
     *   2.1 - O nome do método não precisa coincidir com o nome da rota
     */
    [HttpGet("{id}", Name = "GetCreated")]
    public IActionResult GetCreated(int id) // esse parâmetro, tem que ser exatamente
    {
      try
      {
        var piloto = _pilotoRepository.GetByID(id);
        if (piloto == null)
          return StatusCode(StatusCodes.Status404NotFound, "Não existe piloto cadastrado!");

        piloto.Nome += "(Created v1)";


        return StatusCode(StatusCodes.Status200OK, piloto);
      }
      catch (Exception ex)
      {
        // Alternativa 1
        // retornando a mensagem de erro (analisar se pode enviar a mensagem, talvez tratá-la. Retornar uma genérica, por exemplo, "Entrar em contato com o suporte").
        // além disso, "logar" a mensagem, para futuramente fezer uma analisa (nesse pode ser a mensagem na integra).
        //return BadRequest(ex.ToString());

        // Alternativa 2 (mais elegante)
        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    [HttpPost]
    public IActionResult AddPiloto([FromBody]Piloto piloto)
    {
      /* Exemplo 1:
       * Adiciono o piloto e retorno um ok, apenas.
       * Na parametrização: informo ao .NET Core que os dados do piloto, veem do "corpo" do HTML. No formato JSON.
       * 
       * Não é uma boa práica colocar a entidade direto aqui na requisição. O certo é utilizar o AutoMapper.
       */
      //_pilotoRepository.Add(piloto);
      //return Ok();

      /* Exemplo 2: mais elaborado
       */
      try
      {
        if (_pilotoRepository.ExistByID(piloto.ID))
          return StatusCode(StatusCodes.Status406NotAcceptable, "Já existe piloto com esse ID!");

        _pilotoRepository.Add(piloto);
        // Simplesmente retorno um Ok...
        //return StatusCode(StatusCodes.Status201Created, "Piloto adicionado.");

        // Mas posso utilizar um esquema para, além de informar que Ok(created), mostrar a "rota" onde está esse novo recurso. Tenho que criar a rota.
        /* Leitura:
         * Os parâmetros são:
         * 1 - O nome da rota (o CreateAtRoute vai apontar para essa rota). Essa nova rota é um método aqui no controller mesmo, (HttpGet)
         * 2 - Um objeto anônimo (com ID do piloto adicionado... essa propriedado do obejto anônimo, tem que ser exatamente o mesmo no método que oou criar)
         * 3 - objeto do Piloto adicionado
         */

        piloto.Nome += "(Created v2)";

        return CreatedAtRoute("GetCreated", new { id = piloto.ID }, piloto);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    [HttpPut]
    public IActionResult UpdateFullPiloto([FromBody] Piloto piloto)
    {
      try
      {
        if (!_pilotoRepository.ExistByID(piloto.ID))
          return StatusCode(StatusCodes.Status404NotFound, "Piloto não encontrado.");

        _pilotoRepository.UpdateFull(piloto);

        return StatusCode(StatusCodes.Status204NoContent, "Atualização completa realizada com sucesso.");
      }
      catch (Exception)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    [HttpPatch]
    public IActionResult UpdatePartialPiloto([FromBody] Piloto piloto)
    {
      /* teste
       */
      try
      {

        return StatusCode(StatusCodes.Status200OK, "Atualização parcial realizada com sucesso.");
      }
      catch (Exception)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    [HttpDelete("{ID}")]
    public IActionResult DeleteByID(int ID)
    {
      try
      {
        /* O EF possui um mecanismo de cache, então pode ser que o piloto que está sendo manipulado,
         * já esteja em memória, assim, posso fazer melhor:
         */
        // ANTES:
        //if (!_pilotoRepository.ExistByID(ID))
        //  return StatusCode(StatusCodes.Status404NotFound, "Piloto não encontrado.");
        //DEPOIS:
        Piloto piloto = _pilotoRepository.GetByID(ID);
        if (piloto == null)
          return StatusCode(StatusCodes.Status404NotFound, "Piloto não encontrado.");

        _pilotoRepository.Delete(piloto);

        return StatusCode(StatusCodes.Status204NoContent, "Exclusão realizada com sucesso.");
      }
      catch (Exception)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }
  }
}
