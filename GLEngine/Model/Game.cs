using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLEngine.Model
{
    public class Game
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public string Platform { get; set; }

        public string ImageFilePath { get; set; }
        public string StartDirectory { get; set; }
        public string StartCommand { get; set; }
    }
}
