using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using designhub.Data;

namespace designhub.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170911181454_model-update")]
    partial class modelupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("designhub.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("designhub.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FileID");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("CommentID");

                    b.HasIndex("FileID");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("designhub.Models.File", b =>
                {
                    b.Property<int>("FileID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("FileGroupID");

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("FileID");

                    b.HasIndex("FileGroupID");

                    b.HasIndex("UserId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("designhub.Models.FileGroup", b =>
                {
                    b.Property<int>("FileGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("FileGroupID");

                    b.ToTable("FileGroup");
                });

            modelBuilder.Entity("designhub.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("ProjectID");

                    b.HasIndex("UserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("designhub.Models.ProjectFileGroup", b =>
                {
                    b.Property<int>("ProjectFileGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("FileGroupID");

                    b.Property<int>("ProjectID");

                    b.HasKey("ProjectFileGroupID");

                    b.HasIndex("FileGroupID");

                    b.HasIndex("ProjectID");

                    b.ToTable("ProjectFileGroup");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("designhub.Models.Comment", b =>
                {
                    b.HasOne("designhub.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("designhub.Models.ApplicationUser", "User")
                        .WithMany("Comment")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("designhub.Models.File", b =>
                {
                    b.HasOne("designhub.Models.FileGroup", "FileGroup")
                        .WithMany()
                        .HasForeignKey("FileGroupID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("designhub.Models.ApplicationUser", "User")
                        .WithMany("File")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("designhub.Models.Project", b =>
                {
                    b.HasOne("designhub.Models.ApplicationUser", "User")
                        .WithMany("Project")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("designhub.Models.ProjectFileGroup", b =>
                {
                    b.HasOne("designhub.Models.FileGroup", "FileGroup")
                        .WithMany("ProjectFileGroup")
                        .HasForeignKey("FileGroupID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("designhub.Models.Project", "Project")
                        .WithMany("ProjectFileGroup")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("designhub.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("designhub.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("designhub.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
