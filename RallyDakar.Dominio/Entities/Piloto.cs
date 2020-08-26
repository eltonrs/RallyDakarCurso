namespace RallyDakar.Dominio.Entities
{
  public class Piloto
  {
    public int ID { get; set; }
    public string Nome { get; set; }
    public int EquipeID { get; set; }
    public virtual Equipe Equipe { get; set; }
  }
}
