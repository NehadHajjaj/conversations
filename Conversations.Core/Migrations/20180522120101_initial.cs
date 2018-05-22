using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Conversations.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cnv");

            migrationBuilder.CreateTable(
                name: "Conversation",
                schema: "cnv",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArchivedByUserId = table.Column<int>(nullable: true),
                    ArchivedOn = table.Column<DateTime>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Key = table.Column<string>(unicode: false, maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "cnv",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    ConversationId = table.Column<int>(nullable: false),
                    CorrelationId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    PostedOn = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalSchema: "cnv",
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "cnv",
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversationDocument",
                schema: "cnv",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentDataId = table.Column<int>(nullable: true),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationDocument_Comment_CommentDataId",
                        column: x => x.CommentDataId,
                        principalSchema: "cnv",
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ConversationId",
                schema: "cnv",
                table: "Comment",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                schema: "cnv",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationDocument_CommentDataId",
                schema: "cnv",
                table: "ConversationDocument",
                column: "CommentDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversationDocument",
                schema: "cnv");

            migrationBuilder.DropTable(
                name: "Comment",
                schema: "cnv");

            migrationBuilder.DropTable(
                name: "Conversation",
                schema: "cnv");
        }
    }
}
