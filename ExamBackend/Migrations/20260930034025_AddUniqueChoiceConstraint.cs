using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueChoiceConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionChoices_QuestionId",
                table: "QuestionChoices");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_QuestionId_ChoiceNo",
                table: "QuestionChoices",
                columns: new[] { "QuestionId", "ChoiceNo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionChoices_QuestionId_ChoiceNo",
                table: "QuestionChoices");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoices_QuestionId",
                table: "QuestionChoices",
                column: "QuestionId");
        }
    }
}
