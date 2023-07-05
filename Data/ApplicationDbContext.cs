using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RentCarsApp.Models;
using Azure.Identity;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RentCarsApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Order> Orders { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
