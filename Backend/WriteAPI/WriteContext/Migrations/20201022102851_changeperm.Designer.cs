﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WriteContext;

namespace WriteContext.Migrations
{
    [DbContext(typeof(WriteContextDB))]
    [Migration("20201022102851_changeperm")]
    partial class changeperm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Common.DatabaseModels.models.Backgrounds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Backgrounds");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Folder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FolderType")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int?>("RefType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.FoldersNotes", b =>
                {
                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FolderId")
                        .HasColumnType("uuid");

                    b.HasKey("NoteId", "FolderId");

                    b.HasIndex("FolderId");

                    b.ToTable("FoldersNotes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.LabelsNotes", b =>
                {
                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.Property<int>("LabelId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("NoteId", "LabelId");

                    b.HasIndex("LabelId");

                    b.ToTable("LabelsNotes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NoteType")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int?>("RefType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NotificationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("NotificationSettings");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.PersonalitionSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FontSize")
                        .HasColumnType("integer");

                    b.Property<int>("Theme")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PersonalitionSettings");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CurrentBackgroundId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("Language")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PersonalKey")
                        .HasColumnType("text");

                    b.Property<string>("PhotoId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrentBackgroundId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UserOnNoteNow", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "NoteId");

                    b.HasIndex("NoteId");

                    b.ToTable("UserOnNoteNow");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UserOnPrivateNotes", b =>
                {
                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("AccessType")
                        .HasColumnType("integer");

                    b.HasKey("NoteId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOnPrivateNotes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UsersOnPrivateFolders", b =>
                {
                    b.Property<Guid>("FolderId")
                        .HasColumnType("uuid");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("AccessType")
                        .HasColumnType("integer");

                    b.HasKey("FolderId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersOnPrivateFolders");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Backgrounds", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Backgrounds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Folder", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Folders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.FoldersNotes", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Folder", "Folder")
                        .WithMany("FoldersNotes")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.Note", "Note")
                        .WithMany("FoldersNotes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Label", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Labels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.LabelsNotes", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Label", "Label")
                        .WithMany("LabelsNotes")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.Note", "Note")
                        .WithMany("LabelsNotes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Note", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NotificationSetting", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithOne("NotificationSettings")
                        .HasForeignKey("Common.DatabaseModels.models.NotificationSetting", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.PersonalitionSetting", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithOne("PersonalitionSettings")
                        .HasForeignKey("Common.DatabaseModels.models.PersonalitionSetting", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.User", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Backgrounds", "CurrentBackground")
                        .WithOne("CurrentUserBackground")
                        .HasForeignKey("Common.DatabaseModels.models.User", "CurrentBackgroundId");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UserOnNoteNow", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Note", "Note")
                        .WithMany("UserOnNotesNow")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("UserOnNotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UserOnPrivateNotes", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Note", "Note")
                        .WithMany("UsersOnPrivateNotes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("UserOnPrivateNotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UsersOnPrivateFolders", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Folder", "Folder")
                        .WithMany("UsersOnPrivateFolders")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("UsersOnPrivateFolders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
