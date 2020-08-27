using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.Tests.TemporadaBasicsTests
{
  [TestClass]
  public class AdicionarRetornarEquipeCorretaTests
  {
    Temporada temporada;
    Equipe equipe1;
    Equipe equipe2;
    Equipe equipeRetorno;

    [TestInitialize]
    public void Initializae()
    {
      temporada = new Temporada();
      temporada.ID = 1;
      temporada.Nome = "Temporada 2020";

      equipe1 = new Equipe();
      equipe1.ID = 1;
      equipe1.Nome = "Equipe One";
      equipe1.CodigoIdentificador = "EQ1";

      equipe2 = new Equipe();
      equipe2.ID = 2;
      equipe2.Nome = "Equipe Two";
      equipe2.CodigoIdentificador = "EQ2";

      temporada.AdicionarEquipe(equipe1);
      temporada.AdicionarEquipe(equipe2);
      temporada.AdicionarEquipe(equipe2);

      equipeRetorno = temporada.GetByID(equipe2.ID);
    }

    [TestMethod]
    public void IdEquipe2Ok()
    {
      Assert.IsTrue(equipeRetorno.ID == equipe2.ID, "Equipe de retorno, não é a equipe 2.");
    }
  }
}
