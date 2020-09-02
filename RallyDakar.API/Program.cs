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

      logger.Info("Iniciando a aplicação.");

      try
      {
        /* Forma padrão:
        CreateHostBuilder(args)
          .Build() // cria um serviço "in process" antes de colocar no IIS.
          .Run();  // Executa a aplicação.
        
         * Modificando para atender o DataLoad:
         */

        var host = CreateHostBuilder(args).Build();

        /* Leitura:
         * Criando um escopo com base no host (em memória)
         * 
         * Sobre o "using": dessa forma garante que tudo está sendo criado dentro de { } será descartado no final ( } ), no caso, apenas o "scope". Só funciona em classes que implementado IDisposible
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
        logger.Error(ex, "Aplicação não foi iniciada.");
      }
      finally
      {
        // se deu problema ou não, ao finalizar a aplicação, o log tem que ser finalizado.
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
