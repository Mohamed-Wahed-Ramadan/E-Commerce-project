using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Context.Migrations
{
    /// <inheritdoc />
    public partial class SetuserfullNametoNullableandemailunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FullName", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { 1, null, null, "admin@iti.eg", "Admin", "$2a$11$Wbohky2NKsjWWV/KMuC5feaR5IuYg4mU.7ToAsnxNPfdTFzlktudS", "Admin", null, null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
