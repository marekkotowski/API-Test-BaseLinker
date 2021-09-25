using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker.Modele
{

    public class ProductModel
    {
        public int Storage_id { get; set; }
        public string Product_id { get; set; }
        public string Ean { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price_brutto { get; set; }
        public decimal Price_wholesale_netto { get; set; }
        public int Tax_rate { get; set; }
        public decimal Weight { get; set; }
        public string Description { get; set; }
        public string Description_extra1 { get; set; }
        public string Description_extra2 { get; set; }
        public string Description_extra3 { get; set; }
        public string Description_extra4 { get; set; }
        public string Man_name { get; set; }
        public int Category_id { get; set; }
        public List<string> Images;

        public ProductModel()
        { 
        
        }


    }
}
