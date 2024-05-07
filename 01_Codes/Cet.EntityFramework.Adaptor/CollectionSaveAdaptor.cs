using Cet.EntityFramework.Adaptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.EntityFramework.Adaptor
{
    public class CollectionSaveAdaptor
    {
        public static void Execute<T1, T2>(
           IEnumerable<T1> domainList, IEnumerable<T2> dataList,
           Func<T1, T2, bool> matchFunc,
           Action<T1> removeFunc, Action<T1, T2> updateFunc, Action<T2> addFunc)
        {
            var mergedChangedItems = domainList.ToList()
                .FullOuterJoin(dataList, matchFunc);

            foreach (var item in mergedChangedItems)
            {
                if (item.Item1 == null) { if (addFunc != null) addFunc(item.Item2); }
                else if (item.Item2 == null) { if (removeFunc != null) removeFunc(item.Item1); }
                else { if (updateFunc != null) updateFunc(item.Item1, item.Item2); }
            }
        }


    }


    public static class FullOuterJoinExtension
    {
        public static IEnumerable<Tuple<T1, T2>> FullOuterJoin<T1, T2>(this IEnumerable<T1> one, IEnumerable<T2> two, Func<T1, T2, bool> match)
        {
            var left = from a in one
                       from b in two.Where((b) => match(a, b)).DefaultIfEmpty()
                       select new Tuple<T1, T2>(a, b);

            var right = from b in two
                        from a in one.Where((a) => match(a, b)).DefaultIfEmpty()
                        select new Tuple<T1, T2>(a, b);

            return left.Concat(right).Distinct();
        }
    }



}
