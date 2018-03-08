﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RecipeBook.DataAccess;
using System;

namespace RecipeBook.DataAccess.Migrations
{
    [DbContext(typeof(RecipeBookContext))]
    [Migration("20180308000712_removing-pk-recipebook")]
    partial class removingpkrecipebook
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RecipeBook.DataAccess.Models.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("RecipeBook.DataAccess.Models.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("ID");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("RecipeBook.DataAccess.Models.RecipeBook", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<int>("RecipeID");

                    b.HasKey("BookID", "RecipeID");

                    b.HasIndex("RecipeID");

                    b.ToTable("RecipeBook");
                });

            modelBuilder.Entity("RecipeBook.DataAccess.Models.RecipeBook", b =>
                {
                    b.HasOne("RecipeBook.DataAccess.Models.Book", "Book")
                        .WithMany("RecipeBooks")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RecipeBook.DataAccess.Models.Recipe", "Recipe")
                        .WithMany("RecipeBooks")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
