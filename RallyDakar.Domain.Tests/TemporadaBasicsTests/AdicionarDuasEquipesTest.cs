using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Domain.Tests.TemporadaBasicsTests
{
  [TestClass]
  public class AdicionarDuasEquipesTest
  {
    Temporada temporada;
    Equipe equipe1;
    Equipe equipe2;

    [TestInitialize]
    public void Initialize()
    {
      // Esse método é chamado automaticamente. Deve conter as inicialações dos objetos/variáveis.
      
      temporada = new Temporada();
      temporada.ID = 1;
      temporada.Nome = "Temporada 2020";

      equipe1 = new Equipe();
      equipe1.ID = 1;
      equipe1.Nome = "Equipe One";

      equipe2 = new Equipe();
      equipe2.ID = 2;
      equipe2.Nome = "Equipe Two";

      // Teste 1 (descomentado) - Com 3 equipes adicionadas na Temporada
      // Teste 3 (descomentado) - Com N equipes adicionadas na Temporada
      //equipe3 = new Equipe();
      //equipe3.ID = 3;
      //equipe3.Nome = "Equipe Three";

      // Teste 2 - Com 2 equipes adicionadas na Temporada
      //equipe3 = null;

      temporada.AdicionarEquipe(equipe1);
      temporada.AdicionarEquipe(equipe2);
    }

    [TestMethod]
    public void DuasEquipesAdicionadasCorretamente()
    {
      // (Teste 1) - Não vai passar
      //Assert.IsTrue(temporada.Equipes.Count() == 2, "Quantidade de equipes adicionadas é diferente de 2.");

      // (Teste 2) - Vai passar
      //Assert.IsTrue(temporada.Equipes.Count() == 3, "Quantidade de equipes adicionadas é diferente de 3.");

      // (Teste 3) - Vai passar
      Assert.IsTrue(temporada.Equipes.Count() > 0, "A temporada não tem nenhuma equipe adicionada.");
    }
  }
}
