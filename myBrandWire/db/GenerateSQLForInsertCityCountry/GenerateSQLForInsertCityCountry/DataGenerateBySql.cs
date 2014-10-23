using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GenerateSQLForInsertCityCountry
{
    public class DataGenerateBySql:IDataGenerate
    {
        private string modContent;
        private string modTextFile = "data.txt";
        private string modTextFileResult = "result.txt";
        private IEnumerable<string> modGeneratedSql = new List<string>();
        private IList<IEntity> modGeneratedEntities = new List<IEntity>();
        private IList<Country> modCountries = new List<Country>();
        private IList<City> modCities = new List<City>();
        private List<City> modCitiesResult = new List<City>();

        public void Initialuze()
        {
            modContent = File.ReadAllText(modTextFile);
            modGeneratedSql = modContent.Split(';').AsEnumerable<string>();
            this.PrepareDataForAnalize();
            this.SortData();

        }

        private void SortData()
        {
            this.modCountries = this.modGeneratedEntities.OfType<Country>().ToList();
            this.modCities = this.modGeneratedEntities.OfType<City>().ToList();

            this.modCountries = this.modCountries.OrderBy(x=>x.Name).ToList();
            int iCity = 0;
            for (int i = 0; i < this.modCountries.Count; i++)
            {
                this.modCountries[i].Id = i;
                var citiesByCountry = this.modCities.Where(x=>x.idCountry == this.modCountries[i].idData).ToList();
                for (int i1 = 0; i1 < citiesByCountry.Count(); i1++)
                {
                    citiesByCountry[i1].idCountry = i;
                }
                citiesByCountry = citiesByCountry.OrderBy(x=>x.Name).ToList();

                for (int i1 = 0; i1 < citiesByCountry.Count(); i1++)
                {
                    citiesByCountry[i1].Id = iCity;
                    iCity++;
                }
                
                this.modCitiesResult.AddRange(citiesByCountry);
            }
        }

        public void Generate()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(modTextFileResult))
            {
                foreach (string line in modGeneratedSql)
                {
                    if (line.Contains(csConstants.csREGION) && !line.Contains(csConstants.csCREATE) && !line.Contains(csConstants.csDROP))
                        continue;

                    if (line.Contains(csConstants.csCity) && line.Contains(csConstants.csINSERT) || line.Contains(csConstants.csCountry) && line.Contains(csConstants.csINSERT))
                        continue;

                    var res = string.Format("$query =\"{0}\"; $res = mysqli_query($con, $query);",line);
                    file.WriteLine(res);
                }
                foreach (var item in this.modCountries)
                {
                    string line = string.Format("INSERT INTO country VALUES({0},{1});",item.Id,item.Name);
                    var res = string.Format("$query =\"{0}\"; $res = mysqli_query($con, $query);", line);
                    file.WriteLine(res);
                }

                foreach (var item in this.modCitiesResult)
                {
                    string line = string.Format("INSERT INTO city VALUES({0},{1},{2});", item.Id, item.idCountry, item.Name);
                    var res = string.Format("$query =\"{0}\"; $res = mysqli_query($con, $query);", line);
                    file.WriteLine(res);
                }
            }
        }

        private void PrepareDataForAnalize()
        {

            foreach (var item in this.modGeneratedSql)
            {
                if (item.Contains(csConstants.csCREATE) || item.Contains(csConstants.csDROP) || item.Contains(csConstants.csREGION))
                    continue;

                this.modGeneratedEntities.Add(GetEntityFromSql(item));
            }
        }

        private IEntity GetEntityFromSql(string item)
        {
            if(item.Contains(csConstants.csCountry)){
                var country = new Country();

                var stringValues = item.Substring(item.IndexOf('(') + 1, item.IndexOf(')') - item.IndexOf('(') - 1).Split(',');
                country.idData = Convert.ToInt32(stringValues[0]);
                country.Name = (string)stringValues[2];

                return country;
            }

            if (item.Contains(csConstants.csCity))
            {
                var country = new City();

                var stringValues = item.Substring(item.IndexOf('(') + 1, item.IndexOf(')') - item.IndexOf('(') - 1).Split(',');
                country.idData = Convert.ToInt32(stringValues[0]);
                country.idCountry = Convert.ToInt32(stringValues[1]);
                country.Name = (string)stringValues[3];

                return country;
            }
            return null;
        }
    }
}
