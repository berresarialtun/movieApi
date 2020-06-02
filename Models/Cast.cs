using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Cast
    {
      
        public int ActorId{ get; set; }
        public virtual Actor Actor { get; set; }
        public int MovieId{ get; set; }
        public virtual Movie Movie { get; set; }


    }

    public class CastEntityConfiguration : IEntityTypeConfiguration<Cast>
    {
        public void Configure(EntityTypeBuilder<Cast> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.ActorId });
            builder.HasOne<Movie>(x => x.Movie)
                 .WithMany(y => y.Casts)
                 .HasForeignKey(x => x.MovieId);

            builder.HasOne<Actor>(x => x.Actor)
                .WithMany(y => y.Casts)
                .HasForeignKey(x => x.ActorId);
           
        }
    }
}
