using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using nowayapopitka.Context;

namespace nowayapopitka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        public static MainWindow mainWindow;
        public static Model.User CurrectUser;
        public MainWindow()

        {
            mainWindow = this;
            InitializeComponent();
            Auth.Visibility = Visibility.Visible;
            Products.Visibility = Visibility.Collapsed;
            AddProduct.Visibility = Visibility.Collapsed;
        }

        public void OpenPage(Page pages)
        {
            Frame.Navigate(pages);
        }

        private void Logins(object sender, RoutedEventArgs e)
        {
            try
            {
                using var context = new AppDbContext();
                var user = context.Users.FirstOrDefault(r => r.Login == Login.Text && r.Password == Password.Text);

                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }
                else { 
                    CurrectUser = user;
                    Fio.Text = user.Fio;
                    
                    Auth.Visibility = Visibility.Collapsed;
                    Products.Visibility = Visibility.Visible;

                    switch (user.roleId)
                    {
                        case 1: Products.Visibility = Visibility.Visible; AddProduct.Visibility = Visibility.Visible; break;
                    }


                    OpenPage(new Pages.Main());
                   

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.Main());
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.addProduct());
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.orderMain());
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(new Pages.addOrder());
        }
    }
}