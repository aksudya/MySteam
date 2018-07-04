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
using WpfApplication1;

namespace WpfApp5
{
    /// <summary>
    /// PageDeInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PageDeInfo : Page
    {
        private Window2 _parentWin;
        public PageDeInfo(Window2 parentWindow)
        {
            _parentWin = parentWindow;
            InitializeComponent();
            Changecontent();
        }

        private void Changecontent()
        {
            bool status = GameSql.instance.GetSellingStatus(_parentWin.gid);
            if (status)
            {
                this.Button_unsell.Content = "下架游戏";
            }
            else
            {
                Button_unsell.Content = "上架游戏";
            }

            Label_money.Content = "销售金额：¥" + GameSql.instance.GetSellmoney(_parentWin.gid).ToString("0.00");
            Label_num.Content ="销量："+ GameSql.instance.GetSellNum(_parentWin.gid).ToString();

        }

        private void Serch_Button_click(object sender, RoutedEventArgs e)
        {
            if (DatePicker_start.SelectedDate == null)
            {
                MessageBox.Show("请选择开始时间");
                return;
            }
            else if (DatePicker_end.SelectedDate == null)
            {
                MessageBox.Show("请选择结束时间");
                return;
            }
            
            DateTime start = (DateTime) DatePicker_start.SelectedDate;
            DateTime end = (DateTime)DatePicker_end.SelectedDate;

            if (end < start)
            {
                MessageBox.Show("结束日期需小于开始日期");
                return;
            }
            Label_num.Content = "销量：" + GameSql.instance.GetSellNum(_parentWin.gid, start, end);
            Label_money.Content = "销售金额：¥" + GameSql.instance.GetSellmoney(_parentWin.gid, start, end);
        }

        private void change_Button_click(object sender, RoutedEventArgs e)
        {
            MainWindow window=new MainWindow(_parentWin.gid);
            window.ShowDialog();
            Changecontent();
            _parentWin.setContent();
        }

        private void unsell_Button_click(object sender, RoutedEventArgs e)
        {
            bool status = GameSql.instance.GetSellingStatus(_parentWin.gid);
            if (status)
            {
                if (MessageBox.Show("确定下架此游戏?(下架后用户将无法购买此游戏)", "Confirm Message", MessageBoxButton.OKCancel,
                        MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    GameSql.instance.UpdateSellingStatus(_parentWin.gid,0);
                }
            }
            else
            {
                if (MessageBox.Show("确定重新上架此游戏?(上架后用户将可以购买此游戏)", "Confirm Message", MessageBoxButton.OKCancel,
                        MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    GameSql.instance.UpdateSellingStatus(_parentWin.gid, 1);
                }
            }

            DatePicker_start.SelectedDate = null;
            DatePicker_end.SelectedDate = null;
            Changecontent();
        }
    }
}
