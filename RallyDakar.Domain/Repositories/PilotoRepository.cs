using RallyDakar.Domain.DbContextDomain;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Domain.Repositories
{
  public class PilotoRepository : IPilotoRepository
  {
    private readonly RallyDakarDbContext _rallyDakarDbContext;

    public PilotoRepository(RallyDakarDbContext rallyDakarDbContext)
    {
      _rallyDakarDbContext = rallyDakarDbContext;
    }

    public void Adicionar(Piloto piloto)
    {
      _rallyDakarDbContext.Pilotos.Add(piloto); // nesse ponto, o EF gera os scripts de banco de dados para inserir os dados.
      _rallyDakarDbContext.SaveChanges(); // replica os dados para a base de dados.
    }

    public IEnumerable<Piloto> GetAll()
    {
      /* Leitura: o EF só faz a consulta no banco de dados, no caso, na tabela Pilotos, quando eu invoco algum método do objeto Pilotos (DbSet).
       * Nesse caso, o "ToList()" faz um SELECT simples
       */
      return _rallyDakarDbContext.Pilotos.ToList();
      //return _rallyDakarDbContext.Pilotos.Where(p => p.ID == 1);

      /* Leitura: o EF não faz a consulta, mas conecta ao banco de dados e prepara para consultas.
       */
      //return _rallyDakarDbContext.Pilotos;
    }

    public IEnumerable<Piloto> GetByID(int ID)
    {
      /* Leitura: o EF irá fazer uma consulta (SELECT) filtrando (WHERE) pela coluna ID.
       * Observação: Para o EF executar de fato o SELECT, precisa do ToList().
       */
      return _rallyDakarDbContext.Pilotos.Where(p => p.ID == ID).ToList();
    }
  }
}
