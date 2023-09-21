using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListParser.Models
{
    public class Song
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Duration { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Song other)
            {
                return Title == other.Title && Artist == other.Artist && Duration == other.Duration;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Artist, Duration);
        }

    }
}
