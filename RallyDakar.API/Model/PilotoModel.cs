using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Model
{
  /* NÃO PODE TER regras de negócio
   * 
   * SEMPRE que adicionar uma classe de modelo, adicionar no Profile do AutoMapper */
  public class PilotoModel
  {
    [Key]
    public int ID { get; set; } // na maioria das vezes não precisa do ID aqui no Model. Pq esse campo não é visível para o usuário
    [Required(ErrorMessage = "O nome do piloto é obrigatório.")]
    [MinLength(4, ErrorMessage = "O nome do piloto deve ter no mínimo 4 caracteres.")]
    [MaxLength(500, ErrorMessage = "O nome do piloto excedeu a quantidade de 500 caracteres.")]
    public string Nome { get; set; }
    [Required]
    [MinLength(4, ErrorMessage = "O nome do piloto deve ter no mínimo 4 caracteres.")]
    [MaxLength(500, ErrorMessage = "O nome do piloto excedeu a quantidade de 500 caracteres.")]
    public string Sobrenome { get; set; }
    public int EquipeID { get; set; }
    //public string NomeCompleto // propriedade somente leitura
    //{
    //  get
    //  { 
    //    return Nome + " " + Sobrenome;
    //  }
    //}

    // para o mesmo propósito de código acima (comentado), mas utilizando uma interpolação de string
    public string NomeCompleto
    {
      get
      {
        return $"{Nome} {Sobrenome}"; // mais elegante, não precisa ficar fazendo concatenação
      }
    }
  }
}
