using System;
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
using System.Windows.Shapes;
using WpfApp5;
using WpfApp5.CScode;

namespace WpfApp5
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public string gid;
        public Window2(string gid)
        {
            this.gid = gid;
            InitializeComponent();
            setContent();
        }

        public void setContent()
        {
            if (Datamanager.instance.role == Datamanager.LoginRole.User)
            {
                if (UserSql.instance.GetGameStatus(gid))
                {
                    PageReview Review = new PageReview(this);
                    this.GameFrame.Content = Review;

                }
                else
                {
                    PageCanBuy CanBuy = new PageCanBuy(this);
                    this.GameFrame.Content = CanBuy;
                }

            }
            else
            {
                PageDeInfo deInfo=new PageDeInfo(this);
                this.GameFrame.Content = deInfo;
            }


            TextBlock_Gname.Text = GameSql.instance.GetGameName(gid);
            TextBlock_Gintro.Text = GameSql.instance.GetIntro(gid);
            List<string> imageList = GameSql.instance.GetImages(gid);
            mainShowImage.Source = new BitmapImage(new Uri(imageList[0]));
            Image_1.Source = new BitmapImage(new Uri(imageList[0]));
            Image_2.Source = new BitmapImage(new Uri(imageList[1]));
            Image_3.Source = new BitmapImage(new Uri(imageList[2]));
            Image_4.Source = new BitmapImage(new Uri(imageList[3]));
            int ReviewTotal = GameSql.instance.GetReviewTotal(gid);
            int ReviewGood = GameSql.instance.GetReviewGood(gid);
            string ReviewState;
            if (ReviewTotal != 0)
            {
                int ReviewRate = (ReviewGood * 100) / ReviewTotal;

                if (ReviewRate > 95)
                    ReviewState = "好评如潮";
                else if (ReviewRate > 80)
                    ReviewState = "特别好评";
                else if (ReviewRate > 70)
                    ReviewState = "多半好评";
                else if (ReviewRate > 40)
                    ReviewState = "褒贬不一";
                else if (ReviewRate > 20)
                    ReviewState = "多半差评";
                else
                    ReviewState = "特别差评";
                TextBlock_review2.Text = string.Format("({0}个评价中有{1}%为好评)", ReviewTotal, ReviewRate);
            }
            else
            {
                ReviewState = "尚无评价";
                TextBlock_review2.Text = "";
            }
            TextBlock_review1.Text = ReviewState;
            TextBlock_Gissue.Text = "发行日期：" + GameSql.instance.GetGameIssue(gid).ToLongDateString();
            TextBlock_dname.Text = "开发商：" + GameSql.instance.GetDevelop(gid);
        }

        private void image_enter(object sender, MouseEventArgs e)
        {
            Image thisImage = (Image)sender;
            this.mainShowImage.Source = thisImage.Source;
        }


    }
}
