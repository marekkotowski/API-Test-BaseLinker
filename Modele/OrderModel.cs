using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API_Test_BaseLinker.Modele
{

    public class OrderModel 
    {
        public int order_id { get; set; }
        public int? shop_order_id { get; set; }
        public string external_order_id { get; set; }
        public string order_source { get; set; }
        public int order_source_id { get; set; }
        public string order_source_info { get; set; }
        public int order_status_id { get; set; }
        public long date_add { get; set; }
        public long date_confirmed { get; set; }
        public long date_in_status { get; set; }
        public bool confirmed { get; set; }
        public string user_login { get; set; }
        public string currency { get; set; }
        public string payment_method { get; set; }
        public int payment_method_cod { get; set; }
        public decimal payment_done { get; set; }
        public string user_comments { get; set; }
        public string admin_comments { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string delivery_method { get; set; }
        public decimal delivery_price { get; set; }
        public string delivery_package_module { get; set; }
        public string delivery_package_nr { get; set; }
        public string delivery_fullname { get; set; }
        public string delivery_company { get; set; }
        public string delivery_address { get; set; }
        public string delivery_postcode { get; set; }
        public string delivery_city { get; set; }
        public string delivery_country { get; set; }
        public string delivery_country_code { get; set; }
        public string delivery_point_id { get; set; }
        public string delivery_point_name { get; set; }
        public string delivery_point_address { get; set; }
        public string delivery_point_postcode { get; set; }
        public string delivery_point_city { get; set; }
        public string invoice_fullname { get; set; }
        public string invoice_company { get; set; }
        public string invoice_nip { get; set; }
        public string invoice_address { get; set; }
        public string invoice_postcode { get; set; }
        public string invoice_city { get; set; }
        public string invoice_country { get; set; }
        public string invoice_country_code { get; set; }
        public int want_invoice { get; set; }
        public string extra_field_1 { get; set; }
        public string extra_field_2 { get; set; }
        public string order_page { get; set; }
        public int pick_state { get; set; }
        public int pack_state { get; set; }
        public List<ProductModel> products { get; set; }


        public OrderModel()
        {
            this.products = new List<ProductModel>(); 
        }

        /// <summary>
        /// The method allows you to download orders from a specific date from the BaseLinker order manager
        /// </summary>
        /// <returns></returns>
        public static List<OrderModel> getOrders()
        {
            string metoda = "getOrders";
            //string parametry = @"{""get_unconfirmed_orders"":true} ";

            Dictionary<string, string> parametry = new Dictionary<string, string>();
            parametry.Add("get_unconfirmed_ordes", "true");

            string requestparam = JsonConvert.SerializeObject(parametry);

            List<OrderModel> orders = new List<OrderModel>(); 
            try
            {
                string postOrders = APP.APP.SenderClient.Post(metoda, requestparam);
                orders = GetPostOrders(postOrders);
            }
            catch (Exception ex)
            {
                APP.APP.APPLogger.AddLog(ex.Message);
            }

            return orders; 
        }


        /// <summary>
        /// The method allows adding a new order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int addOrder(OrderModel _order)
        {
            string metoda = "addOrder";
            string postOrders; 
            List<OrderModel> orders = new List<OrderModel>();
            string[] ignoruj = new string[] { "order_id", "shop_order_id", "external_order_id", "order_source", "order_source_id", "order_source_info", "date_confirmed", "date_in_status", "confirmed", "payment_done", "delivery_package_module", "delivery_package_nr", "delivery_country", "invoice_country", "order_page", "pick_state", "pack_state", "price_wholesale_netto", "description", "description_extra1", "description_extra2", "description_extra3", "description_extra4", "man_name", "category_id", "images" }; 
            string jsonorder = Newtonsoft.Json.JsonConvert.SerializeObject(_order, new JsonSerializerSettings { ContractResolver = new APP.JsonIgnoreResolver(ignoruj) });
            try
            {
                postOrders = APP.APP.SenderClient.Post(metoda, jsonorder);
                Newtonsoft.Json.Linq.JObject obiektpobrany = Newtonsoft.Json.Linq.JObject.Parse(postOrders);
                var firstresult = obiektpobrany.Children<JProperty>().First();  //pobieram pierwszy element z otrzymanego wyniku 
                if (firstresult.Value.ToString() == "SUCCESS")
                {
                    Dictionary<string, string> values = obiektpobrany.ToObject<Dictionary<string, string>>(); //pobranie id nowego zamówienia
                    string wynik = values["order_id"];
                    _order.order_id = Int32.Parse(wynik);
                }
                else
                {
                    APP.APP.APPLogger.AddLog($"Błąd dodania nowego zamówienia: {postOrders}");
                    _order.order_id = 0;
                }
            }
            catch (Exception ex)
            {
                APP.APP.APPLogger.AddLog(ex.Message);
            }
            return _order.order_id;
        }

        public void AddProduct(ProductModel _product)
        {
            this.products.Add(_product);
        }

        /// <summary>
        /// Deserialize JSON _postorders to list
        /// </summary>
        /// <param name="_postorders"></param>
        /// <returns></returns>
        private static List<OrderModel> GetPostOrders(string _postorders)
        {
            try
            {
                List<OrderModel> orders = new List<OrderModel>();
                Newtonsoft.Json.Linq.JObject obiektpobrany = Newtonsoft.Json.Linq.JObject.Parse(_postorders);
                var firstresult = obiektpobrany.Children<JProperty>().First() ;  //pierwszy element z odpowiedzi 
                if (firstresult.Value.ToString() == "SUCCESS")
                {
                    IList<Newtonsoft.Json.Linq.JToken> Tokenzamowienia = obiektpobrany["orders"].Children().ToList();
                    orders = new List<Modele.OrderModel>();
                    foreach (Newtonsoft.Json.Linq.JToken token in Tokenzamowienia)
                    {
                        Modele.OrderModel zamowienie = token.ToObject<Modele.OrderModel>();
                        orders.Add(zamowienie);
                    }
                    return orders;
                }
                else
                {
                    APP.APP.APPLogger.AddLog(_postorders);
                    return null; 
                }
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
                APP.APP.APPLogger.AddLog($"Nie otrzymano zamówień: {_postorders}; {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Copy order to new order
        /// </summary>
        /// <param name="_order"></param>
        /// <returns></returns>
        public OrderModel CopyasNew(OrderModel _order)
        {
            OrderModel newOrder = new OrderModel();
            newOrder.order_status_id = _order.order_status_id;

            var DatatimeOffset = new DateTimeOffset(DateTime.Now).ToUniversalTime();
            newOrder.date_add = new DateTimeOffset(DateTime.Now).ToUniversalTime().ToUnixTimeSeconds();
            newOrder.phone = _order.phone;
            newOrder.email = _order.email;
            newOrder.user_login = _order.user_login;
            newOrder.payment_method = _order.payment_method;
            newOrder.user_comments = _order.user_comments;
            newOrder.admin_comments = _order.admin_comments+$"Zamówienie utworzone na podstawie <{_order.order_id}>";
            newOrder.payment_method_cod = _order.payment_method_cod;
            newOrder.delivery_method = _order.delivery_method;
            newOrder.delivery_price = _order.delivery_price;
            newOrder.delivery_fullname = _order.delivery_fullname;
            newOrder.delivery_company = _order.delivery_company;
            newOrder.delivery_address = _order.delivery_address;
            newOrder.delivery_city = _order.delivery_city;
            newOrder.delivery_postcode = _order.delivery_postcode;
            newOrder.delivery_country_code = _order.delivery_country_code;
            newOrder.delivery_point_id = _order.delivery_point_id;
            newOrder.delivery_point_name = _order.delivery_point_name;
            newOrder.delivery_point_address = _order.delivery_point_address;
            newOrder.delivery_point_postcode = _order.delivery_point_postcode;
            newOrder.delivery_point_city = _order.delivery_point_city;
            newOrder.invoice_fullname = _order.invoice_fullname;
            newOrder.invoice_company = _order.invoice_company;
            newOrder.invoice_nip = _order.invoice_nip;
            newOrder.invoice_address = _order.invoice_address;
            newOrder.invoice_city = _order.invoice_city;
            newOrder.invoice_postcode = _order.invoice_postcode;
            newOrder.invoice_country_code = _order.invoice_country_code;
            newOrder.want_invoice = _order.want_invoice;
            newOrder.extra_field_1 = _order.extra_field_1;
            newOrder.extra_field_2 = _order.extra_field_2;
            newOrder.products = _order.products; 
            return newOrder; 
        }


    }
}
