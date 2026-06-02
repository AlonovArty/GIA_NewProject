using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nowayapopitka.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateOnly orderDate {  get; set; }
        public DateOnly deliveryDate { get; set; }
        public int productId { get; set; }
        public int Code { get; set; }
        public int userId { get; set; }
        public int orderStatusId { get; set; }
        public int pickUpPointId { get; set; }
        public Product Product { get; set; }
        public pickUpPoint pickUpPoint { get; set; }
        public orderStatus orderStatus  { get; set; }
        public User User { get; set; }
    }
}
