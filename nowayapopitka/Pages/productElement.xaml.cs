using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для productElement.xaml
    /// </summary>
    public partial class productElement : UserControl
    {
        public AppDbContext context = new AppDbContext();
        Product product;
        Main main;
        public productElement(Main main,Product product)
        {
            InitializeComponent();
            this.product = product;
            this.main = main;
            Edits.Visibility = Visibility.Collapsed;
            var user = MainWindow.CurrectUser;

            switch (user.roleId)
            {
                case 1: Edits.Visibility = Visibility.Visible; break;
            }

            tbArticle.Text = product.Article;
            tbName.Text = product.Name;
            tbDescription.Text = product.Description;
            tbPrice.Text = product.Price.ToString();
            tbAmount.Text = product.Amount.ToString();
            tbCategory.Text = context.Categories.FirstOrDefault(r => r.Id == product.categoryId).Name;
            tbUnit.Text = context.Units.FirstOrDefault(r => r.Id == product.unitId).Name;
            tbManufacturer.Text = context.Manufacturers.FirstOrDefault(r => r.Id == product.manufacturerId).Name;
            tbProvider.Text = context.Providers.FirstOrDefault(r => r.Id == product.providerId).Name;


            if (string.IsNullOrEmpty(product.Photo))
            {
                Photo.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/Image/picture.png"));
            }
            else
            {
                Photo.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/Image/{product.Photo}"));
            }

            if(product.Discount <= 0)
            {
                tbDiscount.Text = Cult(product.Price,product.Discount).ToString();
                tbDiscount.Background = Brushes.Green;
            }
            else
            {
                tbDiscount.Text = Cult(product.Price, product.Discount).ToString();
                tbDiscount.Background = (Brush)new BrushConverter().ConvertFromString("#CD5C5C");
            }
        }

        public double Cult(double basePrice,double Discount)
        {
            return basePrice - (basePrice / 100 * Discount);
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Pages.addProduct(this.product));
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            main.context.Remove(product);
            main.context.SaveChanges();
            main.Parent.Children.Remove(this);
        }
    }
}
