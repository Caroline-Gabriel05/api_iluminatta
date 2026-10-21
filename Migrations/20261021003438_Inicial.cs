using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iluminatta.Api.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_cliente",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SOBRENOME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GENERO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NASCIMENTO = table.Column<DateOnly>(type: "date", nullable: false),
                    CPF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CEP = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CIDADE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BAIRRO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RUA = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NUMERO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EMAIL = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_cliente", x => x.CODIGO);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_codigo_prod",
                columns: table => new
                {
                    COD_CATEGORIA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CATEGORIAS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_codigo_prod", x => x.COD_CATEGORIA);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_marca",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MARCA = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_marca", x => x.CODIGO);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_carrinho",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QUANT_ITENS = table.Column<int>(type: "int", nullable: false),
                    DATA_PEDIDO = table.Column<DateOnly>(type: "date", nullable: false),
                    COD_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    PARCELAS = table.Column<int>(type: "int", nullable: false),
                    VALOR_TOTAL = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_carrinho", x => x.CODIGO);
                    table.ForeignKey(
                        name: "FK_tb_carrinho_tb_cliente_COD_CLIENTE",
                        column: x => x.COD_CLIENTE,
                        principalTable: "tb_cliente",
                        principalColumn: "CODIGO",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    COD_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    LOGINUSU = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SENHA = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.CODIGO);
                    table.ForeignKey(
                        name: "FK_tb_usuario_tb_cliente_COD_CLIENTE",
                        column: x => x.COD_CLIENTE,
                        principalTable: "tb_cliente",
                        principalColumn: "CODIGO",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_produto",
                columns: table => new
                {
                    COD_PRODUTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MARCA = table.Column<int>(type: "int", nullable: true),
                    CATEGORIAS = table.Column<int>(type: "int", nullable: true),
                    NOME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MODELO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCRICAO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PRECO = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ESTOQUE = table.Column<int>(type: "int", nullable: false),
                    ROW_VERSION = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_produto", x => x.COD_PRODUTO);
                    table.ForeignKey(
                        name: "FK_tb_produto_tb_codigo_prod_CATEGORIAS",
                        column: x => x.CATEGORIAS,
                        principalTable: "tb_codigo_prod",
                        principalColumn: "COD_CATEGORIA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_produto_tb_marca_MARCA",
                        column: x => x.MARCA,
                        principalTable: "tb_marca",
                        principalColumn: "CODIGO",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_pagamento",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    COD_PEDIDO = table.Column<int>(type: "int", nullable: false),
                    ID_MERCADO_PAGO = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VALOR = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    STATUS = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    METODO_PAGAMENTO = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DATA_PAGAMENTO = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pagamento", x => x.CODIGO);
                    table.ForeignKey(
                        name: "FK_tb_pagamento_tb_carrinho_COD_PEDIDO",
                        column: x => x.COD_PEDIDO,
                        principalTable: "tb_carrinho",
                        principalColumn: "CODIGO",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_item_carrinho",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CODIGO_CARRINHO = table.Column<int>(type: "int", nullable: false),
                    COD_ITEM = table.Column<int>(type: "int", nullable: false),
                    QUANT_ITEM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_item_carrinho", x => x.CODIGO);
                    table.ForeignKey(
                        name: "FK_tb_item_carrinho_tb_carrinho_CODIGO_CARRINHO",
                        column: x => x.CODIGO_CARRINHO,
                        principalTable: "tb_carrinho",
                        principalColumn: "CODIGO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_item_carrinho_tb_produto_COD_ITEM",
                        column: x => x.COD_ITEM,
                        principalTable: "tb_produto",
                        principalColumn: "COD_PRODUTO",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_varias_img",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CHAVE_IMG_PROD = table.Column<int>(type: "int", nullable: false),
                    CAMINHO_IMG = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_varias_img", x => x.CODIGO);
                    table.ForeignKey(
                        name: "FK_tb_varias_img_tb_produto_CHAVE_IMG_PROD",
                        column: x => x.CHAVE_IMG_PROD,
                        principalTable: "tb_produto",
                        principalColumn: "COD_PRODUTO",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_carrinho_COD_CLIENTE",
                table: "tb_carrinho",
                column: "COD_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_tb_item_carrinho_COD_ITEM",
                table: "tb_item_carrinho",
                column: "COD_ITEM");

            migrationBuilder.CreateIndex(
                name: "IX_tb_item_carrinho_CODIGO_CARRINHO",
                table: "tb_item_carrinho",
                column: "CODIGO_CARRINHO");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pagamento_COD_PEDIDO",
                table: "tb_pagamento",
                column: "COD_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_tb_produto_CATEGORIAS",
                table: "tb_produto",
                column: "CATEGORIAS");

            migrationBuilder.CreateIndex(
                name: "IX_tb_produto_MARCA",
                table: "tb_produto",
                column: "MARCA");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuario_COD_CLIENTE",
                table: "tb_usuario",
                column: "COD_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_tb_varias_img_CHAVE_IMG_PROD",
                table: "tb_varias_img",
                column: "CHAVE_IMG_PROD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_item_carrinho");

            migrationBuilder.DropTable(
                name: "tb_pagamento");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_varias_img");

            migrationBuilder.DropTable(
                name: "tb_carrinho");

            migrationBuilder.DropTable(
                name: "tb_produto");

            migrationBuilder.DropTable(
                name: "tb_cliente");

            migrationBuilder.DropTable(
                name: "tb_codigo_prod");

            migrationBuilder.DropTable(
                name: "tb_marca");
        }
    }
}
