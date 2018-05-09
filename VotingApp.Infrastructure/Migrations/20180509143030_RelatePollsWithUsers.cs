using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VotingApp.Infrastructure.Migrations
{
    public partial class RelatePollsWithUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_ApplicationUserId",
                table: "Polls");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Polls",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Polls_ApplicationUserId",
                table: "Polls",
                newName: "IX_Polls_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_UserId",
                table: "Polls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_UserId",
                table: "Polls");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Polls",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Polls_UserId",
                table: "Polls",
                newName: "IX_Polls_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_ApplicationUserId",
                table: "Polls",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
