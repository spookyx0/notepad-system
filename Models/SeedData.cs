using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyNotesApp.Data;
using System;
using System.Linq;
using MyNotesApp.Models;
namespace MyNotesApp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MyNotesAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MyNotesAppContext>>()))
        {
            if (context.UserModel.Any())
            {
                return;
            }

            context.UserModel.AddRange(
                new UserModel
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "Password123"
                },
                new UserModel
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "SecurePass456"
                },
                new UserModel
                {
                    FirstName = "Michael",
                    LastName = "Johnson",
                    Email = "michael.johnson@example.com",
                    Password = "MySecretPass789"
                },
                new UserModel
                {
                    FirstName = "Emily",
                    LastName = "Davis",
                    Email = "emily.davis@example.com",
                    Password = "SuperSecurePass"
                }
            );

            context.SaveChanges();
        }
    }
}