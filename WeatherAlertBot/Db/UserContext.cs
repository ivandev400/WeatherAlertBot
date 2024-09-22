﻿using Microsoft.EntityFrameworkCore;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Db
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserSettings)
                .WithOne(us => us.User) 
                .HasForeignKey<UserSettings>(us => us.UserID); 
        }
    }
}
