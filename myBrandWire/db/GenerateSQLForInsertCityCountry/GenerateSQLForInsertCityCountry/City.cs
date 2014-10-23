using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSQLForInsertCityCountry
{
    internal class City:IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int idData { get; set; }

        public int idCountry { get; set; }
        
    }
}
