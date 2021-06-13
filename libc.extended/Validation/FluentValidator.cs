using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using libc.extended.Dating;
using libc.extended.Extensions;
using libc.extended.Helpers;
using libc.extended.HttpModels;

namespace libc.extended.Validation
{
    public sealed class FluentValidator
    {
        private readonly ICollection<string> errors;

        public FluentValidator(ICollection<string> errors)
        {
            this.errors = errors;
        }

        public FluentValidator() : this(new List<string>())
        {
        }

        public bool HasError => errors.Count > 0;

        public string[] GetErrors()
        {
            return errors.Distinct(StringComparer.Ordinal).ToArray();
        }

        public FluentValidator AddErrors(IEnumerable<string> errorList)
        {
            foreach (var error in errorList) errors.Add(error);

            return this;
        }

        public FluentValidator AddError(string error)
        {
            errors.Add(error);

            return this;
        }

        public FluentValidator NotNull(object o, string err)
        {
            if (o != null) return this;

            return AddError(err);
        }

        public FluentValidator NotEmpty(Guid o, string err)
        {
            if (o != Guid.Empty) return this;

            return AddError(err);
        }

        public FluentValidator NotEmpty(string o, string err)
        {
            if (!string.IsNullOrWhiteSpace(o)) return this;

            return AddError(err);
        }

        public FluentValidator NotEmpty(Dat o, string err)
        {
            if (o != null && o.IsValid()) return this;

            return AddError(err);
        }

        public FluentValidator MinLength(string o, int min, string err)
        {
            if (o == null || o.Length >= min) return this;

            return AddError(err);
        }

        public FluentValidator MaxLength(string o, int max, string err)
        {
            if (o == null || o.Length <= max) return this;

            return AddError(err);
        }

        public FluentValidator MinValue(int o, int min, string err)
        {
            if (o >= min) return this;

            return AddError(err);
        }

        public FluentValidator MaxValue(int o, int max, string err)
        {
            if (o <= max) return this;

            return AddError(err);
        }

        public FluentValidator MinValue(long o, long min, string err)
        {
            if (o >= min) return this;

            return AddError(err);
        }

        public FluentValidator MaxValue(long o, long max, string err)
        {
            if (o <= max) return this;

            return AddError(err);
        }

        public FluentValidator MinValue(double o, double min, string err)
        {
            if (o >= min) return this;

            return AddError(err);
        }

        public FluentValidator MaxValue(double o, double max, string err)
        {
            if (o <= max) return this;

            return AddError(err);
        }

        public FluentValidator MinValue(Dat o, Dat min, string err)
        {
            if (o == null || min == null || !o.IsValid() || !min.IsValid()) return this;
            if (o.Instant() >= min.Instant()) return this;

            return AddError(err);
        }

        public FluentValidator MaxValue(Dat o, Dat max, string err)
        {
            if (o == null || max == null || !o.IsValid() || !max.IsValid()) return this;
            if (o.Instant() <= max.Instant()) return this;

            return AddError(err);
        }

        public FluentValidator MinValue(DateTime o, DateTime min, string err)
        {
            if (o >= min) return this;

            return AddError(err);
        }

        public FluentValidator MaxValue(DateTime o, DateTime max, string err)
        {
            if (o <= max) return this;

            return AddError(err);
        }

        public FluentValidator Email(string o, string err)
        {
            if (o == null) return this;
            if (string.IsNullOrWhiteSpace(o)) return AddError(err);

            try
            {
                var addr = new MailAddress(o);

                if (addr.Address == o) return this;

                return AddError(err);
            }
            catch
            {
                return AddError(err);
            }
        }

        public FluentValidator Ssn(string o, string countryCode, string err)
        {
            if (o == null) return this;

            if (countryCode.Equals("IR", StringComparison.OrdinalIgnoreCase))
                if (!PersianSsnHelper.IsValidNationalCode(o))
                    return AddError(err);

            return this;
        }

        public FluentValidator Mobile(string o, string countryCode, string err)
        {
            if (o == null) return this;

            if (countryCode.Equals("IR", StringComparison.OrdinalIgnoreCase))
                if (!(o.Length == 11 && o[0] == '0' && o.All(a => a >= '0' && a <= '9')))
                    return AddError(err);

            return this;
        }

        public FluentValidator AlphaNumerical(string o, string err)
        {
            if (o == null) return this;
            var k = o.All(a => a >= '0' && a <= '9' || a >= 'a' && a <= 'z' || a >= 'A' && a <= 'Z');

            if (k) return this;

            return AddError(err);
        }

        public FluentValidator In<T>(T o, T[] arr, string err)
        {
            return In(o, arr, EqualityComparer<T>.Default, err);
        }

        public FluentValidator In<T>(T o, T[] arr, IEqualityComparer<T> eq, string err)
        {
            if (arr == null || arr.Length == 0) return this;
            var k = arr.Contains(o, eq);

            if (k) return this;

            return AddError(err);
        }

        public FluentValidator DataUri(string image, string err, out DataUri dataUri)
        {
            if (!HttpModels.DataUri.TryParse(image, out dataUri)) AddError(err);

            return this;
        }

        public FluentValidator DataUriExtension(DataUri dataUri, string[] validExtensions, string err)
        {
            var e = dataUri.Extension.RemoveStarting(".");
            var arr = validExtensions.Select(a => a.RemoveStarting("."));
            if (!arr.Contains(e, StringComparer.OrdinalIgnoreCase)) AddError(err);

            return this;
        }
        // public FluentValidator DbFilter(DbFilter filter) {
        //     if (filter == null || filter.Ok) return this;
        //     return AddError(filter.Error);
        // }
    }
}