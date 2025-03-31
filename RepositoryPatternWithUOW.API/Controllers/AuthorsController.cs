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
    public class AuthorsController : ControllerBase
    {
        //private readonly IBaseRepository<Author> _unitOfWork.Authors;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Authors.GetAll());
        }
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unitOfWork.Authors.GetAllAsync());
        }
        #endregion

        #region GetById
        [HttpGet("GetById")]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Authors.GetById(1));
        }
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await _unitOfWork.Authors.GetByIdAsync(1));
        }
        #endregion

        #region GetByName
        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_unitOfWork.Authors.Find(b => b.Name == "Test"));
        }
        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByNameAsync()
        {
            return Ok(await _unitOfWork.Authors.FindAsync(b => b.Name == "Test"));
        }
        #endregion

        #region FindAll With Overload
        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_unitOfWork.Authors.FindAll(b => b.Name.Contains("Test"), null, null, b => b.Id, OrderBy.Descending));
        }
        [HttpGet("GetOrderedAsync")]
        public async Task<IActionResult> GetOrderedAsync()
        {
            return Ok(await _unitOfWork.Authors.FindAllAsync(b => b.Name.Contains("Test"), null, null, b => b.Id, OrderBy.Descending));
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
            _unitOfWork.Authors.Add(author);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(author);
        }
        [HttpPost("AddOneAsync")]
        public async Task<IActionResult> AddOneAsync()
        {
            var author = new Author()
            {
                Name = "Author Added",
            };
            await _unitOfWork.Authors.AddAsync(author);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(author);
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
            _unitOfWork.Authors.AddRange(authors);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(authors);
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
            await _unitOfWork.Authors.AddRangeAsync(authors);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(authors);
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public IActionResult Update()
        {
            var author = _unitOfWork.Authors.GetById(1);
            author.Name = "Name Updated";
            _unitOfWork.Authors.Update(author);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(author);
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public IActionResult Delete()
        {
            var author = _unitOfWork.Authors.GetById(1);
            _unitOfWork.Authors.Delete(author);
            //Without the calling Complete method the object will return with id = 0 that means it didn't saved in database
            _unitOfWork.Complete();
            return Ok(author);
        }
        #endregion

        #region Count
        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(_unitOfWork.Authors.Count());
        }
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _unitOfWork.Authors.CountAsync());
        }
        #endregion
    }
}
