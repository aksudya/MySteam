using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.CScode
{
    class GameSql
    {
        SqlConnection sqlConn;
        public static GameSql instance;
        public List<ListItemGame> ItemGames = new List<ListItemGame>();
        public List<ListItemGameMain> ItemGames_recentsellwell = new List<ListItemGameMain>();
        public List<ListItemGame> ItemGames_sellwell = new List<ListItemGame>();
        public List<ListItemGame> ItemGames_discount = new List<ListItemGame>();
        public enum OrderBy
        {
            Gname,
            price,
            gissue
        }

        public GameSql()
        {
            instance = this;
            sqlConn = Sqlmanager.instance.sqlConn;
        }

        public string GetGameName(string gid)   //获取游戏名
        {
            string cmdStr = string.Format("SELECT gname FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                string gname = dataReader[0].ToString();
                dataReader.Close();
                return gname;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string GetIntro(string gid)  //获取游戏描述
        {
            string cmdStr = string.Format("SELECT gintro FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                string gintro = dataReader[0].ToString();
                dataReader.Close();
                return gintro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<string> GetImages(string gid)   //获取图片
        {
            List<string> imageList = new List<string>();
            string cmdStr = string.Format("SELECT gimage1,gimage2,gimage3,gimage4 FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                imageList.Add(dataReader[0].ToString());
                imageList.Add(dataReader[1].ToString());
                imageList.Add(dataReader[2].ToString());
                imageList.Add(dataReader[3].ToString());
                dataReader.Close();
                return imageList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DateTime GetGameIssue(string gid)    //获取发售日期
        {
            string cmdStr = string.Format("SELECT gissue FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                DateTime dateTime = dataReader.GetDateTime(0);
                dataReader.Close();
                return dateTime;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string GetDevelop(string gid)    //获取开发商名称
        {
            string cmdStr = string.Format("SELECT Dname FROM developed WHERE did" +
                                          "=(select did from games where gid='{0}')", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                string dname = dataReader[0].ToString();
                dataReader.Close();
                return dname;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int GetReviewTotal(string gid)   //获取总的评价个数
        {
            string cmdStr = string.Format("SELECT COUNT(*) from Purch WHERE gid='{0}' and review IS NOT NULL GROUP BY gid", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                int ReviewTotal = 0;
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    ReviewTotal = dataReader.GetInt32(0);

                }
                dataReader.Close();
                return ReviewTotal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int GetReviewGood(string gid)       //获取好评数
        {
            string cmdStr = string.Format("SELECT COUNT(*) from Purch WHERE gid='{0}' and review IS NOT NULL and review<>0 GROUP BY gid", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                int ReviewGood = 0;
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    ReviewGood = dataReader.GetInt32(0);
                }
                dataReader.Close();
                return ReviewGood;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool GetDiscountStatus(string gid)   //获取折扣状态
        {
            string cmdStr = string.Format("SELECT dstart,dend FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    dataReader.Close();
                    return false;
                }
                dataReader.Read();
                if (dataReader[0].ToString() == "" || dataReader[1].ToString() == "")
                {
                    dataReader.Close();
                    return false;
                }
                DateTime dstart = dataReader.GetDateTime(0);
                DateTime dend = dataReader.GetDateTime(1);
                dataReader.Close();
                if (dstart > DateTime.Now || dend < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DateTime GetDiscountStartTime(string gid) //获取折扣开始时间
        {
            string cmdStr = string.Format("SELECT dstart FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                DateTime dend = dataReader.GetDateTime(0);
                dataReader.Close();
                return dend;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DateTime GetDiscountEndTime(string gid)  //获取折扣结束时间
        {
            string cmdStr = string.Format("SELECT dend FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                DateTime dend = dataReader.GetDateTime(0);
                dataReader.Close();
                return dend;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<double> GetPrice(string gid) //获取价格，第一个为原价，第二个为折扣后的价格，第三个为折扣百分比   
            
        {
            List<double> priceList = new List<double>();
            string cmdStr = string.Format("SELECT goprice,gcprice FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                double goprice = dataReader.GetDouble(0);
                double gcprice = dataReader.GetDouble(1);
                dataReader.Close();
                if (gcprice == 0)
                {
                    priceList.Add(goprice);
                    return priceList;
                }
                else
                {
                    int discount = (int)(((goprice - gcprice) * 100) / goprice);
                    priceList.Add(goprice);
                    priceList.Add(gcprice);
                    priceList.Add(discount);
                    return priceList;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool GetSellingStatus(string gid) //获取是否还在销售
        {
            string cmdStr = string.Format("SELECT gselling FROM games WHERE Gid={0}", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                bool status;
                if (dataReader[0].ToString() == "True")
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                dataReader.Close();
                return status;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateSellingStatus(string gid,int value) //修改销售状态
        {
            string cmdStr = string.Format("update games set gselling={0} WHERE Gid={1}", value,gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void getGameList()
        {
            string cmdStr1 = string.Format("select gid from games where gselling=1 order by " +
                                                                 "(SELECT COUNT(*) FROM Purch WHERE Purch.gid=games.gid " +
                                                                 "AND (GETDATE()-Ptime)<=30 GROUP BY Purch.Gid) desc,Gname");  //获取近30天销量最好的游戏

            string cmdStr2 = string.Format("select gid from games where gselling=1 order by " +
                                          "(SELECT COUNT(*) FROM Purch WHERE Purch.gid=games.gid " +
                                          "GROUP BY Purch.Gid) desc,Gname");                            //获取总销量最好的游戏

           

            string cmdStr3 = string.Format("select gid from games where gselling=1 " +
                                           "and dstart<GETDATE() AND dend>GETDATE() order by " +
                                           "(SELECT COUNT(*) FROM Purch WHERE Purch.gid=games.gid " +
                                           "AND (GETDATE()-Ptime)<=30 GROUP BY Purch.Gid) desc,Gname"); //获取近30天销量最好且打折的游戏

            SqlCommand cmd1 = new SqlCommand(cmdStr1, sqlConn);
            SqlCommand cmd2 = new SqlCommand(cmdStr2, sqlConn);
            SqlCommand cmd3 = new SqlCommand(cmdStr3, sqlConn);
            List<string> gidList1 = new List<string>();
            List<string> gidList2 = new List<string>();
            List<string> gidList3 = new List<string>();
            if (ItemGames_discount.Count != 0)
            {
                ItemGames_discount.Clear();
            }
            if (ItemGames_recentsellwell.Count != 0)
            {
                ItemGames_recentsellwell.Clear();
            }
            if (ItemGames_sellwell.Count != 0)
            {
                ItemGames_sellwell.Clear();
            }

            try
            {
                SqlDataReader dataReader1 = cmd1.ExecuteReader();
                if (dataReader1.HasRows)
                {
                    while (dataReader1.Read())
                    {
                        string gid = dataReader1[0].ToString();
                        gidList1.Add(gid);
                    }
                }
                dataReader1.Close();

                SqlDataReader dataReader2 = cmd2.ExecuteReader();
                if (dataReader2.HasRows)
                {
                    while (dataReader2.Read())
                    {
                        string gid = dataReader2[0].ToString();
                        gidList2.Add(gid);
                    }
                }
                dataReader2.Close();

                SqlDataReader dataReader3 = cmd3.ExecuteReader();
                if (dataReader3.HasRows)
                {
                    while (dataReader3.Read())
                    {
                        string gid = dataReader3[0].ToString();
                        gidList3.Add(gid);
                    }
                }
                dataReader3.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            foreach (string gid in gidList1)
            {
                List<string> gimagepath = GetImages(gid);
                string gname = GetGameName(gid);
               
                ListItemGameMain item = new ListItemGameMain(gid, gname, gimagepath);
                ItemGames_recentsellwell.Add(item);
            }

            foreach (string gid in gidList2)
            {
                string gimagepath = GetImages(gid)[0];
                string gname = GetGameName(gid);
                bool gdiscountstatus = GetDiscountStatus(gid);
                List<double> priceList = GetPrice(gid);
                double goprice = 0;
                double gcprice = 0;
                int Gdiscout = 0;
                if (priceList.Count == 1)
                {
                    goprice = priceList[0];
                    gcprice = 0;
                    Gdiscout = 0;
                    gdiscountstatus = false;
                }
                else
                {
                    goprice = priceList[0];
                    gcprice = priceList[1];
                    Gdiscout = (int)priceList[2];
                }
                ListItemGame item = new ListItemGame(gid, gname, gimagepath, goprice, gcprice, Gdiscout, gdiscountstatus);
                ItemGames_sellwell.Add(item);
            }

            foreach (string gid in gidList3)
            {
                string gimagepath = GetImages(gid)[0];
                string gname = GetGameName(gid);
                bool gdiscountstatus = GetDiscountStatus(gid);
                List<double> priceList = GetPrice(gid);
                double goprice = 0;
                double gcprice = 0;
                int Gdiscout = 0;
                if (priceList.Count == 1)
                {
                    goprice = priceList[0];
                    gcprice = 0;
                    Gdiscout = 0;
                    gdiscountstatus = false;
                }
                else
                {
                    goprice = priceList[0];
                    gcprice = priceList[1];
                    Gdiscout = (int)priceList[2];
                }
                ListItemGame item = new ListItemGame(gid, gname, gimagepath, goprice, gcprice, Gdiscout, gdiscountstatus);
                ItemGames_discount.Add(item);
            }

        }
        public void getGameList(OrderBy order, string keyword)   //搜索列表
        {
            if (ItemGames.Count != 0)
            {
                ItemGames.Clear();
            }
            string cmdStr = "";

            if (keyword == "")
            {
                switch (order)
                {
                    case OrderBy.Gname:
                        cmdStr = string.Format("select gid from games where gselling=1 order by gname");
                        break;
                    case OrderBy.gissue:
                        cmdStr = string.Format("select gid from games where gselling=1 order by gissue");
                        break;
                    case OrderBy.price:
                        cmdStr = string.Format("select gid from games where gselling=1 order by gcprice");
                        break;
                }
            }
            else
            {
                switch (order)
                {
                    case OrderBy.Gname:
                        cmdStr = string.Format("select gid from games where gselling=1 and gname like '%{0}%' order by gname", keyword);
                        break;
                    case OrderBy.gissue:
                        cmdStr = string.Format("select gid from games where gselling=1 and gname like '%{0}%' order by gissue", keyword);
                        break;
                    case OrderBy.price:
                        cmdStr = string.Format("select gid from games where gselling=1 and gname like '%{0}%' order by gcprice", keyword);
                        break;
                }
            }
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                List<string> gids = new List<string>();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        string gid = dataReader[0].ToString();
                        gids.Add(gid);
                    }
                }
                dataReader.Close();
                foreach (string gid in gids)
                {
                    string gimagepath = GetImages(gid)[0];
                    string gname = GetGameName(gid);
                    bool gdiscountstatus = GetDiscountStatus(gid);
                    List<double> priceList = GetPrice(gid);
                    double goprice = 0;
                    double gcprice = 0;
                    int Gdiscout = 0;
                    if (priceList.Count == 1)
                    {
                        goprice = priceList[0];
                        gcprice = 0;
                        Gdiscout = 0;
                        gdiscountstatus = false;
                    }
                    else
                    {
                        goprice = priceList[0];
                        gcprice = priceList[1];
                        Gdiscout = (int)priceList[2];
                    }
                    ListItemGame item = new ListItemGame(gid, gname, gimagepath, goprice, gcprice, Gdiscout, gdiscountstatus);
                    ItemGames.Add(item);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int GetSellNum(string gid)       //获取总销量
        {
            string cmdStr = string.Format("SELECT COUNT(*) FROM Purch where Gid='{0}'", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                int result = dataReader.GetInt32(0);
                dataReader.Close();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int GetSellNum(string gid,DateTime start,DateTime end)       //获取总销量
        {
            string start_s = string.Format("{0}-{1}-{2}", start.Year, start.Month, start.Day);
            string end_s = string.Format("{0}-{1}-{2}", end.Year, end.Month, end.Day);
            string cmdStr = string.Format("SELECT COUNT(*) FROM Purch where Gid={0} AND Ptime >= convert(varchar(10),'{1}',120) AND Ptime<=convert(varchar(10),'{2}',120) ", gid,start_s,end_s);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                int result = dataReader.GetInt32(0);
                dataReader.Close();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public double GetSellmoney(string gid)      //获取总销售额
        {
            string cmdStr = string.Format("SELECT SUM(Pprice) FROM Purch where Gid='{0}'", gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                double totalsellmoney = 0;
                if (dataReader[0].ToString() == "")
                {
                    dataReader.Close();
                    return totalsellmoney;
                }
                totalsellmoney = dataReader.GetDouble(0);
                dataReader.Close();
                return totalsellmoney;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public double GetSellmoney(string gid, DateTime start, DateTime end) //获取总销售额
        {
            string start_s = string.Format("{0}-{1}-{2}", start.Year, start.Month, start.Day);
            string end_s = string.Format("{0}-{1}-{2}", end.Year, end.Month, end.Day);
            string cmdStr = string.Format("SELECT SUM(Pprice) FROM Purch where Gid={0} AND Ptime >= convert(varchar(10),'{1}',120) AND Ptime<=convert(varchar(10),'{2}',120) ", gid, start_s, end_s);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                double totalsellmoney = 0;
                if (dataReader[0].ToString() == "")
                {
                    dataReader.Close();
                    return totalsellmoney;
                }
                totalsellmoney = dataReader.GetDouble(0);
                dataReader.Close();
                return totalsellmoney;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
