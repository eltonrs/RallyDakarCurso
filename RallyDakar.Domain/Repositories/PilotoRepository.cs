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

    public void Add(Piloto piloto)
    {
      _rallyDakarDbContext.Pilotos.Add(piloto); // nesse ponto, o EF gera os scripts de banco de dados para inserir os dados.
      _rallyDakarDbContext.SaveChanges(); // replica os dados para a base de dados.
    }

    public void Delete(Piloto piloto)
    {
      _rallyDakarDbContext.Pilotos.Remove(piloto);
      _rallyDakarDbContext.SaveChanges();
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

    public Piloto GetByID(int pilotoID)
    {
      /* Leitura: o EF irá fazer uma consulta (SELECT) filtrando (WHERE) pela coluna ID.
       * Observação: Para o EF executar de fato o SELECT, precisa do ToList().
       */
      return _rallyDakarDbContext.Pilotos.FirstOrDefault(p => p.ID == pilotoID);
    }

    public void UpdateFull(Piloto piloto)
    {
      /* Essa instância não está "anexada" ao EF, isso impede que seja feita qlq operação com ela, então é feito o Attach */
      _rallyDakarDbContext.Attach(piloto);
      /* A partir do momento que a instância está no controle do EF, ela é marcada como modificada, ou seja
       * todos as propriedades (campos) estão modificados (conteúdo modificado).
       * Há como marcar somente os campos que de fato foram modificados. Melhor performance. */
      _rallyDakarDbContext.Entry(piloto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      _rallyDakarDbContext.SaveChanges(); // vai gerar o script UPDATE com todas as colunas
    }

    public void UpdatePartial(Piloto piloto)
    {
      //throw new NotImplementedException();
    }

    bool IPilotoRepository.ExistByID(int pilotoID)
    {
      return _rallyDakarDbContext.Pilotos.Any(p => p.ID == pilotoID);
    }
  }
}
