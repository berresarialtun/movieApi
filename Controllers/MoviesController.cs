using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using webapi.Models;
using webapi.Repository;
using webapi.ViewModel;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/movies")]
 
    public class MoviesController : ControllerBase
    {
        private readonly apiDbContext db;
        private IMovieRepository movieRepo;
        private IMapper mapper;
        public MoviesController(apiDbContext _db,IMovieRepository _movieRepo,IMapper _mapper)
        {
            db = _db;
            movieRepo = _movieRepo;
            mapper = _mapper;
          
        }


        [Authorize]
        [HttpGet("{id}/review")]
        public IActionResult GetReview(int id)
        {
            try
            {
                var Reviews = movieRepo.Reviews(id);
                if (Reviews == null)
                {
                    return NotFound();
                }

                return Ok(Reviews);
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        
        }


        [Authorize]
        [HttpPost("{MovieId}/review")]
        public IActionResult PostReview(int movieId,[FromBody]ReviewVM review)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            var userName = claims[0].Value;
            var userId = Convert.ToInt32( claims[1].Value);
            if (ModelState.IsValid)
            {
                try
                {
                    var reviews =  movieRepo.PostReview(review, movieId, userId);
                    if (reviews == null)
                    {
                        return NotFound();
                    }

                    return Ok(reviews);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            else { return BadRequest();  }
        }

        [HttpGet]
        public IActionResult GetMovies([FromQuery] bool Recent, [FromQuery] bool Soon)
        {
            try
            {
                var movies = movieRepo.Movies(Recent, Soon);
                if (movies == null)
                {
                    return NotFound();
                }

                return Ok(movies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("genres")]
        public IActionResult Genres()
        {
            try
            {
                var genres = movieRepo.Genres();
                if (genres == null)
                {
                    return NotFound();
                }

                return Ok(genres);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet("artist")]
        public IActionResult Actors([FromQuery] string keyword)
        {
            try
            {
                var actors = movieRepo.Actors(keyword);
                if (actors == null)
                {
                    return NotFound();
                }

                return Ok(actors);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }




        // POST: api/Movies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
