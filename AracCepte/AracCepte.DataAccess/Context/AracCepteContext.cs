using AracCepte.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracCepte.DataAccess.Context
{
    public class AracCepteContext : DbContext
    {
        public AracCepteContext(DbContextOptions<AracCepteContext> options): base(options)
        {
            
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Vehicle> Vehicles{ get; set; }
    }

}
