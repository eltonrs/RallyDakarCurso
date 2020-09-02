using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using RallyDakar.Domain.DbContextDomain.DataLoad;

namespace RallyDakar.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLogBuilder
        .ConfigureNLog("nlog.config")
        .GetCurrentClassLogger();

      logger.Info("Iniciando a aplica��o.");

      try
      {
        /* Forma padr�o:
        CreateHostBuilder(args)
          .Build() // cria um servi�o "in process" antes de colocar no IIS.
          .Run();  // Executa a aplica��o.
        
         * Modificando para atender o DataLoad:
         */

        var host = CreateHostBuilder(args).Build();

        /* Leitura:
         * Criando um escopo com base no host (em mem�ria)
         * 
         * Sobre o "using": dessa forma garante que tudo est� sendo criado dentro de { } ser� descartado no final ( } ), no caso, apenas o "scope". S� funciona em classes que implementado IDisposible
         */
        using (var scope = host.Services.CreateScope())
        {
          var services = scope.ServiceProvider;

          DataLoadTemporada.LoadInitialData(services);
          DataLoadPiloto.LoadInitialData(services);
          DataLoadEquipe.LoadInitialData(services);
        }

        host.Run();
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Aplica��o n�o foi iniciada.");
      }
      finally
      {
        // se deu problema ou n�o, ao finalizar a aplica��o, o log tem que ser finalizado.
        NLog.LogManager.Shutdown();
      }
      
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder
                .UseStartup<Startup>()
                .UseNLog();
            });
  }
}
