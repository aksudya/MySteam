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
    /// chargewindow.xaml 的交互逻辑
    /// </summary>
    public partial class chargewindow : Window
    {
        public chargewindow()
        {
            InitializeComponent();
        }

        private void confirm_click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text != "")
            {
                UserSql.instance.UpdateMoney(Convert.ToDouble(TextBox.Text));
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入充值金额");
            }
        }

        private void cancle_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool shiftKey = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;//判断shifu键是否按下
            if (shiftKey == true)                  //当按下shift
            {
                e.Handled = true;//不可输入
            }
            else                           //未按shift
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Tab || e.Key == Key.Enter ||e.Key==Key.Decimal))
                {
                    e.Handled = true;//不可输入
                }
            }
        }
    }
}
