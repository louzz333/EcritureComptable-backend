using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcritureComptable.Migrations
{
    /// <inheritdoc />
    public partial class MiseAJourAuditSuppression : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NumeroEcriture",
                table: "AuditSuppressions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Motif",
                table: "AuditSuppressions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompteEcriture",
                table: "AuditSuppressions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEcriture",
                table: "AuditSuppressions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviseEcriture",
                table: "AuditSuppressions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JournalEcriture",
                table: "AuditSuppressions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MontantEcriture",
                table: "AuditSuppressions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceEcriture",
                table: "AuditSuppressions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SensEcriture",
                table: "AuditSuppressions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompteEcriture",
                table: "AuditSuppressions");

            migrationBuilder.DropColumn(
                name: "DateEcriture",
                table: "AuditSuppressions");

            migrationBuilder.DropColumn(
                name: "DeviseEcriture",
                table: "AuditSuppressions");

            migrationBuilder.DropColumn(
                name: "JournalEcriture",
                table: "AuditSuppressions");

            migrationBuilder.DropColumn(
                name: "MontantEcriture",
                table: "AuditSuppressions");

            migrationBuilder.DropColumn(
                name: "ReferenceEcriture",
                table: "AuditSuppressions");

            migrationBuilder.DropColumn(
                name: "SensEcriture",
                table: "AuditSuppressions");

            migrationBuilder.AlterColumn<decimal>(
                name: "NumeroEcriture",
                table: "AuditSuppressions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Motif",
                table: "AuditSuppressions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
