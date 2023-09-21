using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListParser.Models
{
    public class PlayList
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public List<Song> Songs { get; set; }

        public PlayList()
        {
            Songs = new List<Song>();
        }

    }
}
