using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace tutoriaBE.Infrastructure.Data.Migrations;
/// <inheritdoc />
public partial class InitialCreation : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Contributors",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          Status = table.Column<int>(type: "integer", nullable: false),
          PhoneNumber_CountryCode = table.Column<string>(type: "text", nullable: true),
          PhoneNumber_Number = table.Column<string>(type: "text", nullable: true),
          PhoneNumber_Extension = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Contributors", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Courses",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
          Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Courses", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
          Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
          PasswordHash = table.Column<string>(type: "text", nullable: false),
          ProfilePhotoPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
          Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
          CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Users", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Tutors",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false),
          Bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Tutors", x => x.Id);
          table.ForeignKey(
                      name: "FK_Tutors_Users_Id",
                      column: x => x.Id,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "ScheduleSlots",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          TutorId = table.Column<int>(type: "integer", nullable: false),
          DayOfWeek = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
          StartHour = table.Column<int>(type: "integer", nullable: false),
          Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_ScheduleSlots", x => x.Id);
          table.ForeignKey(
                      name: "FK_ScheduleSlots_Tutors_TutorId",
                      column: x => x.TutorId,
                      principalTable: "Tutors",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "Sessions",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          TutorId = table.Column<int>(type: "integer", nullable: false),
          StudentId = table.Column<int>(type: "integer", nullable: false),
          CourseId = table.Column<int>(type: "integer", nullable: false),
          Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
          CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Sessions", x => x.Id);
          table.ForeignKey(
                      name: "FK_Sessions_Courses_CourseId",
                      column: x => x.CourseId,
                      principalTable: "Courses",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          table.ForeignKey(
                      name: "FK_Sessions_Tutors_TutorId",
                      column: x => x.TutorId,
                      principalTable: "Tutors",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          table.ForeignKey(
                      name: "FK_Sessions_Users_StudentId",
                      column: x => x.StudentId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
        });

    migrationBuilder.CreateTable(
        name: "TutorCourses",
        columns: table => new
        {
          CourseId = table.Column<int>(type: "integer", nullable: false),
          TutorId = table.Column<int>(type: "integer", nullable: false),
          HourlyPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_TutorCourses", x => new { x.TutorId, x.CourseId });
          table.ForeignKey(
                      name: "FK_TutorCourses_Courses_CourseId",
                      column: x => x.CourseId,
                      principalTable: "Courses",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                      name: "FK_TutorCourses_Tutors_TutorId",
                      column: x => x.TutorId,
                      principalTable: "Tutors",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "Chats",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          SessionId = table.Column<int>(type: "integer", nullable: false),
          IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Chats", x => x.Id);
          table.ForeignKey(
                      name: "FK_Chats_Sessions_SessionId",
                      column: x => x.SessionId,
                      principalTable: "Sessions",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "Reviews",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          SessionId = table.Column<int>(type: "integer", nullable: false),
          Rating = table.Column<decimal>(type: "numeric(2,1)", precision: 2, scale: 1, nullable: false),
          Comment = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
          CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Reviews", x => x.Id);
          table.ForeignKey(
                      name: "FK_Reviews_Sessions_SessionId",
                      column: x => x.SessionId,
                      principalTable: "Sessions",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "SessionSlots",
        columns: table => new
        {
          SessionId = table.Column<int>(type: "integer", nullable: false),
          ScheduleSlotId = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_SessionSlots", x => new { x.SessionId, x.ScheduleSlotId });
          table.ForeignKey(
                      name: "FK_SessionSlots_ScheduleSlots_ScheduleSlotId",
                      column: x => x.ScheduleSlotId,
                      principalTable: "ScheduleSlots",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                      name: "FK_SessionSlots_Sessions_SessionId",
                      column: x => x.SessionId,
                      principalTable: "Sessions",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "Messages",
        columns: table => new
        {
          Id = table.Column<int>(type: "integer", nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
          ChatId = table.Column<int>(type: "integer", nullable: false),
          SenderId = table.Column<int>(type: "integer", nullable: false),
          Content = table.Column<string>(type: "text", nullable: false),
          CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Messages", x => x.Id);
          table.ForeignKey(
                      name: "FK_Messages_Chats_ChatId",
                      column: x => x.ChatId,
                      principalTable: "Chats",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                      name: "FK_Messages_Users_SenderId",
                      column: x => x.SenderId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Chats_SessionId",
        table: "Chats",
        column: "SessionId",
        unique: true);

    migrationBuilder.CreateIndex(
        name: "IX_Messages_ChatId",
        table: "Messages",
        column: "ChatId");

    migrationBuilder.CreateIndex(
        name: "IX_Messages_SenderId",
        table: "Messages",
        column: "SenderId");

    migrationBuilder.CreateIndex(
        name: "IX_Reviews_SessionId",
        table: "Reviews",
        column: "SessionId",
        unique: true);

    migrationBuilder.CreateIndex(
        name: "IX_ScheduleSlots_TutorId",
        table: "ScheduleSlots",
        column: "TutorId");

    migrationBuilder.CreateIndex(
        name: "IX_Sessions_CourseId",
        table: "Sessions",
        column: "CourseId");

    migrationBuilder.CreateIndex(
        name: "IX_Sessions_StudentId",
        table: "Sessions",
        column: "StudentId");

    migrationBuilder.CreateIndex(
        name: "IX_Sessions_TutorId",
        table: "Sessions",
        column: "TutorId");

    migrationBuilder.CreateIndex(
        name: "IX_SessionSlots_ScheduleSlotId",
        table: "SessionSlots",
        column: "ScheduleSlotId");

    migrationBuilder.CreateIndex(
        name: "IX_TutorCourses_CourseId",
        table: "TutorCourses",
        column: "CourseId");

    migrationBuilder.CreateIndex(
        name: "IX_Users_Email",
        table: "Users",
        column: "Email",
        unique: true);
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Contributors");

    migrationBuilder.DropTable(
        name: "Messages");

    migrationBuilder.DropTable(
        name: "Reviews");

    migrationBuilder.DropTable(
        name: "SessionSlots");

    migrationBuilder.DropTable(
        name: "TutorCourses");

    migrationBuilder.DropTable(
        name: "Chats");

    migrationBuilder.DropTable(
        name: "ScheduleSlots");

    migrationBuilder.DropTable(
        name: "Sessions");

    migrationBuilder.DropTable(
        name: "Courses");

    migrationBuilder.DropTable(
        name: "Tutors");

    migrationBuilder.DropTable(
        name: "Users");
  }
}
