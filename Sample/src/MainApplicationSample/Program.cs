using System;
using Model.ModelServices;

namespace Blank.ConsoleMain
{
    class Program
    {
        [STAThread()]
        static void Main(string[] args)
        {
            try
            {
                var mc = new ModelServices();
                var companies = mc.GetAllUsers();

                companies.ForEach(x => Console.WriteLine(x.Name));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }
    }
}
