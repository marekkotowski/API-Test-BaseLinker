using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 

namespace API_Test_BaseLinker.Logger
{
    /// <summary>
    /// Zapis logów do pliku
    /// </summary>
    public class FileLogger : ILogger
    {
        private string LogFile { get; set; }
        private string Msg { get; set; }

        public FileLogger()
        {
            LogFile = $"{AppDomain.CurrentDomain.BaseDirectory}LogFile.log";
        }


        /// <summary>
        /// Dodaj wpis do pliku Logów
        /// </summary>
        /// <param name="_wpis"></param>
        /// <returns></returns>
        public string AddLog(string _wpis)
        {
            if (!File.Exists(this.LogFile)) CreateLogFile(); 
            try
            {
                using (StreamWriter file = new StreamWriter(this.LogFile, true))
                {
                    file.WriteLine(_wpis);
                    file.Close();
                }
                return "";

            }
            catch (Exception ex)
            {
                return ex.Message; 
            }        
        }

        /// <summary>
        /// Tworzy plik logów 
        /// </summary>
        private void CreateLogFile()
        {
            using (FileStream file = File.Create(this.LogFile))
            { 
            
            } 
        }



    }
}
