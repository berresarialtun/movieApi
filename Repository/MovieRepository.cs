using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using webapi.Models;
using webapi.ViewModel;

namespace webapi.Repository
{
    public class MovieRepository : IMovieRepository
    {
        readonly apiDbContext db;
        private List<Movie> movies;
        private IMapper mapper;

        public MovieRepository(apiDbContext _db, IMapper _mapper)
        {
            db = _db;
            movies = db.Movies.ToList();
            mapper = _mapper;
        }

        public List<Actor> Actors(string actor)
        {
            return db.Actors.Where(x => x.ActorName == actor).ToList();
        }

        public List<Genre> Genres()
        {
            return db.Genres.ToList();
        }

        public List<Movie> Movies(bool recent, bool soon)
        {
            if (soon && !recent)
            {
                return movies.Where(x => x.Relased > DateTime.Now).OrderByDescending(x => x.Relased).ToList();

            }
            else if (recent && !soon)
            {
                return db.Movies.Where(x => x.Relased > DateTime.Now).OrderByDescending(x => x.Relased).ToList();
            }
            return movies;
        }

        public Review PostReview(ReviewVM review, int movieId, int userId)
        {

            Review _review = mapper.Map<Review>(review);
            _review.MovieId = movieId;
            _review.UserId = userId;
            _review.Comment = review.Comment;
            _review.Vote = review.Vote;


            db.Reviews.Add(_review);
            db.SaveChanges();
            return _review;

        }

        public List<ReviewVM> Reviews(int id)
        {
            
            List<Review> reviews = db.Reviews.Where(x => x.MovieId == id)
            .Select(store => new Review { Comment = store.Comment, Vote = store.Vote, Movie = store.Movie }).ToList(); ;
            List<ReviewVM> _reviews = mapper.Map<List<Review>, List<ReviewVM>>(reviews);
            return _reviews;
            //  return db.Movies.Where(x => x.MovieId == id).Select(store => new SelectListItem { Value = store.Reviews, Text = store.ID })
        }
    }
}
