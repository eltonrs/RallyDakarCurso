using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
      
      // adiconado automaticamente
      services.AddControllers();

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