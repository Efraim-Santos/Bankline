using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bankline.Migrations
{
    public partial class AlteracaoNosDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Transaction",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Transaction",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Transaction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "BankStatement",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "BankStatement");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Transaction",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transaction",
                newName: "Descricao");
        }
    }
}
