using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;

namespace RepositoryPatternWithUOW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseRepository<Author> _authorsRepository;
        public AuthorsController(IBaseRepository<Author> authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        #region GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_authorsRepository.GetAll());
        }
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _authorsRepository.GetAllAsync());
        }
        #endregion

        #region GetById
        [HttpGet("GetById")]
        public IActionResult GetById()
        {
            return Ok(_authorsRepository.GetById(1));
        }
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await _authorsRepository.GetByIdAsync(1));
        }
        #endregion

        #region GetByName
        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_authorsRepository.Find(b => b.Name == "Test"));
        }
        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByNameAsync()
        {
            return Ok(await _authorsRepository.FindAsync(b => b.Name == "Test"));
        }
        #endregion

        #region FindAll With Overload
        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_authorsRepository.FindAll(b => b.Name.Contains("Test"), null, null, b => b.Id, OrderBy.Descending));
        }
        [HttpGet("GetOrderedAsync")]
        public async Task<IActionResult> GetOrderedAsync()
        {
            return Ok(await _authorsRepository.FindAllAsync(b => b.Name.Contains("Test"), null, null, b => b.Id, OrderBy.Descending));
        }
        #endregion

        #region AddOne
        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            var author = new Author()
            {
                Name = "Author Added",
            };
            return Ok(_authorsRepository.Add(author));
        }
        [HttpPost("AddOneAsync")]
        public async Task<IActionResult> AddOneAsync()
        {
            var author = new Author()
            {
                Name = "Author Added",
            };
            return Ok(await _authorsRepository.AddAsync(author));
        }
        #endregion

        #region Add Range
        [HttpPost("AddRange")]
        public IActionResult AddRange()
        {
            List<Author> authors = new List<Author>
            {
                new Author()
                {
                    Name = "Add Range 1",
                },
                new Author()
                {
                    Name = "Add Range 2",
                },
            };
            return Ok(_authorsRepository.AddRange(authors));
        }
        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync()
        {
            List<Author> authors = new List<Author>
            {
                new Author()
                {
                    Name = "Add Range 1 Async",
                },
                new Author()
                {
                    Name = "Add Range 2 Async",
                },
            };
            return Ok(await _authorsRepository.AddRangeAsync(authors));
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public IActionResult Update()
        {
            var author = _authorsRepository.GetById(1);
            author.Name = "Name Updated";
            _authorsRepository.Update(author);
            return Ok(author);
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public IActionResult Delete()
        {
            var author = _authorsRepository.GetById(1);
            _authorsRepository.Delete(author);
            return Ok(author);
        }
        #endregion

        #region Count
        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(_authorsRepository.Count());
        }
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _authorsRepository.CountAsync());
        }
        #endregion
    }
}
