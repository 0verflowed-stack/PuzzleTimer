using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace PuzzleTimer {
    public class SolutionsDbContext : DbContext {
        public SolutionsDbContext() {
            Database.EnsureCreated();
        }

        public DbSet<Solution> Solutions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder
           optionsBuilder) {
            string connectionStringBuilder = new
               SqliteConnectionStringBuilder() {
                DataSource = "puzzletimerdb.db"
            }
            .ToString();

            var connection = new SqliteConnection(connectionStringBuilder);
            optionsBuilder.UseSqlite(connection);
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Solution>().HasKey(table => new { table.Id, table.PuzzleName });
        }
    }
}
