using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace GLEngine
{
    public class GameController
    {
        private MapperConfiguration mapperConfig;                   //Configuration for the automapper lib.
        private List<Model.Game> Games = new List<Model.Game>();    //List to hold all the games in the application.

        public GameController()
        {
            //Seting up the automapper so it can map objects correctly
            mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Model.Game, Model.Game>());
        }

        /// <summary>
        /// Used for adding games to the internal list
        /// </summary>
        /// <param name="newGame"></param>
        /// <returns></returns>
        public bool AddGame(Model.Game newGame)
        {
            bool wasGameAdded = false;

            if (newGame != null)
            {
                //Make sure a game has a unique id
                if (newGame.Id == Guid.Empty)
                {
                    newGame.Id = Guid.NewGuid();
                }

                Games.Add(CopyGame(newGame));
                wasGameAdded = true;
            }

            return wasGameAdded;
        }

        /// <summary>
        /// Remove a game from the internal list
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown when the game to delete is not found</exception>
        /// <param name="game"></param>
        /// <returns></returns>
        public bool RemoveGame(Model.Game game)
        {
            bool wasGameRemoved = false;

            if (game != null)
            {
                var gameToRemove = Games.Where(x => x.Id == game.Id).SingleOrDefault();
                if (gameToRemove != null)
                {
                    Games.Remove(gameToRemove);
                    wasGameRemoved = true;
                }
                else
                {
                    throw new InvalidOperationException("Game was not found in internal game list");
                }
            }

            return wasGameRemoved;
        }

        /// <summary>
        /// Update a game with new changes
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public bool UpdateGame(Model.Game game)
        {
            bool wasGameupdated = false;

            if (game != null)
            {
                var gameFromStore = Games.Where(x => x.Id == game.Id).SingleOrDefault();
                if (gameFromStore != null)
                {
                    Games.Remove(gameFromStore);
                    AddGame(game);
                    
                    gameFromStore = game;
                    wasGameupdated = true;
                }
            }

            return wasGameupdated;
        }

        /// <summary>
        /// Get a list of all the added games.
        /// </summary>
        /// <remarks>
        /// Method will send a list with completely new objects back every time so the encapsulation principle is upheld.
        /// </remarks>
        /// <returns></returns>
        public List<Model.Game> GetAllGames()
        {           
            List<Model.Game> result = new List<Model.Game>();

            foreach (var item in Games)
            {
                result.Add(CopyGame(item));
            }

            return result;
        }


        /// <summary>
        /// Used to obtain a deep-copy of a game object
        /// </summary>
        /// <param name="orgGame"></param>
        /// <returns></returns>
        private Model.Game CopyGame(Model.Game orgGame)
        {
            /*Using the fantastic automapper tool to make the items copying a bit easier
             * it may be a bit overkill for this simple task... but it sure makes it easy.*/
            var mapper = mapperConfig.CreateMapper();

            Model.Game gameCopy = mapper.Map<Model.Game>(orgGame);

            return gameCopy;
        }
    }
}
