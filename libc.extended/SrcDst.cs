namespace libc.extended {
    public class SrcDst : NotifyModel {
        private object _Dest;
        private object _Source;
        public SrcDst() {
        }
        public SrcDst(object source, object dest)
            : this() {
            Source = source;
            Dest = dest;
        }
        public object Source {
            get => _Source;
            set => Set(ref _Source, value, () => Source);
        }
        public object Dest {
            get => _Dest;
            set => Set(ref _Dest, value, () => Dest);
        }
    }
    public class SrcDst<TSource, TDest> : NotifyModel {
        private TDest _Dest;
        private TSource _Source;

        // ReSharper disable once MemberCanBeProtected.Global
        public SrcDst() {
        }
        public SrcDst(TSource source, TDest dest)
            : this() {
            Source = source;
            Dest = dest;
        }
        public TSource Source {
            get => _Source;
            set => Set(ref _Source, value, () => Source);
        }
        public TDest Dest {
            get => _Dest;
            set => Set(ref _Dest, value, () => Dest);
        }
    }
    public class SrcDst<T> : SrcDst<T, T> {
        public SrcDst() {
        }
        public SrcDst(T source, T dest)
            : base(source, dest) {
        }
    }
}