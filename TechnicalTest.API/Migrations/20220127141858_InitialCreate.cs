using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TechnicalTest.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    HireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DeclarationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeclaredById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_DeclaredById",
                        column: x => x.DeclaredById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            //create a table Document with the field Id, S3Key, Description and EventId as foreign key
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    S3Key = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });



            migrationBuilder.CreateIndex(
                name: "IX_Events_DeclaredById",
                table: "Events",
                column: "DeclaredById");

            //create an index on the field EventId
            migrationBuilder.CreateIndex(
                name: "IX_Documents_EventId",
                table: "Documents",
                column: "EventId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            //drop the table Documents
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}

//Query for new user insert in postgresql
// INSERT INTO "Users" ("Id", "FirstName", "LastName", "HireDate") VALUES ('c0a80163-7b48-4d36-9bd7-2b0d7b3dcb6d', 'John', 'Doe', '2021-05-01 00:00:00.0000000+00:00');
// list all user query
// SELECT * FROM "Users";

//insert new event
// INSERT INTO "Events" ("Description", "DeclarationDateTime", "DeclaredById") VALUES ('Test', '2020-11-02 00:00:00.0000000+00:00', 'c0a80163-7b48-4d36-9bd7-2b0d7b3dcb6d');
//list all events
// SELECT * FROM "Events";


//select all events from Current Date
// SELECT * FROM "Events" WHERE "DeclarationDateTime" >= '2021-05-01 00:00:00.0000000+00:00';

//make it a proper query


//create a new event with 2 documents
// INSERT INTO "Events" ("Description", "DeclarationDateTime", "DeclaredById", "Documents") VALUES ('Test', '2021-05-01 00:00:00.0000000+00:00', 'c0a80163-7b48-4d36-9bd7-2b0d7b3dcb6d', '[{"Name":"Test1","Url":"Test1"},{"Name":"Test2","Url":"Test2"}]');
//insert into "Events" ("Description", "DeclarationDateTim



//Select all documents
// SELECT * FROM "Documents";
//insert new document
// INSERT INTO "Documents" ("S3Key", "Description", "EventId") VALUES ('Test', 'Test', 2);
//insert new document
// INSERT INTO "Documents" ("S3Key", "Description", "EventId") VALUES ('Test2', 'Test2', 1);

//select all events with documents
// SELECT * FROM "Events" e LEFT JOIN "Documents" d ON e."Id" = d."EventId";