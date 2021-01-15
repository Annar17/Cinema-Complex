using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ergasia_2020
{
    public class Cleaning
    {
        public string Room;
        public string Title;
        public string Item;
        public string Dirty;
        public string Looking;
        public string Time;

        public Cleaning(string r, string t, string i, string d, string l,string time)
        {
            this.Room = r;
            this.Title = t;
            this.Item = i;
            this.Dirty = d;
            this.Looking = l;
            this.Time = time;
        }

        public Cleaning() { }
    }
}
