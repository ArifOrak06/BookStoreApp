using BookStoreApp.Core.DTOs.Concretes.BookDTOs;
using BookStoreApp.Core.Services;
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
        public IActionResult CreateOneBook([FromBody] BookDtoForInsertion bookDtoForInsertion)
        {
            if (bookDtoForInsertion == null)
                return BadRequest();

            var result = _serviceManager.BookService.CreateOneBook(bookDtoForInsertion);
            return StatusCode(201, result);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDtoForUpdate)
        {
            if (bookDtoForUpdate is null)
                return BadRequest();

            var result = _serviceManager.BookService.UpdateOneBook(id, bookDtoForUpdate, true);
            return StatusCode(200, result);

        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            _serviceManager.BookService.DeleteOneBook(id, false);
            return StatusCode(204);
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, JsonPatchDocument<BookDto> bookDtoPatch)
        {
            //check Entity ?

            var entity = _serviceManager.BookService.GetOneBookById(id, true);
            if (entity == null)
                return BadRequest();


            // parametre olarak gelen yamayı takip edilen mevcut entity'e yansıtalım.
            bookDtoPatch.ApplyTo(entity);
            _serviceManager.BookService.UpdateOneBook(id,new BookDtoForUpdate
            {
                Id = entity.Id,
                Price = entity.Price,
                Title = entity.Title
            },true);
            // daha sonra db'ye yanstıalım.
            return NoContent();


        }

    }
}
