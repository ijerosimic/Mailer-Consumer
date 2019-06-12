using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCaller.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationMessageCompanies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessageCompanies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMessageHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MailTo = table.Column<string>(nullable: false),
                    Cc = table.Column<string>(nullable: true),
                    Bcc = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    TriggerTime = table.Column<DateTime>(nullable: false),
                    IsSent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessageHistory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMessageClients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessageClients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotificationMessageClients_NotificationMessageCompanies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "NotificationMessageCompanies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMessageAttachmentHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Body = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessageAttachmentHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotificationMessageAttachmentHistory_NotificationMessageHistory_MessageID",
                        column: x => x.MessageID,
                        principalTable: "NotificationMessageHistory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMessageIdentifiers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GUID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessageIdentifiers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotificationMessageIdentifiers_NotificationMessageClients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "NotificationMessageClients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessageAttachmentHistory_MessageID",
                table: "NotificationMessageAttachmentHistory",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessageClients_CompanyID",
                table: "NotificationMessageClients",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationMessageIdentifiers_ClientID",
                table: "NotificationMessageIdentifiers",
                column: "ClientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationMessageAttachmentHistory");

            migrationBuilder.DropTable(
                name: "NotificationMessageIdentifiers");

            migrationBuilder.DropTable(
                name: "NotificationMessageHistory");

            migrationBuilder.DropTable(
                name: "NotificationMessageClients");

            migrationBuilder.DropTable(
                name: "NotificationMessageCompanies");
        }
    }
}
