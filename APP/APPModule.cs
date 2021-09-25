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
            switch (APP.LogType)
            {
                case 0:
                    Bind<Modele.ILogger>().To<Modele.FileLogger>();
                    break;
                case 1:
                    Bind<Modele.ILogger>().To<Modele.ConsoleLogger>();
                    break; 
            }
        }
    }
}
