using CurrencyAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI
{
    public static class ResponseParser
    {
        public static List<SingleCurrencyDTO> ParseCSVToObjectList (string csv)
        {
            try
            {
                string[] csvAsArray = csv.Split(Environment.NewLine);
                List<SingleCurrencyDTO> returnList = new List<SingleCurrencyDTO>();
                for (int i = 1; i < csvAsArray.Length-1; i++)
                {
                    string[] splitedLine = csvAsArray[i].Split(",");
                    returnList.Add(new SingleCurrencyDTO() {Id = i-1, date = DateTime.Parse(splitedLine[6]), rate = splitedLine[7]});
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
