using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Ninject;

namespace API_Test_BaseLinker.APP
{
    public sealed class APP
    {
        //public static string Token;
        public static string LoggerType;
        public static string SourceClientType; 
        public static Logger.Logger APPLogger; 

        public static SourceClient.SourceClient SenderClient; 

        private APP instance = new APP(); 

        public static APP Instance
        {
            get {
                return Instance;
            }
        }

        private APP()
        {
            
        }

        public static void APPSettings()
        {
            ///ustawienia aplikacji z APP.config
            string token =  ConfigurationManager.AppSettings.Get("Token");
            Uri webaddress = new Uri(ConfigurationManager.AppSettings.Get("webaddress"));
            LoggerType = ConfigurationManager.AppSettings.Get("Logger");
            SourceClientType = ConfigurationManager.AppSettings.Get("SourceClient");

            // wstrzykiwanie zależności 
            IKernel kernel = new StandardKernel(new APPModule());
            SourceClient.ISourceClient senderclient = kernel.Get<SourceClient.ISourceClient>();
            SenderClient = new SourceClient.SourceClient (senderclient, webaddress, token);
            Logger.ILogger kernellogger = kernel.Get<Logger.ILogger>();
                        
            APPLogger = new Logger.Logger(kernellogger);
            APPLogger.AddLog("Start aplikacji");
        }
    }


}
