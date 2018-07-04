using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp5.CScode
{
    class UserSql
    {
        public List<ListItemUser> UserBuyList = new List<ListItemUser>();

        public enum OrderBy
        {
            Gname,
            Ptime,
            Pprice
        }

        SqlConnection sqlConn;
        public static UserSql instance;

        public UserSql()
        {
            instance = this;
            sqlConn = Sqlmanager.instance.sqlConn;
        }

        public string GetUserName()
        {
            string cmdStr = string.Format("SELECT uname FROM users WHERE Uid={0}", Datamanager.instance.id);
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
        }//获取用户名
        public string GetUavatar()  //获取用户头像
        {
            string cmdStr = string.Format("SELECT Uavatar FROM users WHERE Uid={0}", Datamanager.instance.id);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                string Uavatar = dataReader[0].ToString();
                dataReader.Close();                
                return Uavatar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public double GetUbalance() //获取用户余额
        {
            string cmdStr = string.Format("SELECT Ubalance FROM users WHERE Uid={0}", Datamanager.instance.id);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                double Ubalance = dataReader.GetDouble(0);
                dataReader.Close();
                return Ubalance;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int getUserBuyList(OrderBy order)    //获取购买记录列表
        {
            if (UserBuyList.Count != 0)
            {
                UserBuyList.Clear();
            }
            if (order == OrderBy.Gname)
            {
                string cmdStr =
                    string.Format(
                        "SELECT Purch.gid,gname, gimage1, pprice, ptime from games, Purch " +
                        "where Uid = {0} and Purch.Gid = Games.Gid ORDER by Gname",
                        Datamanager.instance.id);
                SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        dataReader.Close();
                        return -1; //还没有购买任何游戏
                    }

                    while (dataReader.Read())
                    {
                        ListItemUser listItem = new ListItemUser(dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(),
                            dataReader.GetDouble(3), dataReader.GetDateTime(4).ToString("g"));
                        UserBuyList.Add(listItem);
                    }
                    dataReader.Close();
                    return 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else if (order == OrderBy.Ptime)
            {
                string cmdStr =
                    string.Format(
                        "SELECT Purch.gid,gname, gimage1, pprice, ptime from games, Purch " +
                        "where Uid = {0} and Purch.Gid = Games.Gid ORDER by Ptime",
                        Datamanager.instance.id);
                SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        dataReader.Close();
                        return -1; //还没有购买任何游戏
                    }

                    while (dataReader.Read())
                    {
                        ListItemUser listItem = new ListItemUser(dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(),
                            dataReader.GetDouble(3), dataReader.GetDateTime(4).ToString("g"));
                        UserBuyList.Add(listItem);
                    }
                    dataReader.Close();
                    return 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                string cmdStr =
                    string.Format(
                        "SELECT Purch.gid,gname, gimage1, pprice, ptime from games, Purch " +
                        "where Uid = {0} and Purch.Gid = Games.Gid ORDER by Pprice",
                        Datamanager.instance.id);
                SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        dataReader.Close();
                        return -1; //还没有购买任何游戏
                    }

                    while (dataReader.Read())
                    {
                        ListItemUser listItem = new ListItemUser(dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(),
                            dataReader.GetDouble(3), dataReader.GetDateTime(4).ToString("g"));
                        UserBuyList.Add(listItem);
                    }
                    dataReader.Close();
                    return 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

        }
        public bool GetGameStatus(string gid) //获取是否已购买gid的游戏
        {
            string cmdStr = string.Format("SELECT * FROM Purch WHERE Uid={0} and Gid={1}",Datamanager.instance.id,gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Close();
                    return true; //已购买此游戏
                }
                else
                {
                    dataReader.Close();
                    return false;
                }               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int GetReview(string gid)        //获取评价 0为空，1为好评，-1为差评
        {
            string cmdStr = string.Format("SELECT review FROM purch WHERE Uid={0} and Gid={1}", Datamanager.instance.id, gid);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                int review;            
                if (dataReader[0].ToString() == "")
                {
                    review = 0;
                }
                else if(dataReader[0].ToString()=="True")
                {
                    review = 1;
                }
                else
                {
                    review = -1;
                }
                dataReader.Close();
                return review;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateReview(string gid,int review)    //更新评价信息
        {
            string cmdStr = string.Format("UPDATE Purch SET review={0} WHERE Uid='{1}' and gid='{2}'",review,Datamanager.instance.id,gid);
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
        public void UpdateAvater(string path)
        {
            string cmdStr = string.Format("UPDATE users SET uavatar='{0}' WHERE Uid='{1}'", path, Datamanager.instance.id);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//更新用户头像
        public int updateUname(string name)
        {
            string cmdStr1 = string.Format("select * from Users where Uname='{0}'", name);
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
            string cmdStr = string.Format("UPDATE users SET uname='{0}' WHERE Uid='{1}'", name, Datamanager.instance.id);
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
        }//更新用户名
        public void UpdateMoney(double money)
        {
            string cmdStr = string.Format("UPDATE users SET ubalance=ubalance+{0} WHERE Uid='{1}'", money, Datamanager.instance.id);
            SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
            try
            {
                cmd.ExecuteNonQuery();                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }//更新余额
        public int BuyGame(string gid,double gprice)
        {
            string sql = "procbuy";
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uid", SqlDbType.Text).Value = Datamanager.instance.id;
            cmd.Parameters.Add("@gid", SqlDbType.Text).Value = gid;
            cmd.Parameters.Add("@gprice", SqlDbType.Float).Value = gprice;
            cmd.Parameters.Add("@return", SqlDbType.Int).Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            int result = Convert.ToInt32(cmd.Parameters["@return"].Value);
            return result;
        }//购买游戏
    }
}
