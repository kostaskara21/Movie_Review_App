using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Controllers;
using CinemaApp.Data.Dto;
using CinemaApp.Interfaces;
using CinemaApp.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CinemaApp.Test.ContellerTest
{
    public  class DashboardControllerTest
    {

        
        private readonly IDashboard _dash;
        private readonly DashboardController _dashboardController;

        public DashboardControllerTest()
        {
            _dash = A.Fake<IDashboard>();


            _dashboardController = new DashboardController(_dash);

        }

        [Fact]
        public async void DashboardController_Index_ReturnsViewResut()
        {
            //Arange
            var movies = A.Fake<List<Movies>>();
            A.CallTo(() => _dash.GetAllMovies()).Returns(movies);
            var DashboardIndexDto=A.Fake<DashboardIndexDto>();

            //Act
            var resutl = await _dashboardController.GetAllMovies();

            //Assert
            resutl.Should().BeOfType<ViewResult>();
            var resultview = resutl as ViewResult;
            resultview!.Model.Should().BeAssignableTo<DashboardIndexDto>();

        }
    }
}
