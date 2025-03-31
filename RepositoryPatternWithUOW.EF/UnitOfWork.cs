using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Repositories;

namespace RepositoryPatternWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Author> Authors { get; private set; }
        public IBooksRepository Books { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            //Initialize the Properties prop of type of the interface (IBaseRepository)
            //the object of the type of the class (BaseRepository)
            //the BaseRepository class constructor have context parameter so pass it
            Authors = new BaseRepository<Author>(_context);
            Books = new BooksRepository(_context);
        }

        //Implement the IUnitOfWork interface
        public int Complete()
        {
            return _context.SaveChanges();
        }

        //since the IUnitOfWork implements the IDisposable interface so u also should implement the IDisposable interface 
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
