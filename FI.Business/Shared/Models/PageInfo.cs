using System.Collections;
using System.Collections.Generic;

namespace FI.Business.Shared.Models
{
    public class PageInfo<T>
    {
        public ICollection<T> Elements { get; set; }
        public int TotalNumberOfElements { get; set; }
    }
}
