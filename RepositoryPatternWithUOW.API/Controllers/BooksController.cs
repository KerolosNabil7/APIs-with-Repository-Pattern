using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _booksRepository;
        public BooksController(IBaseRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        #region GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_booksRepository.GetAll());
        }
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _booksRepository.GetAllAsync());
        }
        #endregion

        #region GetById
        [HttpGet("GetById")]
        public IActionResult GetById()
        {
            return Ok(_booksRepository.GetById(1));
        }
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await _booksRepository.GetByIdAsync(1));
        }
        #endregion

        #region GetByName
        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_booksRepository.Find(b => b.Title == "New Book", new string[] { "Author" }));
        }
        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByNameAsync()
        {
            return Ok(await _booksRepository.FindAsync(b => b.Title == "New Book", new string[] {"Author"}));
        }
        #endregion

        #region FindAll With Criteria
        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors()
        {
            return Ok(_booksRepository.FindAll(b => b.Title.Contains("New Book"), new string[] { "Author" }));
        }
        [HttpGet("GetAllWithAuthorsAsync")]
        public async Task<IActionResult> GetAllWithAuthorsAsync()
        {
            return Ok(await _booksRepository.FindAllAsync(b => b.Title.Contains("New Book"), new string[] { "Author" }));
        }
        #endregion

        #region FindAll With Overload
        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_booksRepository.FindAll(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));
        }
        [HttpGet("GetOrderedAsync")]
        public async Task<IActionResult> GetOrderedAsync()
        {
            return Ok(await _booksRepository.FindAllAsync(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));
        }
        #endregion

        #region AddOne
        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            var book = new Book()
            {
                Title = "Test 3",
                AuthorId = 1
            };
            return Ok(_booksRepository.Add(book));  
        }
        [HttpPost("AddOneAsync")]
        public async Task<IActionResult> AddOneAsync()
        {
            var book = new Book()
            {
                Title = "Test 3 Async",
                AuthorId = 1,
            };
            return Ok(await _booksRepository.AddAsync(book));
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
            return Ok(_booksRepository.AddRange(books));
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
            return Ok(await _booksRepository.AddRangeAsync(books));
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public IActionResult Update()
        {
            var book = _booksRepository.GetById(1);
            book.Title = "Title Updated";
            _booksRepository.Update(book);
            return Ok(book);
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public IActionResult Delete()
        {
            var book = _booksRepository.GetById(1);
            _booksRepository.Delete(book);
            return Ok(book);
        }
        #endregion

        #region Count
        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(_booksRepository.Count());
        }
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _booksRepository.CountAsync());
        }
        #endregion
    }
}
