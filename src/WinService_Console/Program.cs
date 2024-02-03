using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService;

namespace WinServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainService = new MainService();

            //_webApiService.Test();

            mainService.Start();

            Console.ReadKey();

            mainService.Stop();
        }
    }
}
