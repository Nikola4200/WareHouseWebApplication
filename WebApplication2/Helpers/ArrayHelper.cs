using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Helpers
{
    public static class ArrayHelper
    {
        public static long[] ConvertToArrayOfIds(string listOfIds)
        {
            if (string.IsNullOrEmpty(listOfIds))
            {
                return new long[0];
            }

            string[] idsString = listOfIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
            long[] ids = new long[idsString.Length];
            for (int i = 0; i < idsString.Length; i++)
            {
                if(long.TryParse(idsString[i], out long id))
                {
                    ids[i] = id;
                } else
                {
                    return null;
                }
            }
            return ids;
        }
    }
}
