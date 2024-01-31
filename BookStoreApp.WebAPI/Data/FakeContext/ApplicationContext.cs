using BookStoreApp.WebAPI.Models;

namespace BookStoreApp.WebAPI.Data.FakeContext
{
    public static class ApplicationContext
    {
        public static List<Book> Books { get; set; }
        static ApplicationContext()
        {
            Books = new()
            {
                new()
                {
                    Id = 1,
                    Price = 100,
                    Title = "Vatanım"
                },
                new()
                {
                    Id = 2,
                    Price = 300,
                    Title = "Memleket Sevdam"
                },
                new()
                {
                    Id = 3,
                    Price = 350,
                    Title = "Ay Yıldızlı Albayrak Göklere"
                }
            };
        }
    }
}
