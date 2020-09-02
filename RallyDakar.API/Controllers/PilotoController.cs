using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Model;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;
using System;
using System.Linq;

namespace RallyDakar.API.Controllers
{
  [ApiController]
  //[Route("api/[controller]")]
  [Route("api/Pilotos")] // URI (identificação do recurso)
  public class PilotoController : ControllerBase
  {
    private readonly IPilotoRepository _pilotoRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PilotoController> _logger;

    /* Leitura: O ASP.Net Core vai passar a instância do PilotoRepository.
     * Utilizando essa instância passada por parâmetro no construtor, eu a utilizo em qualquer método.
     * 
     * O mecanismo interno de injeção de dependência (lá no MapScope do Startup), fará a isso automaticamente.
     * 
     * Também passo o mapper por injeção de dependência
     */
    public PilotoController(IPilotoRepository pilotoRepository, IMapper mapper, ILogger<PilotoController> logger)
    {
      // injetando a instância de PilotoRepository
      _pilotoRepository = pilotoRepository;
      // injetando a instância do Mapper
      _mapper = mapper;

      _logger = logger;
    }

    /* Pensando no mundo real, o método abaixo é inviável, pois trás TODOS os dados da tabelas,
     * além de não ser performático, é perigoso.
     * O ideal é não existir métodos assim (comentar ou até mesmo, exluir do fonte).
     */
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

    /*
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      try
      {
        _logger.LogInformation("Localizando piloto com ID: ", new { id });
        var piloto = _pilotoRepository.GetByID(id);
        if (piloto == null)
        {
          _logger.LogWarning("Piloto não encontrado.");
          return StatusCode(StatusCodes.Status404NotFound, "Piloto não encontrado.");
        }

        _logger.LogWarning("Piloto encontrado.");
        return StatusCode(StatusCodes.Status200OK, piloto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao localizar piloto.");
        
      }
    }
    */

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
        _logger.LogInformation("Localizando piloto com ID: {id}", new[] { id });

        var piloto = _pilotoRepository.GetByID(id);
        if (piloto == null)
          return StatusCode(StatusCodes.Status404NotFound, "Não existe piloto cadastrado!");

        // piloto.Nome += "(Created v1)";

        //_logger.LogInformation("Mapeando de {entidade} para {modelo}.", new[] { new { conteudo = "Piloto" }, new { conteudo = "PilotoModel" } });
        _logger.LogInformation("Mapeando de {entidade} para {modelo}.", "Piloto", "PilotoModel");

        var pilotoModel = _mapper.Map<PilotoModel>(piloto);
        // Não devolver a entidade da classe de negócio
        return StatusCode(StatusCodes.Status200OK, pilotoModel);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());

        // Alternativa 1
        // retornando a mensagem de erro (analisar se pode enviar a mensagem, talvez tratá-la. Retornar uma genérica, por exemplo, "Entrar em contato com o suporte").
        // além disso, "logar" a mensagem, para futuramente fezer uma analisa (nesse pode ser a mensagem na integra).
        //return BadRequest(ex.ToString());

        // Alternativa 2 (mais elegante)
        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    [HttpPost]
    //public IActionResult AddPiloto([FromBody] Piloto piloto) // Má prática: não expor a classe (lá do dominio) aqui no controller
    public IActionResult AddPiloto([FromBody] PilotoModel pilotoModel)// Boa prática: usar Model. É uma forma de "esconder" e melhorar isso, é utilizando padrão de projeto MVC (no caso, o M).
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
        _logger.LogInformation("Mapeando piloto modelo.");
        /* Leitura:
         * Mapeando a classe "PilotoModel" (origem) para a classe "Piloto" (destino)
         */
        var piloto = _mapper.Map<Piloto>(pilotoModel);

        _logger.LogInformation($"Verificando se existe piloto com ID { piloto.ID }.");
        if (_pilotoRepository.ExistByID(piloto.ID))
        {
          _logger.LogWarning($"Já existe um piloto com ID { piloto.ID }.");
          return StatusCode(StatusCodes.Status406NotAcceptable, "Já existe piloto com esse ID!");
        }

        // forçando um erro para ver o conteúdo do log com Exception
        //int numero = int.Parse("B");

