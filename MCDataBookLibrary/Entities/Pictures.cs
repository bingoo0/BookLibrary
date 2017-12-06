using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDataBookLibrary.Entities
{
    public class Pictures
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(200, MinimumLength = 1)]
        public string Url { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
