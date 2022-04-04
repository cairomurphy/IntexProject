﻿using System;
using Microsoft.EntityFrameworkCore;

namespace IntexProject.Models
{
    public class CrashDbContext : DbContext
    {
        public CrashDbContext(DbContextOptions<CrashDbContext> options) : base(options)
        {

        }

        public DbSet<Crash> Crashes { get; set; }
    }
}