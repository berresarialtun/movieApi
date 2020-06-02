using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Review
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ReviewId {get; set;}
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Comment { get; set; }
        public int Vote { get; set; }

        public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
        {
         
            public void Configure(EntityTypeBuilder<Review> builder)
            {
                builder.HasKey(x => new { x.ReviewId, x.MovieId, x. UserId});
                builder.HasOne<Movie>(x => x.Movie)
                 .WithMany(y => y.Reviews)
                 .HasForeignKey(x => x.MovieId);

                builder.HasOne<User>(x => x.User)
                    .WithMany(y => y.Reviews)
                    .HasForeignKey(x => x.UserId);
            }
        }
    }
}
