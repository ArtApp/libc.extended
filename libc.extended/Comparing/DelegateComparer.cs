using System;
using System.Collections.Generic;
namespace libc.extended.Comparing {
    public class DelegateComparer<T> : IComparer<T> {
        private readonly Func<T, T, int> c;
        public DelegateComparer(Func<T, T, int> c) {
            this.c = c;
        }
        public int Compare(T x, T y) {
            return c(x, y);
        }
    }
}