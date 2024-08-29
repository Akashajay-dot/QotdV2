using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QOTD.Web.Models
{
    public class AppdbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<AnswerKey> AnswerKeys { get; set; }
        public DbSet<ReputationMaster> ReputationMaster { get; set; }
        public DbSet<UserResponse> UserResponse { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}