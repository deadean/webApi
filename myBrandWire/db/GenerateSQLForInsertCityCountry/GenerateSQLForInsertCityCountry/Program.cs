using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSQLForInsertCityCountry
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new DataGenerateBySql();
            manager.Initialuze();
            manager.Generate();
        }
    }
}
