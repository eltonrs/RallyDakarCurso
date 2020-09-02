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
    public IEnumerable<Telemetria> GetAll()
    {
      return _dbContext.Telemetrias.ToList();
    }

    public Telemetria GetByID(int id)
    {
      return _dbContext.Telemetrias.FirstOrDefault(t => t.ID == id);
    }
  }
}
