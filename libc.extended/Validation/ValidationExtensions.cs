namespace libc.extended.Validation
{
    public static class ValidationExtensions
    {
        public static bool IsOk(this FluentValidator res)
        {
            if (res == null) return false;

            return !res.HasError;
        }
    }
}