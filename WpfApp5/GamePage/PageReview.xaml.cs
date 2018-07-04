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
    /// PageReview.xaml 的交互逻辑
    /// </summary>
    public partial class PageReview : Page
    {

        private Window2 _parentWin;   

        public PageReview(Window2 ParentWindow)
        {
            _parentWin = ParentWindow;
            InitializeComponent();
            SetContent();
        }

        private void SetContent()
        {
            Label_gamename.Content = "您已购买："
                            + GameSql.instance.GetGameName(_parentWin.gid);
            int review = UserSql.instance.GetReview(_parentWin.gid);
            if (review == 0)
            {
                Label_review.Content = "您尚未评价此游戏";
                Button_review.Content = "立即评价";
            }
            else if (review == 1)
            {
                Label_review.Content = "您的评价为：好评";
                Button_review.Content = "修改评价";
            }
            else if (review == -1)
            {
                Label_review.Content = "您的评价为：差评";
                Button_review.Content = "修改评价";
            }

            List<double> pricelist = GameSql.instance.GetPrice(_parentWin.gid);
            bool DiscountStatus = GameSql.instance.GetDiscountStatus(_parentWin.gid);
            if (pricelist.Count == 1 || !DiscountStatus)
            {
                Label_discount_value.Visibility = Visibility.Hidden;
                Label_beforesell.Text = "\n";
                Label_aftersell.Text = "¥" + pricelist[0].ToString("0.00");
            }
            else
            {
                Label_discount_value.Content = string.Format("-{0}%", pricelist[2].ToString("0"));
                Label_beforesell.Text = "¥" + pricelist[0].ToString("0.00") + "\n";
                Label_aftersell.Text = "¥" + pricelist[1].ToString("0.00");
            }
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            reviewWindow reviewWindow=new reviewWindow(_parentWin.gid);
            reviewWindow.ShowDialog();
            SetContent();
            _parentWin.setContent();
        }
    }
}
