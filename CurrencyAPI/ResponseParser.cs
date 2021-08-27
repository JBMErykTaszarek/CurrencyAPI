using CurrencyAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI
{
    public class ResponseParser
    {
        public List<SingleCurrencyDTO> ParseCSVToObjectList (string csv)
        {
            try
            {
                string[] csvAsArray = csv.Split(Environment.NewLine);
                List<SingleCurrencyDTO> returnList = new List<SingleCurrencyDTO>();
                for (int i = 1; i < csvAsArray.Length-1; i++)
                {
                    string[] splitedLine = csvAsArray[i].Split(",");
                    returnList.Add(new SingleCurrencyDTO() { date = DateTime.Parse(splitedLine[7]), rate = splitedLine[8] });
                }

                return returnList;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
