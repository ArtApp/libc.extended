using System;
using System.Collections.Generic;

namespace libc.extended.BoolOperations
{
    public abstract class BoolBag<T> : IBoolBag where T : class
    {
        private readonly DisposeLock dLock = new DisposeLock();
        protected readonly List<ItemWrapper> list = new List<ItemWrapper>();

        public abstract bool Value();

        public void Dispose()
        {
            var disposed = dLock.Disposing();

            if (disposed) return;

            lock (list)
            {
                foreach (var item in list) item?.Dispose();
                list.Clear();
            }
        }

        public T Add(bool v)
        {
            var disposed = dLock.Disposed();

            if (!disposed)
                lock (list)
                {
                    list.Add(new ItemWrapper
                    {
                        Bool = v
                    });
                }

            return this as T;
        }

        public T Add(IBoolBag v)
        {
            var disposed = dLock.Disposed();

            if (!disposed)
                lock (list)
                {
                    list.Add(new ItemWrapper
                    {
                        Bag = v
                    });
                }

            return this as T;
        }

        public T Clear()
        {
            var disposed = dLock.Disposed();

            if (!disposed)
                lock (list)
                {
                    foreach (var item in list) item?.Dispose();
                    list.Clear();
                }

            return this as T;
        }

        protected class ItemWrapper : IDisposable
        {
            public bool Bool { get; set; }
            public IBoolBag Bag { get; set; }

            public void Dispose()
            {
                Bag?.Dispose();
            }

            public bool Value()
            {
                return Bag?.Value() ?? Bool;
            }
        }

        private class DisposeLock
        {
            private readonly object disposeLock = new object();
            private bool disposed;

            /// <summary>
            ///     returns true if disposed already (Use this in Dispose method)
            /// </summary>
            /// <returns>true if disposed already</returns>
            public bool Disposing()
            {
                bool res;

                lock (disposeLock)
                {
                    res = disposed;
                    disposed = true;
                }

                return res;
            }

            /// <summary>
            ///     returns true if disposed already
            /// </summary>
            /// <returns>true if disposed</returns>
            public bool Disposed()
            {
                lock (disposeLock)
                {
                    return disposed;
                }
            }
        }
    }
}