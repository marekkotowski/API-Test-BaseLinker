using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker.Modele
{
    public interface IOrder
    {
        OrderModel getOrder(); 
    }


    public class OrderModel :IOrder
    {

        public int Order_ID { get; set; }
        public int date_add { get; set; }

        public int order_status_id { get; set; }

        public List<ProductModel> Products { get; set; }


        public OrderModel()
        {
            this.Products = new List<ProductModel>(); 
        }

        /// <summary>
        /// The method allows you to download orders from a specific date from the BaseLinker order manager
        /// </summary>
        /// <returns></returns>
        public static List<OrderModel> getOrders()
        {
            string metoda = "getOrders";
            string parametry = @"{""get_unconfirmed_orders"":true} ";
            List<OrderModel> orders = new List<OrderModel>(); 
            try
            {
                APP.APIConnectModel ApiConnect = new APP.APIConnectModel();
                string postOrders = ApiConnect.Post(metoda, parametry);
                orders = GetPostOrders(postOrders);
            }
            catch (Exception ex)
            {
                APP.APP.APPLogger.AddLog(ex.Message);
            }

            return orders; 
        }

        public OrderModel getOrder()
        {
            return this; 
        }




        /// <summary>
        /// The method allows adding a new order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int addOrder(OrderModel _order)
        {
            return this.Order_ID;
        }

        public void AddProduct(ProductModel _product)
        {
            this.Products.Add(_product);
        }

        private static List<OrderModel> GetPostOrders(string _postorders)
        {

            try
            {
                List<OrderModel> orders = new List<OrderModel>();
                Newtonsoft.Json.Linq.JObject obiektpobrany = Newtonsoft.Json.Linq.JObject.Parse(_postorders);
                IList<Newtonsoft.Json.Linq.JToken> Tokenzamowienia = obiektpobrany["orders"].Children().ToList();
                //IList<Modele.OrderModel> pobraneZamowienia = new List<Modele.OrderModel>();
                orders = new List<Modele.OrderModel>();
                foreach (Newtonsoft.Json.Linq.JToken token in Tokenzamowienia)
                {
                    Modele.OrderModel zamowienie = token.ToObject<Modele.OrderModel>();
                    IList<Newtonsoft.Json.Linq.JToken> TokenProduktu = token["products"].Children().ToList();
                    foreach (Newtonsoft.Json.Linq.JToken tokenprodukt in TokenProduktu)
                    {
                        Modele.ProductModel produkt = tokenprodukt.ToObject<Modele.ProductModel>();
                        zamowienie.AddProduct(produkt);
                    }
                    orders.Add(zamowienie);
                }
                return orders;
            }
            catch (Newtonsoft.Json.JsonReaderException exjs)
            {
                APP.APP.APPLogger.AddLog(exjs.Message);
                return null;
            }
            catch (ArgumentNullException exnu)
            {
                APP.APP.APPLogger.AddLog(exnu.Message);
                return null;
            }
            catch (Exception ex)
            {
                APP.APP.APPLogger.AddLog(ex.Message);
                return null;
            }
        }

    }
}
