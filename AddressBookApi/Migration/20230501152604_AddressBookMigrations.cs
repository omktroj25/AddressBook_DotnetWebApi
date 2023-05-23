using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AddressBookApi.Migration
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class AddressBookMigrations : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SetRefTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RefTermId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefSetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetRefTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetRefTerms_RefSets_RefSetId",
                        column: x => x.RefSetId,
                        principalTable: "RefSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetRefTerms_RefTerms_RefTermId",
                        column: x => x.RefTermId,
                        principalTable: "RefTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Line1 = table.Column<string>(type: "text", nullable: false),
                    Line2 = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Zipcode = table.Column<string>(type: "text", nullable: false),
                    CountryTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AddressBooks_AddressBookId",
                        column: x => x.AddressBookId,
                        principalTable: "AddressBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_RefTerms_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "RefTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    FileContent = table.Column<byte[]>(type: "bytea", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AddressBooks_AddressBookId",
                        column: x => x.AddressBookId,
                        principalTable: "AddressBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_AddressBooks_AddressBookId",
                        column: x => x.AddressBookId,
                        principalTable: "AddressBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emails_RefTerms_EmailTypeId",
                        column: x => x.EmailTypeId,
                        principalTable: "RefTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_AddressBooks_AddressBookId",
                        column: x => x.AddressBookId,
                        principalTable: "AddressBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phones_RefTerms_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "RefTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RefSets",
                columns: new[] { "Id", "Description", "Key" },
                values: new object[,]
                {
                    { new Guid("17feeaba-5027-4bcb-acd7-9c90ba535c58"), "Address details", "ADDRESS_TYPE" },
                    { new Guid("30bd3b7b-0524-4566-837d-4f5f2d5ac3d1"), "Email details", "EMAIL_ADDRESS_TYPE" },
                    { new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"), "Phone details", "PHONE_NUMBER_TYPE" },
                    { new Guid("dfed98f9-eca9-41bb-8987-f2bc6871b273"), "Country details", "COUNTRY" }
                });

            migrationBuilder.InsertData(
                table: "RefTerms",
                columns: new[] { "Id", "Description", "Key", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("11b7d63c-a713-44a0-9cdb-ac88401f39bb"), "United States", "USA", 4 },
                    { new Guid("3db5e2dd-8220-4cfa-81f9-e638ce92bf66"), "India", "INDIA", 4 },
                    { new Guid("3fa7986c-4024-4b47-a942-6a19d956b5a1"), "Alternate details", "ALTERNATE", 3 },
                    { new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a"), "Work details", "WORK", 2 },
                    { new Guid("e01000f7-473c-4747-a963-65553eaf43cd"), "Personal details", "PERSONAL", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("6dd6bc13-3dbb-434d-b0b6-bbd8dc67a668"), "dM0p8PMqmscp69xac484T6OErIqk5WM4qDtV+MzVGdY=", "Propel" },
                    { new Guid("c48baa66-6efc-4b7b-9ab1-cbca59485d14"), "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8=", "User" }
                });

            migrationBuilder.InsertData(
                table: "SetRefTerms",
                columns: new[] { "Id", "RefSetId", "RefTermId" },
                values: new object[,]
                {
                    { new Guid("114a627d-2993-4957-a59a-985b6e4d7e85"), new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"), new Guid("3fa7986c-4024-4b47-a942-6a19d956b5a1") },
                    { new Guid("2b9c50b5-4423-409b-9c68-60570055d345"), new Guid("17feeaba-5027-4bcb-acd7-9c90ba535c58"), new Guid("e01000f7-473c-4747-a963-65553eaf43cd") },
                    { new Guid("48f32817-4a12-492f-a117-03051cb43855"), new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"), new Guid("e01000f7-473c-4747-a963-65553eaf43cd") },
                    { new Guid("52e1ff88-0409-40cf-8b21-9c4cae9f2265"), new Guid("30bd3b7b-0524-4566-837d-4f5f2d5ac3d1"), new Guid("e01000f7-473c-4747-a963-65553eaf43cd") },
                    { new Guid("6eaa2655-9269-495b-b66e-84a6813b42c6"), new Guid("dfed98f9-eca9-41bb-8987-f2bc6871b273"), new Guid("11b7d63c-a713-44a0-9cdb-ac88401f39bb") },
                    { new Guid("aec4a11a-e2bb-47a5-93bf-2df4f531365a"), new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"), new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a") },
                    { new Guid("e2e197e3-3114-496c-8c9c-c4e8229515ad"), new Guid("17feeaba-5027-4bcb-acd7-9c90ba535c58"), new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a") },
                    { new Guid("e8233d23-ba69-445a-b3e6-71a97a913013"), new Guid("dfed98f9-eca9-41bb-8987-f2bc6871b273"), new Guid("3db5e2dd-8220-4cfa-81f9-e638ce92bf66") },
                    { new Guid("ea900810-c569-402d-b4e7-d85532b444ce"), new Guid("30bd3b7b-0524-4566-837d-4f5f2d5ac3d1"), new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressBooks_UserId",
                table: "AddressBooks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressBookId",
                table: "Addresses",
                column: "AddressBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressTypeId",
                table: "Addresses",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AddressBookId",
                table: "Assets",
                column: "AddressBookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_AddressBookId",
                table: "Emails",
                column: "AddressBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_EmailTypeId",
                table: "Emails",
                column: "EmailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_AddressBookId",
                table: "Phones",
                column: "AddressBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PhoneTypeId",
                table: "Phones",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SetRefTerms_RefSetId",
                table: "SetRefTerms",
                column: "RefSetId");

            migrationBuilder.CreateIndex(
                name: "IX_SetRefTerms_RefTermId",
                table: "SetRefTerms",
                column: "RefTermId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "SetRefTerms");

            migrationBuilder.DropTable(
                name: "AddressBooks");

            migrationBuilder.DropTable(
                name: "RefSets");

            migrationBuilder.DropTable(
                name: "RefTerms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
