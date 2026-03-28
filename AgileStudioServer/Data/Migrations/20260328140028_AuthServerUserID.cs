using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AuthServerUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "auth_server_user_id",
                table: "user",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "auth_server_user_id",
                table: "user");
        }
    }
}
