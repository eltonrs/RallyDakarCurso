using RallyDakar.Domain.DbContextDomain;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Domain.Repositories
{
  public class TemporadaRepository : ITemporadaRepository
  {
    private readonly RallyDakarDbContext _dbContext;

    public TemporadaRepository(RallyDakarDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Add(Temporada temporada)
    {
      _dbContext.Temporadas.Add(temporada);
    }

    public void Delete(Temporada temporada)
    {
      _dbContext.Temporadas.Remove(temporada);
    }

    public bool ExistByID(int id)
    {
      return _dbContext.Temporadas.Any(t => t.ID == id);
    }

    public Temporada GetByID(int id)
    {
      return _dbContext.Temporadas.FirstOrDefault(t => t.ID == id);
    }

    public void UpdateFull(Temporada temporada)
    {
      if (_dbContext.Entry(temporada).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
      {
        _dbContext.Attach(temporada);
        _dbContext.Entry(temporada).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      }
      else
        _dbContext.Update(temporada);

      _dbContext.SaveChanges();
    }

    public void UpdatePartial(Temporada temporada)
    {
      UpdateFull(temporada);
    }
  }
}
