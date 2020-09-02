using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Model
{
  public class TelemetriaModel
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Equipe não identificada")]
    public int EquipeId { get; set; }

    [Required(ErrorMessage = "Campo de data requirido.")]
    [DataType(DataType.Date)]
    public DateTime Data { get; set; }

    [Required(ErrorMessage = "Campo de hora requirido.")]
    [DataType(DataType.Time)]
    public TimeSpan Hora { get; set; }

    public DateTime DataServidor { get; set; }
    public TimeSpan HoraServidor { get; set; }

    [Required(ErrorMessage = "Latitude não informada")]
    public decimal Latitude { get; set; }

    [Required(ErrorMessage = "Longitude não informada")]
    public decimal Longitude { get; set; }

    [Required(ErrorMessage = "Percentual Combustível não informado")]
    public decimal PercentualCombustivel { get; set; }

    [Required(ErrorMessage = "Velocidade não informada")]
    public double Velocidade { get; set; }

    [Required(ErrorMessage = "RPM não informado")]
    public double RPM { get; set; }

    public int TemperaturaExterna { get; set; }
    public int TemperaturaMotor { get; set; }
    public double AltitudeNivelMar { get; set; }

    public bool PedalAcelerador { get; set; }
    public bool PedalFreio { get; set; }
  }
}
