﻿using System;
using System.Data.Entity;
using System.Linq;
using TravelPlain.Data.Models;

namespace TravelPlain.Data.EF
{
    public class TravelPlainContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<BusinessValue> BusinessValues { get; set; }

        public TravelPlainContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new TravelPlainInitializer());
        }
    }
}
