using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace GLEngine
{
    public class GameController
    {
        //Datestore where the games list is stored.
        private Datastore datastore;
        public bool UnsavedChanges { get; set; }

        public GameController()
        {
            datastore = new Datastore();
        }

        /// <summary>
        /// Used for adding games to the internal list
        /// </summary>
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

                datastore.Games.Add(newGame.Clone());
                wasGameAdded = true;
            }

            return wasGameAdded;
        }

        /// <summary>
        /// Remove a game from the internal list
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown when the game to delete is not found</exception>
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
                    UnsavedChanges = true;
                }
            }

            return wasGameupdated;
        }

        /// <summary>
        /// Get a list of all the added games.
        /// </summary>
        /// <remarks>
        /// Method will send a list with completely new objects back every time so the encapsulation is upheld.
        /// </remarks>
        public List<Model.Game> GetAllGames()
        {
            List<Model.Game> result = new List<Model.Game>();

            foreach (var item in datastore.Games)
            {
                result.Add(item.Clone());
            }

            return result;
        }

        /// <summary>
        /// Save game information to permanent storage
        /// </summary>
        /// <exception cref="ArgumentNullException">If no filepath is given</exception>
        /// <param name="filepath"></param>
        public bool SaveGameData(string filepath)
        {
            var wasDataSaved = false;

            if (string.IsNullOrEmpty(filepath) == false)
            {
                //Saving games information with a simple XmlSerializer.
                XmlSerializer serializerObj = new XmlSerializer(typeof(Datastore));
                using (TextWriter tw = new StreamWriter(filepath))
                {
                    serializerObj.Serialize(tw, datastore);
                    tw.Close();

                    wasDataSaved = true;
                    UnsavedChanges = false;
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
        /// <exception cref="FileNotFoundException">When gamedata file is not found at the given location</exception>
        /// <param name="filepath"></param>
        public bool LoadGameData(string filepath)
        {
            var wasDataLoaded = false;

            if (string.IsNullOrEmpty(filepath) == false)
            {
                if (DoesGameDataExist(filepath))
                {
                    XmlSerializer serializerObj = new XmlSerializer(typeof(Datastore));
                    using (TextReader tr = new StreamReader(filepath))
                    {
                        datastore = (Datastore)serializerObj.Deserialize(tr);
                        tr.Close();

                        wasDataLoaded = true;
                        UnsavedChanges = false;
                    }
                }
                else
                {
                    throw new FileNotFoundException("Gamedata file was not found at " + filepath);
                }
            }
            else
            {
                throw new ArgumentNullException("Must specify where to load file");
            }

            return wasDataLoaded;
        }


        /// <summary>
        /// Checks if a gamedata file exists on the given path.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool DoesGameDataExist(string filepath)
        {
            //TODO: Should have a more rigid checking method. (Maybe a Schema)
            return File.Exists(filepath);
        }

        public List<string> GetPlatforms()
        {
            var platforms = new List<string>();

            foreach (var item in GetAllGames())
            {
                if (platforms.Contains(item.Platform) == false)
                {
                    platforms.Add(item.Platform);
                }
            }

            return platforms;
        }
    }
}