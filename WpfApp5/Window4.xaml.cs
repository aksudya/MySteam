using System;
using System.Collections.Generic;
using System.Data;
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
    /// Window4.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
            setcontent();

        }

        private void setcontent()
        {
            Label_uname.Content = DevelopSql.instance.GetDevelopName();
            DataGrid.ItemsSource = DevelopSql.instance.GetDataSet(0, "").DefaultView;
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            int count = DataGrid.SelectedItems.Count;
            DataRowView[] drv = new DataRowView[count];
            for (int i = 0; i < count; i++)
            {
                drv[i] = DataGrid.SelectedItems[i] as DataRowView;
                DevelopSql.instance.DeleteUser(drv[i][0].ToString());
            }

            MessageBox.Show("删除成功");
            setcontent();
        }

        private void resetpass_click(object sender, RoutedEventArgs e)
        {
            int count = DataGrid.SelectedItems.Count;
            DataRowView[] drv = new DataRowView[count];
            for (int i = 0; i < count; i++)
            {
                drv[i] = DataGrid.SelectedItems[i] as DataRowView;
                DevelopSql.instance.Resetpwd(drv[i][0].ToString(),"123456");
            }

            MessageBox.Show("重置成功");
            setcontent();
        }

        private void Search_click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = DevelopSql.instance.GetDataSet(Order_choose.SelectedIndex, TextBox_search.Text).DefaultView;
        }

        private void ContextChange(object sender, SelectionChangedEventArgs e)
        {
            DataGrid.ItemsSource = DevelopSql.instance.GetDataSet(Order_choose.SelectedIndex, TextBox_search.Text).DefaultView;
        }
    }
}
