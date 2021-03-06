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
            APP.APP.APPSettings(); 
            try
            {
                 MainTask(); 
            }
            catch (Exception ex)
            {
                string msg = APP.APP.APPLogger.AddLog(ex.Message);
                if (msg != "") Console.WriteLine(msg);                
            }
            Console.WriteLine("Koniec aplikacji.");
            Console.ReadKey(); 

        }

        /// <summary>
        /// Główna funkcja do wykonania
        /// </summary>
        public static void MainTask()
        {
            //Pobieram wszystkie zamówienia z BaseLinker
            List<Modele.OrderModel> orders = Modele.OrderModel.getOrders();

            //Tworzę nowe zamówienie na kopiując jest z pierwszego zamówienia na liście  i zapisuję nowe zamówienie w BaseLinker
            if (orders!=null && orders.Count>0)
            {
                Modele.OrderModel neworder = new Modele.OrderModel();
                neworder = neworder.CopyasNew(orders[0]);
                neworder.order_id =  neworder.addOrder(neworder);
                if (neworder.order_id > 0)
                {
                    APP.APP.APPLogger.AddLog($"Utworzono nowe zamówienie: order_id={neworder.order_id}");
                }
                else
                {
                    Console.WriteLine("Nie uzyskano order_id nowego zamówienia!!!");
                }
            }            
        }
    }
}
