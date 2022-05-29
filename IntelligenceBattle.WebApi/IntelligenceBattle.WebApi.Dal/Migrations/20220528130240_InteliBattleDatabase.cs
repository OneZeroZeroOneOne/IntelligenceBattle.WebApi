using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IntelligenceBattle.WebApi.Dal.Migrations
{
    public partial class InteliBattleDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "category_id_seq");

            migrationBuilder.CreateSequence(
                name: "game_type_id_seq");

            migrationBuilder.CreateSequence(
                name: "notification_id_seq");

            migrationBuilder.CreateSequence(
                name: "notification_type_id_seq");

            migrationBuilder.CreateSequence(
                name: "search_game_id_seq");

            migrationBuilder.CreateSequence(
                name: "send_question_id_seq");

            migrationBuilder.CreateTable(
                name: "AuthorizationCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('category_id_seq'::regclass)"),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('game_type_id_seq'::regclass)"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PlayerCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('notification_type_id_seq'::regclass)"),
                    Tittle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizationProviderType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying", nullable: true),
                    AuthorizationProviderCenterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationProviderType", x => x.Id);
                    table.ForeignKey(
                        name: "AuthorizationProviderType_AuthorizationProviderCenterId_fkey",
                        column: x => x.AuthorizationProviderCenterId,
                        principalTable: "AuthorizationCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "character varying", nullable: true),
                    CreatedDatetime = table.Column<DateTime>(type: "timestamp(6) without time zone", precision: 6, nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    MediaUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "Question_CategoryId_fkey",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDatetime = table.Column<DateTime>(type: "timestamp(6) without time zone", precision: 6, nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsEnd = table.Column<bool>(type: "boolean", nullable: false),
                    GameTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "Game_CategoryId_fkey",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Game_GameTypeId_fkey",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTranslation",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    LangId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CategoryTranslation_pkey", x => new { x.CategoryId, x.LangId });
                    table.ForeignKey(
                        name: "CategoryTranslation_CategoryId_fkey",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "CategoryTranslation_LangId_fkey",
                        column: x => x.LangId,
                        principalTable: "Lang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameTypeTranslation",
                columns: table => new
                {
                    GameTypeId = table.Column<int>(type: "integer", nullable: false),
                    LangId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GameTypeTranslation_pkey", x => new { x.GameTypeId, x.LangId });
                    table.ForeignKey(
                        name: "GameTypeTranslation_GameTypeId_fkey",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "GameTypeTranslation_LangId_fkey",
                        column: x => x.LangId,
                        principalTable: "Lang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Surname = table.Column<string>(type: "character varying", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: false),
                    CreatedDatetime = table.Column<DateTime>(type: "timestamp(6) without time zone", precision: 6, nullable: false),
                    LangId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "User_LangId_fkey",
                        column: x => x.LangId,
                        principalTable: "Lang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizationProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "character varying", nullable: true),
                    AuthorizationProviderTypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationProvider", x => x.Id);
                    table.ForeignKey(
                        name: "AuthorizationProvider_AuthorizationProviderTypeId_fkey",
                        column: x => x.AuthorizationProviderTypeId,
                        principalTable: "AuthorizationProviderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying", nullable: false),
                    IsTrue = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "Answer_QuestionId_fkey",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTranslation",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    LangId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuestionTranslation_pkey", x => new { x.QuestionId, x.LangId });
                    table.ForeignKey(
                        name: "QuestionTranslation_LangId_fkey",
                        column: x => x.LangId,
                        principalTable: "Lang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "QuestionTranslation_QuestionId_fkey",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameQuestion",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    IsCurrent = table.Column<bool>(name: "IsCurrent ", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GameQuestion_pkey", x => new { x.QuestionId, x.GameId });
                    table.ForeignKey(
                        name: "GameQuestion_GameId_fkey",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "GameQuestion_QuestionId_fkey",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSecurity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AuthorizationCenterId = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "character varying", nullable: true),
                    Password = table.Column<string>(type: "character varying", nullable: true),
                    RealId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserSecurity_pkey", x => new { x.UserId, x.AuthorizationCenterId });
                    table.ForeignKey(
                        name: "UserSecurity_AuthorizationCenterId_fkey",
                        column: x => x.AuthorizationCenterId,
                        principalTable: "AuthorizationCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "UserSecurity_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameUser",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp(6) without time zone", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GameUser_pkey", x => new { x.GameId, x.UserId });
                    table.ForeignKey(
                        name: "GameUser_GameId_fkey",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "GameUser_ProviderId_fkey",
                        column: x => x.ProviderId,
                        principalTable: "AuthorizationProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "GameUser_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('notification_id_seq'::regclass)"),
                    Text = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "Notification_ProviderId_fkey",
                        column: x => x.ProviderId,
                        principalTable: "AuthorizationProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Notification_TypeId_fkey",
                        column: x => x.TypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Notification_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('search_game_id_seq'::regclass)"),
                    GameTypeId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchGame", x => x.Id);
                    table.ForeignKey(
                        name: "SearchGame_CategoryId_fkey",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SearchGame_GameTypeId_fkey",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SearchGame_ProviderId_fkey",
                        column: x => x.ProviderId,
                        principalTable: "AuthorizationProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SearchGame_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SendQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('send_question_id_seq'::regclass)"),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "SendQuestion_GameId_fkey",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SendQuestion_ProviderId_fkey",
                        column: x => x.ProviderId,
                        principalTable: "AuthorizationProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SendQuestion_QuestionId_fkey",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SendQuestion_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerTranslation",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "integer", nullable: false),
                    LangId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AnswerTranslation_pkey", x => new { x.AnswerId, x.LangId });
                    table.ForeignKey(
                        name: "AnswerTranslation_AnswerId_fkey",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "AnswerTranslation_LangId_fkey",
                        column: x => x.LangId,
                        principalTable: "Lang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDatetime = table.Column<DateTime>(type: "timestamp(6) without time zone", precision: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserAnswer_pkey", x => new { x.UserId, x.GameId, x.QuestionId });
                    table.ForeignKey(
                        name: "UserAnswer_AnswerId_fkey",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "UserAnswer_GameId_fkey",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "UserAnswer_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTranslation_LangId",
                table: "AnswerTranslation",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizationProvider_AuthorizationProviderTypeId",
                table: "AuthorizationProvider",
                column: "AuthorizationProviderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizationProviderType_AuthorizationProviderCenterId",
                table: "AuthorizationProviderType",
                column: "AuthorizationProviderCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslation_LangId",
                table: "CategoryTranslation",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_CategoryId",
                table: "Game",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameTypeId",
                table: "Game",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GameQuestion_GameId",
                table: "GameQuestion",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTypeTranslation_LangId",
                table: "GameTypeTranslation",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_ProviderId",
                table: "GameUser",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_UserId",
                table: "GameUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ProviderId",
                table: "Notification",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_TypeId",
                table: "Notification",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CategoryId",
                table: "Question",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTranslation_LangId",
                table: "QuestionTranslation",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchGame_CategoryId",
                table: "SearchGame",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchGame_GameTypeId",
                table: "SearchGame",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchGame_ProviderId",
                table: "SearchGame",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchGame_UserId",
                table: "SearchGame",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SendQuestion_GameId",
                table: "SendQuestion",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SendQuestion_ProviderId",
                table: "SendQuestion",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_SendQuestion_QuestionId",
                table: "SendQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SendQuestion_UserId",
                table: "SendQuestion",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LangId",
                table: "User",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_AnswerId",
                table: "UserAnswer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_GameId",
                table: "UserAnswer",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSecurity_AuthorizationCenterId",
                table: "UserSecurity",
                column: "AuthorizationCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerTranslation");

            migrationBuilder.DropTable(
                name: "CategoryTranslation");

            migrationBuilder.DropTable(
                name: "GameQuestion");

            migrationBuilder.DropTable(
                name: "GameTypeTranslation");

            migrationBuilder.DropTable(
                name: "GameUser");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "QuestionTranslation");

            migrationBuilder.DropTable(
                name: "SearchGame");

            migrationBuilder.DropTable(
                name: "SendQuestion");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "UserSecurity");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "AuthorizationProvider");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AuthorizationProviderType");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "GameType");

            migrationBuilder.DropTable(
                name: "Lang");

            migrationBuilder.DropTable(
                name: "AuthorizationCenter");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropSequence(
                name: "category_id_seq");

            migrationBuilder.DropSequence(
                name: "game_type_id_seq");

            migrationBuilder.DropSequence(
                name: "notification_id_seq");

            migrationBuilder.DropSequence(
                name: "notification_type_id_seq");

            migrationBuilder.DropSequence(
                name: "search_game_id_seq");

            migrationBuilder.DropSequence(
                name: "send_question_id_seq");
        }
    }
}
