using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayMusic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace PlayMusic.Context
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<PlayLists> PlayLists { get; set; }
        public DbSet<Album> album { get; set; }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<Images> Images { get; set; }
    }
}
