using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher
{
    public class AppState
    {
        private static AppState _instance;
        public static AppState GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppState();
                }

                return _instance;
            }
        }

        private ViewHandler _viewHandler;
        public ViewHandler Viewhandler
        {
            get { return _viewHandler; }
            set { _viewHandler = value; }
        }

        private GLEngine.GameController _gameController;
        public GLEngine.GameController GameController
        {
            get { return _gameController; }
            private set { _gameController = value; }
        }
  
        //Hidden to make class a singelton
        private AppState()
        {
            _viewHandler = new ViewHandler();

            GameController = new GLEngine.GameController();
        }
    }
}
