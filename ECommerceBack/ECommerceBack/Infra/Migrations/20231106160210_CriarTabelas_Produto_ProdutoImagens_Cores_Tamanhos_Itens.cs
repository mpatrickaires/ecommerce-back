using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerceBack.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelas_Produto_ProdutoImagens_Cores_Tamanhos_Itens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    preco = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produtos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tamanhos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tamanhos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produto_imagens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    produto_id = table.Column<int>(type: "integer", nullable: false),
                    url_imagem = table.Column<string>(type: "text", nullable: false),
                    ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produto_imagens", x => x.id);
                    table.ForeignKey(
                        name: "fk_produto_imagens_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "itens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    produto_id = table.Column<int>(type: "integer", nullable: false),
                    tamanho_id = table.Column<int>(type: "integer", nullable: false),
                    cor_id = table.Column<int>(type: "integer", nullable: false),
                    quantidade_estoque = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_itens", x => x.id);
                    table.ForeignKey(
                        name: "fk_itens_cores_cor_id",
                        column: x => x.cor_id,
                        principalTable: "cores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_itens_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_itens_tamanhos_tamanho_id",
                        column: x => x.tamanho_id,
                        principalTable: "tamanhos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cores_codigo",
                table: "cores",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cores_nome",
                table: "cores",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_itens_cor_id",
                table: "itens",
                column: "cor_id");

            migrationBuilder.CreateIndex(
                name: "ix_itens_produto_id_tamanho_id_cor_id",
                table: "itens",
                columns: new[] { "produto_id", "tamanho_id", "cor_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_itens_tamanho_id",
                table: "itens",
                column: "tamanho_id");

            migrationBuilder.CreateIndex(
                name: "ix_produto_imagens_produto_id",
                table: "produto_imagens",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "ix_produto_imagens_url_imagem",
                table: "produto_imagens",
                column: "url_imagem",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itens");

            migrationBuilder.DropTable(
                name: "produto_imagens");

            migrationBuilder.DropTable(
                name: "cores");

            migrationBuilder.DropTable(
                name: "tamanhos");

            migrationBuilder.DropTable(
                name: "produtos");
        }
    }
}
