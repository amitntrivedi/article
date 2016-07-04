using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using GameOfLife.Controllers;
using System.Web.Http; 


namespace GameOfLife.Tests.Controllers
{
    [TestClass]
    public class GameOfLifeControllerTest
    {
        [TestMethod]
        public void generateNewBoardShouldReturnGrid()
        {
            // Arrange
            GameOfLifeController controller = new GameOfLifeController();
            controller.Request = new System.Net.Http.HttpRequestMessage (); 
            controller.Configuration = new HttpConfiguration();
            Models.Board prevBoard = new Models.Board(10,10); 


            // Act
            var response = controller.CreateNewBoard(prevBoard);

            // Assert
 
            Models.Board expectedGrid = new Models.Board();
            Assert.AreEqual(10, response.RowCount);  
        }
    }
}
