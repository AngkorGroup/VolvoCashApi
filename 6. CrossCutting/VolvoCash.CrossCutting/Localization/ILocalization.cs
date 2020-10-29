using System;
using System.Globalization;

namespace VolvoCash.CrossCutting.Localization
{
    public interface ILocalization
    {
        string GetStringResource(string key);
        string GetStringResource(string key, CultureInfo culture);
        string GetStringResource<T>(T key) where T : struct, IConvertible;
        string GetStringResource<T>(T key, CultureInfo culture) where T : struct, IConvertible;
    }
}
