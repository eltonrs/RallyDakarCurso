using AppMyTests.Mercado;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMyTests
{
  class Program
  {
    private static bool TestarVendaTemProduto(Venda venda)
    {
      return venda.Produtos.Any();
    }

    static void Main(string[] args)
    {
      var venda = new Venda();

      if (TestarVendaTemProduto(venda))
        Console.WriteLine("Tem produto.");
      else
        Console.WriteLine("Não tem produto.");
    }
  }
}
