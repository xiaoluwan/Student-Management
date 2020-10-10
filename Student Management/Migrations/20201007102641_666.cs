using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Migrations
{
    public partial class _666 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Students",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassName", "Email", "Name", "PhotoPath" },
                values: new object[] { 1, 1, "1136083913@qq.com", "yang", null });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassName", "Email", "Name", "PhotoPath" },
                values: new object[] { 2, 3, "999@qq.com", "李四", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Students");
        }
    }
}
