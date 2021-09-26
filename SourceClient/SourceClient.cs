using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker.SourceClient
{
    public class SourceClient
    {
        ISourceClient sourceClient;
        protected Uri Adres { get; set; }
        protected string Token { get; set; }
        public string Msg {get; set;}

        public SourceClient(ISourceClient _sourceClient, Uri _adres, string _token)
        {
            sourceClient = _sourceClient;
            Adres = _adres;
            Token = _token; 
        }

        public string Post(string _method, string _requestparam)
        {
            try
            {
                sourceClient.SetParam(Adres, Token);
                return sourceClient.Post(_method, _requestparam);
            }
            catch (Exception ex)
            {
                Msg = ex.Message; 
                return "";
            }
            
        }

    }
}
