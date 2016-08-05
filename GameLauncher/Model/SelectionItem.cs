using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Model
{
    public class SelectionItem : PropertyChangedNotification
    {
        public string Caption { get; set; }

        private bool  _isSelected;
        public bool  IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

    }
}
