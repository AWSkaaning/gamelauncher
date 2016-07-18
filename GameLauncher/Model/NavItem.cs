
namespace GameLauncher.Model
{
    public class NavItem : PropertyChangedNotification
    {
        public string Caption { get; set; }
        public int Index { get; set; }
        public string ClassName { get; set; }

        private bool _isSelected;
        public bool IsSelected
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