using System.Reflection;
using libc.translation;
namespace libc.extended.Resources {
    internal static class Tran {
        public static ILocalizer Instance { get; } = new Localizer(new LocalizationSource(Assembly.GetExecutingAssembly(),
            $"{typeof(Tran).Namespace}.tran.i18n.json", LocalizationSourcePropertyCaseSensitivity.CaseInsensitive));
    }
}