using Microsoft.EntityFrameworkCore;
using HealthApp.Core.Entities;

namespace HealthApp.Infrastructure
{
    public class DataSeed
    {
        public static void SeedUsers(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   LastName = "Ispanyol",
                   FirstName = "Ilya"
               },
               new User
               {
                   Id = 2,
                   LastName = "Knyzev",
                   FirstName = "Peter"
               }
           );
        }

    }
}
