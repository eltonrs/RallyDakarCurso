using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AppMyTests.Mercado
{
  public class Venda
  {
    public int VendaID { get; set; }
    public DateTime DataHora { get; set; }
    public IEnumerable<Produto> Produtos { get; set; }

    public Venda()
    {
      // Quaisquer dos códigos abaixo, podem ser utilizados para inicialização objetos que implementam IEnumerable
      
      Produtos = new List<Produto>();
      //Produtos = new Produto[] { };
    }
  }
}
