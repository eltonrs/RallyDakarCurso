namespace RallyDakar.Domain.Entities
{
  public class Piloto
  {
    public int ID { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public int EquipeID { get; set; }
    public int NumeroUnico { get; set; } // não deve ser visivel no Model
    public virtual Equipe Equipe { get; set; }

    public bool Validar()
    {
      return (!string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(Sobrenome));
    }
  }
}
