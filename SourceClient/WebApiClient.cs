using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace API_Test_BaseLinker.SourceClient
{
    public class WebApiClient :  ISourceClient
    {
        Uri Adres;
        string Token; 

        public WebApiClient()
        {
        }


        public WebApiClient(Uri _adres, string _token)
        {

            this.Adres = _adres;
            this.Token = _token;
        }

        public void SetParam(Uri _adres, string _token)
        {
            this.Adres = _adres;
            this.Token = _token; 
        }

        public string Post(string _method, string _requestparams)
        {
            string responseBody = "";
            try
            {
                using (WebClient wc = new WebClient())
                {
                    System.Collections.Specialized.NameValueCollection requestparam = new System.Collections.Specialized.NameValueCollection();
                    requestparam.Add("token", Token);
                    requestparam.Add("method", _method);
                    requestparam.Add("parameters", _requestparams);
                    var Odebrane = wc.UploadValues(this.Adres, requestparam);
                    responseBody = Encoding.UTF8.GetString(Odebrane);
                }

                return responseBody;
            }
            catch (ArgumentNullException ex)
            {
                return responseBody;
            }
            catch (WebException ex)
            {
                return responseBody;
            }

        }




    }
}
