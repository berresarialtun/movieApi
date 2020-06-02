using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;
using webapi.ViewModel;

namespace webapi.Repository
{
   public interface IMovieRepository
    {
        public List<ReviewVM> Reviews(int id);
        public Review PostReview(ReviewVM review,int movieId, int userId);
        public List<Movie> Movies(bool recent, bool soon);
        public List<Genre> Genres();
        public List<Actor> Actors(string actor);

    }
}
