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
            var myGameController = GLEngine.GameController.GetInstance;

            //add some games
            myGameController.AddGame(new GLEngine.Model.Game() { Title = "Doom", Description = "Doom test" });
            myGameController.AddGame(new GLEngine.Model.Game() { Title = "Keen", Description = "Keen test" });

            foreach (var item in myGameController.GetAllGames())
            {
                item.Description += "111";
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
