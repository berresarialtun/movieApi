using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using webapi.Models;
using webapi.Repository;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class MoviesController : ControllerBase
    {
        readonly apiDbContext db;
        IMovieRepository movieRepo;
        public MoviesController(apiDbContext _db,IMovieRepository _movieRepo)
        {
            db = _db;
            movieRepo = _movieRepo;
        }


        [Authorize]
        [HttpGet("{id}/review")]
        public IActionResult GetReview(int Id)
        {
            
           // string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            try
            {
                var Reviews = movieRepo.Reviews(Id);
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
        public IActionResult PostReview(int MovieId,[FromBody]Review Review)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            var UserName = claims[0].Value;
            var UserId = Convert.ToInt32( claims[1].Value);

            if (ModelState.IsValid)
            {
                try
                {
                    var reviews =  movieRepo.PostReview(Review, MovieId,UserId);
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
