using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using System.Xml.Serialization;
using System.IO;

namespace GLEngine
{
    public class GameController
    {
        private MapperConfiguration mapperConfig;    //Configuration for the automapper lib.
        private Datastore datastore;                 //Datestore where the games list is stored.

        public GameController()
        {
            //Seting up the automapper so it can map objects correctly
            mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Model.Game, Model.Game>());

            datastore = new Datastore();
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

                datastore.Games.Add(CopyGame(newGame));
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
                var gameToRemove = datastore.Games.Where(x => x.Id == game.Id).SingleOrDefault();
                if (gameToRemove != null)
                {
                    datastore.Games.Remove(gameToRemove);
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
                var gameFromStore = datastore.Games.Where(x => x.Id == game.Id).SingleOrDefault();
                if (gameFromStore != null)
                {
                    datastore.Games.Remove(gameFromStore);
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

            foreach (var item in datastore.Games)
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

        /// <summary>
        /// Save game information to permanent storage
        /// </summary>
        /// <exception cref="ArgumentNullException">If no filepath is given</exception>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool SaveData(string filepath)
        {
            var wasDataSaved = false;

            if (string.IsNullOrEmpty(filepath))
            {
                //Saving games information with a simple XmlSerializer.
                XmlSerializer serializerObj = new XmlSerializer(typeof(Datastore));
                using (TextWriter tw = new StreamWriter(filepath))
                {
                    serializerObj.Serialize(tw, datastore);
                    tw.Close();

                    wasDataSaved = true;
                }
            }
            else
            {
                throw new ArgumentNullException("Must specify where to save file");
            }

            return wasDataSaved;
        }

        /// <summary>
        /// Load saved game information.
        /// </summary>
        /// <exception cref="ArgumentNullException">When no filepath to data file is given</exception>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool LoadData(string filepath)
        {
            var wasDataLoaded = false;

            if (string.IsNullOrEmpty(filepath))
            {
                XmlSerializer serializerObj = new XmlSerializer(typeof(Datastore));
                using (TextReader tr = new StreamReader(filepath))
                {
                    datastore.Games = (List<Model.Game>)serializerObj.Deserialize(tr);
                    tr.Close();

                    wasDataLoaded = true;
                }
            }
            else
            {
                throw new ArgumentNullException("Must specify where to load file");
            }

            return wasDataLoaded;
        }
    }
}
