using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

namespace GameLauncher.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public AboutViewModel()
        {
            Title = "Game Lanucher";
            Content = "Icons by iconmonstr.com";
        }
    }
}