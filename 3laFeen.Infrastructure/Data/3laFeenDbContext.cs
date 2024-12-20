using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3laFeen.Domain.Entities;

namespace _3laFeen.Infrastructure.Data
{
    public class _3laFeenDbContext : IdentityDbContext
    {


        public _3laFeenDbContext(DbContextOptions<_3laFeenDbContext>options): base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<User> users { get; set; }
        public DbSet<Route> routes { get; set; }

    }
}
