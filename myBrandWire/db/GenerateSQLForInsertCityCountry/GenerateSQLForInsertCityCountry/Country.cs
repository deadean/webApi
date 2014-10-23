using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSQLForInsertCityCountry
{
    class Country:IEntity
    {
        public int idData { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
        
    }
}
