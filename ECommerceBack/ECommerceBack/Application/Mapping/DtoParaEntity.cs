using AutoMapper;
using ECommerceBack.Application.Dtos;
using ECommerceBack.Domain.Entities;

namespace ECommerceBack.Application.Mapping;

public partial class DtoParaEntity : Profile
{
    public DtoParaEntity()
    {
        CreateMap<CadastroUsuarioDto, Usuario>()
            .ForMember(entity => entity.Id, m => m.Ignore());
    }
}
