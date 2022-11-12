using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace entity.Migrations
{
    /// <inheritdoc />
    public partial class NonUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Relevance = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PriorityTask = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2022, 11, 12, 13, 24, 30, 288, DateTimeKind.Local).AddTicks(8170)),
                    DeadLine = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Task_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name", "Relevance" },
                values: new object[,]
                {
                    { new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa7a"), null, "Pending Activities", "40" },
                    { new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa7b"), null, "React Activities", "80" },
                    { new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa7c"), null, "Entity Framework", "50" }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskId", "CategoryId", "DeadLine", "Description", "PriorityTask", "Title" },
                values: new object[,]
                {
                    { new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa71"), new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa7b"), new DateTime(2022, 11, 12, 13, 24, 30, 288, DateTimeKind.Local).AddTicks(4390), "We need to understand how UseContext works.", 1, " Use UseContext" },
                    { new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa72"), new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa7a"), new DateTime(2022, 11, 12, 13, 24, 30, 288, DateTimeKind.Local).AddTicks(4420), "To Create wonderfull interface is essential for us to inhance our skills.", 0, "Create Interface" },
                    { new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa73"), new Guid("e2411a88-eb28-4ea5-a220-85d5e2d4fa7b"), new DateTime(2022, 11, 12, 13, 24, 30, 288, DateTimeKind.Local).AddTicks(4430), "Today we have to finish this course.", 2, " Finish EF Course" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_CategoryId",
                table: "Task",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
