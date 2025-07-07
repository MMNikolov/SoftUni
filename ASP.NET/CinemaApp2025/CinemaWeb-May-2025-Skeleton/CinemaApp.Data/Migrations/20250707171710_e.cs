using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class e : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Cinemas_CinemaId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Cinemas_CinemaId",
                table: "Ticket",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Cinemas_CinemaId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Cinemas_CinemaId",
                table: "Ticket",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
