using System;
using System.Resources;
using System.Reflection;
using System.Globalization;
using VolvoCash.CrossCutting.Localization;
using VolvoCash.CrossCutting.NetFramework.Resources;

namespace VolvoCash.CrossCutting.NetFramework.Localization
{
    public class ResourcesManager : ILocalization
    {
        #region Properties
        public ResourceManager ResourceManager { get; }
        #endregion

        #region Constructor
        public ResourcesManager()
        {
            ResourceManager = new ResourceManager(typeof(Messages));
        }
        #endregion

        #region Private Methods
        private string GetKeyFromEnum<T>(T key)
            where T : struct, IConvertible
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Infraestructure.exception_InvalidEnumeratedType));
            }
            return string.Format("{0}_{1}", typeof(T).Name, key.ToString());
        }
        #endregion

        #region Public Methods
        public string GetStringResource(string key)
        {
            var message = ResourceManager.GetString(key);
            if (string.IsNullOrEmpty(message))
                return key;
            return message;
        }

        public string GetStringResource(string key, CultureInfo culture)
        {
            var message = ResourceManager.GetString(key, culture);
            if (string.IsNullOrEmpty(message))
                return key;
            return message;
        }

        public string GetStringResource<T>(T key) where T : struct, IConvertible
        {
            var keyFromEnum = GetKeyFromEnum<T>(key);
            var message = ResourceManager.GetString(keyFromEnum);
            if (string.IsNullOrEmpty(message))
                return keyFromEnum;
            return message;
        }

        public string GetStringResource<T>(T key, CultureInfo culture) where T : struct, IConvertible
        {
            var keyFromEnum = GetKeyFromEnum<T>(key);
            var message = ResourceManager.GetString(keyFromEnum, culture);
            if (string.IsNullOrEmpty(message))
                return keyFromEnum;
            return message;
        }
        #endregion
    }
}
