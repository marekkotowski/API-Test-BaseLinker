using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Test_BaseLinker
{
    class Program
    {
        static void Main(string[] args)
        {
            Modele.APIConnectModel APIConnect = new Modele.APIConnectModel();

            try
            {
                APIConnect.PobierzZamówieniaOdDnia(DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
