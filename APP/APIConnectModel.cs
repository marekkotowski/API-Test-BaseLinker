using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using Newtonsoft.Json; 


namespace API_Test_BaseLinker.APP
{
    public class APIConnectModel
    {
        private string Token { get; set; }
        private Uri Adres {get;set;}
        private string Metoda { get; set; }
        private string Parametr { get; set; }


        public APIConnectModel()
        {
            this.Adres = new Uri("https://api.baselinker.com/connector.php"); 
        }


        public string Post(string _metoda, string _parametry)
        {
            string responseBody=""; 
            try
            {
                using (WebClient wc = new WebClient())
                {
                    System.Collections.Specialized.NameValueCollection requestparam = new System.Collections.Specialized.NameValueCollection();
                    requestparam.Add("token", APP.Token);
                    requestparam.Add("method", _metoda);
                    requestparam.Add("parameters", _parametry);
                    var Odebrane = wc.UploadValues(this.Adres, requestparam);
                    responseBody = Encoding.UTF8.GetString(Odebrane);
                }

                return responseBody; 
            }
            catch (ArgumentNullException ex)
            {
                APP.APPLogger.AddLog(ex.Message);
                return responseBody;
            }
            catch (WebException ex)
            {
                APP.APPLogger.AddLog(ex.Message);
                return responseBody; 
            }
        }
    }
}
