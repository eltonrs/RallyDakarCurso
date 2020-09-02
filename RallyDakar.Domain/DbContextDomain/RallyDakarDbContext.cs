using Microsoft.EntityFrameworkCore;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.DbContextDomain
{
  public class RallyDakarDbContext : DbContext
  {
    // Adicionando os DataSets. O EntityFramework "traduz" a tipagem dos objetos declarados com DbSet, para uma tabela em banco de dados ou memória
    // o DbSet (do EF) "ajuda" a traduzir os objetos (C#) em entidades do banco de dados (tabelas).
    public DbSet<Temporada> Temporadas { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    // Leitura: DbSet tipado por Piloto
    public DbSet<Piloto> Pilotos { get; set; }
    public DbSet<Telemetria> Telemetrias { get; set; }

    /* Lendo o construtor abaixo:
     * O construtor é obrigatório, senão o EntityFramework não funciona.
     * Leitura: o parâmetro está sendo passado uma configuração, do tipo da classe que eu quero, no caso, a própria RallyDakarDbContext,
     * ou seja, esse próprio contexto de banco de dados.
     * o " : base(<parâmetro>)" nada mais é que a passagem do parâmetro para o método (construtor) da classe pai.
     * 
     * Assim, o EF consegue identificar as propriedades (identificadas com DbSet) e realizar as devidas criações/atualizações no banco de dados.
     */
    public RallyDakarDbContext(DbContextOptions<RallyDakarDbContext> options) : base(options)
    {

    }
  }
}