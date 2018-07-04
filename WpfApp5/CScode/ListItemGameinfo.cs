using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.CScode
{
    class ListItemGameinfo
    {
        public string GameImagePath { get; set; }
        public string GameName { get; set; }
        public string Gid { get; set; }
        public string Gissue { get; set; }
        public string Gsellnumber { get; set; }
        public string Gsellmoney { get; set; }

        public ListItemGameinfo(string gid, string GameName, string GameImagePath, DateTime gissue,int Gsellnumber,double Gsellmoney)
        {
            this.GameName = GameName;
            this.GameImagePath = GameImagePath;
            this.Gid = gid;

            this.Gissue = "发售日期:"+gissue.ToLongDateString();
            this.Gsellnumber = "总销量：" + Gsellnumber.ToString();
            this.Gsellmoney = "总销售金额：¥" + Gsellmoney.ToString("0.00");

        }
    }
}
