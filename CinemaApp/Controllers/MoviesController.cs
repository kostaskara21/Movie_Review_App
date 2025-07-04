﻿using CinemaApp.Data;
using CinemaApp.Data.Dto;
using CinemaApp.Helpers;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers
{

    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovies _movies;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;

        //private readonly ApplicationDbContext _context;
        

        //In the Constructor we had the ApplicationDbContext
        public MoviesController( IMovies movies, IPhotoService photoService,UserManager<AppUser> userManager)
        {
            //_context = context;
            _movies = movies;
            _photoService = photoService;
            _userManager = userManager;
        }


        //This Index is for the Pagiantion
        public async Task<IActionResult> Index(int pg=1)
        {
            //var Movies=_context.Movies.ToList();
            var (Movies,p)= await _movies.Index(pg);
            ViewBag.Pager = p;
            return View(Movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            //We are using the Repo and getting Back A View Model that we made (for passing to the View both List and also Single Movie) 
            //We mostly care about the Specific movie thats why we are only ckecking one attribut of the ModelView
            var DetailedMovie= await _movies.GetMovieById(id);
            var RelatedMovies=await _movies.GetRelatedMovies(id);
            if (DetailedMovie != null)
            {
                var MovieDetailViewModel = new MovieDetailDto
                {
                    MyMovie = DetailedMovie,
                    MoreMoviesM = RelatedMovies,
                    Reviews = DetailedMovie.Reviews

                };
                return View(MovieDetailViewModel);
            }
            return NotFound();
            
        }


        public async Task<IActionResult> Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateDto movies)
        {
           
            if (ModelState.IsValid)
            {
                var userid = _userManager.GetUserId(User);
                var result = await _photoService.AddPhotoAsync(movies.Image);
                var movie = new Movies
                {
                    Title = movies.Title,
                    Description = movies.Description,
                    Image = result.Url.ToString(),
                    Categories = movies.Categories,
                    AppUserId=userid
                };
                _movies.Add(movie);
                return RedirectToAction("Index");
            }
            
            return View(movies);
           
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie=await _movies.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            var EditModel = new MovieEditDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                URL = movie.Image,
                
                Categories = movie.Categories
            };
            return View(EditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,MovieEditDto movieEditDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Changes Could not be made Try Again");
                return View();
            }
            //var movie = await _movies.GetMovieById(id);
            var movie = await _movies.GetMovieByIdNoTrack(id);
            //_context.Entry(movie).State = EntityState.Detached; // Detach the original entity

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    var imgcloudinary = await _photoService.DeletePhotoAsync(movie.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete the Photo");
                    return View();

                }
                var result = await _photoService.AddPhotoAsync(movieEditDto.Image);
                var NewMovie = new Movies
                {
                    Id = movieEditDto.Id,
                    Title = movieEditDto.Title,
                    Description = movieEditDto.Description,
                    Image = result.Url.ToString(),
                    Categories = movieEditDto.Categories,
                    AppUserId=_userManager.GetUserId(User),
                };
                
                _movies.Update(NewMovie);
                return RedirectToAction("Index");
            }
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movies.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            var DeleteModel = new MovieDeleteDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                URL = movie.Image,
                Categories = movie.Categories
            };
            return View(DeleteModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id,MovieDeleteDto m)
        {
            var movie = await _movies.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            try
            {
                var imgcloudinary = await _photoService.DeletePhotoAsync(movie.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete the Photo");
                return View();

            }
            _movies.Remove(movie);
            return RedirectToAction("Index");
        }



    }
}
