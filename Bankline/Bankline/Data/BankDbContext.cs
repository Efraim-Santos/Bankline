﻿using Bankline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<BankStatementModel> BankStatement { get; set; }
        public DbSet<TransactionModel> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankLineDbContext).Assembly);
            //modelBuilder.Entity<BankStatementModel>().HasKey(m => m.Id);
            //modelBuilder.Entity<TransactionModel>().HasKey(m => m.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
