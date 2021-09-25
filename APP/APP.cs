using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace API_Test_BaseLinker.APP
{
    public sealed class APP
    {
        public static int LogType;   ///0 - LogFile, 1 - LogConsole
        public static string Token = "***tu należy wprowadzić token ***";
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
            LogType = 1;           
            IKernel kernel = new StandardKernel(new APPModule());
            Modele.ILogger kernellogger = kernel.Get<Modele.ILogger>();
            APPLogger = new Modele.Logger(kernellogger);
            APPLogger.AddLog("Start aplikacji");
        }
    }


}
