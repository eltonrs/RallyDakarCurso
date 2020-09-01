namespace RallyDakar.Domain.Entities
{
  public class Piloto
  {
    public int ID { get; set; } // na maioria das vezes não precisa do ID aqui no Model. Pq esse campo não é visível para o usuário
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public int EquipeID { get; set; }
    public int NumeroUnico { get; set; } // não deve ser visivel no Model
    public virtual Equipe Equipe { get; set; }
  }
}
