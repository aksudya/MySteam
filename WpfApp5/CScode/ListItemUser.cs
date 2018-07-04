using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp5.CScode
{
    class ListItemUser
    {
        public string GameImagePath { get; set; }
        public double Pprice { get; set; }
        public string Ptime { get; set; }
        public string GameName { get; set; }
        public string Gid { get; set; }

        public ListItemUser(string gid, string GameName ,string GameImagePath, double Pprice, string Ptime)
        {
            this.GameName = GameName;
            this.GameImagePath = GameImagePath;
            this.Pprice = Pprice;
            this.Ptime = Ptime;
            this.Gid = gid;
        }
    }
}
