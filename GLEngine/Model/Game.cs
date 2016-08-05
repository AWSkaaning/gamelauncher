using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace GLEngine.Model
{
    public class Game : IDataErrorInfo, INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public string Platform { get; set; }

        [XmlIgnore]
        private string _coverImagePath = "";
        public string CoverImagePath
        {
            get { return _coverImagePath; }
            set
            {
                _coverImagePath = value;
                NotifyPropertyChanged("CoverImagePath");
                NotifyPropertyChanged("HasCoverImage");
            }
        }

        public string StartDirectory { get; set; }
        public string StartCommand { get; set; }

        [XmlIgnore]
        public bool HasCoverImage
        {
            get
            {
                var result = false;
                if (string.IsNullOrEmpty(CoverImagePath) == false)
                {
                    result = true;
                }

                return result;
            }
        }

        //Hidden to make sure that a new game object is only created through the CreateGame method
        private Game()
        { }        

        public static Model.Game CreateGame(string title, string description, string platform)
        {
            Model.Game newGame = new Model.Game();
            newGame.Id = Guid.NewGuid();
            newGame.Title = title;
            newGame.Description = description;
            newGame.Platform = platform;
            newGame.CoverImagePath = "";

            return newGame;
        }

        [XmlIgnore]
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                    {
                        result = "A game must have a name";
                    }
                }

                if (columnName == "StartCommand")
                {
                    if (string.IsNullOrEmpty(StartCommand))
                    {
                        result = "A game must have a command to run";
                    }
                }

                return result;
            }
        }

        public Game Clone()
        {
            return (Game)this.MemberwiseClone();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}