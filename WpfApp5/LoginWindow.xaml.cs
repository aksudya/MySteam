using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {       
            
        public LoginWindow()
        {
            Sqlmanager sqlmanager=new Sqlmanager();
            Datamanager datamanager=new Datamanager();
            UserSql userSql = new UserSql();
            GameSql gameSql=new GameSql();
            DevelopSql developSql=new DevelopSql();
            InitializeComponent();
        }

        private void Register_Clik(object sender, RoutedEventArgs e)
        {
            RegisterWindow window=new RegisterWindow();
            window.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {                       
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
              
            int result=Sqlmanager.instance.Login(TextBox_name.Text.Trim(),TextBox_password.Password.Trim(),Role_choose.SelectedIndex);
            if (result == 0)
            {
                Window1 window1 = new Window1();
                window1.Show();
                this.Close();
            }
            else if(result==1)
            {
                Window3 window3 = new Window3();
                window3.Show();
                this.Close();
            }
            else if (result == 2)
            {
                Window4 window4 = new Window4();
                window4.Show();
                this.Close();
            }
        }
    }
}
