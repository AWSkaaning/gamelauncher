using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace GameLauncher.ViewModel
{
    public class EditGameViewModel : ViewModelBase
    {
        public bool CreatingNew { get; set; }
        public GLEngine.Model.Game Game { get; set; }

        public ICommand AcceptCommand { get; set; }

        #region Constructors
        public EditGameViewModel()
        {
            CreatingNew = true;
            Game = GLEngine.Model.Game.CreateGame("", "", "");

            SettingUpCommands();
        }

        public EditGameViewModel(GLEngine.Model.Game gameToEdit)
        {
            CreatingNew = false;
            Game = gameToEdit;

            SettingUpCommands();
        }
        #endregion

        private void SettingUpCommands()
        {
            AcceptCommand = new RelayCommand(Accept);
        }

        public void Accept()
        {
            if (Game != null)
            {
                if (CreatingNew)
                {
                    AppState.GetInstance.GameController.AddGame(Game);
                }
                else
                {
                    AppState.GetInstance.GameController.UpdateGame(Game);
                }
            }
        }
    }
}
