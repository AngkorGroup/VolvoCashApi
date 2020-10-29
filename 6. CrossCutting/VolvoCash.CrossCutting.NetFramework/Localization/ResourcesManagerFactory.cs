using VolvoCash.CrossCutting.Localization;

namespace VolvoCash.CrossCutting.NetFramework.Localization
{
    public class ResourcesManagerFactory : ILocalizationFactory
    {
        public ILocalization Create()
        {
            return new ResourcesManager();
        }
    }
}
