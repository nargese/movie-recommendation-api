using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    IdRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Compte",
                columns: table => new
                {
                    IdCompte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Motdepasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    access = table.Column<bool>(type: "bit", nullable: false),
                    FK_Role = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compte", x => x.IdCompte);
                    table.ForeignKey(
                        name: "FK_Compte_Role_FK_Role",
                        column: x => x.FK_Role,
                        principalTable: "Role",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    IdMovie = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieApiId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: true),
                    PosterUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageRating = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_Compte = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.IdMovie);
                    table.ForeignKey(
                        name: "FK_Movie_Compte_FK_Compte",
                        column: x => x.FK_Compte,
                        principalTable: "Compte",
                        principalColumn: "IdCompte");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    IdComment = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_Compte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_Movie = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.IdComment);
                    table.ForeignKey(
                        name: "FK_Comment_Compte_FK_Compte",
                        column: x => x.FK_Compte,
                        principalTable: "Compte",
                        principalColumn: "IdCompte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Movie_FK_Movie",
                        column: x => x.FK_Movie,
                        principalTable: "Movie",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    IdGenre = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieIdMovie = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.IdGenre);
                    table.ForeignKey(
                        name: "FK_Genre_Movie_MovieIdMovie",
                        column: x => x.MovieIdMovie,
                        principalTable: "Movie",
                        principalColumn: "IdMovie");
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    IdLike = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_Compte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_Movie = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.IdLike);
                    table.ForeignKey(
                        name: "FK_Like_Compte_FK_Compte",
                        column: x => x.FK_Compte,
                        principalTable: "Compte",
                        principalColumn: "IdCompte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Movie_FK_Movie",
                        column: x => x.FK_Movie,
                        principalTable: "Movie",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    IdRating = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_Compte = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FK_Movie = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.IdRating);
                    table.ForeignKey(
                        name: "FK_Rating_Compte_FK_Compte",
                        column: x => x.FK_Compte,
                        principalTable: "Compte",
                        principalColumn: "IdCompte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Movie_FK_Movie",
                        column: x => x.FK_Movie,
                        principalTable: "Movie",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    IdMovie = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGenre = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => new { x.IdMovie, x.IdGenre });
                    table.ForeignKey(
                        name: "FK_MovieGenre_Genre_IdGenre",
                        column: x => x.IdGenre,
                        principalTable: "Genre",
                        principalColumn: "IdGenre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenre_Movie_IdMovie",
                        column: x => x.IdMovie,
                        principalTable: "Movie",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FK_Compte",
                table: "Comment",
                column: "FK_Compte");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FK_Movie",
                table: "Comment",
                column: "FK_Movie");

            migrationBuilder.CreateIndex(
                name: "IX_Compte_FK_Role",
                table: "Compte",
                column: "FK_Role");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MovieIdMovie",
                table: "Genre",
                column: "MovieIdMovie");

            migrationBuilder.CreateIndex(
                name: "IX_Like_FK_Compte",
                table: "Like",
                column: "FK_Compte");

            migrationBuilder.CreateIndex(
                name: "IX_Like_FK_Movie",
                table: "Like",
                column: "FK_Movie");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_FK_Compte",
                table: "Movie",
                column: "FK_Compte");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_IdGenre",
                table: "MovieGenre",
                column: "IdGenre");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_FK_Compte",
                table: "Rating",
                column: "FK_Compte");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_FK_Movie",
                table: "Rating",
                column: "FK_Movie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "MovieGenre");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Compte");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
