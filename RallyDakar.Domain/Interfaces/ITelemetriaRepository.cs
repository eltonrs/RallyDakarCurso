using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.Interfaces
{
  public interface ITelemetriaRepository
  {
    Telemetria GetByID(int id);
    IEnumerable<Telemetria> GetAll();
  }
}
