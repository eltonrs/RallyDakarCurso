using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.Interfaces
{
  public interface IPilotoRepository
  {
    void Adicionar(Piloto piloto);
    IEnumerable<Piloto> GetAll();
    IEnumerable<Piloto> GetByID(int ID);
  }
}
