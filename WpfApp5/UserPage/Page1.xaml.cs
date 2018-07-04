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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        private int mainIndex = 0;

        public Page1()
        {
            InitializeComponent();
            GameSql.instance.getGameList();
            mainIndex = 0;
            SetMainGame();
            SetSellWellGame();
            SetdiscountGame();

        }

        


        private void image_enter(object sender, MouseEventArgs e)
        {
            Image thisImage = (Image)sender;
            this.middleShowImage.Source = thisImage.Source;
        }

        private void middle_right_click(object sender, RoutedEventArgs e)
        {
            mainIndex = (mainIndex + 1) % 4;

            SetMainGame();
        }

        private void middle_left_click(object sender, RoutedEventArgs e)
        {
            mainIndex--;
            if (mainIndex == -1)
            {
                mainIndex = 4;
            }
            SetMainGame();
        }

        private void SetMainGame()
        {
            middleName.Content = GameSql.instance.ItemGames_recentsellwell[mainIndex].GameName;
            List<string> imageList = GameSql.instance.ItemGames_recentsellwell[mainIndex].GameImagePath;
            middleShowImage.Source = new BitmapImage(new Uri(imageList[0]));
            middleImage1.Source = new BitmapImage(new Uri(imageList[0]));
            middleImage2.Source = new BitmapImage(new Uri(imageList[1]));
            middleImage3.Source = new BitmapImage(new Uri(imageList[2]));
            middleImage4.Source = new BitmapImage(new Uri(imageList[3]));
            Button_main.Tag = GameSql.instance.ItemGames_recentsellwell[mainIndex].Gid;
        }

        private void SetSellWellGame()
        {

            if (GameSql.instance.ItemGames_sellwell.Count < 4)
            {
                Button_11.Visibility = Visibility.Hidden;
                Button_12.Visibility = Visibility.Hidden;
                Button_13.Visibility = Visibility.Hidden;
                Button_14.Visibility = Visibility.Hidden;
                return;
            }

            Image_11.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_sellwell[0].GameImagePath));
            Image_12.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_sellwell[1].GameImagePath));
            Image_13.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_sellwell[2].GameImagePath));
            Image_14.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_sellwell[3].GameImagePath));

            Label_name11.Content = GameSql.instance.ItemGames_sellwell[0].GameName;
            Label_name12.Content = GameSql.instance.ItemGames_sellwell[1].GameName;
            Label_name13.Content = GameSql.instance.ItemGames_sellwell[2].GameName;
            Label_name14.Content = GameSql.instance.ItemGames_sellwell[3].GameName;

            Label_discount11.Content = GameSql.instance.ItemGames_sellwell[0].Gdiscout;
            Label_discount12.Content = GameSql.instance.ItemGames_sellwell[1].Gdiscout;
            Label_discount13.Content = GameSql.instance.ItemGames_sellwell[2].Gdiscout;
            Label_discount14.Content = GameSql.instance.ItemGames_sellwell[3].Gdiscout;

            Label_cprice11.Text = GameSql.instance.ItemGames_sellwell[0].Gcprice;
            Label_cprice12.Text = GameSql.instance.ItemGames_sellwell[1].Gcprice;
            Label_cprice13.Text = GameSql.instance.ItemGames_sellwell[2].Gcprice;
            Label_cprice14.Text = GameSql.instance.ItemGames_sellwell[3].Gcprice;

            Label_oprice11.Text = GameSql.instance.ItemGames_sellwell[0].Goprice;
            Label_oprice12.Text = GameSql.instance.ItemGames_sellwell[1].Goprice;
            Label_oprice13.Text = GameSql.instance.ItemGames_sellwell[2].Goprice;
            Label_oprice14.Text = GameSql.instance.ItemGames_sellwell[3].Goprice;

            Button_11.Tag = GameSql.instance.ItemGames_sellwell[0].Gid;
            Button_12.Tag = GameSql.instance.ItemGames_sellwell[1].Gid;
            Button_13.Tag = GameSql.instance.ItemGames_sellwell[2].Gid;
            Button_14.Tag = GameSql.instance.ItemGames_sellwell[3].Gid;
        }

        private void SetdiscountGame()
        {
            if (GameSql.instance.ItemGames_discount.Count < 4)
            {
                Button_21.Visibility = Visibility.Hidden;
                Button_22.Visibility = Visibility.Hidden;
                Button_23.Visibility = Visibility.Hidden;
                Button_24.Visibility = Visibility.Hidden;
                return;
            }

            Image_21.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_discount[0].GameImagePath));
            Image_22.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_discount[1].GameImagePath));
            Image_23.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_discount[2].GameImagePath));
            Image_24.Source = new BitmapImage(
                new Uri(GameSql.instance.ItemGames_discount[3].GameImagePath));

            Label_name21.Content = GameSql.instance.ItemGames_discount[0].GameName;
            Label_name22.Content = GameSql.instance.ItemGames_discount[1].GameName;
            Label_name23.Content = GameSql.instance.ItemGames_discount[2].GameName;
            Label_name24.Content = GameSql.instance.ItemGames_discount[3].GameName;

            Label_discount21.Content = GameSql.instance.ItemGames_discount[0].Gdiscout;
            Label_discount22.Content = GameSql.instance.ItemGames_discount[1].Gdiscout;
            Label_discount23.Content = GameSql.instance.ItemGames_discount[2].Gdiscout;
            Label_discount24.Content = GameSql.instance.ItemGames_discount[3].Gdiscout;

            Label_cprice21.Text = GameSql.instance.ItemGames_discount[0].Gcprice;
            Label_cprice22.Text = GameSql.instance.ItemGames_discount[1].Gcprice;
            Label_cprice23.Text = GameSql.instance.ItemGames_discount[2].Gcprice;
            Label_cprice24.Text = GameSql.instance.ItemGames_discount[3].Gcprice;

            Label_oprice21.Text = GameSql.instance.ItemGames_discount[0].Goprice;
            Label_oprice22.Text = GameSql.instance.ItemGames_discount[1].Goprice;
            Label_oprice23.Text = GameSql.instance.ItemGames_discount[2].Goprice;
            Label_oprice24.Text = GameSql.instance.ItemGames_discount[3].Goprice;

            Button_21.Tag = GameSql.instance.ItemGames_discount[0].Gid;
            Button_22.Tag = GameSql.instance.ItemGames_discount[1].Gid;
            Button_23.Tag = GameSql.instance.ItemGames_discount[2].Gid;
            Button_24.Tag = GameSql.instance.ItemGames_discount[3].Gid;
        }

        private void Main_click(object sender, RoutedEventArgs e)
        {
            Button Btn = (Button)sender;
            Window2 window2 = new Window2(Btn.Tag.ToString());
            window2.Show();
        }


    }
}
