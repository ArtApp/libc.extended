using System;
using libc.extended.Resources;
using Microsoft.Extensions.Logging;

namespace libc.extended.Validation
{
    public abstract class Validator<T> : IValidator
    {
        protected readonly ILogger log;

        protected Validator(ILogger log)
        {
            this.log = log;
        }

        public FluentValidator Validate(object item)
        {
            var res = new FluentValidator();

            try
            {
                var a = (T) item;
                Validate(res, a);

                return res;
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());

                return res.AddError(Tran.Instance.Get("UnknownError"));
            }
        }

        public FluentValidator Check(T item)
        {
            if (item == null) return new FluentValidator().AddError(Tran.Instance.Get("InvalidInput"));

            return Validate(item);
        }

        public void Validate(object item, FluentValidator k)
        {
            var a = (T) item;
            Validate(k, a);
        }

        protected abstract void Validate(FluentValidator k, T a);
    }
}