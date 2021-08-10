using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

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

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Hasan",
                        Surname = "Doğan",
                        BirthDate = new DateTime(1970, 12, 12)
                    },
                    new Author
                    {
                        Name = "Ali",
                        Surname = "Doğanay",
                        BirthDate = new DateTime(1983, 11, 11),
                    },
                    new Author
                    {
                        Name = "Halide",
                        Surname = "Gürsoy",
                        BirthDate = new DateTime(1968, 10, 10),
                    },
                    new Author
                    {
                        Name = "Hüseyin",
                        Surname = "Çalı",
                        BirthDate = new DateTime(1978, 03, 11),
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Novel"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 254,
                        PublishDate = System.DateTime.Now.AddDays(-124),
                        AuthorId = 1,
                        IsActive = false
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 311,
                        PublishDate = System.DateTime.Now.AddDays(-753),
                        AuthorId = 1
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 3,
                        PageCount = 187,
                        PublishDate = System.DateTime.Now.AddDays(-582),
                        AuthorId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}