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
using WpfApp5.CScode;

namespace WpfApp5
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            ChangeContent();
            frameMain.Navigate(new Uri("UserPage/Page1.xaml", UriKind.Relative));
        }

        private void ChangeContent()
        {
            Image_Ua.Source = new BitmapImage(new Uri(UserSql.instance.GetUavatar()));
            Label_uname.Content = "ID:" + UserSql.instance.GetUserName();
            Label_ubalance.Content = "余额：¥" + UserSql.instance.GetUbalance().ToString("0.00");
        }

        private void Store_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("UserPage/Page1.xaml", UriKind.Relative));
            ChangeContent();
        }


        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            UserSql.instance.getUserBuyList(UserSql.OrderBy.Gname);
            frameMain.Navigate(new Uri("UserPage/Page2.xaml", UriKind.Relative));
            ChangeContent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            GameSql.instance.getGameList(GameSql.OrderBy.Gname,"");
            frameMain.Navigate(new Uri("UserPage/Page3.xaml", UriKind.Relative));
            ChangeContent();
        }

        private void Filechange_click(object sender, RoutedEventArgs e)
        {
            Changefile changefile = new Changefile();
            changefile.ShowDialog();
            ChangeContent();
        }

        private void charge_click(object sender, RoutedEventArgs e)
        {
            chargewindow chargewindow=new chargewindow();
            chargewindow.ShowDialog();
            ChangeContent();
        }
    }
}
