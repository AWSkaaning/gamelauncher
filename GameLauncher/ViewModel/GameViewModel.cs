using System.Diagnostics;
using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace GameLauncher.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        //TODO: Implement viewhandler to control views

        private bool _bigIconView;
        public bool BigIconView
        {
            get { return _bigIconView; }
            set
            {
                _bigIconView = value;
                RaisePropertyChanged("BigIconView");
            }
        }

        private bool _showFilterMenu;
        public bool ShowFilterMenu
        {
            get { return _showFilterMenu; }
            set
            {
                _showFilterMenu = value;
                RaisePropertyChanged("ShowFilterMenu");
            }
        }

        private Model.GameViewData _gameData = new Model.GameViewData();
        public Model.GameViewData GameData
        {
            get { return _gameData; }
            set { _gameData = value; }
        }
        
        private AppState appstate = AppState.GetInstance;

        public GameViewModel()
        {
            BigIconView = true;
            GameData.SearchQuery = "";
            GameData.Platforms = GetPlatforms();
        }

        public void EditGame(GLEngine.Model.Game game)
        {
            appstate.ChangeView(new ViewModel.EditGameViewModel(game), false);
        }

        public void EditGame(Guid gameId)
        {
            var game = appstate.GameController.GetAllGames().Where(x => x.Id == gameId).SingleOrDefault();
            if (game != null)
            {
                appstate.ChangeView(new ViewModel.EditGameViewModel(game), false);
            }
        }

        public bool LaunchGame(GLEngine.Model.Game game)
        {
            var result = false;
            var command = new List<string>();

            try
            {
                command = CommandParser(game.StartCommand);
            }
            catch (ArgumentException ex)
            {
                //Do something; log event!!
                result = false;
            }

            if (command.Count > 0)
            {
                var argString = "";
                for (int i = 1; i < command.Count; i++)
                {
                    argString += command[i] + " ";
                }

                var emuApp = new ProcessStartInfo();
                emuApp.FileName = command[0];
                emuApp.Arguments = argString;
                Process.Start(emuApp);

                result = true;
            }


            return result;
        }

        /// <summary>
        /// Parses the command line into a string list.
        /// </summary>
        /// <exception cref="ArgumentException">If the command line uses '"' markers and doesn't close the correctly</exception>
        /// <param name="command"></param>
        /// <returns>First item in list is this emu filepath after that are the parameters</returns>
        public List<string> CommandParser(string command)
        {
            /* Psudo code:
             * 1. Find index of first .exe" + 1 (+1: for the space separating the emu from parameters, if any)
             * 2. Everything after that should be parameters
             * 
             * Test string: //CommandParser(@"E:\Emulators\Playstation 2\pcsx2\pcsx2.exe ""d:\Games\PS2 games\gta 3.iso"" --fullscreen --C");
             */

            //First item should be the emu filepath.
            //Parameter after that.
            List<string> result = new List<string>();

            int firstExeIndex = command.IndexOf(".exe") + 4; //(+4: to account for the '.exe' part)
            result.Add(command.Substring(0, firstExeIndex)); //Emu filepath

            //Getting parameters (if any)
            if (firstExeIndex + 1 < command.Length)
            {
                var parameters = command.Substring(firstExeIndex + 1); //+1: to account for the space after the '.exe' part
                var paraArray = parameters.Split(' ');

                bool section = false;
                string temp = "";
                foreach (var item in paraArray)
                {
                    //Flip section switch on or off
                    if (item.Contains("\""))
                    {
                        if (item.Count(x => x == '\"') % 2 != 0)
                        {
                            //Do nothing
                            section = !section;
                        }               
                    }

                    //If inside a section just add item to the previous string
                    if (section)
                    {
                        temp += item + " ";
                    }
                    else
                    {
                        result.Add(temp + item); //add complete parameter to result list.
                        temp = ""; //Reset temp string to make it ready for the next item (if any)
                    }
                }

                //A section was not close correctly thus the command line is not valid and will give a wrong result.
                if (section == true)
                {
                    throw new ArgumentException("Section was not closed, please check command line!");
                }
            }

            return result;
        }


        private List<Model.SelectionItem> GetPlatforms()
        {
            List<Model.SelectionItem> result = new List<Model.SelectionItem>();

            foreach (var item in appstate.GameController.GetPlatforms())
            {
                result.Add(new Model.SelectionItem() { Caption = item, IsSelected = true });
            }

            //Make sure the output list is sorted
            result = result.OrderBy(o => o.Caption).ToList();

            return result;
        }

    }
}