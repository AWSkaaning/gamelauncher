using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

namespace GameLauncher
{
    public class ViewHandler : ViewModelBase
    {
        private ViewModelBase _previousView;
        public ViewModelBase PreviousView
        {
            get { return _previousView; }
            set
            {
                _previousView = value;
                RaisePropertyChanged("PreviousView");
            }
        }

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        public void ChangeView(ViewModelBase newView, bool disposeCurrent)
        {
            if (disposeCurrent == false)
            {
                PreviousView = CurrentView;
            }

            CurrentView = newView;
        }

        public void GotoPreviousView()
        {
            CurrentView = PreviousView;
        }

    }
}
