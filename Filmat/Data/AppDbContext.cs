﻿using Filmat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Filmat.Areas.Identity.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }



    public DbSet<Movie> Movies { get; set; }
    public DbSet<Client> Clients { get; set; }

}

//protected override void OnModelCreating(ModelBuilder builder)
//{
//	base.OnModelCreating(builder);
//	builder.Seed();

//}
//}
