using System.Linq;
using System.Collections.Generic;

namespace Asklepios.Web.ServiceClasses
{
    public class Pagination
    {
        public static IQueryable<T> GetPageItems<T>(int pageNum, int itempsPerPage, IQueryable<T> data)
        {
            return data.Skip((pageNum-1)*itempsPerPage).Take(itempsPerPage).AsQueryable<T>();            
        }
    }
}
