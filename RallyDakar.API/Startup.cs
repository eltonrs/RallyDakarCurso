using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RallyDakar.Domain.DbContextDomain;
using RallyDakar.Domain.Interfaces;
using RallyDakar.Domain.Repositories;

namespace RallyDakar.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    private void MapScopes(IServiceCollection services)
    {
      services.AddScoped<IPilotoRepository, PilotoRepository>();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      /* Leitura:
       * devo adicionar o servi�o abaixo, tipificando o contexto que foi herdado de DbContext.
       * Opcional (no caso do curso): parametrizar o servi�o com a utiliza��o do banco de dados em mem�ria (atrav�s de uma express�o Lambda).
       * 
       * Estou registrando a minha classe (RallyDakarDbContext), que herdade de DBContext, no mecanismo de inve��o de depend�ncia do .NET Core. (n�o � invers�o de controle)
       * 
       * ServiceLifetime.Scope: quando h� uma requisi��o no WEB API, ir� existir apenas uma inst�ncia do RallyDakarDbContext.
       * 
       * Mecanismo interno de inje��o de depend�ncia.
       */
      services.AddDbContext<RallyDakarDbContext>(opt => opt.UseInMemoryDatabase("RallyDakarDB"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

      // adicionado automaticamente (em seguida, adicionar o "AddNewtonsoftJson()")...
      services.AddControllers().AddNewtonsoftJson();
      // se adicionar (services.AddNewtonsoftJson();) aqui embaixo, n�o d� certo, pq ele n�o � um servi�o, � uma "extens�o" feita para trabalhar com o controler.

      /* Leitura:
       * Registrar os mapeamentos entre as interfaces e os reposit�rios
       * 
       * O "IPilotoRepository" est� mapeado para minha classe concreta "PilotoRepository". Permite referenciar a inst�ncia da classe concreta, apenas pela classe da interface.
       * Ou seja, projetos/sistemas externos, podem ter "acesso" � classe concreta apenas implementando/herdando da interface.
       * 
       * Obs.: s� vai funcionar se a classe concreta, implementa/herda da interface correspondente.
       */
      //services.AddScoped<IPilotoRepository, PilotoRepository>();
      MapScopes(services);
      /* Leitura:
       * Ao iniciar a aplica��o, ser�o inciados/configurados os servi�os, dentre eles o AutoMapper.
       * A configura��o abaixo, indica que ser� utilizada a thread atual, pegando os Assemblies (classes compiladas).
       * 
       * Obs.: o pr�ximo passo � injetar o mapper no controller.
       */
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
