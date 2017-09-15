using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using designhub.Models;

namespace designhub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Document> Document { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<DocumentGroup> DocumentGroup { get; set; }

        public DbSet<ProjectDocumentGroup> ProjectDocumentGroup { get; set; }


    }
}
