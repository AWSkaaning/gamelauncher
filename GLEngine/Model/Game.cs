using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLEngine.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public string Platform { get; set; }

        public string ImageFilePath { get; set; }
        public string StartDirectory { get; set; }
        public string StartCommand { get; set; }

        //Hidden to make sure that a new game object is created through the CreateGame method
        private Game()
        { }

        public static Model.Game CreateGame(string title, string description, string platform)
        {
            Model.Game newGame = new Model.Game();
            newGame.Id = Guid.NewGuid();
            newGame.Title = title;
            newGame.Description = description;
            newGame.Platform = platform;

            return newGame;
        }
    }
}
