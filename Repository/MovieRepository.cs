using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Repository
{
    public  class MovieRepository: IMovieRepository
    {
        readonly  apiDbContext db;
        private  List<Movie> movies;
       
        public MovieRepository(apiDbContext _db)
        {
            db = _db;
            movies = db.Movies.ToList();
        }

        public List<Actor> Actors(string actor)
        {
            return db.Actors.Where(x => x.ActorName == actor).ToList();
        }

        public List<Genre> Genres()
        {
            return db.Genres.ToList();
        }

        public List<Movie> Movies(bool recent,bool soon)
        {
            if (soon && !recent)
            {
               return movies.Where(x => x.Relased > DateTime.Now).OrderByDescending(x => x.Relased).ToList();
              
            }
            else if (recent&& !soon)
            {
                return db.Movies.Where(x => x.Relased > DateTime.Now).OrderByDescending(x => x.Relased).ToList();
            }
            return movies;
        }

        public Review PostReview(Review review,int MovieId,int userId)
        {

            review.MovieId = MovieId;
            review.UserId = userId;
                                  
            db.Reviews.Add(review);
            db.SaveChanges();
            return review;
           
        }

        public List<Review> Reviews(int id)
        {

            List<Review> stores = db.Reviews.Where(x => x.MovieId == id)
        .Select(store => new Review { Comment = store.Comment, Vote = store.Vote, Movie=store.Movie }).ToList(); ;
            return stores;
          //  return db.Movies.Where(x => x.MovieId == id).Select(store => new SelectListItem { Value = store.Reviews, Text = store.ID })
        }
    }
}
