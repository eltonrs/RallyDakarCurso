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
    public virtual ICollection<Equipe> Equipes { get; set; } // Collection tipada por "Equipe" (class). Com o "virtual" o Entity Framework altera a instância desse objeto com a instância de um objeto que contém os registros do banco de dados.

    public Temporada()
    {
      // Lendo: lista tipada de Equipe. List, pq é uma herança de ICollection (pra ver, só dar F12, o VS vai mostrar os metadados da classe.
      Equipes = new List<Equipe>();
    }

    // Comportamentos (métodos). Sem esses, as classes são chamadas de anêmicas.

    public void AdicionarEquipe(Equipe equipe)
    {
      if ((equipe != null) && (!string.IsNullOrEmpty(equipe.Nome)))
        Equipes.Add(equipe);
    }
  }
}
