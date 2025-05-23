using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNotesApp.Models;
namespace MyNotesApp.Data;
    public class MyNotesAppContext : DbContext
    {
        public MyNotesAppContext (DbContextOptions<MyNotesAppContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> UserModel { get; set; } = default!;
        public DbSet<NoteModel> NoteModel { get; set; }
    }
