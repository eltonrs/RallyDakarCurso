using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.Interfaces
{
  public interface ITemporadaRepository
  {
    Temporada GetByID(int id);
    bool ExistByID(int id);
    void Add(Temporada temporada);
    void UpdateFull(Temporada temporada);
    void UpdatePartial(Temporada temporada);
    void Delete(Temporada temporada);
  }
}
