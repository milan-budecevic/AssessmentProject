using DataLayer.IRepository;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class DataRepository : IDataRepository
    {
        public async Task<ParsedData> GetDataFromCsv()
        {
            var postalCodes = new List<PostalCode>();
            var houses = new List<House>();
            var streets = new List<Street>();
            var localities = new List<Locality>();

            string DATA_DIRECTORY = @"D:\IT-engine\Post_Adressdaten20170425.csv";
            using (StreamReader sourceReader = new StreamReader(DATA_DIRECTORY))
            {
                while (!sourceReader.EndOfStream)
                {
                    var line = await sourceReader.ReadLineAsync();
                    var lineElements = line.Split(";");
                    if (lineElements[0] == "01")
                    {

                        postalCodes.Add(new PostalCode()
                        {
                            PostalCodeId = lineElements[1],
                            Zip = lineElements[4] 
                        });

                    }
                    else if (lineElements[0] == "02")
                    {
                        localities.Add(new Locality()
                        {
                            PostalCodeId = lineElements[1],
                            LocalityName = lineElements[5]
                        });
                    }
                    else if (lineElements[0] == "04")
                    {
                        streets.Add(new Street()
                        {
                            StreetId = lineElements[1],
                            PostalCodeId = lineElements[2],
                            StreetName = lineElements[4]
                        }); ;
                    }
                    else if (lineElements[0] == "06")
                    {
                        houses.Add(new House()
                        {
                            StreetId = lineElements[2],
                            HouseNumber = lineElements[3]
                        });
                    }
                }
            }
            var parsedData = new ParsedData()
            {
                PostalCodes = postalCodes,
                Houses = houses,
                Streets = streets,
                Localities = localities
            };

            return parsedData;
        }

    }
}
