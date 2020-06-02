using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Repository
{
   public interface IMovieRepository
    {
        public List<Review> Reviews(int id);
        public Review PostReview(Review review,int MovieId, int userId);
        public List<Movie> Movies(bool recent, bool soon);
        public List<Genre> Genres();
        public List<Actor> Actors(string actor);

    }
}
