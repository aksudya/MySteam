using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp5.CScode;
using WpfApp5;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Gid;
        public string Did;        
        public string storepath1 = "";
        public string storepath3 = "";
        public string storepath4 = "";
        public string storepath2 = "";



        public MainWindow(string gid)
        {
            InitializeComponent();
            this.Gid = gid;
            Did = Datamanager.instance.id;
            if (Gid != "")
            {
                Button_add.Visibility = Visibility.Hidden;
                changeContent();
            }
            else
            {                
                Button_update.Visibility = Visibility.Hidden;
            }
        }

        private void changeContent()  //填入游戏内容
        {
            textBox_name.Text = GameSql.instance.GetGameName(Gid);
            textBox_intro.Text = GameSql.instance.GetIntro(Gid);
            dp_issue.Text = GameSql.instance.GetGameIssue(Gid).ToLongDateString();
            textBox_oprice.Text = GameSql.instance.GetPrice(Gid)[0].ToString();
            textBox_cprice.Text = GameSql.instance.GetPrice(Gid)[1].ToString();
            dp_dstart.Text = GameSql.instance.GetDiscountStartTime(Gid).ToLongDateString();  //在GameSql中添加GetDiscountStartTime
            dp_dend.Text = GameSql.instance.GetDiscountEndTime(Gid).ToLongDateString();
            image1.Source = new BitmapImage(new Uri(GameSql.instance.GetImages(Gid)[0].ToString()));
            image2.Source = new BitmapImage(new Uri(GameSql.instance.GetImages(Gid)[1].ToString()));
            image3.Source = new BitmapImage(new Uri(GameSql.instance.GetImages(Gid)[2].ToString()));
            image4.Source = new BitmapImage(new Uri(GameSql.instance.GetImages(Gid)[3].ToString()));
            selling.IsChecked = GameSql.instance.GetSellingStatus(Gid);
            unselling.IsChecked = !GameSql.instance.GetSellingStatus(Gid);
            storepath1 = GameSql.instance.GetImages(Gid)[0].ToString();
            storepath2 = GameSql.instance.GetImages(Gid)[1].ToString();
            storepath3 = GameSql.instance.GetImages(Gid)[2].ToString();
            storepath4 = GameSql.instance.GetImages(Gid)[3].ToString();
        }

        public static int ExecuteSql(string sql)
        {
            SqlConnection con = Sqlmanager.instance.sqlConn;
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {               
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (SqlException ee)
            {
                throw new Exception(ee.Message);
            }
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string DatetimetoString(DateTime dateTime)
        {
            string re = string.Format("{0}-{1}-{2}", dateTime.Year, dateTime.Month, dateTime.Day);
            return re;
        }
        

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_name.Text.Trim() == "")
            {
                MessageBox.Show("游戏名为空");
                return;
            }
            if (textBox_intro.Text.Trim() == "")
            {
                MessageBox.Show("游戏介绍为空");
                return;
            }

            if (dp_issue.SelectedDate == null)
            {
                MessageBox.Show("发售日期为空");
                return;
            }

            if (textBox_oprice.Text.Trim() ==""|| textBox_cprice.Text.Trim()=="")
            {
                MessageBox.Show("价格为空");
                return;
            }

            if (dp_dstart.SelectedDate == null || dp_dend.SelectedDate == null ||dp_dstart.SelectedDate>dp_dend.SelectedDate)
            {
                MessageBox.Show("折扣日期设置不合法");
                return;
            }

            string name = textBox_name.Text.Trim();
            string intro = textBox_intro.Text.Trim();
            string issue = DatetimetoString((DateTime)dp_issue.SelectedDate);
            string oprice = textBox_oprice.Text.Trim();
            string cprice = textBox_cprice.Text.Trim();
            string dstart = DatetimetoString((DateTime)dp_dstart.SelectedDate);
            string dend = DatetimetoString((DateTime)dp_dend.SelectedDate);  
            
            bool onsell;

            if (selling.IsChecked == true && unselling.IsChecked == false)
                onsell = true;
            else
                onsell = false;

            string sql = "insert into Games(gname,gintro,gimage1,gimage2,gimage3,gimage4,gissue,dstart,dend,did,goprice,gcprice,gselling) values('" + name + "','" + intro + "','" + storepath1 + "','" + storepath2 + "','" + storepath3 + "','" + storepath4 + "','" + issue + "','" + dstart + "','" + dend + "','" + Did + "','" + oprice + "','" + cprice + "','" + onsell + "')";
            ExecuteSql(sql);

            

            this.Close();
        }

        private string chooseimage(object sender, RoutedEventArgs e)
        {
            string storepath = "";
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "image file|*.jgp;*.png;*.jpeg"
            };
            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string path = openFileDialog.FileName;
                if (!Directory.Exists(Environment.CurrentDirectory + "\\resource"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\resource");   //目标目录下不存在此文件夹即创建resource文件夹
                }
                string destPath = Environment.CurrentDirectory + "\\resource\\" + System.IO.Path.GetFileName(path);
                System.IO.File.Copy(path, destPath, true);                                   //复制到工作路径/resource目录下
                storepath = "pack://SiteOfOrigin:,,,/resource/" + System.IO.Path.GetFileName(path);    //存储进数据库的路径，之前的写法是错的
            }
            return storepath;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_name.Text.Trim() == "")
            {
                MessageBox.Show("游戏名为空");
                return;
            }
            if (textBox_intro.Text.Trim() == "")
            {
                MessageBox.Show("游戏介绍为空");
                return;
            }

            if (dp_issue.SelectedDate == null)
            {
                MessageBox.Show("发售日期为空");
                return;
            }

            if (textBox_oprice.Text.Trim() == "" || textBox_cprice.Text.Trim() == "")
            {
                MessageBox.Show("价格为空");
                return;
            }

            if (dp_dstart.SelectedDate == null || dp_dend.SelectedDate == null || dp_dstart.SelectedDate > dp_dend.SelectedDate)
            {
                MessageBox.Show("折扣日期设置不合法");
                return;
            }

            string name = textBox_name.Text.Trim();
            string intro = textBox_intro.Text.Trim();
            string issue = DatetimetoString((DateTime)dp_issue.SelectedDate);
            string oprice = textBox_oprice.Text.Trim();
            string cprice = textBox_cprice.Text.Trim();
            string dstart = DatetimetoString((DateTime)dp_dstart.SelectedDate);
            string dend = DatetimetoString((DateTime)dp_dend.SelectedDate);

            bool onsell;

            if (selling.IsChecked == true && unselling.IsChecked == false)
                onsell = true;
            else
                onsell = false;

           
        
            if (selling.IsChecked == unselling.IsChecked)
            {
                MessageBox.Show("上架状态错误");
                return;
            }

            string sql = "update  Games set Gname='" + name + "',Gintro='" + intro + "',Gimage1='" + storepath1 + "',Gimage2='" + storepath2 + "',Gimage3='" + storepath3 + "',Gimage4='" + storepath4 + "',Gissue='" + issue + "',dstart='" + dstart + "',dend='" + dend + "',Did='" + Did + "',Goprice='" + oprice + "',Gcprice='" + cprice + "',Gselling='" + onsell + "' where Gid='" + Gid + "'";
            ExecuteSql(sql);
            this.Close();
        }

        private void Button_image1_Click(object sender, RoutedEventArgs e)
        {
            string storepath;
            storepath = chooseimage(sender, e);
            if (storepath != "")
            {
                storepath1 = storepath;
            }
            image1.Source = new BitmapImage(new Uri(storepath1));
        }

        private void Button_image2_Click(object sender, RoutedEventArgs e)
        {
            string storepath;
            storepath = chooseimage(sender, e);
            if (storepath!="")
            {
                storepath2 = storepath;
            }           
            
            image2.Source = new BitmapImage(new Uri(storepath2));
        }

        private void Button_image3_Click(object sender, RoutedEventArgs e)
        {
            string storepath;
            storepath = chooseimage(sender, e);
            if (storepath != "")
            {
                storepath3 = storepath;
            }
            image3.Source = new BitmapImage(new Uri(storepath3));
        }

        private void Button_image4_Click(object sender, RoutedEventArgs e)
        {
            string storepath;
            storepath = chooseimage(sender, e);
            if (storepath != "")
            {
                storepath4 = storepath;
            }
            image4.Source = new BitmapImage(new Uri(storepath4));
        }
    }
}
