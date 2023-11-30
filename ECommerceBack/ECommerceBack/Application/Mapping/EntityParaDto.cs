using AutoMapper;
using ECommerceBack.Application.Dtos;
using ECommerceBack.Domain.Entities;

namespace ECommerceBack.Application.Mapping;

public class EntityParaDto : Profile
{
    public EntityParaDto()
    {
        CreateMap<Produto, ProdutoVitrineDto>()
            .ForMember(dto => dto.Imagem, m => m.MapFrom(entity =>
                entity.ImagensOrdenadas.First().UrlImagem));

        CreateMap<IEnumerable<Item>, IEnumerable<ItemDetalhesDto>>()
            .ConvertUsing((entities, _, _) => entities
                    .GroupBy(e => e.Tamanho.Nome)
                    .Select(g => new ItemDetalhesDto
                    {
                        Tamanho = g.Key,
                        Cores = g.Select(e => new ItemDetalhesCorDto
                        {
                            Id = e.Id,
                            Nome = e.Cor.Nome,
                            Codigo = e.Cor.Codigo,
                            QuantidadeEstoque = e.QuantidadeEstoque,
                        })
                    })
            );

        CreateMap<ProdutoImagem, ProdutoImagemDto>();

        CreateMap<Produto, ProdutoDetalhesDto>()
            .ForMember(dto => dto.Imagens, m => m.MapFrom(entity => entity.ImagensOrdenadas));

        CreateMap<CarrinhoItem, CarrinhoDetalhesItemDto>()
            .ForMember(dto => dto.Id, m => m.MapFrom(entity => entity.Item.Id))
            .ForMember(dto => dto.Nome, m => m.MapFrom(entity => entity.Item.Produto.Nome))
            .ForMember(dto => dto.Tamanho, m => m.MapFrom(entity => entity.Item.Tamanho.Nome))
            .ForMember(dto => dto.Cor, m => m.MapFrom(entity => entity.Item.Cor.Nome))
            .ForMember(dto => dto.Quantidade, m => m.MapFrom(entity => entity.QuantidadeItem))
            .ForMember(dto => dto.PrecoUnitario, m => m.MapFrom(entity => entity.Item.Produto.Preco));

        CreateMap<PedidoItem, PedidoItemDto>()
            .ForMember(dto => dto.Tamanho, m => m.MapFrom(entity => entity.Item.Tamanho.Nome))
            .ForMember(dto => dto.Cor, m => m.MapFrom(entity => entity.Item.Cor.Nome));

        CreateMap<Pedido, PedidoDto>();
    }
}
