using Microsoft.AspNetCore.Mvc;
using WebApi.BooksOperations.CreateBook;
using WebApi.DBOperation;
using static WebApi.BooksOperations.CreateBook.CreateBookCommand;

namespace WebApi.addControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : Controller
    {  
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public IActionResult GetBooks() 
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        
        [HttpGet("{id}")]
        public Book GetById(int id) 
        {
            var bookId = _context.Books.Where(b => b.Id == id).SingleOrDefault();

            return bookId;
        }

        // [HttpGet]
        // public Book Get(string id) 
        // {
        //     var bookname = BookList.Where(n => n.Id == Convert.ToInt32(id)).SingleOrDefault();

        //     return bookname;
        // }

        //Post  

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook) {
        
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        //Put

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
            {
                return BadRequest();

            }
            // check database field, if it's changed (mean it is not 0) update else add new field 

            book.GenreId = updatedBook.GenreId != default  
            ? updatedBook.GenreId 
            : book.GenreId;

            book.PageCount = updatedBook.PageCount != default 
            ? updatedBook.PageCount 
            : book.PageCount;

            book.PublishDate = updatedBook.PublishDate != default
            ? updatedBook.PublishDate 
            : book.PublishDate;

            book.Title = updatedBook.Title != default
            ? updatedBook.Title 
            : book.Title;

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);
            return Ok();
        }

    } 
}
