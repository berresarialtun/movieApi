using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Genre
    {
        public Genre()
        {
            movies = new HashSet<Movie>();
        }
        public int GenreId{ get; set; }

        public string GenreName { get; set; }

        public virtual ICollection<Movie> movies { get; set; }
    }
}
