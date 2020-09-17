namespace libc.extended {
    public static class ModelsExtensions {
        public static bool IsOk(this FluentResult res) {
            return res?.Success ?? false;
        }
        public static bool IsOk<T>(this FluentResult<T> res) {
            return res?.Success ?? false;
        }
    }
}