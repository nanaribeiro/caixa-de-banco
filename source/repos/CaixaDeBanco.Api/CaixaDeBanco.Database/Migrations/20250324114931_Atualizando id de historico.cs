using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaixaDeBanco.Database.Migrations
{
    /// <inheritdoc />
    public partial class Atualizandoiddehistorico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountHistory_Account_AccountIdId",
                table: "AccountHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_Account_AccountIdId",
                table: "TransactionHistory");

            migrationBuilder.RenameColumn(
                name: "AccountIdId",
                table: "TransactionHistory",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistory_AccountIdId",
                table: "TransactionHistory",
                newName: "IX_TransactionHistory_AccountId");

            migrationBuilder.RenameColumn(
                name: "AccountIdId",
                table: "AccountHistory",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountHistory_AccountIdId",
                table: "AccountHistory",
                newName: "IX_AccountHistory_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountHistory_Account_AccountId",
                table: "AccountHistory",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_Account_AccountId",
                table: "TransactionHistory",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountHistory_Account_AccountId",
                table: "AccountHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_Account_AccountId",
                table: "TransactionHistory");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "TransactionHistory",
                newName: "AccountIdId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionHistory_AccountId",
                table: "TransactionHistory",
                newName: "IX_TransactionHistory_AccountIdId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountHistory",
                newName: "AccountIdId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountHistory_AccountId",
                table: "AccountHistory",
                newName: "IX_AccountHistory_AccountIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountHistory_Account_AccountIdId",
                table: "AccountHistory",
                column: "AccountIdId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_Account_AccountIdId",
                table: "TransactionHistory",
                column: "AccountIdId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
