using BookStoreApp.WebAPI.Data.FakeContext;
using BookStoreApp.WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")] int id)
        {
            var book  = ApplicationContext.Books
                .Where(x => x.Id.Equals(id))
                .SingleOrDefault();

            if(book is null)
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

                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")]int id, [FromBody]Book book)
        {
            var entity = ApplicationContext.Books.Find(x => x.Id.Equals(id));
            if(entity == null)
                return BadRequest();

            if (id != book.Id)
                return BadRequest();

            ApplicationContext.Books.Remove(entity);
            ApplicationContext.Books.Add(book);
            return Ok(book.Id);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")]int id)
        {
            try
            {
                var deletedEntity = ApplicationContext.Books.Where(x => x.Id == id).FirstOrDefault();
                if (deletedEntity == null)
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = $"Book with id: {id} couldn't found."
                    });
                ApplicationContext.Books.Remove(deletedEntity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
            
            
        }
        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name ="id")]int id, JsonPatchDocument<Book> bookPatch)
        {

        }

    }
}
