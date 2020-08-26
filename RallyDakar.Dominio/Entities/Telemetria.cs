using System;

namespace RallyDakar.Domain.Entities
{
  public class Telemetria
  {
    public int ID { get; set; }
    public int TemporadaID { get; set; }
    public int PilotoID { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Hora { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal PercentualCombustivel { get; set; }
    public double Velocidade { get; set; }
    public double RPM { get; set; }
    public int TemperaturaExterna { get; set; }
    public int TemperaturaMotor { get; set; }
    public double AltitudeNivelMar { get; set; }
    public bool PedalAcelerador { get; set; }
    public bool PedalFreio { get; set; }
    public bool FarolAltoLigado { get; set; }
  }
}