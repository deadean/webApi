using System;
using Model.ModelServices;
using Model.Entity;

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

                companies.ForEach(x => Console.WriteLine(x.Username));
                mc.AddNewEntity(new User() { Firstname = "TEST", Lastname="TEST", Username = "TEST"});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }
    }
}
