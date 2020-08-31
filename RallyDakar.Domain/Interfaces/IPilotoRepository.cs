using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.Interfaces
{
  public interface IPilotoRepository
  {
    void Add(Piloto piloto);
    IEnumerable<Piloto> GetAll();
    Piloto GetByID(int pilotoID);
    bool ExistByID(int pilotoID);
    void UpdateFull(Piloto piloto);
    void UpdatePartial(Piloto piloto);
    void Delete(Piloto piloto);
  }
}
