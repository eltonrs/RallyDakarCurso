using AutoMapper;
using RallyDakar.API.Model;
using RallyDakar.Domain.Entities;

namespace RallyDakar.API.AutoMapper.Profiles
{
  public class PilotoProfile : Profile
  {
    public PilotoProfile()
    {
      /* Leitura: 
       * Na maioria dos casos, precisa fazer as duas vias de mapeamento.
       * 
       * Mapeamento da entidade de negócio para a entidade de modelo (visão) e vice-versa.
       */
      CreateMap<Piloto, PilotoModel>();
      CreateMap<PilotoModel, Piloto>();
    }
  }
}
