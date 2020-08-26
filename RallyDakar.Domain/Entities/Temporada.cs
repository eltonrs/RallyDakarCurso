using System;
using System.Collections.Generic;

namespace RallyDakar.Domain.Entities
{
  public class Temporada
  {
    public int ID { get; set; }
    public string Nome { get; set; }
    public DateTime DataInicio { get; set; }
    // o "?" na frente do tipo, indica que o campo/propriedade é nullable A(aceita nulos)
    public DateTime? DataFim { get; set; }

    // Relacionamentos (Entity Framework)

    // Uma temporada, tem várias equipes...
    public ICollection<Equipe> Equipes { get; set; } // Collection tipada por "Equipe" (class)

    // Comportamentos (métodos). Sem esses, as classes são chamadas de anêmicas.

    public void AdicionarEquipe(Equipe equipe)
    {
      if ((equipe != null) && (!string.IsNullOrEmpty(equipe.Nome)) && (!string.IsNullOrEmpty(equipe.CodigoIdentificador)))
        Equipes.Add(equipe);
    }
  }
}
