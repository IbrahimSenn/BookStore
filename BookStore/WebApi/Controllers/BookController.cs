using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi;
#nullable enable

namespace WebAi.AddControllers
{
    [ApiController]
[Route("[controller]s")]//requesti hangi resourcenin karşılayacığını belirtiyor.(controllerin ismine "s" ekleyerek getiriyor.)

    public class BookController : ControllerBase
    {

        private static List<Book> BookList = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2011,12,06)
            },

            new Book
            {
                Id = 2,
                Title = "Herland",
                GenreId = 2, //Science Finction
                PageCount = 250,
                PublishDate = new DateTime(2001,06,12)
            },

            new Book
            {
                Id = 3,
                Title = "Tetup",
                GenreId = 2,
                PageCount = 540,
                PublishDate = new DateTime(2004,07,06)
            }

        };


        [HttpGet]

        public List<Book> GetBooks()
        {

            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();

            return bookList ;

        }

        [HttpGet("{id}")]

        public Book GetById(int id)
        {

            var book = BookList.Where(book => book.Id == id).SingleOrDefault() ;

            return book! ;

        }



        [HttpPost] 

        public IActionResult AddBook([FromBody] Book newBook) //Validasyon yapıyoruz eğer hata olursa geriye hata döndürmek için  IActionResult kullanılır.
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title );
            
            if (book != null)
            
                return BadRequest();
            BookList.Add(newBook);
            return Ok();

        }

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook )
        {
            var book = BookList.SingleOrDefault(x =>x.Id == id );

            if ( book == null )
                
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId ;

            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount ;

            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate ;

            book.Title = updatedBook.Title != default ?  updatedBook.Title : book.Title ;

            return Ok(); 

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);

            if( book == null )
                
                return BadRequest();

            BookList.Remove(book);
            return Ok();

        }


    }

}