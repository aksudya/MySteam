using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WpfApp5.CScode;

namespace WpfApp5
{
    class Sqlmanager
    {
        public SqlConnection sqlConn;
        public static Sqlmanager instance;        

        public bool IsSafeStr(string str)   //防止sql注入
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\{|\}|%|@|\*|!|\']");
        }

        public Sqlmanager()
        {
            instance = this;            
            ConnectWithAdmin();
        }

        public void ConnectWithAdmin()
        {
            string connStr = "server = localhost; database = steam; uid = sa; pwd = 123456";
            sqlConn = new SqlConnection(connStr);
            try
            {
                sqlConn.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }   //链接数据库

        public int Login(string username, string password,int role)
        {
            if (!IsSafeStr(username) || username == "")
            {
                MessageBox.Show("登录名为空或不合法");
                return -1;
            }
            if (!IsSafeStr(password) || password == "")
            {
                MessageBox.Show("密码为空或不合法");
                return -1;
            }

            byte[] src = Encoding.UTF8.GetBytes(password);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(src);
            string pwdmd5=BitConverter.ToString(result).Replace("-", "");

            if (role == 0)  //选择的角色是玩家
            {
                string cmdStr = string.Format("select Upwd from Users where Uname='{0}'", username);
                //查询用户名是否存在以及核对密码

                SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {

                        dataReader.Close();
                        MessageBox.Show("用户名不存在");
                        return -1;
                    }

                    dataReader.Read();
                    if (dataReader[0].ToString().Trim() != pwdmd5)
                    {
                        dataReader.Close();
                        MessageBox.Show("密码错误");
                        return -1;
                    }
                    else
                    {
                        dataReader.Close();
                        
                        MessageBox.Show("登录成功");
                        Datamanager.instance.role = Datamanager.LoginRole.User;
                        string cmdStr_id = string.Format("select uid from Users where Uname='{0}'", username);
                        SqlCommand cmd_id = new SqlCommand(cmdStr_id, sqlConn);
                        try
                        {
                            SqlDataReader dataReader_id = cmd_id.ExecuteReader();
                            dataReader_id.Read();
                            Datamanager.instance.id = dataReader_id[0].ToString().Trim();
                            dataReader_id.Close();
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                        //获取用户id
                        return 0;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else if (role == 1 || role==2)  //选择的角色是开发商
            {
                string cmdStr = string.Format("select dpwd from Developed where dname='{0}'", username);
                //查询用户名是否存在以及核对密码

                SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {

                        dataReader.Close();
                        MessageBox.Show("用户名不存在");
                        return -1;
                    }

                    dataReader.Read();
                    if (dataReader[0].ToString().Trim() != pwdmd5)
                    {
                        dataReader.Close();
                        MessageBox.Show("密码错误");
                        return -1;
                    }
                    else
                    {
                        dataReader.Close();
                        

                        MessageBox.Show("登录成功");
                        if(role==1)
                            Datamanager.instance.role = Datamanager.LoginRole.Developer;
                        else
                            Datamanager.instance.role = Datamanager.LoginRole.admin;
                        string cmdStr_id = string.Format("select did from developed where dname='{0}'", username);
                        SqlCommand cmd_id = new SqlCommand(cmdStr_id, sqlConn);
                        try
                        {
                            SqlDataReader dataReader_id = cmd_id.ExecuteReader();
                            dataReader_id.Read();
                            Datamanager.instance.id = dataReader_id[0].ToString().Trim();
                            dataReader_id.Close();
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                        //获取用户id
                        return role;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                MessageBox.Show("请选择登录角色");
                return -1;
            }            
        }   //登录

        public int Register(string username, string password, int role)
        {
            if (!IsSafeStr(username) || username == "")
            {
                MessageBox.Show("登录名为空或不合法");
                return -1;
            }
            if (!IsSafeStr(password) || password == "")
            {
                MessageBox.Show("密码为空或不合法");
                return -1;
            }

            byte[] src = Encoding.UTF8.GetBytes(password);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(src);
            string pwdmd5 = BitConverter.ToString(result).Replace("-", "");

            if (role == 0)  //选择的角色是玩家
            {
                string cmdStr = string.Format("select * from Users where Uname='{0}'", username);
                //查询用户名是否存在
                SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
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

                string cmdStr1 = string.Format(
             
                                                "INSERT INTO Users(Uname,Upwd) VALUES('{0}','{1}');"
                                               , username, pwdmd5 );
                //将用户名密码插入数据库
                SqlCommand cmd1 = new SqlCommand(cmdStr1, sqlConn);
                try
                {
                    int rows=cmd1.ExecuteNonQuery();
                    
                    MessageBox.Show("注册成功");
                    return 0;

                   
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else if (role == 1 ||role==2)  //选择的角色是开发商
            {
                string cmdStr = string.Format("select * from developed where dname='{0}'", username);
                //查询用户名是否存在
                SqlCommand cmd = new SqlCommand(cmdStr, sqlConn);
                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
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

                string cmdStr1 = string.Format( 
                                                "INSERT INTO developed(dname,dpwd) VALUES('{0}','{1}');"
                                              , username, pwdmd5 );
                //将用户名密码插入数据库
                SqlCommand cmd1 = new SqlCommand(cmdStr1, sqlConn);
                try
                {
                    int rows = cmd1.ExecuteNonQuery();
                    if (rows == 1)
                    {
                        MessageBox.Show("注册成功");
                        return 0;
                    }
                    return -1;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return -1;
        }//注册
    }
}
