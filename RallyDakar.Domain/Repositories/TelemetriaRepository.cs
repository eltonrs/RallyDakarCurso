using RallyDakar.Domain.DbContextDomain;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Domain.Repositories
{
  public class TelemetriaRepository : ITelemetriaRepository
  {
    private readonly RallyDakarDbContext _dbContext;

    public TelemetriaRepository(RallyDakarDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Add(Telemetria telemetria)
    {
      _dbContext.Telemetrias.Add(telemetria);
      _dbContext.SaveChanges();
    }

    public void Delete(Telemetria telemetria)
    {
      _dbContext.Remove(telemetria);
    }

    public bool ExistByID(int id)
    {
      return _dbContext.Telemetrias.Any(t => t.ID == id);
    }

    public IEnumerable<Telemetria> GetAll()
    {
      return _dbContext.Telemetrias.ToList();
    }

    public Telemetria GetByID(int id)
    {
      return _dbContext.Telemetrias.FirstOrDefault(t => t.ID == id);
    }

    public void Update(Telemetria telemetria)
    {
      if (_dbContext.Entry(telemetria).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
      {
        _dbContext.Attach(telemetria);
        _dbContext.Entry(telemetria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      }
      else
        _dbContext.Update(telemetria);

      _dbContext.SaveChanges();
    }
  }
}
