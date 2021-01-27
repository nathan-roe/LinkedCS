﻿// <auto-generated />
using System;
using LinkedCS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LinkedCS.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210124184103_LinkedCSMigration")]
    partial class LinkedCSMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LinkedCS.Models.Bookmark", b =>
                {
                    b.Property<int>("BookmarkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PostBookmarkedId")
                        .HasColumnType("int");

                    b.Property<int>("UserWhoBookmarkedId")
                        .HasColumnType("int");

                    b.HasKey("BookmarkId");

                    b.HasIndex("PostBookmarkedId");

                    b.HasIndex("UserWhoBookmarkedId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("LinkedCS.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("LinkedCS.Models.LikedPost", b =>
                {
                    b.Property<int>("LikedPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PostLikedId")
                        .HasColumnType("int");

                    b.Property<int>("UserWhoLikedId")
                        .HasColumnType("int");

                    b.HasKey("LikedPostId");

                    b.HasIndex("PostLikedId");

                    b.HasIndex("UserWhoLikedId");

                    b.ToTable("LikedPosts");
                });

            modelBuilder.Entity("LinkedCS.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Article")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Desc")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Image")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Video")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("LinkedCS.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("HasLogged")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Summary")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LinkedCS.Models.UserConnection", b =>
                {
                    b.Property<int>("UserConnectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FollowerId")
                        .HasColumnType("int");

                    b.Property<int>("UserFollowedId")
                        .HasColumnType("int");

                    b.HasKey("UserConnectionId");

                    b.HasIndex("FollowerId");

                    b.HasIndex("UserFollowedId");

                    b.ToTable("UserConnections");
                });

            modelBuilder.Entity("LinkedCS.Models.UserView", b =>
                {
                    b.Property<int>("UserViewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("UserViewedId")
                        .HasColumnType("int");

                    b.Property<int>("ViewerId")
                        .HasColumnType("int");

                    b.HasKey("UserViewId");

                    b.HasIndex("UserViewedId");

                    b.HasIndex("ViewerId");

                    b.ToTable("UserViews");
                });

            modelBuilder.Entity("LinkedCS.Models.Bookmark", b =>
                {
                    b.HasOne("LinkedCS.Models.Post", "PostBookmarked")
                        .WithMany("UsersWhoBookmarked")
                        .HasForeignKey("PostBookmarkedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LinkedCS.Models.User", "UserWhoBookmarked")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserWhoBookmarkedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkedCS.Models.Comment", b =>
                {
                    b.HasOne("LinkedCS.Models.Post", "PostCommentedOn")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LinkedCS.Models.User", "UserWhoCommented")
                        .WithMany("UserComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkedCS.Models.LikedPost", b =>
                {
                    b.HasOne("LinkedCS.Models.Post", "PostLiked")
                        .WithMany("UsersWhoLiked")
                        .HasForeignKey("PostLikedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LinkedCS.Models.User", "UserWhoLiked")
                        .WithMany("LikedPosts")
                        .HasForeignKey("UserWhoLikedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkedCS.Models.Post", b =>
                {
                    b.HasOne("LinkedCS.Models.User", "UserWhoPosted")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkedCS.Models.UserConnection", b =>
                {
                    b.HasOne("LinkedCS.Models.User", "Follower")
                        .WithMany("UsersFollowed")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LinkedCS.Models.User", "UserFollowed")
                        .WithMany("Followers")
                        .HasForeignKey("UserFollowedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinkedCS.Models.UserView", b =>
                {
                    b.HasOne("LinkedCS.Models.User", "UserViewed")
                        .WithMany("ViewedUsers")
                        .HasForeignKey("UserViewedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LinkedCS.Models.User", "Viewer")
                        .WithMany("Viewers")
                        .HasForeignKey("ViewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}