        _logger.LogInformation("Adicionando piloto...");
        _logger.LogInformation($"ID: { piloto.ID }.");
        _logger.LogInformation($"Nome: { piloto.Nome }.");
        _logger.LogInformation($"Sobrenome: { piloto.Sobrenome }.");
        _pilotoRepository.Add(piloto);
        _logger.LogInformation("Piloto adicionado com sucesso.");
        // Simplesmente retorno um Ok...
        //return StatusCode(StatusCodes.Status201Created, "Piloto adicionado.");

        // Mas posso utilizar um esquema para, além de informar que Ok(created), mostrar a "rota" onde está esse novo recurso. Tenho que criar a rota.
        /* Leitura:
         * Os parâmetros são:
         * 1 - O nome da rota (o CreateAtRoute vai apontar para essa rota). Essa nova rota é um método aqui no controller mesmo, (HttpGet)
         * 2 - Um objeto anônimo (com ID do piloto adicionado... essa propriedado do obejto anônimo, tem que ser exatamente o mesmo no método que oou criar)
         * 3 - Objeto do Piloto adicionado
         */

        //piloto.Nome += "(Created v2)"; // testes
        
        _logger.LogInformation("Mapeando o retorno.");
        // Para não passar o objeto da instância da classe do repositório novamente, altero para a classe model...
        // return CreatedAtRoute("GetCreated", new { id = piloto.ID }, piloto);
        var pilotoModelResponse = _mapper.Map<PilotoModel>(piloto);

        _logger.LogInformation("Chamando a rota 'GetCreated'.");
        return CreatedAtRoute("GetCreated", new { id = piloto.ID }, pilotoModelResponse);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    [HttpPut]
    public IActionResult UpdateFullPiloto([FromBody] PilotoModel pilotoModel)
    {
      try
      {
        if (!_pilotoRepository.ExistByID(pilotoModel.ID))
          return StatusCode(StatusCodes.Status404NotFound, "Piloto não encontrado.");

        var piloto = _mapper.Map<Piloto>(pilotoModel);
        _pilotoRepository.UpdateFull(piloto);

        return StatusCode(StatusCodes.Status204NoContent, "Atualização completa realizada com sucesso.");
      }
      catch (Exception)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!!");
      }
    }

    [HttpPatch("{ID}")]
    public IActionResult UpdatePartialPiloto(int ID, [FromBody] JsonPatchDocument<PilotoModel> patchPilotoModel)
    {
      try
      {
        if (!_pilotoRepository.ExistByID(ID))
          return StatusCode(StatusCodes.Status404NotFound, "Piloto não encontrado.");

        /* Erro 1: relacionado ao erro da instância já monitorada (tracked):
         * Foi nesse ponto que o EF gerou uma instância
         */
        var piloto = _pilotoRepository.GetByID(ID);

        var pilotoModel = _mapper.Map<PilotoModel>(piloto);

        patchPilotoModel.ApplyTo(pilotoModel);

        /* Erro 1: o AutoMapper gera outra instância e essa que é passada para atualização, para corrigir
         * basta:
         */
        //var pilotoAtualizado = _mapper.Map<Piloto>(pilotoModel);
        piloto = _mapper.Map(pilotoModel, piloto); // dessa forma, o AutoMapper utiliza a mesma instância gerada anteriormente e que já está sendo monitorada.

        _pilotoRepository.UpdatePartial(piloto);

        return StatusCode(StatusCodes.Status204NoContent, "Atualização parcial realizada com sucesso.");
      }
      catch (Exception ex)
      {

        return StatusCode(StatusCodes.Status500InternalServerError, "Erro. Entrar em contato com o suporte!!! Mensagem: " + ex.Message);
      }
    }

    [HttpDelete("{ID}")]
    public IActionResult DeleteByID(int ID) // não precisa utlizar o AutoMapper aqui, pois o parâmetro é apenas o ID e o objeto encontrado (classe do repositório) é passado para o método interno.
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
    
    /* Pensando em "ir" para o Nível 3 do HATEAOS, mas ainda não é de fato:
    // */
    [HttpOptions]
    public IActionResult ListarOperacoesPermitidas()
    {
      // Isso mostra aos "consumidores da API", quais dos recursos habilitados. Os consumidores devem ser essa informação.
      Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH and OPTIONS");
      return Ok();
    }
  }
}
