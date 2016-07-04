// This app is just used as a scratchpad for testing the GLEngine implementation

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncherDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            var myGameController = new GLEngine.GameController();

            //add some games
            Console.WriteLine("Adding games");
            myGameController.AddGame(GLEngine.Model.Game.CreateGame("Doom", "Doom test", "PC"));
            myGameController.AddGame(GLEngine.Model.Game.CreateGame("Keen", "Keen test", "PC"));

            ////Changing the games description on the ref list
            //foreach (var item in myGameController.GetAllGames())
            //{
            //    item.Description += "111";
            //}

            //var internalGameList = myGameController.GetAllGames();
            //var gameToUpdate = internalGameList[0];
            //gameToUpdate.Title += "2222";

            //myGameController.UpdateGame(gameToUpdate);

            foreach (var item in myGameController.GetAllGames())
            {
                myGameController.RemoveGame(item);
            }

            Console.WriteLine("Printing out all games :");
            foreach (var item in myGameController.GetAllGames())
            {
                Console.WriteLine(String.Format("{0}\t{1}", item.Title, item.Description));
            }

            Console.ReadKey();
        }
    }
}
