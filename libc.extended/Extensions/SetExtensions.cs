using System;
using System.Collections.Generic;
using System.Linq;
namespace libc.extended.Extensions {
    public static class SetExtensions {
        public static void Compare<TCurrent, TNew>(this IList<TCurrent> currentSet, IList<TNew> newSet,
            Func<TCurrent, TNew, bool> comparer, out List<TNew> insertList,
            out List<SrcDst<TCurrent, TNew>> updateList,
            out List<TCurrent> deleteList) {
            insertList = newSet.Where(n => currentSet.All(c => !comparer(c, n))).ToList();
            deleteList = new List<TCurrent>();
            updateList = new List<SrcDst<TCurrent, TNew>>();
            foreach (var c in currentSet) {
                var n = newSet.FirstOrDefault(a => comparer(c, a));
                if (n == null)
                    deleteList.Add(c);
                else
                    updateList.Add(new SrcDst<TCurrent, TNew>(c, n));
            }
        }
        public static void CompareDataGuid<TCurrent, TNew>(this IList<TCurrent> currentSet, IList<TNew> newSet,
            out List<TNew> insertList, out List<SrcDst<TCurrent, TNew>> updateList, out List<TCurrent> deleteList)
            where TCurrent : IHasId<Guid>
            where TNew : IHasId<Guid> {
            currentSet.Compare(newSet, (c, n) => c.Id == n.Id, out insertList, out updateList, out deleteList);
        }
        public static void CompareDataLong<TCurrent, TNew>(this IList<TCurrent> currentSet, IList<TNew> newSet,
            out List<TNew> insertList, out List<SrcDst<TCurrent, TNew>> updateList, out List<TCurrent> deleteList)
            where TCurrent : IHasId<long>
            where TNew : IHasId<long> {
            currentSet.Compare(newSet, (c, n) => c.Id == n.Id, out insertList, out updateList, out deleteList);
        }
    }
}