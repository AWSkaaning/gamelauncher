using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLEngineTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddingGameTest()
        {
            var gameEngine = new GLEngine.GameController();

            //Create new game to add
            GLEngine.Model.Game newGame = GLEngine.Model.Game.CreateGame("Test game", "", "");

            //Add game to controller
            gameEngine.AddGame(newGame);

            //There should be one game with the same title as the game we added.
            Assert.AreSame(newGame.Title, gameEngine.GetAllGames()[0].Title);
        }

        [TestMethod]
        public void RemovingGameTest()
        {
            var gameEngine = new GLEngine.GameController();

            //Create a new game to add to games
            GLEngine.Model.Game newGame = GLEngine.Model.Game.CreateGame("Test game", "", "");

            //Add game to the internal list in the gamecontroller
            gameEngine.AddGame(newGame);

            //Remove game
            gameEngine.RemoveGame(newGame);

            int gamesInInternalList = gameEngine.GetAllGames().Count;

            //There should be zero games in the internal game list.
            Assert.AreEqual(0, gamesInInternalList);
        }

        [TestMethod]
        public void UpdatingGameTest()
        {
            var gameEngine = new GLEngine.GameController();

            //Create a new game and added to the internal list
            var newGame = GLEngine.Model.Game.CreateGame("Test game", "", "");
            gameEngine.AddGame(newGame);            

            //Get a referance of the game and make a change
            var gameCopy = gameEngine.GetAllGames()[0];
            gameCopy.Title = "Test game 2";

            //Tell engine to update internal game to the new changes
            gameEngine.UpdateGame(gameCopy);

            //Test to make sure the internal game is not just a referance to the 'newGame' object
            newGame.Title = "Test game 3";

            //Game name should be "Test game 2"
            Assert.AreEqual("Test game 2", gameEngine.GetAllGames()[0].Title);            
        }
    }
}
