using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Entities.RequestFeatures;
using BookStoreApp.Core.Repositories;
using BookStoreApp.Persistence.Contexts;
using BookStoreApp.Persistence.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Persistence.Repositories
{
    // Abstract olarak tanımlanan baseRepository'deki temel Crudları customRepository yapımızda kullandık.
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);
        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            // belirlenen fiyat aralığına göre listeleme 
            var books = await GetAll(trackChanges)
            .FilterBooks(bookParameters.MinPrice,bookParameters.MaxPrice)
            .Search(bookParameters.SearchTerm)
            .Sort(bookParameters.OrderBy)
            .ToListAsync();
            // sayfalamayı iptal ettik çünkü PagedList içerisinde sayfalama mekanizması tanımladık., sayfalamadan varlıkların tamamını aldık. 

            // hatırlarsanız PagedList içerisinde nesne örneği almamıza yarayacak olan ToPagedList adında bir method yazmıştık, şimdi kullanım sırası geldi.

            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber,bookParameters.PageSize);



        }
        public async Task<Book> GetOneBookByIdAsync(int bookId, bool trackChanges) => await GetByFilter(trackChanges, x => x.Id == bookId).SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
