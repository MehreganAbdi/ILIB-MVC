using ILIb1._1.Models;
using System.Collections.Generic;

﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ILIb1._1.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser >
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Loan> Loans { get; set; }
      
        public DbSet<AppUser> Users { get; set; }


    }
}
