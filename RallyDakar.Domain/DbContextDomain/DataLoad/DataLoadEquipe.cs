using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.DbContextDomain.DataLoad
{
  public class DataLoadEquipe
  {
    public static void LoadInitialData(IServiceProvider serviceProvider)
    {
      using (var context = new RallyDakarDbContext(serviceProvider.GetRequiredService<DbContextOptions<RallyDakarDbContext>>()))
      {
        var equipe = new Equipe
        {
          ID = 1,
          CodigoIdentificador = "TMZ",
          Nome = "The MotorZ",
          TemporadaID = 1
        };
        context.Equipes.Add(equipe);

        equipe = new Equipe
        {
          ID = 2,
          CodigoIdentificador = "XYZ",
          Nome = "Abecedário",
          TemporadaID = 1
        };
        context.Equipes.Add(equipe);

        context.SaveChanges();
      }
    }
  }
}
