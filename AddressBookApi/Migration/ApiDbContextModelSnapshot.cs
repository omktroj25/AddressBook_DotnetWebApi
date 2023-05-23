﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Entity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AddressBookApi.Migration
{
    [DbContext(typeof(ApiDbContext))]
    [ExcludeFromCodeCoverage]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CountryTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Line2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Zipcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId");

                    b.HasIndex("AddressTypeId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Entities.Models.AddressBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AddressBooks");
                });

            modelBuilder.Entity("Entities.Models.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("FileContent")
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId")
                        .IsUnique();

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Entities.Models.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EmailTypeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId");

                    b.HasIndex("EmailTypeId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Entities.Models.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PhoneTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId");

                    b.HasIndex("PhoneTypeId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Entities.Models.RefSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RefSets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("30bd3b7b-0524-4566-837d-4f5f2d5ac3d1"),
                            Description = "Email details",
                            Key = "EMAIL_ADDRESS_TYPE"
                        },
                        new
                        {
                            Id = new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"),
                            Description = "Phone details",
                            Key = "PHONE_NUMBER_TYPE"
                        },
                        new
                        {
                            Id = new Guid("17feeaba-5027-4bcb-acd7-9c90ba535c58"),
                            Description = "Address details",
                            Key = "ADDRESS_TYPE"
                        },
                        new
                        {
                            Id = new Guid("dfed98f9-eca9-41bb-8987-f2bc6871b273"),
                            Description = "Country details",
                            Key = "COUNTRY"
                        });
                });

            modelBuilder.Entity("Entities.Models.RefTerm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SortOrder")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RefTerms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e01000f7-473c-4747-a963-65553eaf43cd"),
                            Description = "Personal details",
                            Key = "PERSONAL",
                            SortOrder = 1
                        },
                        new
                        {
                            Id = new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a"),
                            Description = "Work details",
                            Key = "WORK",
                            SortOrder = 2
                        },
                        new
                        {
                            Id = new Guid("3fa7986c-4024-4b47-a942-6a19d956b5a1"),
                            Description = "Alternate details",
                            Key = "ALTERNATE",
                            SortOrder = 3
                        },
                        new
                        {
                            Id = new Guid("3db5e2dd-8220-4cfa-81f9-e638ce92bf66"),
                            Description = "India",
                            Key = "INDIA",
                            SortOrder = 4
                        },
                        new
                        {
                            Id = new Guid("11b7d63c-a713-44a0-9cdb-ac88401f39bb"),
                            Description = "United States",
                            Key = "USA",
                            SortOrder = 4
                        });
                });

            modelBuilder.Entity("Entities.Models.SetRefTerm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("RefSetId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RefTermId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RefSetId");

                    b.HasIndex("RefTermId");

                    b.ToTable("SetRefTerms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("52e1ff88-0409-40cf-8b21-9c4cae9f2265"),
                            RefSetId = new Guid("30bd3b7b-0524-4566-837d-4f5f2d5ac3d1"),
                            RefTermId = new Guid("e01000f7-473c-4747-a963-65553eaf43cd")
                        },
                        new
                        {
                            Id = new Guid("ea900810-c569-402d-b4e7-d85532b444ce"),
                            RefSetId = new Guid("30bd3b7b-0524-4566-837d-4f5f2d5ac3d1"),
                            RefTermId = new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a")
                        },
                        new
                        {
                            Id = new Guid("48f32817-4a12-492f-a117-03051cb43855"),
                            RefSetId = new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"),
                            RefTermId = new Guid("e01000f7-473c-4747-a963-65553eaf43cd")
                        },
                        new
                        {
                            Id = new Guid("aec4a11a-e2bb-47a5-93bf-2df4f531365a"),
                            RefSetId = new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"),
                            RefTermId = new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a")
                        },
                        new
                        {
                            Id = new Guid("114a627d-2993-4957-a59a-985b6e4d7e85"),
                            RefSetId = new Guid("6bb56121-f6b8-4b2e-9ec8-45fca6125433"),
                            RefTermId = new Guid("3fa7986c-4024-4b47-a942-6a19d956b5a1")
                        },
                        new
                        {
                            Id = new Guid("2b9c50b5-4423-409b-9c68-60570055d345"),
                            RefSetId = new Guid("17feeaba-5027-4bcb-acd7-9c90ba535c58"),
                            RefTermId = new Guid("e01000f7-473c-4747-a963-65553eaf43cd")
                        },
                        new
                        {
                            Id = new Guid("e2e197e3-3114-496c-8c9c-c4e8229515ad"),
                            RefSetId = new Guid("17feeaba-5027-4bcb-acd7-9c90ba535c58"),
                            RefTermId = new Guid("8dceb509-2d75-44bf-89d9-1d58470a3d7a")
                        },
                        new
                        {
                            Id = new Guid("e8233d23-ba69-445a-b3e6-71a97a913013"),
                            RefSetId = new Guid("dfed98f9-eca9-41bb-8987-f2bc6871b273"),
                            RefTermId = new Guid("3db5e2dd-8220-4cfa-81f9-e638ce92bf66")
                        },
                        new
                        {
                            Id = new Guid("6eaa2655-9269-495b-b66e-84a6813b42c6"),
                            RefSetId = new Guid("dfed98f9-eca9-41bb-8987-f2bc6871b273"),
                            RefTermId = new Guid("11b7d63c-a713-44a0-9cdb-ac88401f39bb")
                        });
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c48baa66-6efc-4b7b-9ab1-cbca59485d14"),
                            Password = "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8=",
                            UserName = "User"
                        },
                        new
                        {
                            Id = new Guid("6dd6bc13-3dbb-434d-b0b6-bbd8dc67a668"),
                            Password = "dM0p8PMqmscp69xac484T6OErIqk5WM4qDtV+MzVGdY=",
                            UserName = "Propel"
                        });
                });

            modelBuilder.Entity("Entities.Models.Address", b =>
                {
                    b.HasOne("Entities.Models.AddressBook", "AddressBook")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.RefTerm", "RefTerm")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");

                    b.Navigation("RefTerm");
                });

            modelBuilder.Entity("Entities.Models.AddressBook", b =>
                {
                    b.HasOne("Entities.Models.User", "User")
                        .WithMany("AddressBooks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Models.Asset", b =>
                {
                    b.HasOne("Entities.Models.AddressBook", "AddressBook")
                        .WithOne("Asset")
                        .HasForeignKey("Entities.Models.Asset", "AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");
                });

            modelBuilder.Entity("Entities.Models.Email", b =>
                {
                    b.HasOne("Entities.Models.AddressBook", "AddressBook")
                        .WithMany("Emails")
                        .HasForeignKey("AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.RefTerm", "RefTerm")
                        .WithMany("Emails")
                        .HasForeignKey("EmailTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");

                    b.Navigation("RefTerm");
                });

            modelBuilder.Entity("Entities.Models.Phone", b =>
                {
                    b.HasOne("Entities.Models.AddressBook", "AddressBook")
                        .WithMany("Phones")
                        .HasForeignKey("AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.RefTerm", "RefTerm")
                        .WithMany("Phones")
                        .HasForeignKey("PhoneTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");

                    b.Navigation("RefTerm");
                });

            modelBuilder.Entity("Entities.Models.SetRefTerm", b =>
                {
                    b.HasOne("Entities.Models.RefSet", "RefSet")
                        .WithMany("SetRefTerms")
                        .HasForeignKey("RefSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.RefTerm", "RefTerm")
                        .WithMany("SetRefTerms")
                        .HasForeignKey("RefTermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RefSet");

                    b.Navigation("RefTerm");
                });

            modelBuilder.Entity("Entities.Models.AddressBook", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Asset");

                    b.Navigation("Emails");

                    b.Navigation("Phones");
                });

            modelBuilder.Entity("Entities.Models.RefSet", b =>
                {
                    b.Navigation("SetRefTerms");
                });

            modelBuilder.Entity("Entities.Models.RefTerm", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Emails");

                    b.Navigation("Phones");

                    b.Navigation("SetRefTerms");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Navigation("AddressBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
