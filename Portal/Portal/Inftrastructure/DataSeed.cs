using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using System.Net.Mail;

namespace Portal.Inftrastructure
{
    public class DataSeed
    {
        public static void SeedNews(ModelBuilder builder)
        {

            builder.Entity<News>().HasData(
               new News
               {
                   Id = 1,
                   Title = "Новости комнании 1",
                   Content = "Текст новости 1",
                   ViewsCount = 0,
                   LikesCount = 0
               },
               new News
               {
                   Id = 2,
                   Title = "Новости комнании 2",
                   Content = "Текст новости 2",
                   ViewsCount = 0,
                   LikesCount = 0
               });
        }
    }
}
