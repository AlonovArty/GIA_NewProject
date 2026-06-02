using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nowayapopitka.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Article { get; set; }
        public string Name  { get; set; }
        public string Photo {  get; set; }
        public string Description { get; set; }
        public double Price     { get; set; }
        public double Discount { get; set; }
        public int Amount   { get; set; }
        public int categoryId   { get; set; }
        public int manufacturerId { get; set; }
        public int providerId   { get; set; }
        public int unitId   { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Category Category { get; set; }
        public Provider Provider { get; set; }
        public Unit Unit { get; set; }
    }
}
