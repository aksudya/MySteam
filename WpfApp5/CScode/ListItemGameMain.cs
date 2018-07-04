using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.CScode
{
    class ListItemGameMain
    {
        public List<string> GameImagePath=new List<string>();
        public string GameName { get; set; }
        public string Gid { get; set; }
        

        public ListItemGameMain(string gid, string GameName,
            List<string> GameImagePath)
        {
            this.GameName = GameName;
            this.GameImagePath.Add("");
            this.GameImagePath.Add("");
            this.GameImagePath.Add("");
            this.GameImagePath.Add("");
            this.GameImagePath[0] =  GameImagePath[0];
            this.GameImagePath[1] =  GameImagePath[1];
            this.GameImagePath[2] =  GameImagePath[2];
            this.GameImagePath[3] =  GameImagePath[3];

            this.Gid = gid;
        }
    }
}
