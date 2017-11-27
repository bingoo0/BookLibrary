using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDataBookLibrary.Entities
{
    public class Book
    {
        [Key]
        public int bookId { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public int AuthorId { get; set; }

        
        [Display(Name = "Writer")]
        public int? WriterId { get; set; }
        public virtual Writer Writer { get; set; }

        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }               

        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
