using System;
using SimplexMethod.Model;

namespace FI.Business.Meals.SimplexImplementation.Model
{
    public class SimplexIndexResult
    {
        public Tuple<int, int> index;
        public SimplexResult result;

        public SimplexIndexResult(Tuple<int, int> index, SimplexResult result)
        {
            this.index = index;
            this.result = result;
        }
    }
}
