using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TApi
{
    public class ConnectionDataBase : DbContext
    {
        public DbSet<UserDataVkDB> UserDataVkDB { get; set; } = null!;

        public ConnectionDataBase()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=VkUserInfo;Username=postgres;Password=qwerty123456");
        }
    }
}
