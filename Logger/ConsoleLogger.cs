using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker.Logger
{
    public class ConsoleLogger :ILogger
    {
        public string AddLog(string _wpis)
        {
            try
            {
                Console.WriteLine(_wpis);
                return ""; 
            }
            catch (System.IO.IOException ex)
            {
                return ex.Message;
            }
        }
    }
}
