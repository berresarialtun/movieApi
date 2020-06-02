using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Movie
    {
        public Movie()
        {
            Casts = new HashSet<Cast>();
            Reviews = new HashSet<Review>();

        }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime Relased { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        
    }

    public class MovieEntityConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasOne(x => x.Genre)
                .WithMany(y => y.movies)
                .HasForeignKey(z=> z.GenreId);

            
        }
    }
}
