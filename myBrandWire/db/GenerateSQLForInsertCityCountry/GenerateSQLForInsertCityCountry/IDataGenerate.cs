using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSQLForInsertCityCountry
{
    public interface IDataGenerate
    {
        void Initialuze();
        void Generate();
    }
}
