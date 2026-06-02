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

namespace nowayapopitka.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public AppDbContext context = new AppDbContext();
        public Main()
        {
            InitializeComponent();
            FilterCombo.Items.Add("Все поставщики");
            foreach (var p in context.Providers)
                FilterCombo.Items.Add(p.Name);
            FilterCombo.SelectedIndex = 0;

            var user = MainWindow.CurrectUser;
            Sort.Visibility = Visibility.Collapsed;
            switch (user.roleId)
            {
                case 1: Sort.Visibility = Visibility.Visible; break;
            }
        }


        private void Update(object sender, RoutedEventArgs e)
        {
            if (Parent == null || FilterCombo.SelectedItem == null) return;

            var query = context.Products.AsQueryable();
            string text = SearchBox.Text.ToLower();
            Parent.Children.Clear();
            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(p => p.Name.ToLower().Contains(text) || p.Article.ToLower().Contains(text) || p.Description.ToLower().Contains(text));
            }

            if(FilterCombo.SelectedIndex >0)
            {
                var selectProvider = FilterCombo.SelectedItem.ToString();
                query = query.Where(p=>p.Provider.Name == selectProvider);
            }

            query = SortCombo.SelectedIndex switch
            {
                1 => query.OrderBy(p => p.Amount),
                2 => query.OrderByDescending(p => p.Amount),
                _ => query
            };

            foreach( var item in query)
            {
                Parent.Children.Add(new Pages.productElement(this,item));
            }

        }
    }
}
