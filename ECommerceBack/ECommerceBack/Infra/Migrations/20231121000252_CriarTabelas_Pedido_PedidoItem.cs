using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerceBack.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelas_Pedido_PedidoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<int>(type: "integer", nullable: false),
                    data_pedido = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedidos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pedido_itens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pedido_id = table.Column<int>(type: "integer", nullable: false),
                    item_id = table.Column<int>(type: "integer", nullable: false),
                    quantidade_item = table.Column<int>(type: "integer", nullable: false),
                    preco_unitario_item = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedido_itens", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedido_itens_itens_item_id",
                        column: x => x.item_id,
                        principalTable: "itens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pedido_itens_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pedido_itens_item_id",
                table: "pedido_itens",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_itens_pedido_id_item_id",
                table: "pedido_itens",
                columns: new[] { "pedido_id", "item_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_usuario_id",
                table: "pedidos",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedido_itens");

            migrationBuilder.DropTable(
                name: "pedidos");
        }
    }
}
