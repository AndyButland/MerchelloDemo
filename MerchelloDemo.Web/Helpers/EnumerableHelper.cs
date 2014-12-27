namespace MerchelloDemo.Web.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableHelper
    {
        public static bool IsAndAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }
    }
}
