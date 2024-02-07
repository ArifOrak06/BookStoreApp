using BookStoreApp.Core.DTOs.Concretes.BookDTOs;
using BookStoreApp.Core.Entities.RequestFeatures;
using BookStoreApp.Core.Services;
using BookStoreApp.WebAPI.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookStoreApp.WebAPI.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
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
        public async Task<IActionResult> GetAllBooks([FromQuery]BookParameters bookParameters)
        {
            var pagedResult = await _serviceManager.BookService.GetAllBooksAsync(bookParameters,false);

            // PagedResult içerisinde bize hem kaynak hemde sayfalandırmaya ilişkin metaData geldiği için metaData'yı Response'umuzun Headers'ına ekleyelim ki API'mizi tüketen
            // clientlara künye bilgilerini sunarak işlerini kolaylaştıralım, kullanıcı dostu API sunalım.

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData)); // JsonSerializer yapısını System.text üzerinden kullanmanız gerekmektedir.Aksi takdirde kullanıma izin vermeyecek hata dönecektir.

            return Ok(pagedResult.bookDtos); 
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBook([FromRoute(Name = "id")] int id)
        {
            return Ok(await _serviceManager.BookService.GetOneBookByIdAsync(id, false));
        }
   
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneBook([FromBody] BookDtoForInsertion bookDtoForInsertion)
        {
            if (bookDtoForInsertion == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var result = await _serviceManager.BookService.CreateOneBookAsync(bookDtoForInsertion);
            return StatusCode(201, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDtoForUpdate)
        {
            if (bookDtoForUpdate is null)
                return BadRequest();
            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState); 
            var result = await _serviceManager.BookService.UpdateOneBookAsync(id, bookDtoForUpdate, false);
            return StatusCode(200, result);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            await _serviceManager.BookService.DeleteOneBookAsync(id, false);
            return StatusCode(204);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, JsonPatchDocument<BookDtoForUpdate> bookDtoForUpdatePatch)
        {
            //check Entity ?

            
            if (bookDtoForUpdatePatch == null)
                return BadRequest();

            var result = await _serviceManager.BookService.GetOneBookForPatchAsync(id,false);
            // parametre olarak aldığımız bookDtoForUpdatePatch yamasını bookDtoForUpdate dto nesnemize yamalayalım. :)

            bookDtoForUpdatePatch.ApplyTo(result.bookDtoForUpdate,ModelState);

            // Daha sonra yamadan sonraki halin geçerliliğini kontrol edelim.

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity();

            await _serviceManager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

            // daha sonra db'ye yanstıalım.
            return NoContent();


        }

    }
}
