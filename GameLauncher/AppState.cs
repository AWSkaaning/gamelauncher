using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace GameLauncher
{
    public class AppState : PropertyChangedNotification
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
        private ViewHandler Viewhandler
        {
            get { return _viewHandler; }
            set { _viewHandler = value; }
        }

        private ObservableCollection<Model.NavItem> _navItems = new ObservableCollection<Model.NavItem>();
        public ObservableCollection<Model.NavItem> NavItems
        {
            get { return _navItems; }
            set { _navItems = value; }
        }

        private GLEngine.GameController _gameController;
        public GLEngine.GameController GameController
        {
            get { return _gameController; }
            private set { _gameController = value; }
        }

        /// <summary>
        /// Return the path where the gamedata file is saved.
        /// </summary>
        public string GameDataPath
        {
            get
            {
                return string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "GameData.xml");
            }
        }

        #region Wrapper methods for the ViewHandler
        /*************************************************************************************************
         * Wrapper methods and properties for the viewhandler to implment the navitem selection concept. *
         *************************************************************************************************/

        private ViewModelBase _currentView = null;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                NotifyPropertyChanged("CurrentView");
            }
        }

        public ViewModelBase PreviousView
        {
            get
            {
                return Viewhandler.PreviousView;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="disposeCurrentView"></param>
        public void ChangeView(ViewModelBase view, bool disposeCurrentView)
        {
            Viewhandler.ChangeView(view, disposeCurrentView);
            CurrentView = Viewhandler.CurrentView;
            SelectionUpdate();
        }

        public void GotoPreviouView()
        {
            Viewhandler.GotoPreviousView();
            CurrentView = Viewhandler.CurrentView;
            SelectionUpdate();
        }

        #endregion

        /// <summary>
        /// Mark the right NavItem as selected.
        /// </summary>
        private void SelectionUpdate()
        {
            string viewclass = CurrentView.GetType().Name;

            foreach (var item in NavItems)
            {
                //Deselected the old selected navitem
                if (item.IsSelected == true && item.ClassName != CurrentView.GetType().Name)
                {
                    item.IsSelected = false;
                }

                //Mark the new selected navitem
                if (item.IsSelected == false && item.ClassName == CurrentView.GetType().Name)
                {
                    item.IsSelected = true;
                }
            }
        }


        //Hidden to make class a singelton
        private AppState()
        {
            _viewHandler = new ViewHandler();
            GameController = new GLEngine.GameController();
        }

    }
}