using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp5.CScode
{
    class DevelopSql
    {
        public enum OrderBy
        {
            Gname,
            Sellnumber,
            Gissue
        }

        SqlConnection sqlConn;
        public static DevelopSql instance;
        public DevelopSql()
        {
            instance = this;
            sqlConn = Sqlmanager.instance.sqlConn;
        }
        public List<ListItemGameinfo> Gameinfos=new List<ListItemGameinfo>();

        public string GetDevelopName()
        {
            string cmdStr = string.Format("SELECT dname FROM developed WHERE did={0}", Datamanager.instance.id);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                string uname = dataReader[0].ToString();
                dataReader.Close();
                return uname;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//获取开发商id
        public int updateUname(string name)
        {
            string cmdStr1 = string.Format("select * from developed where dname='{0}'", name);
            //查询用户名是否存在
            SqlCommand cmd1 = new SqlCommand(cmdStr1, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd1.ExecuteReader();
                if (dataReader.HasRows)
                {

                    dataReader.Close();
                    MessageBox.Show("该用户名已存在");
                    return -1;
                }
                dataReader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            string cmdStr = string.Format("UPDATE developed SET dname='{0}' WHERE did='{1}'", name, Datamanager.instance.id);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//更新开发商id
        public int Gettotalsellnum()
        {
            string cmdStr = string.Format("SELECT COUNT(*) from Purch WHERE gid IN(SELECT gid from Games WHERE did={0})", Datamanager.instance.id);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                int totalsellnum = dataReader.GetInt32(0);
                dataReader.Close();
                return totalsellnum;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//获取总销售量
        public double Gettotalsellmoney()
        {
            string cmdStr = string.Format("SELECT sum(pprice) from Purch WHERE gid IN(SELECT gid from Games WHERE did={0})", Datamanager.instance.id);
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
        }//获取总销售额
        public void GetGameList(OrderBy order, string keyword)
        {
            if (Gameinfos.Count != 0)
            {
                Gameinfos.Clear();
            }
            string cmdStr = "";

            if (keyword == "")
            {
                switch (order)
                {
                    case OrderBy.Gname:
                        cmdStr = string.Format("select gid from games where Did='{0}' order by gname",Datamanager.instance.id);
                        break;
                    case OrderBy.Gissue:
                        cmdStr = string.Format("select gid from games where Did='{0}' order by Gissue", Datamanager.instance.id);
                        break;
                    case OrderBy.Sellnumber:
                        cmdStr = string.Format("select gid from games where Did='{0}' order by (SELECT COUNT(*) FROM Purch where Purch.Gid=games.gid) DESC", Datamanager.instance.id);
                        break;
                }
            }
            else
            {
                switch (order)
                {
                    case OrderBy.Gname:
                        cmdStr = string.Format("select gid from games where Did='{0}' and gname like '%{1}%' order by gname", Datamanager.instance.id,keyword);
                        break;
                    case OrderBy.Gissue:
                        cmdStr = string.Format("select gid from games where Did='{0}' and gname like '%{1}%' order by Gissue", Datamanager.instance.id, keyword);
                        break;
                    case OrderBy.Sellnumber:
                        cmdStr = string.Format("select gid from games where Did='{0}' and gname like '%{1}%' order by (SELECT COUNT(*) FROM Purch where Purch.Gid=games.gid) DESC", Datamanager.instance.id, keyword);
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
                foreach (var gid in gids)
                {
                    string gimagepath = GameSql.instance.GetImages(gid)[0];
                    string gname = GameSql.instance.GetGameName(gid);
                    DateTime gissure = GameSql.instance.GetGameIssue(gid);
                    int gsellnum = GameSql.instance.GetSellNum(gid);
                    double gsellmoney = GameSql.instance.GetSellmoney(gid);
                    ListItemGameinfo gameinfo=new ListItemGameinfo(gid,gname,gimagepath,gissure,gsellnum,gsellmoney);
                    Gameinfos.Add(gameinfo);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//获取游戏列表
        public void DeleteUser(string uid)
        {
            string cmdStr = string.Format("DELETE FROM Users WHERE Uid='{0}'",uid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//删除用户
        public void Resetpwd(string uid,string pwd)    //将用户密码重置为pwd
        {
            byte[] src = Encoding.UTF8.GetBytes(pwd);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(src);
            string pwdmd5 = BitConverter.ToString(result).Replace("-", "");
            string cmdStr = string.Format("update users set upwd='{0}' where uid='{1}'",pwdmd5, uid);
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
        public DataTable GetDataSet(int order, string keyword)
        {
            string cmdStr = "";

            if (keyword == "")
            {
                switch (order)
                {
                    case 0:
                        cmdStr = string.Format("select uid,uname from users order by uid");
                        break;
                    case 1:
                        cmdStr = string.Format("select uid,uname from users order by uname");
                        break;                    
                }
            }
            else
            {
                switch (order)
                {
                    case 0:
                        cmdStr = string.Format("select uid,uname from users where uname like '%{0}%' order by uid", keyword);
                        break;
                    case 1:
                        cmdStr = string.Format("select uid,uname from users where uname like '%{0}%' order by uname", keyword);
                        break;
                }
            }
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            SqlDataAdapter sqlada = new SqlDataAdapter(cmd);
            try
            {
                
             
                DataTable table1 = new DataTable();
                sqlada.Fill(table1);
                return table1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//获取用户列表
    }
}
