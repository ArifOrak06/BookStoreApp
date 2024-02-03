using BookStoreApp.Core.Entities;
using BookStoreApp.Core.Repositories;
using BookStoreApp.Core.Services;
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
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _serviceManager.BookService.GetAllBooks(false);
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            return Ok(_serviceManager.BookService.GetOneBookById(id, false));
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest();

            var result = _serviceManager.BookService.CreateOneBook(book);
            return StatusCode(201, result);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            if (book is null)
                return BadRequest();

            var result = _serviceManager.BookService.UpdateOneBook(id, book, true);
            return StatusCode(200, result);

        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            _serviceManager.BookService.DeleteOneBook(id, false);
            return StatusCode(204);
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, JsonPatchDocument<Book> bookPatch)
        {
            //check Entity ?
            try
            {
                var entity = _serviceManager.BookService.GetOneBookById(id, true);

                // parametre olarak gelen yamayı takip edilen mevcut entity'e yansıtalım.
                bookPatch.ApplyTo(entity);
                // daha sonra db'ye yanstıalım.
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }





        }

    }
}
