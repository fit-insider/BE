using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.Business.Meals.Utils
{
    public class SolverResultErrorComparer : IComparer<SolverResult>
    {
        public int Compare(SolverResult x, SolverResult y)
        {

            if (x == null || y == null)
            {
                return 0;
            }

            return x.Error.CompareTo(y.Error);

        }
    }

}
