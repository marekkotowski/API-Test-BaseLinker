using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker.Logger
{
    public class Logger
    {
        ILogger logger;

        public Logger(ILogger _logger)
        {
            logger = _logger; 
        }

        public string AddLog(string _wpis)
        {
            return logger.AddLog(_wpis);
        }
    }
}
