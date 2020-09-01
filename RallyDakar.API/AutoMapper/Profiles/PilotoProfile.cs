using AutoMapper;
using RallyDakar.API.Model;
using RallyDakar.Domain.Entities;

namespace RallyDakar.API.AutoMapper.Profiles
{
  public class PilotoProfile : Profile
  {
    public PilotoProfile()
    {
      // Na maioria dos casos, precisa fazer as duas vias de mapeamento
      CreateMap<Piloto, PilotoModel>();
      CreateMap<PilotoModel, Piloto>();
    }
  }
}
