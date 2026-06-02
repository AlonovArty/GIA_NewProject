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
using nowayapopitka.Context;
using nowayapopitka.Model;

namespace nowayapopitka.Pages
{
    /// <summary>
    /// Логика взаимодействия для orderElements.xaml
    /// </summary>
    public partial class orderElements : UserControl
    {
        public AppDbContext context = new AppDbContext();
        Order order;
        orderMain main;
        public orderElements(orderMain main, Order order)
        {
            InitializeComponent();
            Edits.Visibility = Visibility.Collapsed;
            var user = MainWindow.CurrectUser;
            switch (user.roleId)
            {
                case 1: Edits.Visibility = Visibility.Visible; break;
            }
            this.order = order; 
            this.main = main;
            tbOrderDate.Text = order.orderDate.ToString();
            tbDelivery.Text = order.deliveryDate.ToString();
            tbProduct.Text = context.Products.FirstOrDefault(r => r.Id == order.productId).Article;
            tbCode.Text = order.Code.ToString();
            tbUser.Text = context.Users.FirstOrDefault(r => r.Id == order.userId).Fio;
            tborderStatus.Text = context.orderStatuses.FirstOrDefault(r => r.Id == order.orderStatusId).Name;
            tbPick.Text = context.pickUpPoints.FirstOrDefault(r => r.Id == order.pickUpPointId).Address;
        }

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Pages.addOrder(this.order));
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            main.context.Remove(order);
            main.context.SaveChanges();
            main.Parent.Children.Remove(this);
        }
    }
}
