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
    [Migration("20210303152824_requiredFields")]
    partial class requiredFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("AlbumNoteAppFile", b =>
                {
                    b.Property<Guid>("AlbumNotesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PhotosId")
                        .HasColumnType("uuid");

                    b.HasKey("AlbumNotesId", "PhotosId");

                    b.HasIndex("PhotosId");

                    b.ToTable("AlbumNoteAppFile");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.AppFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Backgrounds", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

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

                    b.Property<Guid>("FolderTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("RefTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FolderTypeId");

                    b.HasIndex("RefTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.FolderType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FoldersTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a80c6e40-0326-4466-b200-173eed7de458"),
                            Name = "Private"
                        },
                        new
                        {
                            Id = new Guid("decf8345-8287-4a6d-bc40-189418b2665e"),
                            Name = "Shared"
                        },
                        new
                        {
                            Id = new Guid("e4a902e9-428c-4e85-a501-e76f505d02ad"),
                            Name = "Deleted"
                        },
                        new
                        {
                            Id = new Guid("9af0fde5-e710-453e-93de-48a5943b4315"),
                            Name = "Archive"
                        });
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

            modelBuilder.Entity("Common.DatabaseModels.models.FontSize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FontSizes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("86782d1f-ffc3-4b4b-bcb1-f9a473273004"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("ff4b6aef-2113-4a07-bd40-42c6b8dae518"),
                            Name = "Big"
                        });
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Label", b =>
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.LabelsNotes", b =>
                {
                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LabelId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("NoteId", "LabelId");

                    b.HasIndex("LabelId");

                    b.ToTable("LabelsNotes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4dd4d3f4-ff8f-4b62-9267-a0f1bd7816fc"),
                            Name = "Ukraine"
                        },
                        new
                        {
                            Id = new Guid("d36886ae-c9bf-4c56-b147-273e9f63fe26"),
                            Name = "Russian"
                        },
                        new
                        {
                            Id = new Guid("e607a029-53ec-4213-aa3e-74336eaab35d"),
                            Name = "English"
                        });
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

                    b.Property<Guid>("NoteTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("RefTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NoteTypeId");

                    b.HasIndex("RefTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteContent.BaseNoteContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("NoteId")
                        .HasColumnType("uuid");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("BaseNoteContents");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NotesTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2cb46ba3-88e3-4933-a46f-d0b121fa9dfd"),
                            Name = "Private"
                        },
                        new
                        {
                            Id = new Guid("59fab245-5381-4f21-abe8-94a3fcbf91a9"),
                            Name = "Shared"
                        },
                        new
                        {
                            Id = new Guid("15ae25ad-2872-4942-8761-3ff33cad47dd"),
                            Name = "Deleted"
                        },
                        new
                        {
                            Id = new Guid("76c7849d-9407-48fa-ac26-449b4879222c"),
                            Name = "Archive"
                        });
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NotificationSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("NotificationSettings");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.RefType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RefTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a54c3821-2e20-497d-ae6c-0b65df49d48a"),
                            Name = "Viewer"
                        },
                        new
                        {
                            Id = new Guid("b2a544a3-c6e1-4340-8644-bc848c88742f"),
                            Name = "Editor"
                        });
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Theme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Themes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("61618455-6fe5-4206-b520-fdf2810ca723"),
                            Name = "Light"
                        },
                        new
                        {
                            Id = new Guid("35019d7b-dedd-4901-9257-534f1e5edf84"),
                            Name = "Dark"
                        });
                });

            modelBuilder.Entity("Common.DatabaseModels.models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CurrentBackgroundId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("FontSizeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PersonalKey")
                        .HasColumnType("text");

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ThemeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CurrentBackgroundId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("FontSizeId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.HasIndex("ThemeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UserOnNoteNow", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccessTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("NoteId", "UserId");

                    b.HasIndex("AccessTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOnPrivateNotes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UsersOnPrivateFolders", b =>
                {
                    b.Property<Guid>("FolderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccessTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("FolderId", "UserId");

                    b.HasIndex("AccessTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersOnPrivateFolders");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteContent.AlbumNote", b =>
                {
                    b.HasBaseType("Common.DatabaseModels.models.NoteContent.BaseNoteContent");

                    b.ToTable("AlbumNote");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteContent.TextNote", b =>
                {
                    b.HasBaseType("Common.DatabaseModels.models.NoteContent.BaseNoteContent");

                    b.Property<bool>("Checked")
                        .HasColumnType("boolean");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("HeadingType")
                        .HasColumnType("text");

                    b.Property<string>("TextType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("TextNote");
                });

            modelBuilder.Entity("AlbumNoteAppFile", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.NoteContent.AlbumNote", null)
                        .WithMany()
                        .HasForeignKey("AlbumNotesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.AppFile", null)
                        .WithMany()
                        .HasForeignKey("PhotosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Backgrounds", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.AppFile", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Backgrounds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Folder", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.FolderType", "FolderType")
                        .WithMany("Folders")
                        .HasForeignKey("FolderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.RefType", "RefType")
                        .WithMany("Folders")
                        .HasForeignKey("RefTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Folders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FolderType");

                    b.Navigation("RefType");

                    b.Navigation("User");
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

                    b.Navigation("Folder");

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Label", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Labels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

                    b.Navigation("Label");

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Note", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.NoteType", "NoteType")
                        .WithMany("Notes")
                        .HasForeignKey("NoteTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.RefType", "RefType")
                        .WithMany("Notes")
                        .HasForeignKey("RefTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoteType");

                    b.Navigation("RefType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteContent.BaseNoteContent", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Note", "Note")
                        .WithMany("Contents")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NotificationSetting", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.User", "User")
                        .WithOne("NotificationSettings")
                        .HasForeignKey("Common.DatabaseModels.models.NotificationSetting", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.User", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.Backgrounds", "CurrentBackground")
                        .WithOne("CurrentUserBackground")
                        .HasForeignKey("Common.DatabaseModels.models.User", "CurrentBackgroundId");

                    b.HasOne("Common.DatabaseModels.models.FontSize", "FontSize")
                        .WithMany("Users")
                        .HasForeignKey("FontSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.Language", "Language")
                        .WithMany("Users")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.DatabaseModels.models.AppFile", "Photo")
                        .WithOne("User")
                        .HasForeignKey("Common.DatabaseModels.models.User", "PhotoId");

                    b.HasOne("Common.DatabaseModels.models.Theme", "Theme")
                        .WithMany("Users")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentBackground");

                    b.Navigation("FontSize");

                    b.Navigation("Language");

                    b.Navigation("Photo");

                    b.Navigation("Theme");
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

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UserOnPrivateNotes", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.RefType", "AccessType")
                        .WithMany("UserOnPrivateNotes")
                        .HasForeignKey("AccessTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("AccessType");

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.UsersOnPrivateFolders", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.RefType", "AccessType")
                        .WithMany("UsersOnPrivateFolders")
                        .HasForeignKey("AccessTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("AccessType");

                    b.Navigation("Folder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteContent.AlbumNote", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.NoteContent.BaseNoteContent", null)
                        .WithOne()
                        .HasForeignKey("Common.DatabaseModels.models.NoteContent.AlbumNote", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteContent.TextNote", b =>
                {
                    b.HasOne("Common.DatabaseModels.models.NoteContent.BaseNoteContent", null)
                        .WithOne()
                        .HasForeignKey("Common.DatabaseModels.models.NoteContent.TextNote", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.DatabaseModels.models.AppFile", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Backgrounds", b =>
                {
                    b.Navigation("CurrentUserBackground");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Folder", b =>
                {
                    b.Navigation("FoldersNotes");

                    b.Navigation("UsersOnPrivateFolders");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.FolderType", b =>
                {
                    b.Navigation("Folders");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.FontSize", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Label", b =>
                {
                    b.Navigation("LabelsNotes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Language", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Note", b =>
                {
                    b.Navigation("Contents");

                    b.Navigation("FoldersNotes");

                    b.Navigation("LabelsNotes");

                    b.Navigation("UserOnNotesNow");

                    b.Navigation("UsersOnPrivateNotes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.NoteType", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.RefType", b =>
                {
                    b.Navigation("Folders");

                    b.Navigation("Notes");

                    b.Navigation("UserOnPrivateNotes");

                    b.Navigation("UsersOnPrivateFolders");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.Theme", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Common.DatabaseModels.models.User", b =>
                {
                    b.Navigation("Backgrounds");

                    b.Navigation("Folders");

                    b.Navigation("Labels");

                    b.Navigation("Notes");

                    b.Navigation("NotificationSettings");

                    b.Navigation("UserOnNotes");

                    b.Navigation("UserOnPrivateNotes");

                    b.Navigation("UsersOnPrivateFolders");
                });
#pragma warning restore 612, 618
        }
    }
}
