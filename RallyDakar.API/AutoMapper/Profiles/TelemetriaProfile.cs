using AutoMapper;
using RallyDakar.API.Model;
using RallyDakar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.AutoMapper.Profiles
{
  public class TelemetriaProfile : Profile
  {
    public TelemetriaProfile()
    {
      CreateMap<Telemetria, TelemetriaModel>();
      CreateMap<TelemetriaModel, Telemetria>();
    }
  }
}
