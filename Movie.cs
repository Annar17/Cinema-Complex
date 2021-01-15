using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ergasia_2020
{
    public class Movie
    {
        public string Title;
        public string Genre;
        public string Starting;
        public string Ending;
        public string Room;
        public Movie(string t,string g,string st,string en, string r)
        {
            this.Title = t;
            this.Genre = g;
            this.Starting = st;
            this.Ending = en;
            this.Room = r;

        }

        public Movie() { }

        public override string ToString()
        {
            return Title;
        }
    }

}
