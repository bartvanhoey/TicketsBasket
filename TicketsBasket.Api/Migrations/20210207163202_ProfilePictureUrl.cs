using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsBasket.Api.Migrations
{
    public partial class ProfilePictureUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "UserProfiles",
                newName: "ProfilePictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "UserProfiles",
                newName: "ProfilePicture");
        }
    }
}
