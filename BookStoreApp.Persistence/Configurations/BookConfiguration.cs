using BookStoreApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreApp.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(new Book[]
            {
                new()
                {
                    Id = 1,
                    Price = 100,
                    Title = "Vatan Sevdası"
                },
                new()
                {
                    Id = 2,
                    Price = 200,
                    Title = "Karagöz ve Hacivat"
                },
                new()
                {
                    Id = 3,
                    Price = 200,
                    Title = "Memleket Meselesi"
                }
            });
        }
    }
}
