using BookStoreApp.Core.Entities;
using BookStoreApp.Persistence.Contexts;
using BookStoreApp.WebAPI.Data.FakeContext;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = _context.Books
                .Where(x => x.Id.Equals(id))
                .SingleOrDefault();

            if (book is null)
                return NotFound(); //404

            return Ok(book);
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();

                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
                if (entity == null)
                    return BadRequest();

                if (id != book.Id)
                    return BadRequest();

                entity.Title = book.Title;
                entity.Price = book.Price;

                _context.SaveChanges();
                return StatusCode(200,book.Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var deletedEntity = _context.Books.Where(x => x.Id == id).FirstOrDefault();
                if (deletedEntity == null)
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"Book with id: {id} couldn't found."
                    });
                _context.Books.Remove(deletedEntity);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
     
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, JsonPatchDocument<Book> bookPatch)
        {
            //check Entity ?
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).FirstOrDefault();
                if (entity == null)
                    return NotFound();
                // parametre olarak gelen yamayı takip edilen mevcut entity'e yansıtalım.
                bookPatch.ApplyTo(entity);
                // daha sonra db'ye yanstıalım.
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            

          

          
        }

    }
}
