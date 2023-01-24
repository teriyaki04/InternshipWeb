using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipWebTask.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CONSTRAINT_BOOKGENRE",
                table: "Books");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CONSTRAINT_BOOKGENRE",
                table: "Books",
                sql: "Genre in ('Хоррор','Триллер','Фентези')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CONSTRAINT_BOOKGENRE",
                table: "Books");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CONSTRAINT_BOOKGENRE",
                table: "Books",
                sql: "Genre in ('Horror','Thriller','Fantasy')");
        }
    }
}
