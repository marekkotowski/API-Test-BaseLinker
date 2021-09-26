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
        public static string Token;
        public static string LoggerType;
        public static Modele.Logger APPLogger; 

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
            Token = ConfigurationManager.AppSettings.Get("Token");
            LoggerType = ConfigurationManager.AppSettings.Get("Logger");
            IKernel kernel = new StandardKernel(new APPModule());
            Modele.ILogger kernellogger = kernel.Get<Modele.ILogger>();
            APPLogger = new Modele.Logger(kernellogger);
            APPLogger.AddLog("Start aplikacji");
        }
    }


}
