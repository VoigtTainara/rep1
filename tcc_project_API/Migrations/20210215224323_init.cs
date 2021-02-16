using Microsoft.EntityFrameworkCore.Migrations;

namespace tcc_project_API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cnpj = table.Column<string>(type: "TEXT", nullable: true),
                    nome = table.Column<string>(type: "TEXT", nullable: true),
                    nomeFantasia = table.Column<string>(type: "TEXT", nullable: true),
                    endereco = table.Column<string>(type: "TEXT", nullable: true),
                    telefone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "setores",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: true),
                    empresaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setores", x => x.id);
                    table.ForeignKey(
                        name: "FK_setores_empresas_empresaId",
                        column: x => x.empresaId,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: true),
                    sobrenome = table.Column<string>(type: "TEXT", nullable: true),
                    dataNasc = table.Column<string>(type: "TEXT", nullable: true),
                    cpf = table.Column<string>(type: "TEXT", nullable: true),
                    rg = table.Column<string>(type: "TEXT", nullable: true),
                    endereco = table.Column<string>(type: "TEXT", nullable: true),
                    pis = table.Column<string>(type: "TEXT", nullable: true),
                    cargaHoraria = table.Column<string>(type: "TEXT", nullable: true),
                    salario = table.Column<string>(type: "TEXT", nullable: true),
                    dataAdmissao = table.Column<string>(type: "TEXT", nullable: true),
                    dataDesligamento = table.Column<string>(type: "TEXT", nullable: true),
                    setorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_funcionarios_setores_setorId",
                        column: x => x.setorId,
                        principalTable: "setores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "empresas",
                columns: new[] { "id", "cnpj", "endereco", "nome", "nomeFantasia", "telefone" },
                values: new object[] { 1, "123456789", "Rua x", "Empresa da Tai", "Empresa da Tai", "123456" });

            migrationBuilder.InsertData(
                table: "empresas",
                columns: new[] { "id", "cnpj", "endereco", "nome", "nomeFantasia", "telefone" },
                values: new object[] { 2, "2345678910", "Rua x", "Empresa da Carol", "Empresa da Carol", "234567" });

            migrationBuilder.InsertData(
                table: "setores",
                columns: new[] { "id", "empresaId", "nome" },
                values: new object[] { 1, 1, "Setor da Empresa da Tai" });

            migrationBuilder.InsertData(
                table: "setores",
                columns: new[] { "id", "empresaId", "nome" },
                values: new object[] { 2, 2, "Setor da Empresa da Carol" });

            migrationBuilder.InsertData(
                table: "funcionarios",
                columns: new[] { "id", "cargaHoraria", "cpf", "dataAdmissao", "dataDesligamento", "dataNasc", "endereco", "nome", "pis", "rg", "salario", "setorId", "sobrenome" },
                values: new object[] { 1, "40", "08953779936", "14/06/2020", " ", "14/06/2000", "Rua Arnoldo Hardt", "Tai", "5678912", "53779936", "2100", 1, "Voigt" });

            migrationBuilder.InsertData(
                table: "funcionarios",
                columns: new[] { "id", "cargaHoraria", "cpf", "dataAdmissao", "dataDesligamento", "dataNasc", "endereco", "nome", "pis", "rg", "salario", "setorId", "sobrenome" },
                values: new object[] { 2, "40", "08953779936", "22/09/2004", " ", "14/06/2000", "Rua Arnoldo Hardt", "Carol", "5678912", "53779936", "2100", 2, "Voigt" });

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_setorId",
                table: "funcionarios",
                column: "setorId");

            migrationBuilder.CreateIndex(
                name: "IX_setores_empresaId",
                table: "setores",
                column: "empresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "setores");

            migrationBuilder.DropTable(
                name: "empresas");
        }
    }
}
