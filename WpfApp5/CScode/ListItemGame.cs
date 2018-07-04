using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.CScode
{
    class ListItemGame
    {
        public string GameImagePath { get; set; }
        public string GameName { get; set; }
        public string Gid { get; set; }
        public string Goprice { get; set; }
        public string Gcprice { get; set; }
        public string Gdiscout { get; set; }        

        public ListItemGame(string gid, string GameName, 
            string GameImagePath, double Goprice, double Gcprice
            ,int Gdiscout,bool GdiscoutStatus)
        {
            this.GameName = GameName;
            this.GameImagePath = GameImagePath;
            if (!GdiscoutStatus)
            {
                this.Goprice = "\n";
                this.Gcprice = "¥" + Goprice.ToString("0.00");
                this.Gdiscout = "";
            }
            else
            {
                this.Goprice = "¥" + Goprice.ToString("0.00")+"\n";
                this.Gcprice = "¥" + Gcprice.ToString("0.00");
                this.Gdiscout = string.Format("-{0}%",Gdiscout);
            }
            
            
            this.Gid = gid;            
        }
    }
}
