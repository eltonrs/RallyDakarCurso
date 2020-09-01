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
       * devo adicionar o serviço abaixo, tipificando o contexto que foi herdado de DbContext.
       * Opcional (no caso do curso): parametrizar o serviço com a utilização do banco de dados em memória (através de uma expressão Lambda).
       * 
       * Estou registrando a minha classe (RallyDakarDbContext), que herdade de DBContext, no mecanismo de inveção de dependência do .NET Core. (não é inversão de controle)
       * 
       * ServiceLifetime.Scope: quando há uma requisição no WEB API, irá existir apenas uma instância do RallyDakarDbContext.
       * 
       * Mecanismo interno de injeção de dependência.
       */
      services.AddDbContext<RallyDakarDbContext>(opt => opt.UseInMemoryDatabase("RallyDakarDB"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

      // adicionado automaticamente (em seguida, adicionar o "AddNewtonsoftJson()")...
      services.AddControllers().AddNewtonsoftJson();
      // se adicionar (services.AddNewtonsoftJson();) aqui embaixo, não dá certo, pq ele não é um serviço, é uma "extensão" feita para trabalhar com o controler.

      /* Leitura:
       * Registrar os mapeamentos entre as interfaces e os repositórios
       * 
       * O "IPilotoRepository" está mapeado para minha classe concreta "PilotoRepository". Permite referenciar a instância da classe concreta, apenas pela classe da interface.
       * Ou seja, projetos/sistemas externos, podem ter "acesso" à classe concreta apenas implementando/herdando da interface.
       * 
       * Obs.: só vai funcionar se a classe concreta, implementa/herda da interface correspondente.
       */
      //services.AddScoped<IPilotoRepository, PilotoRepository>();
      MapScopes(services);
      /* Leitura:
       * Ao iniciar a aplicação, serão inciados/configurados os serviços, dentre eles o AutoMapper.
       * A configuração abaixo, indica que será utilizada a thread atual, pegando os Assemblies (classes compiladas).
       * 
       * Obs.: o próximo passo é injetar o mapper no controller.
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
