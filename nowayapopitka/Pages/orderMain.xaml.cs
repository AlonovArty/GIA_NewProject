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
    /// Логика взаимодействия для orderMain.xaml
    /// </summary>
    public partial class orderMain : Page
    {
        public AppDbContext context = new AppDbContext();
        public orderMain()
        {
            InitializeComponent();
            foreach(var item in context.Orders.ToList())
            {
                Parent.Children.Add(new Pages.orderElements(this, item));
            }
        }
    }
}
