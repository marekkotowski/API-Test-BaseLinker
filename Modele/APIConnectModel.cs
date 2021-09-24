using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net; 

namespace API_Test_BaseLinker.Modele
{
    public class APIConnectModel
    {
        private string Token { get; set; }
        private Uri Adres {get;set;}
        private string Metoda { get; set; }
        private string Parametr { get; set; }


        public APIConnectModel()
        {
            this.Token = "3002555-3011404-RCY8JRTXCWXWJU5BZHSJI8KS9K4LQ9801OUIH18QS192F727NWE622D67L9H49YR";
            this.Adres = new Uri("https://api.baselinker.com/connector.php");
            this.Metoda = "getOrders";
            this.Parametr = " \"date_form\" ";        

        }


        public bool PobierzZamówieniaOdDnia(DateTime data)
        {
            //https://api.baselinker.com/connector.php , POST: token=1-23-ASOIM234MFDSK534LDS &method=getOrders &parameters={"date_from": 1407341754} 

            try
            {
                using (WebClient client = new WebClient())
                {
                    var DatatimeOffset = new DateTimeOffset(data).ToUniversalTime();
                    long unixdata = DatatimeOffset.ToUnixTimeMilliseconds();

                    string parametry = @" token=3002555-3011404-RCY8JRTXCWXWJU5BZHSJI8KS9K4LQ9801OUIH18QS192F727NWE622D67L9H49YR &method=getOrders &parameters={""date_from"": 1407341754} ";


                    string Odebrane = client.UploadString(this.Adres.ToString(), "POST", parametry);


                }

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }        
        }

        private void ZbudujParametrWywolania(WebClient wc, DateTime data)
        {


                
  

        }
    }
}
