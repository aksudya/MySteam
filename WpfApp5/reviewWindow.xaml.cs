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
    /// reviewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class reviewWindow : Window
    {
        public string gid;
        public reviewWindow(string gid)
        {
            this.gid = gid;
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButton_good.IsChecked==true)
            {
                UserSql.instance.UpdateReview(gid,1);
            }
            else if (RadioButton_bad.IsChecked == true)
            {
                UserSql.instance.UpdateReview(gid, 0);
            }
            else
            {
                MessageBox.Show("请选择评价");
            }
            this.Close();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void good_click(object sender, RoutedEventArgs e)
        {
            RadioButton_bad.IsChecked = false;
        }

        private void bad_click(object sender, RoutedEventArgs e)
        {
            RadioButton_good.IsChecked = false;
        }
    }
}
