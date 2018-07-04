using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp5.CScode;

namespace WpfApp5
{
    /// <summary>
    /// Changefile.xaml 的交互逻辑
    /// </summary>
    public partial class Changefile : Window
    {
        private string storepath = "";

        public Changefile()
        {
            InitializeComponent();
            changeContent();
        }

        private void changeContent()
        {
            Image_uav.Source = new BitmapImage(new Uri(UserSql.instance.GetUavatar()));//从数据库读取图片路径
            TextBox_uname.Text = UserSql.instance.GetUserName();
        }

        private void changeuav_click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "image file|*.jgp;*.png;*.jpeg"
            };
            var result = openFileDialog.ShowDialog();
            
            if (result == true)
            {                
                string path= openFileDialog.FileName;
                if (!Directory.Exists(Environment.CurrentDirectory + "\\resource"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\resource");   
                    //目标目录下不存在此文件夹即创建resource文件夹
                }
                string destPath = Environment.CurrentDirectory+"\\resource\\" + Path.GetFileName(path);
                System.IO.File.Copy(path, destPath, true);                                   
                //复制到工作路径/resource目录下
                storepath = "pack://SiteOfOrigin:,,,/resource/" + Path.GetFileName(path);    
                //存储进数据库的路径
                Image_uav.Source = new BitmapImage(new Uri(storepath));      
            }
        }

        private void confirm_click(object sender, RoutedEventArgs e)
        {
            if (!Sqlmanager.instance.IsSafeStr(TextBox_uname.Text.Trim()) || TextBox_uname.Text.Trim() == "")
            {
                MessageBox.Show("登录名为空或不合法");
                return;
            }
            if (TextBox_uname.Text == UserSql.instance.GetUserName() 
                || UserSql.instance.updateUname(TextBox_uname.Text.Trim()) != -1)
            {
                if (storepath != "")
                {
                    UserSql.instance.UpdateAvater(storepath);
                }
                this.Close();
            }

        }

        private void cancle_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
