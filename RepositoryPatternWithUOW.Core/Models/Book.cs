using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public Author Author { get; set; } //Navgation Property
        public int AuthorId { get; set; }
    }
}
