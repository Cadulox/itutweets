﻿// <auto-generated />
using System;
using ItuTweets.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ItuTweets.Data.Migrations
{
    [DbContext(typeof(ItuTweetsContext))]
    [Migration("20220516211216_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ItuTweets.Domain.Models.Tweet", b =>
                {
                    b.Property<string>("Uuid")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Author_id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Id")
                        .HasColumnType("longtext");

                    b.Property<string>("Lang")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Uuid");

                    b.ToTable("tb_tweets", (string)null);
                });

            modelBuilder.Entity("ItuTweets.Domain.Models.TwitterUser", b =>
                {
                    b.Property<string>("Uuid")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("FollowersCount")
                        .HasColumnType("int");

                    b.Property<int>("FollowingCount")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("longtext");

                    b.Property<int>("ListedCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("TweetCount")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Uuid");

                    b.ToTable("tb_twitter_users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}