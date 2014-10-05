using System;
using Blank.Data.Implementations.Entities;
using Blank.Data.SQLite.ModelServices;

namespace Blank.ConsoleMain
{
    class Program
    {
        private static ModelServices mvModel = new ModelServices();
        static void Main(string[] args)
        {
            try
            {
                var users = mvModel.GetAllUsers();
                users.ForEach(x=>Console.WriteLine(x.Firstname));
                var count = users.Count;

                mvModel.AddNewEntity(new User() { Firstname = "TEST", Lastname = "TEST", Username = "TEST" });

                users = mvModel.GetAllUsers();
                var count1 = users.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
