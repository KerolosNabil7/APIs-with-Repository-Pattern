using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //private readonly IBaseRepository<Book> _unitOfWork.Books;
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Books.GetAll());
        }
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unitOfWork.Books.GetAllAsync());
        }
        #endregion

        #region GetById
        [HttpGet("GetById")]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Books.GetById(1));
        }
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await _unitOfWork.Books.GetByIdAsync(1));
        }
        #endregion

        #region GetByName
        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_unitOfWork.Books.Find(b => b.Title == "New Book", new string[] { "Author" }));
        }
        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByNameAsync()
        {
            return Ok(await _unitOfWork.Books.FindAsync(b => b.Title == "New Book", new string[] {"Author"}));
        }
        #endregion

        #region FindAll With Criteria
        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors()
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("New Book"), new string[] { "Author" }));
        }
        [HttpGet("GetAllWithAuthorsAsync")]
        public async Task<IActionResult> GetAllWithAuthorsAsync()
        {
            return Ok(await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains("New Book"), new string[] { "Author" }));
        }
        #endregion

        #region FindAll With Overload
        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));
        }
        [HttpGet("GetOrderedAsync")]
        public async Task<IActionResult> GetOrderedAsync()
        {
            return Ok(await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));
        }
        #endregion

        #region AddOne
        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            var book = new Book()
            {
                Title = "Test 3",
                AuthorId = 2
            };
            _unitOfWork.Books.Add(book);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(book);  
        }
        [HttpPost("AddOneAsync")]
        public async Task<IActionResult> AddOneAsync()
        {
            var book = new Book()
            {
                Title = "Test 3 Async",
                AuthorId = 2,
            };
            await _unitOfWork.Books.AddAsync(book);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(book);
        }
        #endregion

        #region Add Range
        [HttpPost("AddRange")]
        public IActionResult AddRange()
        {
            List<Book> books = new List<Book>
            {
                new Book()
                {
                    Title = "Add Range 1",
                    AuthorId = 1,
                },
                new Book()
                {
                    Title = "Add Range 2",
                    AuthorId = 1,
                },
            };
            _unitOfWork.Books.AddRange(books);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(books);
        }
        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync()
        {
            List<Book> books = new List<Book>
            {
                new Book()
                {
                    Title = "Add Range 1 Async",
                    AuthorId = 1,
                },
                new Book()
                {
                    Title = "Add Range 2 Async",
                    AuthorId = 1,
                },
            };
            await _unitOfWork.Books.AddRangeAsync(books);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(books);
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public IActionResult Update()
        {
            var book = _unitOfWork.Books.GetById(1);
            book.Title = "Title Updated";
            _unitOfWork.Books.Update(book);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(book);
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public IActionResult Delete()
        {
            var book = _unitOfWork.Books.GetById(1);
            _unitOfWork.Books.Delete(book);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(book);
        }
        #endregion

        #region Count
        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(_unitOfWork.Books.Count());
        }
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _unitOfWork.Books.CountAsync());
        }
        #endregion
    }
}
