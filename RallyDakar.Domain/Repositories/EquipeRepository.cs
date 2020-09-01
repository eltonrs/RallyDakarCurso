using RallyDakar.Domain.DbContextDomain;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Domain.Repositories
{
  public class EquipeRepository : IEquipeRepository
  {
    private readonly RallyDakarDbContext _dbContext;

    public EquipeRepository(RallyDakarDbContext context)
    {
      _dbContext = context;
    }

    public void Add(Equipe equipe)
    {
      _dbContext.Equipes.Add(equipe);
    }

    public void Delete(Equipe equipe)
    {
      _dbContext.Equipes.Remove(equipe);
    }

    public bool ExistByID(int id)
    {
      return _dbContext.Equipes.Any(e => e.ID == id);
    }

    public Equipe GetByID(int id)
    {
      return _dbContext.Equipes.FirstOrDefault(e => e.ID == id);
    }

    public void UpdateFull(Equipe equipe)
    {
      if (_dbContext.Entry(equipe).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
      {
        _dbContext.Attach(equipe);
        _dbContext.Entry(equipe).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      }
      else
        _dbContext.Equipes.Update(equipe);

      _dbContext.SaveChanges();
    }

    public void UpdatePartial(Equipe equipe)
    {
      UpdateFull(equipe);
    }
  }
}
