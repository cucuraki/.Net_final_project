using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace test1.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public BookController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return Ok(await _dataContext.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
           
            Book ? book = await _dataContext.Books.FindAsync(id);
            if (book == null)
                return BadRequest("Id does not exists");
            

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> Add(Book book)
        {
            
            string valid = Book.Valid(book);
            Book? myBook = await _dataContext.Books.FindAsync(book.Id);
            if (myBook != null)
            {
                valid += "Id already exists\n";
            }
           
            if(valid.Length != 0)
            {
                return BadRequest(valid);
            }
            
            _dataContext.Books.Add(book);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Books.ToListAsync());
        }



        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book requestBook)
        {
      
            string valid = Book.Valid(requestBook);
            Book? book = await _dataContext.Books.FindAsync(requestBook.Id);
            if (book == null)
                valid +="Id does not exists";

            if (valid.Length != 0)
                return BadRequest(valid);

            book.copy(requestBook);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Books.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Book>>> Delete(string id)
        {
            
            Book? book = await _dataContext.Books.FindAsync(id);
            if (book == null)
                return BadRequest("Id does not exists");
            _dataContext.Books.Remove(book);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Books.ToListAsync());
        }
    }
}
