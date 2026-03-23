using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusHistory_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStatusHistory_JobApplications_JobApplicationId",
                table: "ApplicationStatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationStatusHistory",
                table: "ApplicationStatusHistory");

            migrationBuilder.RenameTable(
                name: "ApplicationStatusHistory",
                newName: "ApplicationStatusHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationStatusHistory_JobApplicationId",
                table: "ApplicationStatusHistories",
                newName: "IX_ApplicationStatusHistories_JobApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationStatusHistories",
                table: "ApplicationStatusHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStatusHistories_JobApplications_JobApplicationId",
                table: "ApplicationStatusHistories",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStatusHistories_JobApplications_JobApplicationId",
                table: "ApplicationStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationStatusHistories",
                table: "ApplicationStatusHistories");

            migrationBuilder.RenameTable(
                name: "ApplicationStatusHistories",
                newName: "ApplicationStatusHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationStatusHistories_JobApplicationId",
                table: "ApplicationStatusHistory",
                newName: "IX_ApplicationStatusHistory_JobApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationStatusHistory",
                table: "ApplicationStatusHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStatusHistory_JobApplications_JobApplicationId",
                table: "ApplicationStatusHistory",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
