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
using WpfApplication1;

namespace WpfApp5
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            ChangeContent();
            frameMain.Navigate(new Uri("DevelopPage/Page4.xaml", UriKind.Relative));
        }

        private void ChangeContent()
        {
            Label_uname.Content = "ID:"+DevelopSql.instance.GetDevelopName();
            Label_dnumber.Content = "总销售游戏份数:"+DevelopSql.instance.Gettotalsellnum();
            Label_dmoney.Content = "总销售金额：¥" + DevelopSql.instance.Gettotalsellmoney();
        }

        private void List_Click(object sender, MouseButtonEventArgs e)
        {
            frameMain.Navigate(new Uri("DevelopPage/Page4.xaml", UriKind.Relative));
        }

        private void changefile_click(object sender, RoutedEventArgs e)
        {
            ChangeFileDe changefile = new ChangeFileDe();
            changefile.ShowDialog();
            ChangeContent();
        }

        private void NewGame_click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow("");
            window.ShowDialog();
            ChangeContent();
        }
    }
}
