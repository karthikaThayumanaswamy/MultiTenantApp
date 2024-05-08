using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentitySample.Migrations
{
    public partial class TestMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_LoginProvider_ProviderKey_TenantId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetRoleClaims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AspNetUserTokens",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AspNetUserRoles",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AspNetUserLogins",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AspNetUserLogins",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AspNetUserClaims",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AspNetRoles",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AspNetRoleClaims",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                columns: new[] { "NormalizedUserName", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_LoginProvider_ProviderKey_TenantId",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                columns: new[] { "NormalizedName", "TenantId" },
                unique: true);
        }
    }
}
