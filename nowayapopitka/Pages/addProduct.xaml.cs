using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Win32;
using nowayapopitka.Context;
using nowayapopitka.Model;

namespace nowayapopitka.Pages
{
    /// <summary>
    /// Логика взаимодействия для addProduct.xaml
    /// </summary>
    public partial class addProduct : Page
    {
        public AppDbContext context = new AppDbContext();
        Product changeProduct;
        public addProduct(Product changeProduct = null)
        {
            InitializeComponent();

            var Category = context.Categories.ToList();
            var Units = context.Units.ToList();
            var Manufacturers = context.Manufacturers.ToList();
            var Providers = context.Providers.ToList();

            tbCategory.ItemsSource = Category;
            tbUnit.ItemsSource = Units;
            tbManufacturer.ItemsSource = Manufacturers;
            tbProvider.ItemsSource = Providers;

            if(changeProduct != null)
            {
                this.changeProduct = changeProduct;

                tbArticle.Text = changeProduct.Article;
                tbName.Text = changeProduct.Name;
                tbDescription.Text = changeProduct.Description;
                tbPrice.Text = changeProduct.Price.ToString();
                tbDiscount.Text = changeProduct.Discount.ToString();
                tbAmount.Text = changeProduct.Amount.ToString();
                tbCategory.SelectedValue = changeProduct.categoryId;
                tbUnit.SelectedValue = changeProduct.unitId;
                tbManufacturer.SelectedValue = changeProduct.manufacturerId;
                tbProvider.SelectedValue = changeProduct.providerId;

                if (!string.IsNullOrEmpty(changeProduct.Photo))
                {
                    Photo.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}/Image/{changeProduct.Photo}"));
                    Photo.Tag = changeProduct.Photo;    
                }

                Button.Content = "Изменить";
            }
          

        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            if (changeProduct == null)
            {
                Model.Product product = new()
                {
                    Article = tbArticle.Text,
                    Name = tbName.Text,
                    Description = tbDescription.Text,
                    Price = int.Parse(tbPrice.Text),
                    Discount = int.Parse(tbDiscount.Text),
                    Amount = int.Parse(tbAmount.Text),
                    categoryId = (int)tbCategory.SelectedValue,
                    providerId = (int)tbProvider.SelectedValue,
                    manufacturerId = (int)tbManufacturer.SelectedValue,
                    unitId = (int)tbUnit.SelectedValue,

                    Photo = Photo.Tag as string ?? string.Empty,
                };

                context.Add(product);
                context.SaveChanges();
                MainWindow.mainWindow.OpenPage(new Pages.Main());
            }
            else
            {
                changeProduct.Article = tbArticle.Text;
                changeProduct.Name = tbName.Text;
                changeProduct.Description = tbDescription.Text;
                changeProduct.Price = int.Parse(tbPrice.Text);
                changeProduct.Discount = int.Parse(tbDiscount.Text);
                changeProduct.Amount = int.Parse(tbAmount.Text);
                changeProduct.categoryId = (int)tbCategory.SelectedValue;
                changeProduct.providerId = (int)tbProvider.SelectedValue;
                changeProduct.manufacturerId = (int)tbManufacturer.SelectedValue;
                changeProduct.unitId = (int)tbUnit.SelectedValue;
                changeProduct.Photo = Photo.Tag.ToString() ?? string.Empty;

                context.Entry(changeProduct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                MainWindow.mainWindow.OpenPage(new Pages.Main());
            }
        }
        public string ImagePath(string fileName) => $"{Directory.GetCurrentDirectory()}/Image/{fileName}";

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "Image view|*.png;*.jpg;*.jpeg"
            };
            if (ofd.ShowDialog() != true) return;
            if (Photo.Tag is string oldImg && !string.IsNullOrEmpty(oldImg))
                File.Delete(ImagePath(oldImg));
            var imageName = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(ofd.FileName)}";
            var newImage = ImagePath(imageName);
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(newImage));
            File.Copy(ofd.FileName, newImage);
            Photo.Source = new BitmapImage(new Uri(newImage));
            Photo.Tag = imageName;
        }
    }
}
