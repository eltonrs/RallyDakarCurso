using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.DbContextDomain.DataLoad
{
  public class DataLoadPiloto
  {
    public static void LoadInitialData(IServiceProvider serviceProvider)
    {
      using (var context = new RallyDakarDbContext(serviceProvider.GetRequiredService<DbContextOptions<RallyDakarDbContext>>()))
      {
        
        
        var piloto = new Piloto
        {
          ID = 1,
          Nome = "Elton",
          Sobrenome = "Souza",
          NumeroUnico = 123,
          EquipeID = 1
        };

        // Adiconando no dataset (DbSet) de pilotos a entidade piloto.
        context.Pilotos.Add(piloto);

        piloto = new Piloto
        {
          ID = 2,
          Nome = "Lívia",
          Sobrenome = "Souza",
          NumeroUnico = 456,
          EquipeID = 1
        };
        context.Pilotos.Add(piloto);

        piloto = new Piloto
        {
          ID = 3,
          Nome = "Patrícia",
          Sobrenome = "Souza",
          NumeroUnico = 789,
          EquipeID = 2
        };
        context.Pilotos.Add(piloto);

        context.SaveChanges();
      }
    }
  }
}
