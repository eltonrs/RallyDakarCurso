using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Domain.Tests.TemporadaBasicsTests
{
  [TestClass]
  public class ValidarPropriedadesFalhaTest
  {
    Temporada temporada;
    Equipe equipe;

    [TestInitialize]
    public void Initialize()
    {
      temporada = new Temporada();
      temporada.ID = 1;
      temporada.Nome = "Temporada 2020";
    }

    [TestMethod]
    public void ValidarPropriedadeNomeVazia()
    {
      equipe = new Equipe()
      {
        ID = 1,
        Nome = "",
        CodigoIdentificador = "EQ1"
      };

      Assert.IsFalse(equipe.ValidarPropriedades(), "O nome da equipe está preenchido.");
    }
  }
}
