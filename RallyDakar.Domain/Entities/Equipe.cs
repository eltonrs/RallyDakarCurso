using System.Collections.Generic;
using System.Linq;

namespace RallyDakar.Domain.Entities
{
  public class Equipe
  {
    public int ID { get; set; }
    public string CodigoIdentificador { get; set; }
    public string Nome { get; set; }

    // Referências (Entity Framework)

    // Referência à temporada (lá tem o relacionamento)
    public int TemporadaID { get; set; } // essa "referência" é o obrigatória
    /* A propriedade abaixo, indica (ao Entity Framework) para carregar o objeto Temporada com uma instância de Temporada.
     * Isso de forma automática.
     * CUDIADO: Pode onerar a memória, pois, é se a tabela for muito grande, irá carregar tudo. Verificar a possibilidade de carregar colunas/linhas manualmente sobre demanda. */
    public virtual Temporada Temporada { get; set; }
    public ICollection<Piloto> Pilotos { get; set; }

    // Comportamento

    public Equipe()
    {
      Pilotos = new List<Piloto>();
    }

    private bool ValidarPodeAdicionarPiloto(Piloto piloto)
    {
      return (piloto != null && piloto.Validar());
    }

    public bool ValidarPropriedades()
    {
      return (!string.IsNullOrEmpty(Nome));
    }

    public void AddPiloto(Piloto piloto)
    {
      if (ValidarPodeAdicionarPiloto(piloto))
        Pilotos.Add(piloto);
    }

    public Piloto GetPilotoByID(int id)
    {
      return Pilotos.FirstOrDefault(p => p.ID == id);
    }
  }
}
