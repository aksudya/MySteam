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
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            //UserSql.instance.getUserBuyList(UserSql.OrderBy.Gname);
            InitializeComponent();
            Lst.ItemsSource = UserSql.instance.UserBuyList;
        }

        private void Game_Btn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = (Button) sender;
            Window2 window2 = new Window2(Btn.Tag.ToString());
            window2.Show();            
        }

        private void ContextChange(object sender, SelectionChangedEventArgs e)
        {
            
            if (Order_choose.SelectedIndex == 0)
            {
                UserSql.instance.getUserBuyList(UserSql.OrderBy.Gname);
                this.Lst.Items.Refresh();
            }
            else if(Order_choose.SelectedIndex==1)
            {
                UserSql.instance.getUserBuyList(UserSql.OrderBy.Ptime);                
                this.Lst.Items.Refresh();
            }
            else if (Order_choose.SelectedIndex == 2)
            {
                UserSql.instance.getUserBuyList(UserSql.OrderBy.Pprice);
                this.Lst.Items.Refresh();
            }
        }       
    }
}
