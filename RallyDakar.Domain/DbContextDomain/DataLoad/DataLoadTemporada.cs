using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.DbContextDomain.DataLoad
{
  public class DataLoadTemporada
  {
    public static void LoadInitialData(IServiceProvider serviceProvider)
    {
      using (var context = new RallyDakarDbContext(serviceProvider.GetRequiredService<DbContextOptions<RallyDakarDbContext>>()))
      {
        var temporada = new Temporada
        {
          ID = 1,
          Nome = "Temporada 2020",
          DataInicio = DateTime.Now,
          DataFim = null
        };
      }
    }
  }
}
