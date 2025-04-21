using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Controllers;
using CinemaApp.Data;
using CinemaApp.Data.Dto;
using CinemaApp.Data.Enum;
using CinemaApp.Helpers;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CinemaApp.Test.ContellerTest
{
    public class MovieControllerTest
    {
        private readonly MoviesController _moviesController; 
        private readonly IMovies _movies;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public MovieControllerTest()
        {

            //First thing we need to do is to bring our Dependencies to the repos 
           
            _movies= A.Fake<IMovies>();
            _photoService= A.Fake<IPhotoService>();
            _userManager= A.Fake<UserManager<AppUser>>();
           
            

            //SUT
            //As we see in our controller(our controller class) is based on these dependencies
            //So we will make our Test class to also be Depending on the mocked ones
            _moviesController = new MoviesController( _movies, _photoService, _userManager);

        }

            [Fact]
            public async void MoviesContoller_Index_ReturnViewWithListOfMovies()
            {

                //Arange -->We need to bring Everything 
                //As we see in the Index our Index Returns a List Of Movies and page
                //Sowe need to Fake them
                //
                //Fake pg
                int pg = 1;
                //Fake the List
                var movies = A.Fake<List<Movies>>();
                //Fake the Index Method(logic)
                //So here we are calling the Index and say that we wantr our method to return movie List
                A.CallTo(() => _movies.Index()).Returns((movies));

                //Act--> Call the method we are testing 
                var result= await _moviesController.Index();

                // Assert-->Checking if the Return is what we want 
                //We are returning View so we hae to check if returnms ViewModel 
                result.Should().BeOfType<ViewResult>();
                //We are making the viewresult like a relut view we got so we can look inside 
                var viewResult = result as ViewResult;
                //and the view result thta got the data (Model named) should be type of movies
                viewResult!.Model.Should().BeAssignableTo<List<Movies>>();
           
            }

            [Fact]
            public  async void MoviesController_Details_ReturnsViewWithMoviedetailDto()
            {
                //Arange -->Bring everything we need
                int id = 1;
                var movie = A.Fake<Movies>();
                var relatedmovies= A.Fake<List<Movies>>();
                var reviews= A.Fake<List<Reviews>>();

                var Detailedmovie = A.CallTo(() => _movies.GetMovieById(id)).Returns(movie);
                var RelatedMovies = A.CallTo(() => _movies.GetRelatedMovies(id)).Returns(relatedmovies);

            
         
                var MovieDetailDto = new MovieDetailDto
                {
                    MyMovie = movie,
                    MoreMoviesM = relatedmovies,
                    Reviews = reviews
                };


          
                //Act -->Call the method you want 
                var result = await _moviesController.Details(id);
            
                var NotdfoundResult = await _moviesController.Details(5);

                //Assert -->check if what we are getting is what we need
                result.Should().BeOfType<ViewResult>();
                var viewResult = result as ViewResult;
                viewResult!.Model.Should().BeAssignableTo<MovieDetailDto>();

           
            }

            [Fact]
            public async void MoviesController_Details_ReturnsNContent()
            {
                //Arange -->Bring everything we need
                int id = 1;


                A.CallTo(() => _movies.GetMovieById(id)).Returns<Movies>(null); // return null


                //Act -->Call the method you want 
                var result = await _moviesController.Details(id);

                //Assert -->check if what we are getting is what we need
                result.Should().BeOfType<NotFoundResult>();


            }

       
            [Fact]
            public async Task MoviesController_Create_ReturnsRedirectToAction()
            {

                // Arrange-->bring all the variables(these are the values we will put to the MovieDto so we need to fake them 
                var Title = "Test Movie";
                var Description = "Some Description";
                var Image = A.Fake<IFormFile>();
                var Categories = new Categories();
            
            

                //Even tho we are testing the logic of the controller we still need to call pur dependencies
                //if we dont when we call the method we will have nul values
                A.CallTo(() => _photoService.AddPhotoAsync(Image))
                    .Returns(Task.FromResult(new ImageUploadResult
                    {
                        Url = new Uri("http://fake.com/fake.jpg"),  // Fake URL for the uploaded image
                        PublicId = "fake-id"  // Fake public ID of the uploaded image
                    }));

                A.CallTo(() => _userManager.GetUserId(A<ClaimsPrincipal>.Ignored))
                    .Returns("fake-user-id");

                var movieDto = new MovieCreateDto
                {
                    Title = Title,
                    Description = Description,
                    Image = Image,
                    Categories = Categories
                
                };

                // Act
                var result = await _moviesController.Create(movieDto);

                // Assert
                result.Should().BeOfType<RedirectToActionResult>();
                var redirect = result as RedirectToActionResult;
                redirect!.ActionName.Should().Be("Index");
            }

            [Fact]
            public async Task MoviesController_Create_ReturnsRedirectToView()
            {

                // Arrange-->bring all the variables(these are the values we will put to the MovieDto so we need to fake them 
                var Title = "Test Movie";
                var Description = "Some Description";
                var Image = A.Fake<IFormFile>();
            



                //Even tho we are testing the logic of the controller we still need to call pur dependencies
                //if we dont when we call the method we will have nul values
                A.CallTo(() => _photoService.AddPhotoAsync(Image))
                    .Returns(Task.FromResult(new ImageUploadResult
                    {
                        Url = new Uri("http://fake.com/fake.jpg"),  // Fake URL for the uploaded image
                        PublicId = "fake-id"  // Fake public ID of the uploaded image
                    }));

                A.CallTo(() => _userManager.GetUserId(A<ClaimsPrincipal>.Ignored))
                    .Returns("fake-user-id");

                var movieDto = new MovieCreateDto
                {
                    Title = Title,
                    Description = Description,
                    Image = Image,
                

                };
                _moviesController.ModelState.AddModelError("Title", "Title is required");

                // Act
                var result = await _moviesController.Create(movieDto);

                // Assert
                result.Should().BeOfType<ViewResult>();
                var redirect = result as ViewResult;
                redirect!.Model.Should().BeAssignableTo<MovieCreateDto>();
            }


            [Fact]
            public async Task MoviesController_EditGet_ReturnViewResult()
            {
                //Arange-->Bring all the methods/Variable we need
                int id = 1;
                var movie = A.Fake<Movies>();
                A.CallTo(() => _movies.GetMovieById(id)).Returns(movie);

                //Act
                var result=await _moviesController.Edit(id);

                //Assert
                result.Should().BeOfType<ViewResult>();
                var resultview = result as ViewResult;
                resultview!.Model.Should().BeAssignableTo<MovieEditDto>();
            }

            [Fact]
            public async Task MoviesController_EditGet_ReturnNotFound()
            {
                //Arange-->Bring all the methods/Variable we need
                int id = 1;
            
                A.CallTo(() => _movies.GetMovieById(id)).Returns<Movies>(null);

                //Act
                var result = await _moviesController.Edit(id);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            
            }

            [Fact]
            public async Task MoviesController_EditPost_ReturnsModelStateInvalid()
            {
                int id = 1;
                var MovieEditDto=A.Fake<MovieEditDto>();    
                _moviesController.ModelState.AddModelError("", "Chnages Could not Be made");
                var result = await _moviesController.Edit(id, MovieEditDto);
                result.Should().BeOfType<ViewResult>();


            }

        [Fact]
        public async void MoviesController_EditPost_ReturnNotFound()
        {
            //Arange
            int id = 1;
            A.CallTo(() => _movies.GetMovieById(id)).Returns<Movies>(null);

            //Act
            var result = await _moviesController.Edit(id);

            //Assert
            result?.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void MoviesController_EditPost_ReturnsRedirectrActionTOIndex()
        {
            int id = 1;
            var Image = A.Fake<IFormFile>();
            var movieEditDto = new MovieEditDto
            {
                Id = id,
                Title = "Title",
                Description = "Description",
                Image = Image
            };
            var movie=A.Fake<Movies>();
            A.CallTo(() => _movies.GetMovieByIdNoTrack(id)).Returns(movie);
           
            movie.Image = "http/MyImage/ThisUrl";
            A.CallTo(() => _photoService.DeletePhotoAsync(movie.Image)).Returns(Task.FromResult(new DeletionResult
            {
                Result = "ok"
            }));

           
            //Even tho we are testing the logic of the controller we still need to call pur dependencies
            //if we dont when we call the method we will have nul values
            var result=A.CallTo(() => _photoService.AddPhotoAsync(Image))
                .Returns(Task.FromResult(new ImageUploadResult
                {
                    Url = new Uri("http://fake.com/fake.jpg"),  
                    PublicId = "fake-id"  
                }));

            var uid=A.CallTo(() => _userManager.GetUserId(A<ClaimsPrincipal>.Ignored))
                .Returns("fake-user-id");

            var NewMovie = new Movies
            {
                Id = movieEditDto.Id,
                Title = movieEditDto.Title,
                Description = movieEditDto.Description,
                Image = result.ToString(),
                Categories = new Categories(),
                AppUserId = uid.ToString(),
            };
            // Act
            var status = await _moviesController.Edit(id,movieEditDto);

            // Assert
            status.Should().BeOfType<RedirectToActionResult>();
            var redirect = status as RedirectToActionResult;
           
            redirect!.ActionName.Should().Be("Index");


        }


        [Fact]
        public async Task MoviesController_DeleteGet_ReturnViewResult()
        {
            //Arange-->Bring all the methods/Variable we need
            int id = 1;
            var movie = A.Fake<Movies>();
            A.CallTo(() => _movies.GetMovieById(id)).Returns(movie);

            //Act
            var result = await _moviesController.Delete(id);

            //Assert
            result.Should().BeOfType<ViewResult>();
            var resultview = result as ViewResult;
            resultview!.Model.Should().BeAssignableTo<MovieDeleteDto>();
        }

        [Fact]
        public async Task MoviesController_DeleteGet_ReturnNotFound()
        {
            //Arange-->Bring all the methods/Variable we need
            int id = 1;

            A.CallTo(() => _movies.GetMovieById(id)).Returns<Movies>(null);

            //Act
            var result = await _moviesController.Delete(id);

            //Assert
            result.Should().BeOfType<NotFoundResult>();

        }

        [Fact]
        public async void MoviesController_DeletePost_ReturnNotFound()
        {
            //Arange
            int id = 1;
            A.CallTo(()=>_movies.GetMovieById(id)).Returns<Movies>(null);

            //Act
            var result = await _moviesController.Delete(id);

            //Assert
            result.Should().BeOfType<NotFoundResult>();

        }

        [Fact]
        public async void MoviesController_DeletePost_ReturnRedirectToActionResult()
        {
            int id = 1;
            var Image = A.Fake<IFormFile>();
            var movieDeleteDto = new MovieDeleteDto
            {
                Id = id,
                Title = "Title",
                Description = "Description",
                Image = Image
            };
            var movie = A.Fake<Movies>();
            A.CallTo(() => _movies.GetMovieById(id)).Returns(movie);

            movie.Image = "http/MyImage/ThisUrl";
            A.CallTo(() => _photoService.DeletePhotoAsync(movie.Image)).Returns(Task.FromResult(new DeletionResult
            {
                Result = "ok"
            }));

            //Act
            var result = await _moviesController.Delete(id, movieDeleteDto);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            var redirect = result as RedirectToActionResult;

            redirect!.ActionName.Should().Be("Index");


        }



    }
}
