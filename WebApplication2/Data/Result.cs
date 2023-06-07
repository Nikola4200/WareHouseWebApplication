using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseWeb.Model
{
    public class Result
    {
        public int? StatusCode { get; set; }
        public Object? Value { get; set; }
        public string ErrorMessage { get; set; }
        //Da li treba builder.services deklarisati za Result i da li je scoped
        public int? TotalCount { get; set; } = 0;


        public static Result Create(Object? newValue, int? status, string errMessage, int totalCount)
        {
            var result = new Result();
            result.TotalCount = totalCount;
            result.Value = newValue;
            result.StatusCode = status;
            result.ErrorMessage = errMessage; 
            return result;
        }

        //public static implicit operator Result(Product v)
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class Result<T> : Result{

        }
}
