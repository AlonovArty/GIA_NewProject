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
using Microsoft.VisualBasic;
using nowayapopitka.Context;
using nowayapopitka.Model;

namespace nowayapopitka.Pages
{
    /// <summary>
    /// Логика взаимодействия для addOrder.xaml
    /// </summary>
    public partial class addOrder : Page
    {
        public AppDbContext context = new AppDbContext();
        Order changeOrder;

        public addOrder(Order changeOrder = null)
        {
            InitializeComponent();
            var productId = context.Products.ToList();
            var Status = context.orderStatuses.ToList();
            var User = context.Users.ToList();
            var Pick = context.pickUpPoints.ToList();

            tbProduct.ItemsSource = productId;
            tbStatus.ItemsSource = Status;  
            tbUser.ItemsSource = User;
            tbPick.ItemsSource = Pick;

            if(changeOrder != null)
            {
                this.changeOrder = changeOrder;
                tborderDate.SelectedDate = changeOrder.orderDate.ToDateTime(TimeOnly.MinValue);
                tbdelivery.SelectedDate = changeOrder.deliveryDate.ToDateTime(TimeOnly.MinValue);
                tbProduct.SelectedValue = changeOrder.productId;
                tbUser.SelectedValue = changeOrder.userId;
                tbStatus.SelectedValue = changeOrder.orderStatusId;
                tbPick.SelectedValue = changeOrder.pickUpPointId;
                tbCode.Text = changeOrder.Code.ToString();

                Button.Content = "Изменить";
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            if(changeOrder == null)
            {
                Order order = new()
                {
                    orderDate = DateOnly.FromDateTime(tborderDate.SelectedDate.Value),
                    deliveryDate = DateOnly.FromDateTime(tbdelivery.SelectedDate.Value),
                    productId = (int)tbProduct.SelectedValue,
                    Code = int.Parse(tbCode.Text),
                    userId = (int)tbUser.SelectedValue,
                    pickUpPointId = (int)tbPick.SelectedValue,
                    orderStatusId = (int)tbStatus.SelectedValue,

                };
                context.Add(order);
                context.SaveChanges();
                MainWindow.mainWindow.OpenPage(new Pages.orderMain());
            }
            else
            {
                changeOrder.orderDate = DateOnly.FromDateTime(tborderDate.SelectedDate.Value);
                changeOrder.deliveryDate = DateOnly.FromDateTime(tbdelivery.SelectedDate.Value);
                changeOrder.productId = (int)tbProduct.SelectedValue;
                changeOrder.Code = int.Parse(tbCode.Text);
                changeOrder.userId = (int)tbUser.SelectedValue;
                changeOrder.pickUpPointId = (int)tbPick.SelectedValue;
                changeOrder.orderStatusId = (int)tbStatus.SelectedValue;

                context.Entry(changeOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                MainWindow.mainWindow.OpenPage(new Pages.orderMain());

            }
        }
    }
}
