using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KasirApp.Migrations
{
    /// <inheritdoc />
    public partial class InitMsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Jalankan seluruh DDL + INSERT dari file SQL-mu
            // Copy isi lengkap QUERY CREATE TABLE N INSERT DATA.sql ke sini
            var sqlPath = Path.Combine(AppContext.BaseDirectory, "Sql", "init_schema.sql");
            var sql = File.ReadAllText(sqlPath);
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrTransactionDetail");

            migrationBuilder.DropTable(
                name: "MsProduct");

            migrationBuilder.DropTable(
                name: "TrTransaction");

            migrationBuilder.DropTable(
                name: "LtPaymentMethod");

            migrationBuilder.DropTable(
                name: "MsUser");
        }
    }
}
