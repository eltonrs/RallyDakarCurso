using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.Interfaces
{
  public interface IEquipeRepository
  {
    Equipe GetByID(int id);
    bool ExistByID(int id);
    void Add(Equipe equipe);
    void UpdateFull(Equipe equipe);
    void UpdatePartial(Equipe equipe);
    void Delete(Equipe equipe);
  }
}
