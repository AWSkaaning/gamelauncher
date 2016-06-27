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
        private static GameController _instance;
        public static GameController GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameController();
                }

                return _instance;
            }
        }

        private List<Model.Game> Games = new List<Model.Game>();    //List to hold all the games in the application.

        //Hidden to make this class a singelton
        private GameController()
        { }


        public bool AddGame(Model.Game newGame)
        {
            bool wasGameAdded = false;

            if (newGame != null)
            {
                Games.Add(newGame);
                wasGameAdded = true;
            }

            return wasGameAdded;
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
            /*Using the fantastic automapper tool to make the items copying a bit easier
             * it may be a bit overkill for this simple task... but it sure makes it easy.*/
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Model.Game, Model.Game>());
            var mapper = mapperConfig.CreateMapper();

            List<Model.Game> result = new List<Model.Game>();

            foreach (var item in Games)
            {                
                Model.Game gameCopy = mapper.Map<Model.Game>(item);
                result.Add(gameCopy);
            }

            return result;
        }
    }
}
