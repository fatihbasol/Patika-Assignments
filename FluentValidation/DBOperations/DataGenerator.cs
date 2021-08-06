using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entity;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 254,
                        PublishDate = System.DateTime.Now.AddDays(-124),
                    },
                    new Book
                    {
                        // Id = 2,
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 311,
                        PublishDate = System.DateTime.Now.AddDays(-753),
                    },
                    new Book
                    {
                        // Id = 3,
                        Title = "Dune",
                        GenreId = 3,
                        PageCount = 187,
                        PublishDate = System.DateTime.Now.AddDays(-582),
                    }
                );
                context.SaveChanges();
            }
        }
    }
}