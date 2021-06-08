﻿// <auto-generated />
using System;
using Accord.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accord.Domain.Migrations
{
    [DbContext(typeof(AccordContext))]
    partial class AccordContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Accord.Domain.Model.ChannelFlag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("DiscordChannelId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiscordChannelId", "Type")
                        .IsUnique();

                    b.ToTable("ChannelFlags");
                });

            modelBuilder.Entity("Accord.Domain.Model.User", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTimeOffset>("FirstSeenDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("LastSeenDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<float>("Xp")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
