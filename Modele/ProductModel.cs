using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker.Modele
{

    public class ProductModel
    {
        public string storage { get; set; }
        public int storage_id { get; set; }
        public string product_id { get; set; }
        public string ean { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public decimal price_brutto { get; set; }
        public decimal price_wholesale_netto { get; set; }
        public int tax_rate { get; set; }
        public decimal weight { get; set; }
        public string description { get; set; }
        public string description_extra1 { get; set; }
        public string description_extra2 { get; set; }
        public string description_extra3 { get; set; }
        public string description_extra4 { get; set; }
        public string mMan_name { get; set; }
        public int category_id { get; set; }
        public List<string> images;

        public ProductModel()
        {
            images = new List<string>(); 
        }


    }
}
