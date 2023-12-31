﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceBack.Domain.Entities;

[Index(nameof(ProdutoId), nameof(TamanhoId), nameof(CorId), IsUnique = true)]
public class Item : Entity
{
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public int TamanhoId { get; set; }
    public Tamanho Tamanho { get; set; }
    public int CorId { get; set; }
    public Cor Cor { get; set; }
    public int QuantidadeEstoque { get; set; }
    [NotMapped]
    public bool EstaDisponivel => QuantidadeEstoque > 0;

    public bool EstaValidoParaAdicionarAoCarrinho(int quantidadeAdicionar, out string razaoInvalido)
    {
        if (!EstaDisponivel)
        {
            razaoInvalido = "Esse item não está disponível.";
            return false;
        }
        if (QuantidadeEstoque < quantidadeAdicionar)
        {
            razaoInvalido = $"Quantidade insuficiente do item em estoque. Quantidade máxima: {QuantidadeEstoque}.";
            return false;
        }

        razaoInvalido = "";
        return true;
    }
}
