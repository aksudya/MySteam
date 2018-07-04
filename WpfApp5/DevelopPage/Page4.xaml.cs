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
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
            Lst.ItemsSource = DevelopSql.instance.Gameinfos;
        }

        private void Game_Btn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = (Button)sender;
            Window2 window2 = new Window2(Btn.Tag.ToString());
            window2.ShowDialog();
            Lst.ItemsSource = DevelopSql.instance.Gameinfos;
            this.Lst.Items.Refresh();
        }

        private void Search_click(object sender, RoutedEventArgs e)
        {
            if (Order_choose.SelectedIndex == 0)
            {
                DevelopSql.instance.GetGameList(DevelopSql.OrderBy.Gname, TextBox_search.Text);
                this.Lst.Items.Refresh();
            }
            else if (Order_choose.SelectedIndex == 1)
            {
                DevelopSql.instance.GetGameList(DevelopSql.OrderBy.Sellnumber, TextBox_search.Text);
                this.Lst.Items.Refresh();
            }
            else if (Order_choose.SelectedIndex == 2)
            {
                DevelopSql.instance.GetGameList(DevelopSql.OrderBy.Gissue, TextBox_search.Text);
                this.Lst.Items.Refresh();
            }
        }

        private void ContextChange(object sender, SelectionChangedEventArgs e)
        {
            if (Order_choose.SelectedIndex == 0)
            {
                DevelopSql.instance.GetGameList(DevelopSql.OrderBy.Gname, TextBox_search.Text);
                this.Lst.Items.Refresh();
            }
            else if (Order_choose.SelectedIndex == 1)
            {
                DevelopSql.instance.GetGameList(DevelopSql.OrderBy.Sellnumber, TextBox_search.Text);
                this.Lst.Items.Refresh();
            }
            else if (Order_choose.SelectedIndex == 2)
            {
                DevelopSql.instance.GetGameList(DevelopSql.OrderBy.Gissue, TextBox_search.Text);
                this.Lst.Items.Refresh();
            }
        }
    }
}
