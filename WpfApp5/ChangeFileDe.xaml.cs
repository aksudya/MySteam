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
    /// ChangeFileDe.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeFileDe : Window
    {
        public ChangeFileDe()
        {
            InitializeComponent();
            changeContent();
        }

        private void changeContent()
        {
           
            TextBox_uname.Text = DevelopSql.instance.GetDevelopName();
        }

        private void confirm_click(object sender, RoutedEventArgs e)
        {
            if (!Sqlmanager.instance.IsSafeStr(TextBox_uname.Text.Trim()) || TextBox_uname.Text.Trim() == "")
            {
                MessageBox.Show("登录名为空或不合法");
                return;
            }
            if (DevelopSql.instance.updateUname(TextBox_uname.Text.Trim()) != -1)
            {         
                this.Close();
            }
        }


        private void cancle_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
