using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryProject.Migrations
{
    /// <inheritdoc />
    public partial class AddRolUseCat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    idCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    registrationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__79D361B6930E16FF", x => x.idCategory);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    registrationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__3C872F76804F2E15", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    studentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    studentDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    studentFee = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    idUsers = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idRol = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    registrationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__981CF2B10C1B1086", x => x.idUsers);
                    table.ForeignKey(
                        name: "FK__Users__idRol__1BFD2C07",
                        column: x => x.idRol,
                        principalTable: "Rols",
                        principalColumn: "idRol");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_idRol",
                table: "Users",
                column: "idRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Rols");
        }
    }
}
