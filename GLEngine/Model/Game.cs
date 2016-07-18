using System;
using System.ComponentModel;

namespace GLEngine.Model
{
    public class Game : IDataErrorInfo
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

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                    {
                        result = "A game must have a name";
                    }
                }

                if (columnName == "StartCommand")
                {
                    if (string.IsNullOrEmpty(StartCommand))
                    {
                        result = "A game must have a command to run";
                    }
                }
                
                return result;
            }
        }

        public Game Clone()
        {
            return (Game)this.MemberwiseClone();
        }
    }
}
