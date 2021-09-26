using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

namespace API_Test_BaseLinker.APP
{
    public class APPModule: NinjectModule
    {
        public override void Load()
        {
            switch (APP.LoggerType)
            {
                case "File":
                    Bind<Logger.ILogger>().To<Logger.FileLogger>();
                    break;
                case "Console":
                    Bind<Logger.ILogger>().To<Logger.ConsoleLogger>();
                    break; 
            }

            switch (APP.SourceClientType)
            {
                case "WebAPI":
                    Bind<SourceClient.ISourceClient>().To<SourceClient.WebApiClient>();
                    break;
            }
            
        }
    }
}
