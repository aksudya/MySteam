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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp5.CScode;

namespace WpfApp5
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class PageCanBuy : Page
    {

        private Window2 _parentWin;
        private double buyprice=0;

        public PageCanBuy(Window2 parentWindow)
        {
            _parentWin = parentWindow;
            InitializeComponent();

            List<double> pricelist = GameSql.instance.GetPrice(_parentWin.gid);
            DateTime gissue = GameSql.instance.GetGameIssue(_parentWin.gid);
            if (gissue > DateTime.Now)
            {
                Label_GameName.Content = "尚未发售：" + GameSql.instance.GetGameName(_parentWin.gid);
                Label_text.Content = "发售日期" + gissue.ToLongDateString();
                Label_selling_value.Visibility = Visibility.Hidden;
                beforesell.Text = "\n";
                aftersell.Text = "¥" + pricelist[0].ToString("0.00");
                Button_buy.Visibility = Visibility.Hidden;
            }
            else
            {
                Label_GameName.Content = "购买" + GameSql.instance.GetGameName(_parentWin.gid);
                if (GameSql.instance.GetDiscountStatus(_parentWin.gid))
                {
                    DateTime discontend = GameSql.instance.GetDiscountEndTime(_parentWin.gid);
                    Label_text.Content = "特别价格！" + discontend.ToLongDateString() + "截止";
                    Label_selling_value.Content = string.Format("-{0}%", (int)pricelist[2]);
                    beforesell.Text = "¥" + pricelist[0].ToString("0.00")+"\n";
                    aftersell.Text = "¥" + pricelist[1].ToString("0.00");
                    buyprice = pricelist[1];
                }
                else
                {
                    Label_text.Content = "";
                    Label_selling_value.Visibility = Visibility.Hidden;
                    beforesell.Text = "\n";
                    aftersell.Text = "¥" + pricelist[0].ToString("0.00");
                    buyprice = pricelist[0];
                }
            }

        }

        private void Buy_click(object sender, RoutedEventArgs e)
        {
            int state=UserSql.instance.BuyGame(_parentWin.gid, buyprice);
            if (state == 1)
            {
                MessageBox.Show("购买成功");
            }
            else
            {
                MessageBox.Show("余额不足");
            }
        }
    }
}
