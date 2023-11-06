using ECommerceBack.Domain.Entities;

namespace ECommerceBack.Infra.Database;

public class InicializadorBanco
{
    private readonly ECommerceDbContext _context;

    public InicializadorBanco(ECommerceDbContext context)
    {
        _context = context;
    }

    public void PopularBanco()
    {
        if (_context.Itens.Any())
        {
            return;
        }

        var tamanhoPP = new Tamanho { Nome = "PP" };
        var tamanhoP = new Tamanho { Nome = "P" };
        var tamanhoM = new Tamanho { Nome = "M" };
        var tamanhoG = new Tamanho { Nome = "G" };
        var tamanhoGG = new Tamanho { Nome = "GG" };
        var tamanhoXGG = new Tamanho { Nome = "XGG" };
        _context.Tamanhos.AddRange(tamanhoPP, tamanhoP, tamanhoM, tamanhoG, tamanhoGG, tamanhoXGG);

        var corPreta = new Cor { Nome = "Preta", Codigo = "000000" };
        var corBranca = new Cor { Nome = "Branca", Codigo = "ffffff" };
        var corAzul = new Cor { Nome = "Azul", Codigo = "33405b" };
        var corDirtyBlonde = new Cor { Nome = "Dirty Blonde", Codigo = "ca9f74" };
        var corAzulMarinho = new Cor { Nome = "Azul Marinho", Codigo = "494e67" };
        var corLilas = new Cor { Nome = "Lilás", Codigo = "d4c3e4" };
        var corCafe = new Cor { Nome = "Café", Codigo = "4c3838" };
        _context.Cores.AddRange(corPreta, corBranca, corAzul, corDirtyBlonde, corLilas, corCafe);

        _context.Produtos.AddRange(
            // Cores: preta, branca e azul
            new Produto
            {
                Nome = "Tech T-Shirt",
                Descricao = "A Tech T-shirt é a releitura da camiseta básica masculina com os benefícios únicos da tecnologia Insider. Feita com fibras que se adaptam ao corpo, acompanha seus movimentos e não esquenta, não precisa ser passada porque desamassa no corpo e não desbota com o tempo. Também oferece maciez incomparável e ação anti odor que deixa o tecido desfavorável à proliferação de bactérias que causam mau cheiro.",
                Preco = 159,
                Imagens = new List<ProdutoImagem>
                {
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/tech-t-shirt/1.webp", Ordem = 1 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/tech-t-shirt/2.webp", Ordem = 2 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/tech-t-shirt/3.webp", Ordem = 3 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/tech-t-shirt/4.webp", Ordem = 4 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/tech-t-shirt/5.webp", Ordem = 5 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/tech-t-shirt/6.webp", Ordem = 6 },
                },
                Itens = new List<Item>
                {
                    new Item { Cor = corPreta, Tamanho = tamanhoP, QuantidadeEstoque = 100 },
                    new Item { Cor = corPreta, Tamanho = tamanhoM, QuantidadeEstoque = 125 },
                    new Item { Cor = corPreta, Tamanho = tamanhoG, QuantidadeEstoque = 70 },
                    new Item { Cor = corBranca, Tamanho = tamanhoP, QuantidadeEstoque = 170 },
                    new Item { Cor = corBranca, Tamanho = tamanhoM, QuantidadeEstoque = 100 },
                    new Item { Cor = corBranca, Tamanho = tamanhoG, QuantidadeEstoque = 120 },
                    new Item { Cor = corAzul, Tamanho = tamanhoP, QuantidadeEstoque = 30 },
                    new Item { Cor = corAzul, Tamanho = tamanhoM, QuantidadeEstoque = 120 },
                    new Item { Cor = corAzul, Tamanho = tamanhoG, QuantidadeEstoque = 100 },
                },
            },
            // Cores: branca e preta
            new Produto
            {
                Nome = "Daily T-Shirt",
                Descricao = "Além de ser extremamente leve e macia, ideal pra qualquer ocasião e estação do ano, a Daily T-Shirt  tem impacto ambiental reduzido: 50% menos uso de água e 50% menos emissões de CO2 na sua produção que uma camiseta de algodão, livre do uso de inseticidas e pesticidas.",
                Preco = 125,
                Imagens = new List<ProdutoImagem>
                {
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/daily-t-shirt/1.webp", Ordem = 1 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/daily-t-shirt/2.webp", Ordem = 2 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/daily-t-shirt/3.webp", Ordem = 3 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/daily-t-shirt/4.webp", Ordem = 4 },
                },
                Itens = new List<Item>
                {
                    new Item { Cor = corPreta, Tamanho = tamanhoM, QuantidadeEstoque = 25 },
                    new Item { Cor = corPreta, Tamanho = tamanhoGG, QuantidadeEstoque = 30 },
                    new Item { Cor = corBranca, Tamanho = tamanhoP, QuantidadeEstoque = 90 },
                    new Item { Cor = corBranca, Tamanho = tamanhoM, QuantidadeEstoque = 30 },
                    new Item { Cor = corBranca, Tamanho = tamanhoG, QuantidadeEstoque = 80 },
                },
            },
            // Cores: branca e dirty blonde
            new Produto
            {
                Nome = "Turtleneck Masculino",
                Descricao = "O essencial fora da curva. Criada em colaboração com Glória Coelho, ícone da moda brasileira, essa peça tem design atemporal com detalhes que a tornam singular. Com mangas compridas e punho estendido, tem inserção para o polegar que faz um efeito luva para maior conforto, podendo ou não ser usado. Esse detalhe confere um ar moderno e dinâmico. Feito com experimentação de materiais e cores, tecidos excepcionais, detalhes impecáveis e alfaiataria precisa, através de formas que evocam o estilo de vida urbano.",
                Preco = 499,
                Imagens = new List<ProdutoImagem>
                {
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/turtleneck-masculino/1.webp", Ordem = 1 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/turtleneck-masculino/2.webp", Ordem = 2 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/turtleneck-masculino/3.webp", Ordem = 3 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/turtleneck-masculino/4.webp", Ordem = 4 },
                },
                Itens = new List<Item>
                {
                    new Item { Cor = corBranca, Tamanho = tamanhoPP, QuantidadeEstoque = 10 },
                    new Item { Cor = corBranca, Tamanho = tamanhoM, QuantidadeEstoque = 40 },
                    new Item { Cor = corBranca, Tamanho = tamanhoG, QuantidadeEstoque = 40 },
                    new Item { Cor = corDirtyBlonde, Tamanho = tamanhoM, QuantidadeEstoque = 25 },
                    new Item { Cor = corDirtyBlonde, Tamanho = tamanhoG, QuantidadeEstoque = 20 },
                },
            },
            // Cores: preta e azul marinho
            new Produto
            {
                Nome = "Calvin Klein",
                Descricao = "Completo sucesso a Calvin Klein, é ícone do minimalismo e da sofisticação na caminhada pelo concorrido universo da moda. As roupas de linhas simples e sofisticadas colocaram a Calvin Klein entre as marcas de moda mais importantes e influentes do mercado, sendo sempre ousada, imponente e sem medo de abraçar culturas, discursos e pregar a despadronização. Já a Calvin Klein Jeans está mais focada para o jeanswear, moda streetstyle, misturando o contemporâneo com a jovialidade da marca, trabalhada em inovação, estampas e cores características de toda sua pluralidade.",
                Preco = 189,
                Imagens = new List<ProdutoImagem>
                {
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/calvin-klein/1.webp", Ordem = 1 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/calvin-klein/2.webp", Ordem = 2 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/calvin-klein/3.webp", Ordem = 3 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/calvin-klein/4.webp", Ordem = 4 },
                },
                Itens = new List<Item>
                {
                    new Item { Cor = corPreta, Tamanho = tamanhoM, QuantidadeEstoque = 140 },
                    new Item { Cor = corPreta, Tamanho = tamanhoG, QuantidadeEstoque = 145 },
                    new Item { Cor = corPreta, Tamanho = tamanhoXGG, QuantidadeEstoque = 40 },
                    new Item { Cor = corAzulMarinho, Tamanho = tamanhoM, QuantidadeEstoque = 110 },
                    new Item { Cor = corAzulMarinho, Tamanho = tamanhoG, QuantidadeEstoque = 100 },
                    new Item { Cor = corAzulMarinho, Tamanho = tamanhoXGG, QuantidadeEstoque = 60 },
                },
            },
            // Cores: preta e lilás
            new Produto
            {
                Nome = "Hang Loose",
                Descricao = "Fundada em 1982, a Hang Loose é uma das maiores marcas no conceito surfwear. Suas coleções são produzidas por especialistas no Brasil e no exterior, com qualidade, modelagens e designs modernos para atender a todos.",
                Preco = 99,
                Imagens = new List<ProdutoImagem>
                {
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/hang-loose/1.webp", Ordem = 1 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/hang-loose/2.webp", Ordem = 2 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/hang-loose/3.webp", Ordem = 3 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/hang-loose/4.webp", Ordem = 4 },
                },
                Itens = new List<Item>
                {
                    new Item { Cor = corPreta, Tamanho = tamanhoPP, QuantidadeEstoque = 90 },
                    new Item { Cor = corPreta, Tamanho = tamanhoP, QuantidadeEstoque = 100 },
                    new Item { Cor = corPreta, Tamanho = tamanhoM, QuantidadeEstoque = 150 },
                    new Item { Cor = corPreta, Tamanho = tamanhoG, QuantidadeEstoque = 110 },
                    new Item { Cor = corPreta, Tamanho = tamanhoGG, QuantidadeEstoque = 70 },
                    new Item { Cor = corPreta, Tamanho = tamanhoXGG, QuantidadeEstoque = 40 },
                    new Item { Cor = corLilas, Tamanho = tamanhoM, QuantidadeEstoque = 80 },
                },
            },
            // Cores: café
            new Produto
            {
                Nome = "Aramis",
                Descricao = "A camiseta é confeccionada em malha de algodão texturizada com modelagem reta. A peça da linha move possui gola arredondada, acabamento da gola em malha canelada, mangas longas e placa de massa bordada na nuca.",
                Preco = 229,
                Imagens = new List<ProdutoImagem>
                {
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/aramis/1.webp", Ordem = 1 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/aramis/2.webp", Ordem = 2 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/aramis/3.webp", Ordem = 3 },
                },
                Itens = new List<Item>
                {
                    new Item { Cor = corCafe, Tamanho = tamanhoM, QuantidadeEstoque = 35 },
                },
            },
            // Cores: branca
            new Produto
            {
                Nome = "John John",
                Descricao = "A John John traz em seu DNA o mood urban rocker e noturno, com peças cheias de atitude e acabamentos únicos.",
                Preco = 118,
                Imagens = new List<ProdutoImagem>
                {
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/john-john/1.webp", Ordem = 1 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/john-john/2.webp", Ordem = 2 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/john-john/3.webp", Ordem = 3 },
                    new ProdutoImagem { UrlImagem = "https://raw.githubusercontent.com/mpatrickaires/ecommerce-back/main/Imagens/john-john/4.webp", Ordem = 4 },
                },
                Itens = new List<Item>
                {
                    new Item { Cor = corBranca, Tamanho = tamanhoM, QuantidadeEstoque = 200 },
                    new Item { Cor = corBranca, Tamanho = tamanhoG, QuantidadeEstoque = 190 },
                    new Item { Cor = corBranca, Tamanho = tamanhoGG, QuantidadeEstoque = 100 },
                    new Item { Cor = corBranca, Tamanho = tamanhoXGG, QuantidadeEstoque = 90 },
                },
            });

        _context.SaveChanges();
    }

    private record ItemInicializador(string Nome, string Descricao, decimal Preco, string UrlImagemPrincipal,
        string[] UrlImagens, CorTamanhoEstoque[] CorTamanhoEstoque);

    private record CorTamanhoEstoque(Cor cor, Tamanho Tamanho, int Estoque);
}
