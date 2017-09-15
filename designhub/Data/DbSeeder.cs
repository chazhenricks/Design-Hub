using designhub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designhub.Data
{
    public class DbSeeder
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                //Look for Projects
                if (context.Project.Any())
                {
                    return; //this means that the table exists and doesnt need to run
                }

                //***********************************
                //Create new instances of Users
                //***********************************

                var userStore = new UserStore<ApplicationUser>(context);

                var userList = new ApplicationUser[]
                {
                    new ApplicationUser {
                        UserName = "a@a.com",
                        NormalizedUserName = "A@A.COM",
                        Email = "a@a.com",
                        NormalizedEmail = "A@A.com",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        FirstName = "Bob",
                        LastName = "Smith"
                  
                    },
                    new ApplicationUser {
                        UserName = "b@b.com",
                        NormalizedUserName = "B@B.COM",
                        Email = "b@b.com",
                        NormalizedEmail = "B@B.com",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        FirstName = "Jenny",
                        LastName = "Frazier"
                    },
                    new ApplicationUser
                    {
                        UserName = "c@c.com",
                        NormalizedUserName = "C@C.COM",
                        Email = "c@c.com",
                        NormalizedEmail = "C@C.com",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        FirstName = "Tinker",
                        LastName = "Bell",
                    }
                };


                foreach (ApplicationUser user in userList)
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "password");
                    user.PasswordHash = hashed;
                    await userStore.CreateAsync(user);
                }
                await context.SaveChangesAsync();


                //***********************************
                //Create new instances of Projects
                //***********************************
                var projects = new Project[]
                {
                    new Project{
                       Name = "Walmart Campaign",
                       DateCreated = DateTime.Now,
                       User = userStore.Users.First<ApplicationUser>(user => user.UserName == "a@a.com")
                    },
                    new Project{
                        Name = "LP Campaign",
                        DateCreated = new DateTime(2016, 03, 28),
                        User = userStore.Users.First<ApplicationUser>(user => user.UserName == "b@b.com")
                    },
                    new Project{
                        Name = "Exxon Campaign",
                        DateCreated = new DateTime(2015, 03, 28),
                        User = userStore.Users.First<ApplicationUser>(user => user.UserName == "c@c.com")
                    }
                };
                // Adds each new Project into the context
                foreach (Project p in projects)
                {
                    context.Project.Add(p);
                }
                // Saves the customers to the database
                context.SaveChanges();


                //***********************************
                //Create new instances of DocumentGroup
                //***********************************

                var documentGroups = new DocumentGroup[]
                {
                    new DocumentGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Walmart CEO Photo",
                    },
                    new DocumentGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Walmart Speech",
                    },
                    new DocumentGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "LP CEO Photo",
                    },
                    new DocumentGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "LP CEO Speech",
                    },
                    new DocumentGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Exxon CEO Photo",
                    },
                    new DocumentGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Exxon Pitch Proposal",
                    }
                };

                // Adds each new DocumentGroup into the context
                foreach (DocumentGroup dg in documentGroups)
                {
                    context.DocumentGroup.Add(dg);
                }
                // Saves the customers to the database
                context.SaveChanges();

                //***********************************
                //Create new instances of Document
                //***********************************

                var documents = new Document[]
                {
                    new Document{
                        DateCreated = new DateTime(2017, 03, 28),
                        DocumentPath = "http://localhost:5000/Document/getpicture",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Walmart CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 29),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Walmart CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 30),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Walmart CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 28),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Walmart Speech").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 29),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Walmart Speech").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 30),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Walmart Speech").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 28),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "LP CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 29),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "LP CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 30),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "LP CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 28),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "LP CEO Speech").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 29),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "LP CEO Speech").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 30),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "LP CEO Speech").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 28),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Exxon CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 29),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Exxon CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 30),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Exxon CEO Photo").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 28),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Exxon Pitch Proposal").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 29),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Exxon Pitch Proposal").DocumentGroupID
                    },
                    new Document{
                        DateCreated = new DateTime(2017, 03, 30),
                        DocumentPath = "Path/To/Document",
                        DocumentGroupID = documentGroups.Single(fg => fg.Name == "Exxon Pitch Proposal").DocumentGroupID
                    }
                };
                // Adds each new Document into the context
                foreach (Document d in documents)
                {
                    context.Document.Add(d);
                }
                // Saves the customers to the database
                context.SaveChanges();

                //***********************************
                //Create new instances of Comments
                //***********************************

                var comments = new Comment[]{
                    new Comment{
                        Message = "Change one thing",
                        DateCreated = new DateTime(2017, 03, 30),
                        DocumentID = documents.Single(f => f.DocumentID == 1).DocumentID
                    },
                    new Comment{
                        Message = "Great!",
                        DateCreated = new DateTime(2017, 03, 31),
                        DocumentID = documents.Single(f => f.DocumentID == 16).DocumentID
                    }
                };

                foreach (Comment c in comments)
                {
                    context.Comment.Add(c);
                }
                context.SaveChanges();



                //***********************************
                //Create new instances of ProjectDocumentGroups
                //***********************************

                var projectDocumentGroups = new ProjectDocumentGroup[]{
                    new ProjectDocumentGroup{
                        ProjectID = 1,
                        DocumentGroupID = 1
                    },
                     new ProjectDocumentGroup{
                        ProjectID = 1,
                        DocumentGroupID = 2
                    },
                     new ProjectDocumentGroup{
                        ProjectID = 2,
                        DocumentGroupID = 3
                    },
                     new ProjectDocumentGroup{
                        ProjectID = 2,
                        DocumentGroupID = 4
                    },
                     new ProjectDocumentGroup{
                        ProjectID = 3,
                        DocumentGroupID = 5
                    },
                     new ProjectDocumentGroup{
                        ProjectID = 3,
                        DocumentGroupID = 6
                    }
                };

                foreach (ProjectDocumentGroup pfg in projectDocumentGroups)
                {
                    context.ProjectDocumentGroup.Add(pfg);
                }

                context.SaveChanges();

            }//End Using
        }//End Intialize method
    }
}
