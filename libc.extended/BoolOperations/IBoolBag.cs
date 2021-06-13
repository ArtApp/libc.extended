using System;

namespace libc.extended.BoolOperations
{
    public interface IBoolBag : IDisposable
    {
        bool Value();
    }
}