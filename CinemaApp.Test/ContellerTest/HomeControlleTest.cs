using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Controllers;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CinemaApp.Test.ContellerTest
{

    public class HomeControlleTest
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IHome _home;
        private readonly HomeController _homeController;

        public HomeControlleTest()
        {
            _home=A.Fake<IHome>();
            _logger=A.Fake<ILogger<HomeController>>();

            _homeController=new HomeController(_logger, _home);

        }

        [Fact]
        public async void HomeController_Index_ReturnsViewResut()
        {
            //Arange
            var movies=A.Fake<List<Movies>>();
            A.CallTo(()=>_home.GetMoviesThreeMovies()).Returns(movies);

            //Act
            var resutl=await _homeController.Index();

            //Assert
            resutl.Should().BeOfType<ViewResult>();
            var resultview = resutl as ViewResult;
            resultview!.Model.Should().BeAssignableTo<List<Movies>>();

        }

    }
}
